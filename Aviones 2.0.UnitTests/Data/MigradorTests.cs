using System.Data.SqlClient;
using System.Linq;
using Aviones_2._0.Data;

namespace Aviones_2._0.UnitTests.Data
{
    [TestClass]
    public class MigradorTests : BaseDePrueba
    {
        private static int Contar(string sql)
        {
            using (var cn = Db.Abrir())
            {
                return (int)new SqlCommand(sql, cn).ExecuteScalar();
            }
        }

        [TestMethod]
        public void BaseNueva_QuedanTodasLasMigracionesAplicadas()
        {
            var numeros = Migrador.Todas().Select(m => m.Numero).ToList();

            Assert.IsTrue(numeros.Count > 0);
            CollectionAssert.AreEqual(numeros.OrderBy(n => n).ToList(), numeros);
            Assert.AreEqual(numeros.Count, Contar("SELECT COUNT(*) FROM dbo.Migraciones"));
        }

        [TestMethod]
        public void SinPendientes_NoHaceNadaNiCopia()
        {
            var resultado = Migrador.Actualizar(Db.ConnectionString);

            Assert.AreEqual(0, resultado.Aplicadas.Count);
            Assert.IsNull(resultado.Copia);
        }

        [TestMethod]
        public void BaseDeUnaVersionMasNueva_NoSeToca()
        {
            using (var cn = Db.Abrir())
            {
                new SqlCommand("INSERT INTO dbo.Migraciones (Numero, Nombre) VALUES (9999, N'del futuro')", cn).ExecuteNonQuery();
            }

            Assert.ThrowsExactly<System.InvalidOperationException>(() => Migrador.Actualizar(Db.ConnectionString));
        }

        [TestMethod]
        public void BaseAnteriorALasMigraciones_SeMarcaConElEsquemaInicial()
        {
            // Una base de antes: sólo el esquema inicial, sin tabla de migraciones
            BorrarTodo();
            using (var cn = Db.Abrir())
            {
                foreach (string lote in Migrador.Lotes(Migrador.Todas().First().Script))
                {
                    new SqlCommand(lote, cn).ExecuteNonQuery();
                }
            }

            var resultado = Migrador.Actualizar(Db.ConnectionString);

            Assert.AreEqual(1, Contar("SELECT COUNT(*) FROM dbo.Migraciones WHERE Numero = 1"));
            Assert.AreEqual(Migrador.Todas().Count, Contar("SELECT COUNT(*) FROM dbo.Migraciones"));
            CollectionAssert.DoesNotContain(resultado.Aplicadas.Select(m => m.Numero).ToList(), 1);
            // Ya tenía datos (el esquema inicial): antes de cambiarla se hizo una copia
            Assert.IsNotNull(resultado.Copia);
        }

        [TestMethod]
        public void Migracion3_ArmaLasListasConLoQueEstabaEscrito()
        {
            // Una base con sólo el esquema inicial y lotes con el fabricante y la ubicación escritos a mano
            BorrarTodo();
            using (var cn = Db.Abrir())
            {
                foreach (string lote in Migrador.Lotes(Migrador.Todas().First().Script))
                {
                    new SqlCommand(lote, cn).ExecuteNonQuery();
                }
                new SqlCommand(@"
                    INSERT INTO dbo.Familias (Nombre) VALUES (N'Electrónica');
                    INSERT INTO dbo.TiposUnidad (Nombre) VALUES (N'Unidades');
                    INSERT INTO dbo.Materiales (IdFamilia, Grupo, IdTipoUnidad) VALUES (1, N'Servo', 1);
                    INSERT INTO dbo.Lotes (IdMaterial, Fabricante, Ubicacion, Cantidad) VALUES
                        (1, N'Hitec', N'Estante', 1), (1, N' hitec ', N'Estante', 1), (1, N'Hitec', NULL, 1),
                        (1, N'', N'  Cajón', 1), (1, NULL, N'cajón', 1);", cn).ExecuteNonQuery();
            }

            Migrador.Actualizar(Db.ConnectionString);

            Assert.AreEqual(1, Contar("SELECT COUNT(*) FROM dbo.Fabricantes WHERE Nombre = N'Hitec' COLLATE Latin1_General_BIN"));
            Assert.AreEqual(1, Contar("SELECT COUNT(*) FROM dbo.Fabricantes"));
            Assert.AreEqual(2, Contar("SELECT COUNT(*) FROM dbo.Ubicaciones"));
            Assert.AreEqual(3, Contar("SELECT COUNT(*) FROM dbo.Lotes WHERE IdFabricante IS NOT NULL"));
            Assert.AreEqual(4, Contar("SELECT COUNT(*) FROM dbo.Lotes WHERE IdUbicacion IS NOT NULL"));
            Assert.AreEqual(0, Contar("SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.Lotes') AND name IN (N'Fabricante', N'Ubicacion')"));
        }

        [TestMethod]
        public void Lotes_SeSeparanPorGo()
        {
            var lotes = Migrador.Lotes("SELECT 1;\r\nGO\r\n  go  \r\nSELECT 2;\nGO\n").ToList();

            Assert.AreEqual(2, lotes.Count);
            StringAssert.Contains(lotes[1], "SELECT 2");
        }
    }
}
