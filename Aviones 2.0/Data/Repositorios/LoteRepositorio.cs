using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    public class LoteRepositorio
    {
        private const string Consulta = @"
            SELECT L.IdLote, L.IdMaterial, L.IdFabricante, F.Nombre AS Fabricante, L.IdUbicacion, U.Nombre AS Ubicacion,
                   L.Cantidad, L.CompradoUsado, L.DisponibleNuevo, L.DisponibleUsado, L.ValorUnitario, L.FechaIngreso
            FROM dbo.Lotes L
            LEFT JOIN dbo.Fabricantes F ON F.IdFabricante = L.IdFabricante
            LEFT JOIN dbo.Ubicaciones U ON U.IdUbicacion = L.IdUbicacion";

        public List<Lote> ListarPorMaterial(int idMaterial, bool soloConStock = false)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<Lote>(
                    $"{Consulta} WHERE L.IdMaterial = @idMaterial"
                    + (soloConStock ? " AND L.DisponibleNuevo + L.DisponibleUsado > 0" : "")
                    + " ORDER BY L.FechaIngreso, L.IdLote",
                    new { idMaterial }).ToList();
            }
        }

        public Lote Obtener(int idLote)
        {
            using (var cn = Db.Abrir())
            {
                return cn.QuerySingleOrDefault<Lote>($"{Consulta} WHERE L.IdLote = @idLote", new { idLote });
            }
        }

        /// <summary>Ingresa un lote; arranca con todo disponible como nuevo, o como usado si se compró usado.</summary>
        public int Crear(Lote lote)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                lote.IdLote = Insertar(cn, tx, lote);
                tx.Commit();
                return lote.IdLote;
            }
        }

        /// <summary>
        /// Corrige los datos del lote. Si cambia la cantidad, el stock de la compra (nuevo, o usado si se
        /// compró usado) cambia en la misma diferencia; no puede quedar negativo. Si se compró nuevo o usado
        /// se puede cambiar mientras no haya salido nada del lote: todo pasa al otro stock.
        /// </summary>
        public void Actualizar(Lote lote)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                Actualizar(cn, tx, lote);
                tx.Commit();
            }
        }

        /// <summary>Lo mismo dentro de una transacción ya abierta (por ejemplo, al modificar el material con su lote).</summary>
        internal static void Actualizar(IDbConnection cn, IDbTransaction tx, Lote lote)
        {
            Validar(lote);
            var antes = cn.QuerySingleOrDefault<Lote>($"{Consulta} WHERE L.IdLote = @IdLote", lote, tx)
                ?? throw new ReglaNegocioException("El lote ya no existe.");
            if (lote.CompradoUsado != antes.CompradoUsado)
            {
                int usos = cn.ExecuteScalar<int>("SELECT COUNT(*) FROM dbo.MaterialesModelo WHERE IdLote = @IdLote", lote, tx);
                decimal deLaCompra = antes.CompradoUsado ? antes.DisponibleUsado : antes.DisponibleNuevo;
                decimal delOtro = antes.CompradoUsado ? antes.DisponibleNuevo : antes.DisponibleUsado;
                if (usos > 0 || deLaCompra != antes.Cantidad || delOtro != 0)
                {
                    throw new ReglaNegocioException(
                        "Si se compró nuevo o usado se puede cambiar sólo mientras no salió nada del lote (no se usó en modelos ni se descartó).");
                }
                cn.Execute(@"
                    UPDATE dbo.Lotes SET CompradoUsado = @CompradoUsado,
                        DisponibleNuevo = CASE WHEN @CompradoUsado = 1 THEN 0 ELSE Cantidad END,
                        DisponibleUsado = CASE WHEN @CompradoUsado = 1 THEN Cantidad ELSE 0 END
                    WHERE IdLote = @IdLote", lote, tx);
            }
            int filas = cn.Execute(@"
                UPDATE dbo.Lotes SET
                    IdFabricante = @IdFabricante, IdUbicacion = @IdUbicacion, ValorUnitario = @ValorUnitario,
                    FechaIngreso = @FechaIngreso,
                    DisponibleNuevo = CASE WHEN CompradoUsado = 0 THEN DisponibleNuevo + (@Cantidad - Cantidad) ELSE 0 END,
                    DisponibleUsado = CASE WHEN CompradoUsado = 1 THEN DisponibleUsado + (@Cantidad - Cantidad) ELSE DisponibleUsado END,
                    Cantidad = @Cantidad
                WHERE IdLote = @IdLote
                  AND (CASE WHEN CompradoUsado = 0 THEN DisponibleNuevo ELSE DisponibleUsado END) + (@Cantidad - Cantidad) >= 0", lote, tx);
            if (filas == 0)
            {
                throw new ReglaNegocioException("La cantidad no puede ser menor que lo que ya salió del lote (usado en modelos o descartado).");
            }
            var actual = cn.QuerySingle<Lote>($"{Consulta} WHERE L.IdLote = @IdLote", lote, tx);
            lote.CompradoUsado = actual.CompradoUsado;
            lote.DisponibleNuevo = actual.DisponibleNuevo;
            lote.DisponibleUsado = actual.DisponibleUsado;
            lote.Fabricante = actual.Fabricante;
            lote.Ubicacion = actual.Ubicacion;
        }

        public void Eliminar(int idLote)
        {
            using (var cn = Db.Abrir())
            {
                int usos = cn.ExecuteScalar<int>("SELECT COUNT(*) FROM dbo.MaterialesModelo WHERE IdLote = @idLote", new { idLote });
                if (usos > 0)
                {
                    throw new ReglaNegocioException($"El lote se usa en {usos} modelos; quítelo de ellos primero.");
                }
                using (var tx = cn.BeginTransaction())
                {
                    // El pedido recibido sigue en el historial, sin el lote
                    cn.Execute("UPDATE dbo.PedidosCompra SET IdLote = NULL WHERE IdLote = @idLote", new { idLote }, tx);
                    cn.Execute("DELETE FROM dbo.Lotes WHERE IdLote = @idLote", new { idLote }, tx);
                    tx.Commit();
                }
            }
        }

        internal static int Insertar(IDbConnection cn, IDbTransaction tx, Lote lote)
        {
            Validar(lote);
            lote.DisponibleNuevo = lote.CompradoUsado ? 0m : lote.Cantidad;
            lote.DisponibleUsado = lote.CompradoUsado ? lote.Cantidad : 0m;
            return cn.ExecuteScalar<int>(@"
                INSERT INTO dbo.Lotes (IdMaterial, IdFabricante, IdUbicacion, Cantidad, CompradoUsado, DisponibleNuevo, DisponibleUsado, ValorUnitario, FechaIngreso)
                VALUES (@IdMaterial, @IdFabricante, @IdUbicacion, @Cantidad, @CompradoUsado, @DisponibleNuevo, @DisponibleUsado, @ValorUnitario, @FechaIngreso);
                SELECT CAST(SCOPE_IDENTITY() AS int);", lote, tx);
        }

        private static void Validar(Lote lote)
        {
            if (lote.Cantidad < 0)
            {
                throw new ReglaNegocioException("La cantidad del lote no puede ser negativa.");
            }
            if (lote.ValorUnitario < 0)
            {
                throw new ReglaNegocioException("El valor unitario no puede ser negativo.");
            }
        }
    }
}
