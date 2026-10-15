using System.Data.SqlClient;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.UnitTests.Data
{
    /// <summary>
    /// Pruebas contra una base real (Aviones_Test en localhost).
    /// Cada prueba arranca con el esquema vacío recién creado.
    /// </summary>
    public abstract class BaseDePrueba
    {
        private const string Servidor = "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True";
        private const string NombreBase = "Aviones_Test";
        private static bool baseCreada;

        protected int IdFamilia;
        protected int IdMetros;

        [TestInitialize]
        public void ReiniciarBase()
        {
            if (!baseCreada)
            {
                using (var cn = new SqlConnection(Servidor + ";Initial Catalog=master"))
                {
                    cn.Open();
                    new SqlCommand($"IF DB_ID(N'{NombreBase}') IS NULL CREATE DATABASE [{NombreBase}];", cn).ExecuteNonQuery();
                }
                baseCreada = true;
            }

            Db.ConnectionString = Servidor + ";Initial Catalog=" + NombreBase;
            using (var cn = Db.Abrir())
            {
                // Base vacía: se borra todo y se vuelve a armar con las migraciones, como en una instalación nueva
                new SqlCommand(@"
                    DECLARE @sql NVARCHAR(MAX) = N'';
                    SELECT @sql += N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(parent_object_id))
                                 + N' DROP CONSTRAINT ' + QUOTENAME(name) + N';' FROM sys.foreign_keys;
                    SELECT @sql += N'DROP VIEW ' + QUOTENAME(SCHEMA_NAME(schema_id)) + N'.' + QUOTENAME(name) + N';' FROM sys.views;
                    SELECT @sql += N'DROP TABLE ' + QUOTENAME(SCHEMA_NAME(schema_id)) + N'.' + QUOTENAME(name) + N';' FROM sys.tables;
                    EXEC (@sql);", cn).ExecuteNonQuery();
            }
            Migrador.Actualizar(Db.ConnectionString);

            IdFamilia = new FamiliaRepositorio().Crear("Revestimientos");
            IdMetros = new TipoUnidadRepositorio().Crear("Metros");
        }

        /// <summary>Crea un material con un lote y devuelve (IdMaterial, IdLote).</summary>
        protected (int IdMaterial, int IdLote) CrearMaterialConLote(decimal cantidad, decimal valor = 10m,
            string grupo = "Plástico", string fabricante = "Top Flite", string ubicacion = "Bolsa Macro")
        {
            var lote = new Lote { Cantidad = cantidad, ValorUnitario = valor, Fabricante = fabricante, Ubicacion = ubicacion };
            int idMaterial = new MaterialRepositorio().Crear(
                new Material { IdFamilia = IdFamilia, Grupo = grupo, Caracteristica = "Monokote", IdTipoUnidad = IdMetros },
                lote);
            return (idMaterial, lote.IdLote);
        }

        protected static int EstadoActivo => 1;

        protected static decimal DisponibleDe(int idLote) => new LoteRepositorio().Obtener(idLote).Disponible;
    }
}
