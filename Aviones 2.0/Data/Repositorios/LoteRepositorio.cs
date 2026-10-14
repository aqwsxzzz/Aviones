using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    public class LoteRepositorio
    {
        private const string Columnas =
            "IdLote, IdMaterial, Fabricante, Ubicacion, Cantidad, CompradoUsado, DisponibleNuevo, DisponibleUsado, ValorUnitario, FechaIngreso";

        public List<Lote> ListarPorMaterial(int idMaterial, bool soloConStock = false)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<Lote>(
                    $"SELECT {Columnas} FROM dbo.Lotes WHERE IdMaterial = @idMaterial"
                    + (soloConStock ? " AND DisponibleNuevo + DisponibleUsado > 0" : "")
                    + " ORDER BY FechaIngreso, IdLote",
                    new { idMaterial }).ToList();
            }
        }

        public Lote Obtener(int idLote)
        {
            using (var cn = Db.Abrir())
            {
                return cn.QuerySingleOrDefault<Lote>($"SELECT {Columnas} FROM dbo.Lotes WHERE IdLote = @idLote", new { idLote });
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
        /// Corrige los datos del lote (no si se compró nuevo o usado). Si cambia la cantidad, el stock
        /// de la compra (nuevo, o usado si se compró usado) cambia en la misma diferencia;
        /// no puede quedar negativo.
        /// </summary>
        public void Actualizar(Lote lote)
        {
            Validar(lote);
            using (var cn = Db.Abrir())
            {
                int filas = cn.Execute(@"
                    UPDATE dbo.Lotes SET
                        Fabricante = @Fabricante, Ubicacion = @Ubicacion, ValorUnitario = @ValorUnitario,
                        FechaIngreso = @FechaIngreso,
                        DisponibleNuevo = CASE WHEN CompradoUsado = 0 THEN DisponibleNuevo + (@Cantidad - Cantidad) ELSE 0 END,
                        DisponibleUsado = CASE WHEN CompradoUsado = 1 THEN DisponibleUsado + (@Cantidad - Cantidad) ELSE DisponibleUsado END,
                        Cantidad = @Cantidad
                    WHERE IdLote = @IdLote
                      AND (CASE WHEN CompradoUsado = 0 THEN DisponibleNuevo ELSE DisponibleUsado END) + (@Cantidad - Cantidad) >= 0", lote);
                if (filas == 0)
                {
                    throw new ReglaNegocioException("La cantidad no puede ser menor que lo que ya salió del lote (usado en modelos o descartado).");
                }
                var actual = cn.QuerySingle<Lote>($"SELECT {Columnas} FROM dbo.Lotes WHERE IdLote = @IdLote", lote);
                lote.CompradoUsado = actual.CompradoUsado;
                lote.DisponibleNuevo = actual.DisponibleNuevo;
                lote.DisponibleUsado = actual.DisponibleUsado;
            }
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
                cn.Execute("DELETE FROM dbo.Lotes WHERE IdLote = @idLote", new { idLote });
            }
        }

        /// <summary>Fabricantes ya usados, para sugerirlos al escribir.</summary>
        public List<string> ListarFabricantes() => ListarDistintos("Fabricante");

        /// <summary>Ubicaciones ya usadas, para sugerirlas al escribir.</summary>
        public List<string> ListarUbicaciones() => ListarDistintos("Ubicacion");

        private static List<string> ListarDistintos(string columna)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<string>(
                    $"SELECT DISTINCT {columna} FROM dbo.Lotes WHERE {columna} IS NOT NULL AND {columna} <> '' ORDER BY {columna}").ToList();
            }
        }

        internal static int Insertar(IDbConnection cn, IDbTransaction tx, Lote lote)
        {
            Validar(lote);
            lote.DisponibleNuevo = lote.CompradoUsado ? 0m : lote.Cantidad;
            lote.DisponibleUsado = lote.CompradoUsado ? lote.Cantidad : 0m;
            return cn.ExecuteScalar<int>(@"
                INSERT INTO dbo.Lotes (IdMaterial, Fabricante, Ubicacion, Cantidad, CompradoUsado, DisponibleNuevo, DisponibleUsado, ValorUnitario, FechaIngreso)
                VALUES (@IdMaterial, @Fabricante, @Ubicacion, @Cantidad, @CompradoUsado, @DisponibleNuevo, @DisponibleUsado, @ValorUnitario, @FechaIngreso);
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
