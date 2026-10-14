using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    public class LoteRepositorio
    {
        private const string Columnas =
            "IdLote, IdMaterial, Fabricante, Ubicacion, Cantidad, Disponible, ValorUnitario, FechaIngreso";

        public List<Lote> ListarPorMaterial(int idMaterial, bool soloConStock = false)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<Lote>(
                    $"SELECT {Columnas} FROM dbo.Lotes WHERE IdMaterial = @idMaterial"
                    + (soloConStock ? " AND Disponible > 0" : "")
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

        /// <summary>Ingresa un lote nuevo; arranca con todo disponible.</summary>
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
        /// Corrige los datos del lote. Si cambia la cantidad, el disponible se ajusta en la misma diferencia;
        /// no se puede bajar la cantidad por debajo de lo ya usado en modelos.
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
                        Disponible = Disponible + (@Cantidad - Cantidad),
                        Cantidad = @Cantidad
                    WHERE IdLote = @IdLote AND Disponible + (@Cantidad - Cantidad) >= 0", lote);
                if (filas == 0)
                {
                    throw new ReglaNegocioException("La cantidad no puede ser menor que lo que ya se usó en modelos.");
                }
                lote.Disponible = cn.ExecuteScalar<decimal>("SELECT Disponible FROM dbo.Lotes WHERE IdLote = @IdLote", lote);
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

        internal static int Insertar(IDbConnection cn, IDbTransaction tx, Lote lote)
        {
            Validar(lote);
            lote.Disponible = lote.Cantidad;
            return cn.ExecuteScalar<int>(@"
                INSERT INTO dbo.Lotes (IdMaterial, Fabricante, Ubicacion, Cantidad, Disponible, ValorUnitario, FechaIngreso)
                VALUES (@IdMaterial, @Fabricante, @Ubicacion, @Cantidad, @Disponible, @ValorUnitario, @FechaIngreso);
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
