using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    public class ModeloRepositorio
    {
        private const string SelectModelo = @"
            SELECT Mo.IdModelo, Mo.Nombre, Mo.Alias, Mo.IdEstadoModelo, E.Nombre AS Estado, Mo.Desde,
                   (SELECT COUNT(*) FROM dbo.MaterialesModelo MM WHERE MM.IdModelo = Mo.IdModelo) AS CantidadMateriales,
                   (SELECT ISNULL(SUM(MM.Cantidad * L.ValorUnitario), 0)
                      FROM dbo.MaterialesModelo MM JOIN dbo.Lotes L ON L.IdLote = MM.IdLote
                     WHERE MM.IdModelo = Mo.IdModelo) AS ValorMateriales
            FROM dbo.Modelos Mo
            JOIN dbo.EstadosModelo E ON E.IdEstadoModelo = Mo.IdEstadoModelo";

        public List<Modelo> Listar()
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<Modelo>(SelectModelo + " ORDER BY Mo.Nombre").ToList();
            }
        }

        public Modelo Obtener(int idModelo)
        {
            using (var cn = Db.Abrir())
            {
                return cn.QuerySingleOrDefault<Modelo>(SelectModelo + " WHERE Mo.IdModelo = @idModelo", new { idModelo });
            }
        }

        /// <summary>
        /// Crea el modelo y descuenta del stock cada lote asignado, todo o nada:
        /// si un lote no alcanza no se guarda nada.
        /// </summary>
        public int Crear(Modelo modelo, IEnumerable<LoteAsignado> materiales = null)
        {
            Validar(modelo);
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                int id = cn.ExecuteScalar<int>(@"
                    INSERT INTO dbo.Modelos (Nombre, Alias, IdEstadoModelo, Desde)
                    VALUES (@Nombre, @Alias, @IdEstadoModelo, @Desde);
                    SELECT CAST(SCOPE_IDENTITY() AS int);", modelo, tx);

                foreach (var asignado in materiales ?? Enumerable.Empty<LoteAsignado>())
                {
                    AgregarMaterial(cn, tx, id, asignado.IdLote, asignado.Cantidad);
                }

                tx.Commit();
                modelo.IdModelo = id;
                return id;
            }
        }

        public void Actualizar(Modelo modelo)
        {
            Guardar(modelo, new CambiosDeMateriales());
        }

        /// <summary>
        /// Guarda los datos del modelo y todos los cambios de materiales en una sola transacción:
        /// primero se quitan (devolviendo al stock si corresponde), después se cambian cantidades
        /// y por último se agregan. Si algo falla no se guarda nada.
        /// </summary>
        public void Guardar(Modelo modelo, CambiosDeMateriales cambios)
        {
            Validar(modelo);
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                cn.Execute(@"
                    UPDATE dbo.Modelos SET Nombre = @Nombre, Alias = @Alias, IdEstadoModelo = @IdEstadoModelo, Desde = @Desde
                    WHERE IdModelo = @IdModelo", modelo, tx);

                foreach (var q in cambios.Quitados)
                {
                    QuitarMaterial(cn, tx, modelo.IdModelo, q.IdMaterialModelo, q.DevolverAlStock);
                }
                foreach (var c in cambios.Cantidades)
                {
                    CambiarCantidad(cn, tx, modelo.IdModelo, c.IdMaterialModelo, c.Cantidad);
                }
                foreach (var a in cambios.Agregados)
                {
                    AgregarMaterial(cn, tx, modelo.IdModelo, a.IdLote, a.Cantidad);
                }

                tx.Commit();
            }
        }

        /// <summary>Borra el modelo. Si <paramref name="devolverAlStock"/>, sus materiales vuelven a los lotes.</summary>
        public void Eliminar(int idModelo, bool devolverAlStock)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                if (devolverAlStock)
                {
                    var renglones = cn.Query<LoteAsignado>(
                        "SELECT IdLote, Cantidad FROM dbo.MaterialesModelo WHERE IdModelo = @idModelo", new { idModelo }, tx);
                    foreach (var r in renglones)
                    {
                        Stock.Devolver(cn, tx, r.IdLote, r.Cantidad);
                    }
                }
                cn.Execute("DELETE FROM dbo.Modelos WHERE IdModelo = @idModelo", new { idModelo }, tx);
                tx.Commit();
            }
        }

        public List<MaterialDeModelo> ListarMateriales(int idModelo)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<MaterialDeModelo>(@"
                    SELECT MM.IdMaterialModelo, MM.IdModelo, MM.IdLote, M.IdMaterial, F.Nombre AS Familia,
                           M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo, L.Fabricante,
                           T.Nombre AS TipoUnidad, MM.Cantidad, L.ValorUnitario
                    FROM dbo.MaterialesModelo MM
                    JOIN dbo.Lotes L        ON L.IdLote = MM.IdLote
                    JOIN dbo.Materiales M   ON M.IdMaterial = L.IdMaterial
                    JOIN dbo.Familias F     ON F.IdFamilia = M.IdFamilia
                    JOIN dbo.TiposUnidad T  ON T.IdTipoUnidad = M.IdTipoUnidad
                    WHERE MM.IdModelo = @idModelo
                    ORDER BY F.Nombre, M.Grupo, M.Caracteristica", new { idModelo }).ToList();
            }
        }

        /// <summary>Asigna material de un lote al modelo y lo descuenta del stock.</summary>
        public void AgregarMaterial(int idModelo, int idLote, decimal cantidad)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                AgregarMaterial(cn, tx, idModelo, idLote, cantidad);
                tx.Commit();
            }
        }

        /// <summary>Cambia cuánto se usó; la diferencia se descuenta o se devuelve al lote.</summary>
        public void CambiarCantidad(int idMaterialModelo, decimal nuevaCantidad)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                CambiarCantidad(cn, tx, null, idMaterialModelo, nuevaCantidad);
                tx.Commit();
            }
        }

        /// <summary>
        /// Quita un material del modelo. Si se recicla (<paramref name="devolverAlStock"/>) vuelve al lote;
        /// si no, se da por consumido.
        /// </summary>
        public void QuitarMaterial(int idMaterialModelo, bool devolverAlStock)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                QuitarMaterial(cn, tx, null, idMaterialModelo, devolverAlStock);
                tx.Commit();
            }
        }

        public byte[] ObtenerFoto(int idModelo)
        {
            using (var cn = Db.Abrir())
            {
                return cn.ExecuteScalar<byte[]>("SELECT Foto FROM dbo.Modelos WHERE IdModelo = @idModelo", new { idModelo });
            }
        }

        /// <summary>Guarda la foto; null la borra.</summary>
        public void GuardarFoto(int idModelo, byte[] foto)
        {
            using (var cn = Db.Abrir())
            {
                cn.Execute("UPDATE dbo.Modelos SET Foto = @foto WHERE IdModelo = @idModelo", new { idModelo, foto });
            }
        }

        /// <summary>Si el lote ya estaba en el modelo se suma al mismo renglón.</summary>
        private static void AgregarMaterial(IDbConnection cn, IDbTransaction tx, int idModelo, int idLote, decimal cantidad)
        {
            Stock.Descontar(cn, tx, idLote, cantidad);
            int filas = cn.Execute(
                "UPDATE dbo.MaterialesModelo SET Cantidad = Cantidad + @cantidad WHERE IdModelo = @idModelo AND IdLote = @idLote",
                new { idModelo, idLote, cantidad }, tx);
            if (filas == 0)
            {
                cn.Execute("INSERT INTO dbo.MaterialesModelo (IdModelo, IdLote, Cantidad) VALUES (@idModelo, @idLote, @cantidad)",
                    new { idModelo, idLote, cantidad }, tx);
            }
        }

        private static void CambiarCantidad(IDbConnection cn, IDbTransaction tx, int? idModelo, int idMaterialModelo, decimal nuevaCantidad)
        {
            Stock.ValidarCantidad(nuevaCantidad);
            var actual = ObtenerRenglon(cn, tx, idModelo, idMaterialModelo);
            decimal diferencia = nuevaCantidad - actual.Cantidad;
            if (diferencia > 0) Stock.Descontar(cn, tx, actual.IdLote, diferencia);
            if (diferencia < 0) Stock.Devolver(cn, tx, actual.IdLote, -diferencia);
            cn.Execute("UPDATE dbo.MaterialesModelo SET Cantidad = @nuevaCantidad WHERE IdMaterialModelo = @idMaterialModelo",
                new { idMaterialModelo, nuevaCantidad }, tx);
        }

        private static void QuitarMaterial(IDbConnection cn, IDbTransaction tx, int? idModelo, int idMaterialModelo, bool devolverAlStock)
        {
            var actual = ObtenerRenglon(cn, tx, idModelo, idMaterialModelo);
            if (devolverAlStock)
            {
                Stock.Devolver(cn, tx, actual.IdLote, actual.Cantidad);
            }
            cn.Execute("DELETE FROM dbo.MaterialesModelo WHERE IdMaterialModelo = @idMaterialModelo", new { idMaterialModelo }, tx);
        }

        /// <summary>Lee el renglón; si se pasa el modelo, verifica que sea de ese modelo.</summary>
        private static LoteAsignado ObtenerRenglon(IDbConnection cn, IDbTransaction tx, int? idModelo, int idMaterialModelo)
        {
            return cn.QuerySingleOrDefault<LoteAsignado>(
                       "SELECT IdLote, Cantidad FROM dbo.MaterialesModelo WHERE IdMaterialModelo = @idMaterialModelo"
                       + (idModelo.HasValue ? " AND IdModelo = @idModelo" : ""),
                       new { idMaterialModelo, idModelo }, tx)
                   ?? throw new ReglaNegocioException("Ese material ya no está en el modelo.");
        }

        private static void Validar(Modelo modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.Nombre))
            {
                throw new ReglaNegocioException("El nombre del modelo es obligatorio.");
            }
        }
    }
}
