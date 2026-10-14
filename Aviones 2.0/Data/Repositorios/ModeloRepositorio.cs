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
                    AgregarMaterial(cn, tx, id, asignado.IdLote, asignado.Estado, asignado.Cantidad);
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
        /// primero se quitan (volviendo al stock nuevo o usado, o descartándose), después se cambian
        /// cantidades y por último se agregan. Si algo falla no se guarda nada.
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
                    QuitarMaterial(cn, tx, modelo.IdModelo, q.IdMaterialModelo, q.Destino);
                }
                foreach (var c in cambios.Cantidades)
                {
                    CambiarCantidad(cn, tx, modelo.IdModelo, c.IdMaterialModelo, c.Cantidad, c.Destino);
                }
                foreach (var a in cambios.Agregados)
                {
                    AgregarMaterial(cn, tx, modelo.IdModelo, a.IdLote, a.Estado, a.Cantidad);
                }

                tx.Commit();
            }
        }

        /// <summary>Borra el modelo. Si <paramref name="devolverAlStock"/>, todos sus materiales se reciclan (vuelven como usados).</summary>
        public void Eliminar(int idModelo, bool devolverAlStock)
        {
            var devolver = devolverAlStock
                ? ListarMateriales(idModelo).Select(m => new MaterialADevolver(m.IdMaterialModelo, m.Cantidad))
                : Enumerable.Empty<MaterialADevolver>();
            Eliminar(idModelo, devolver);
        }

        /// <summary>
        /// Borra el modelo devolviendo al stock (nuevo o usado) sólo lo indicado, que puede ser una parte
        /// de lo usado. El resto se descarta. Si algo no es válido no se borra nada.
        /// </summary>
        public void Eliminar(int idModelo, IEnumerable<MaterialADevolver> devolver)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                foreach (var d in devolver ?? Enumerable.Empty<MaterialADevolver>())
                {
                    var renglon = ObtenerRenglon(cn, tx, idModelo, d.IdMaterialModelo);
                    if (d.Cantidad > renglon.Cantidad)
                    {
                        throw new ReglaNegocioException(
                            $"No se puede devolver {d.Cantidad}: el modelo sólo usó {renglon.Cantidad} de ese material.");
                    }
                    if (d.Cantidad > 0)
                    {
                        Stock.Devolver(cn, tx, renglon.IdLote, renglon.Estado, d.Destino, d.Cantidad);
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
                    SELECT MM.IdMaterialModelo, MM.IdModelo, MM.IdLote, MM.Estado, M.IdMaterial, F.Nombre AS Familia,
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

        /// <summary>Asigna material de un lote (de su stock nuevo o usado) al modelo y lo descuenta.</summary>
        public void AgregarMaterial(int idModelo, int idLote, decimal cantidad, string estado = EstadosMaterial.Nuevo)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                AgregarMaterial(cn, tx, idModelo, idLote, estado, cantidad);
                tx.Commit();
            }
        }

        /// <summary>
        /// Cambia cuánto se usó. Si sube, la diferencia se descuenta del mismo stock;
        /// si baja, lo que se quita va según <paramref name="destinoSiBaja"/>.
        /// </summary>
        public void CambiarCantidad(int idMaterialModelo, decimal nuevaCantidad, DestinoMaterial destinoSiBaja)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                CambiarCantidad(cn, tx, null, idMaterialModelo, nuevaCantidad, destinoSiBaja);
                tx.Commit();
            }
        }

        /// <summary>Quita un material del modelo: vuelve como nuevo, como usado (se recicla) o se descarta.</summary>
        public void QuitarMaterial(int idMaterialModelo, DestinoMaterial destino)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                QuitarMaterial(cn, tx, null, idMaterialModelo, destino);
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

        /// <summary>Si el mismo lote y stock ya estaba en el modelo se suma al mismo renglón.</summary>
        private static void AgregarMaterial(IDbConnection cn, IDbTransaction tx, int idModelo, int idLote, string estado, decimal cantidad)
        {
            Stock.Descontar(cn, tx, idLote, estado, cantidad);
            int filas = cn.Execute(
                "UPDATE dbo.MaterialesModelo SET Cantidad = Cantidad + @cantidad WHERE IdModelo = @idModelo AND IdLote = @idLote AND Estado = @estado",
                new { idModelo, idLote, estado, cantidad }, tx);
            if (filas == 0)
            {
                cn.Execute("INSERT INTO dbo.MaterialesModelo (IdModelo, IdLote, Estado, Cantidad) VALUES (@idModelo, @idLote, @estado, @cantidad)",
                    new { idModelo, idLote, estado, cantidad }, tx);
            }
        }

        private static void CambiarCantidad(IDbConnection cn, IDbTransaction tx, int? idModelo, int idMaterialModelo,
            decimal nuevaCantidad, DestinoMaterial destinoSiBaja)
        {
            Stock.ValidarCantidad(nuevaCantidad);
            var actual = ObtenerRenglon(cn, tx, idModelo, idMaterialModelo);
            decimal diferencia = nuevaCantidad - actual.Cantidad;
            if (diferencia > 0) Stock.Descontar(cn, tx, actual.IdLote, actual.Estado, diferencia);
            if (diferencia < 0) Stock.Devolver(cn, tx, actual.IdLote, actual.Estado, destinoSiBaja, -diferencia);
            cn.Execute("UPDATE dbo.MaterialesModelo SET Cantidad = @nuevaCantidad WHERE IdMaterialModelo = @idMaterialModelo",
                new { idMaterialModelo, nuevaCantidad }, tx);
        }

        private static void QuitarMaterial(IDbConnection cn, IDbTransaction tx, int? idModelo, int idMaterialModelo, DestinoMaterial destino)
        {
            var actual = ObtenerRenglon(cn, tx, idModelo, idMaterialModelo);
            Stock.Devolver(cn, tx, actual.IdLote, actual.Estado, destino, actual.Cantidad);
            cn.Execute("DELETE FROM dbo.MaterialesModelo WHERE IdMaterialModelo = @idMaterialModelo", new { idMaterialModelo }, tx);
        }

        /// <summary>Lee el renglón; si se pasa el modelo, verifica que sea de ese modelo.</summary>
        private static LoteAsignado ObtenerRenglon(IDbConnection cn, IDbTransaction tx, int? idModelo, int idMaterialModelo)
        {
            return cn.QuerySingleOrDefault<LoteAsignado>(
                       "SELECT IdLote, Estado, Cantidad FROM dbo.MaterialesModelo WHERE IdMaterialModelo = @idMaterialModelo"
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
