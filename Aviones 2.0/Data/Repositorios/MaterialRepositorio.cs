using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    public class MaterialRepositorio
    {
        private const string ColumnasMaterial =
            "IdMaterial, IdFamilia, Grupo, Caracteristica, Color, Medidas, Codigo, IdTipoUnidad, StockMinimo, SeRepone, Nota";

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
                    OR EXISTS (SELECT 1 FROM dbo.Lotes L
                               LEFT JOIN dbo.Fabricantes F ON F.IdFabricante = L.IdFabricante
                               LEFT JOIN dbo.Ubicaciones U ON U.IdUbicacion = L.IdUbicacion
                               WHERE L.IdMaterial = S.IdMaterial AND (F.Nombre LIKE @texto OR U.Nombre LIKE @texto)))");
                parametros.Add("texto", "%" + EscaparLike(filtro.Texto.Trim()) + "%");
            }
            if (filtro.SoloSinStock)
            {
                condiciones.Add("S.Disponible = 0");
            }
            if (filtro.SoloAComprar)
            {
                condiciones.Add("S.PedidoPendiente > 0");
            }

            string sql = "SELECT * FROM dbo.vMaterialesStock S"
                + (condiciones.Count > 0 ? " WHERE " + string.Join(" AND ", condiciones) : "")
                + " ORDER BY S.Familia, S.Grupo, S.Caracteristica, S.Medidas";

            using (var cn = Db.Abrir())
            {
                return cn.Query<MaterialStock>(sql, parametros).ToList();
            }
        }

        /// <summary>Un material con su stock sumado.</summary>
        public MaterialStock ObtenerStock(int idMaterial)
        {
            using (var cn = Db.Abrir())
            {
                return cn.QuerySingleOrDefault<MaterialStock>(
                    "SELECT * FROM dbo.vMaterialesStock WHERE IdMaterial = @idMaterial", new { idMaterial });
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
                    INSERT INTO dbo.Materiales (IdFamilia, Grupo, Caracteristica, Color, Medidas, Codigo, IdTipoUnidad, StockMinimo, SeRepone, Nota)
                    VALUES (@IdFamilia, @Grupo, @Caracteristica, @Color, @Medidas, @Codigo, @IdTipoUnidad, @StockMinimo, @SeRepone, @Nota);
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
                        Medidas = @Medidas, Codigo = @Codigo, IdTipoUnidad = @IdTipoUnidad,
                        StockMinimo = @StockMinimo, SeRepone = @SeRepone, Nota = @Nota
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
                cn.Execute("DELETE FROM dbo.PedidosCompra WHERE IdMaterial = @idMaterial", new { idMaterial }, tx);
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
                    SELECT Mo.IdModelo, Mo.Nombre AS Modelo, Mo.Alias, MM.IdLote, MM.Estado, MM.Cantidad
                    FROM dbo.MaterialesModelo MM
                    JOIN dbo.Lotes L    ON L.IdLote = MM.IdLote
                    JOIN dbo.Modelos Mo ON Mo.IdModelo = MM.IdModelo
                    WHERE L.IdMaterial = @idMaterial
                    ORDER BY Mo.Nombre", new { idMaterial }).ToList();
            }
        }

        /// <summary>
        /// Materiales que describen lo mismo: misma familia, grupo, característica, color, medidas y código
        /// (sin distinguir mayúsculas ni espacios de más; vacío y sin dato cuentan igual).
        /// Sirve para avisar antes de crear un repetido.
        /// </summary>
        public List<MaterialStock> BuscarIguales(Material m)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<MaterialStock>(@"
                    SELECT * FROM dbo.vMaterialesStock S
                    WHERE S.IdFamilia = @IdFamilia
                      AND S.IdMaterial <> @IdMaterial
                      AND LOWER(LTRIM(RTRIM(ISNULL(S.Grupo, ''))))          = @grupo
                      AND LOWER(LTRIM(RTRIM(ISNULL(S.Caracteristica, '')))) = @caracteristica
                      AND LOWER(LTRIM(RTRIM(ISNULL(S.Color, ''))))          = @color
                      AND LOWER(LTRIM(RTRIM(ISNULL(S.Medidas, ''))))        = @medidas
                      AND LOWER(LTRIM(RTRIM(ISNULL(S.Codigo, ''))))         = @codigo",
                    new
                    {
                        m.IdFamilia,
                        m.IdMaterial,
                        grupo = Normalizar(m.Grupo),
                        caracteristica = Normalizar(m.Caracteristica),
                        color = Normalizar(m.Color),
                        medidas = Normalizar(m.Medidas),
                        codigo = Normalizar(m.Codigo)
                    }).ToList();
            }
        }

        private static string Normalizar(string texto) =>
            System.Text.RegularExpressions.Regex.Replace((texto ?? "").Trim(), @"\s+", " ").ToLowerInvariant();

        /// <summary>Grupos ya usados, para sugerirlos al escribir.</summary>
        public List<string> ListarGrupos() => ListarDistintos("Grupo");

        public List<string> ListarCaracteristicas() => ListarDistintos("Caracteristica");

        public List<string> ListarColores() => ListarDistintos("Color");

        public List<string> ListarMedidas() => ListarDistintos("Medidas");

        /// <summary>Lo ya escrito en una columna, para sugerirlo al escribir.</summary>
        private static List<string> ListarDistintos(string columna)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<string>(
                    $"SELECT DISTINCT {columna} FROM dbo.Materiales WHERE {columna} IS NOT NULL AND {columna} <> '' ORDER BY {columna}").ToList();
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
            // Texto prolijo: sin espacios de más, y vacío se guarda como sin dato
            m.Grupo = Limpiar(m.Grupo);
            m.Caracteristica = Limpiar(m.Caracteristica);
            m.Color = Limpiar(m.Color);
            m.Medidas = Limpiar(m.Medidas);
            m.Codigo = Limpiar(m.Codigo);

            if (string.IsNullOrWhiteSpace(m.Grupo))
            {
                throw new ReglaNegocioException("El grupo del material es obligatorio.");
            }
            if (m.StockMinimo < 0)
            {
                throw new ReglaNegocioException("El stock mínimo no puede ser negativo.");
            }
            m.Nota = string.IsNullOrWhiteSpace(m.Nota) ? null : m.Nota.Trim();
            if (m.Nota?.Length > 500)
            {
                throw new ReglaNegocioException("La nota no puede tener más de 500 letras.");
            }
        }

        private static string Limpiar(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return null;
            return System.Text.RegularExpressions.Regex.Replace(texto.Trim(), @"\s+", " ");
        }

        private static string EscaparLike(string texto) =>
            texto.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
    }
}
