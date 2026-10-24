using System;
using System.Linq;
using Aviones_2._0.Common;

namespace Aviones_2._0.UnitTests.Common
{
    [TestClass]
    public class NovedadesTests
    {
        private const string Texto = @"Encabezado que no se muestra.
= 0.2.0
* Pedido viejo
> Hecho viejo

= 0.3.0
* Pedido A
> Hecho A
! Distinto A
* Pedido B
> Hecho B
";

        [TestMethod]
        public void Leer_ArmaVersionesYCambios()
        {
            var versiones = Novedades.Leer(Texto);

            CollectionAssert.AreEqual(new[] { new Version(0, 3, 0), new Version(0, 2, 0) }, versiones.Select(v => v.Version).ToList());
            var a = versiones[0].Cambios[0];
            Assert.AreEqual("Pedido A", a.Pedido);
            Assert.AreEqual("Hecho A", a.Hecho);
            Assert.AreEqual("Distinto A", a.Diferencia);
            Assert.IsNull(versiones[0].Cambios[1].Diferencia);
        }

        [TestMethod]
        public void Pendientes_LasPosterioresALaVistaHastaLaInstalada()
        {
            var todas = Novedades.Leer(Texto + "= 0.4.0\n* Futuro\n> Futuro\n");

            // Nunca vio ninguna: sólo la instalada
            CollectionAssert.AreEqual(new[] { new Version(0, 3, 0) },
                Novedades.Pendientes(todas, new Version(0, 3, 0), null).Select(v => v.Version).ToList());
            // Vio la 0.2.0 y saltó a la 0.4.0: ve las dos que se perdió
            CollectionAssert.AreEqual(new[] { new Version(0, 4, 0), new Version(0, 3, 0) },
                Novedades.Pendientes(todas, new Version(0, 4, 0), new Version(0, 2, 0)).Select(v => v.Version).ToList());
            // Ya marcó la instalada como vista
            Assert.AreEqual(0, Novedades.Pendientes(todas, new Version(0, 3, 0), new Version(0, 3, 0)).Count);
        }

        [TestMethod]
        public void ElArchivoDelProgramaSeLeeYCadaCambioTieneLoHecho()
        {
            var todas = Novedades.Todas();

            Assert.IsTrue(todas.Count > 0);
            Assert.IsTrue(todas.All(v => v.Cambios.Count > 0));
            Assert.IsTrue(todas.SelectMany(v => v.Cambios).All(c => !string.IsNullOrEmpty(c.Pedido) && !string.IsNullOrEmpty(c.Hecho)));
        }
    }
}
