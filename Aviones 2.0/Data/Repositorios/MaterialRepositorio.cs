using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    public class MaterialRepositorio
    {
        private const string ColumnasMaterial =
            "IdMaterial, IdFamilia, Grupo, Caracteristica, Color, Medidas, Codigo, Estado, IdTipoUnidad, CantidadAComprar";

        /// <summary>Todos los materiales (también los sin stock), con el stock sumado de sus lotes.</summary>
        public List<MaterialStock> ListarStock(FiltroMateriales filtro = null)
        {
            filtro = filtro ?? new FiltroMateriales();
            var condiciones = new List<string>();
            var parametros = new DynamicParameters();

            if (filtro.IdFamilia.HasValue)
            {
                condiciones.Add("S.IdFamilia = @idFamilia");
                parametros.Add("idFamilia", filtro.IdFamilia.Value);
            }
            if (!string.IsNullOrWhiteSpace(filtro.Texto))
            {
                condiciones.Add(@"(S.Grupo LIKE @texto OR S.Caracteristica LIKE @texto OR S.Color LIKE @texto
                    OR S.Medidas LIKE @texto OR S.Codigo LIKE @texto
                    OR EXISTS (SELECT 1 FROM dbo.Lotes L WHERE L.IdMaterial = S.IdMaterial
                               AND (L.Fabricante LIKE @texto OR L.Ubicacion LIKE @texto)))");
                parametros.Add("texto", "%" + EscaparLike(filtro.Texto.Trim()) + "%");
            }
            if (filtro.SoloSinStock)
            {
                condiciones.Add("S.Disponible = 0");
            }
            if (filtro.SoloAComprar)
            {
                condiciones.Add("S.CantidadAComprar > 0");
            }

            string sql = "SELECT * FROM dbo.vMaterialesStock S"
                + (condiciones.Count > 0 ? " WHERE " + string.Join(" AND ", condiciones) : "")
                + " ORDER BY S.Familia, S.Grupo, S.Caracteristica, S.Medidas";

            using (var cn = Db.Abrir())
            {
                return cn.Query<MaterialStock>(sql, parametros).ToList();
            }
        }

        public Material Obtener(int idMaterial)
        {
            using (var cn = Db.Abrir())
            {
                return cn.QuerySingleOrDefault<Material>(
                    $"SELECT {ColumnasMaterial} FROM dbo.Materiales WHERE IdMaterial = @idMaterial", new { idMaterial });
            }
        }

        /// <summary>Crea el material y, si se pasa, su primer lote, en una sola transacción.</summary>
        public int Crear(Material material, Lote loteInicial = null)
        {
            Validar(material);
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                int id = cn.ExecuteScalar<int>(@"
                    INSERT INTO dbo.Materiales (IdFamilia, Grupo, Caracteristica, Color, Medidas, Codigo, Estado, IdTipoUnidad, CantidadAComprar)
                    VALUES (@IdFamilia, @Grupo, @Caracteristica, @Color, @Medidas, @Codigo, @Estado, @IdTipoUnidad, @CantidadAComprar);
                    SELECT CAST(SCOPE_IDENTITY() AS int);", material, tx);

                if (loteInicial != null)
                {
                    loteInicial.IdMaterial = id;
                    loteInicial.IdLote = LoteRepositorio.Insertar(cn, tx, loteInicial);
                }

                tx.Commit();
                material.IdMaterial = id;
                return id;
            }
        }

        public void Actualizar(Material material)
        {
            Validar(material);
            using (var cn = Db.Abrir())
            {
                cn.Execute(@"
                    UPDATE dbo.Materiales SET
                        IdFamilia = @IdFamilia, Grupo = @Grupo, Caracteristica = @Caracteristica, Color = @Color,
                        Medidas = @Medidas, Codigo = @Codigo, Estado = @Estado, IdTipoUnidad = @IdTipoUnidad,
                        CantidadAComprar = @CantidadAComprar
                    WHERE IdMaterial = @IdMaterial", material);
            }
        }

        /// <summary>
        /// Borra el material, sus lotes y los renglones de modelos que usaban esos lotes.
        /// Antes de llamar, la pantalla debería avisar con <see cref="ListarUsos"/>.
        /// </summary>
        public void Eliminar(int idMaterial)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                cn.Execute(@"
                    DELETE MM FROM dbo.MaterialesModelo MM
                    JOIN dbo.Lotes L ON L.IdLote = MM.IdLote
                    WHERE L.IdMaterial = @idMaterial", new { idMaterial }, tx);
                cn.Execute("DELETE FROM dbo.Materiales WHERE IdMaterial = @idMaterial", new { idMaterial }, tx);
                tx.Commit();
            }
        }

        /// <summary>En qué modelos se usa el material y cuánto de cada lote.</summary>
        public List<UsoDeMaterial> ListarUsos(int idMaterial)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<UsoDeMaterial>(@"
                    SELECT Mo.IdModelo, Mo.Nombre AS Modelo, Mo.Alias, MM.IdLote, MM.Cantidad
                    FROM dbo.MaterialesModelo MM
                    JOIN dbo.Lotes L    ON L.IdLote = MM.IdLote
                    JOIN dbo.Modelos Mo ON Mo.IdModelo = MM.IdModelo
                    WHERE L.IdMaterial = @idMaterial
                    ORDER BY Mo.Nombre", new { idMaterial }).ToList();
            }
        }

        public byte[] ObtenerImagen(int idMaterial)
        {
            using (var cn = Db.Abrir())
            {
                return cn.ExecuteScalar<byte[]>("SELECT Imagen FROM dbo.Materiales WHERE IdMaterial = @idMaterial", new { idMaterial });
            }
        }

        /// <summary>Guarda la imagen; null la borra.</summary>
        public void GuardarImagen(int idMaterial, byte[] imagen)
        {
            using (var cn = Db.Abrir())
            {
                cn.Execute("UPDATE dbo.Materiales SET Imagen = @imagen WHERE IdMaterial = @idMaterial",
                    new { idMaterial, imagen });
            }
        }

        private static void Validar(Material m)
        {
            if (string.IsNullOrWhiteSpace(m.Grupo))
            {
                throw new ReglaNegocioException("El grupo del material es obligatorio.");
            }
            if (m.Estado != EstadosMaterial.Nuevo && m.Estado != EstadosMaterial.Usado)
            {
                throw new ReglaNegocioException("El estado del material tiene que ser Nuevo o Usado.");
            }
            if (m.CantidadAComprar < 0)
            {
                throw new ReglaNegocioException("La cantidad a comprar no puede ser negativa.");
            }
        }

        private static string EscaparLike(string texto) =>
            texto.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
    }
}
