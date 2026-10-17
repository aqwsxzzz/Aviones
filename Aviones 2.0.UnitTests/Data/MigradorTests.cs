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
        public void Lotes_SeSeparanPorGo()
        {
            var lotes = Migrador.Lotes("SELECT 1;\r\nGO\r\n  go  \r\nSELECT 2;\nGO\n").ToList();

            Assert.AreEqual(2, lotes.Count);
            StringAssert.Contains(lotes[1], "SELECT 2");
        }
    }
}
