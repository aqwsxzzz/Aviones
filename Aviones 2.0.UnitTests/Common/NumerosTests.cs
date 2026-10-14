using Aviones_2._0.Common;

namespace Aviones_2._0.UnitTests.Common
{
    [TestClass]
    public class NumerosTests
    {
        [TestMethod]
        [DataRow("1,5", 1.5)]
        [DataRow("1.5", 1.5)]
        [DataRow(" 0,66 ", 0.66)]
        [DataRow("3", 3.0)]
        [DataRow("1.234,5", 1234.5)]
        [DataRow("1,234.5", 1234.5)]
        [DataRow("-2", -2.0)]
        public void TryLeer_AceptaComaOPunto(string texto, double esperado)
        {
            Assert.IsTrue(Numeros.TryLeer(texto, out decimal valor));
            Assert.AreEqual((decimal)esperado, valor);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("abc")]
        [DataRow("1,2,3")]
        public void TryLeer_RechazaTextoInvalido(string texto)
        {
            Assert.IsFalse(Numeros.TryLeer(texto, out _));
        }

        [TestMethod]
        public void TryLeerOpcional_VacioEsNull()
        {
            Assert.IsTrue(Numeros.TryLeerOpcional("", out decimal? valor));
            Assert.IsNull(valor);
        }
    }
}
