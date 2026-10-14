using System.Linq;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.UnitTests.Data
{
    [TestClass]
    public class ModeloTests : BaseDePrueba
    {
        private readonly ModeloRepositorio modelos = new ModeloRepositorio();

        private int CrearModelo(params LoteAsignado[] materiales) =>
            modelos.Crear(new Modelo { Nombre = "Piper J-3 Cub", Alias = "Cubby", IdEstadoModelo = EstadoActivo }, materiales);

        [TestMethod]
        public void Crear_DescuentaDelLote()
        {
            var (_, idLote) = CrearMaterialConLote(3.6m);

            int idModelo = CrearModelo(new LoteAsignado(idLote, 1.25m));

            Assert.AreEqual(2.35m, DisponibleDe(idLote));
            var renglon = modelos.ListarMateriales(idModelo).Single();
            Assert.AreEqual(idLote, renglon.IdLote);
            Assert.AreEqual(1.25m, renglon.Cantidad);
            Assert.AreEqual("Top Flite", renglon.Fabricante);
        }

        [TestMethod]
        public void Crear_SinStockSuficienteNoGuardaNada()
        {
            var (_, loteConStock) = CrearMaterialConLote(5m);
            var (_, loteCorto) = CrearMaterialConLote(1m, grupo: "Tela");

            Assert.ThrowsExactly<StockInsuficienteException>(() =>
                CrearModelo(new LoteAsignado(loteConStock, 2m), new LoteAsignado(loteCorto, 3m)));

            Assert.AreEqual(0, modelos.Listar().Count);
            Assert.AreEqual(5m, DisponibleDe(loteConStock));
            Assert.AreEqual(1m, DisponibleDe(loteCorto));
        }

        [TestMethod]
        public void AgregarMaterial_MismoLoteSeSumaAlRenglon()
        {
            var (_, idLote) = CrearMaterialConLote(5m);
            int idModelo = CrearModelo(new LoteAsignado(idLote, 1m));

            modelos.AgregarMaterial(idModelo, idLote, 2m);

            Assert.AreEqual(3m, modelos.ListarMateriales(idModelo).Single().Cantidad);
            Assert.AreEqual(2m, DisponibleDe(idLote));
        }

        [TestMethod]
        public void CambiarCantidad_DescuentaODevuelveLaDiferencia()
        {
            var (_, idLote) = CrearMaterialConLote(5m);
            int idModelo = CrearModelo(new LoteAsignado(idLote, 2m));
            int idRenglon = modelos.ListarMateriales(idModelo).Single().IdMaterialModelo;

            modelos.CambiarCantidad(idRenglon, 3.5m, Devolucion.Nada);
            Assert.AreEqual(1.5m, DisponibleDe(idLote));

            modelos.CambiarCantidad(idRenglon, 0.5m, Devolucion.ComoNuevo(3m));
            Assert.AreEqual(4.5m, DisponibleDe(idLote));
        }

        [TestMethod]
        public void QuitarMaterial_ReciclableVuelveAlStock()
        {
            var (_, idLote) = CrearMaterialConLote(5m);
            int idModelo = CrearModelo(new LoteAsignado(idLote, 2m));

            modelos.QuitarMaterial(modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, Devolucion.ComoUsado(2m));

            Assert.AreEqual(5m, DisponibleDe(idLote));
            Assert.AreEqual(0, modelos.ListarMateriales(idModelo).Count);
        }

        [TestMethod]
        public void QuitarMaterial_NoReciclableSeDaPorConsumido()
        {
            var (_, idLote) = CrearMaterialConLote(5m);
            int idModelo = CrearModelo(new LoteAsignado(idLote, 2m));

            modelos.QuitarMaterial(modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, Devolucion.Nada);

            Assert.AreEqual(3m, DisponibleDe(idLote));
        }

        [TestMethod]
        public void Eliminar_PuedeDevolverTodoAlStock()
        {
            var (_, lote1) = CrearMaterialConLote(5m);
            var (_, lote2) = CrearMaterialConLote(2m, grupo: "Tela");
            int idModelo = CrearModelo(new LoteAsignado(lote1, 1m), new LoteAsignado(lote2, 2m));

            modelos.Eliminar(idModelo, devolverAlStock: true);

            Assert.IsNull(modelos.Obtener(idModelo));
            Assert.AreEqual(5m, DisponibleDe(lote1));
            Assert.AreEqual(2m, DisponibleDe(lote2));
        }

        [TestMethod]
        public void Guardar_AplicaQuitarCambiarYAgregarJuntos()
        {
            var (_, loteA) = CrearMaterialConLote(5m);
            var (_, loteB) = CrearMaterialConLote(5m, grupo: "Tela");
            var (_, loteC) = CrearMaterialConLote(5m, grupo: "Cinta");
            int idModelo = CrearModelo(new LoteAsignado(loteA, 2m), new LoteAsignado(loteB, 2m));
            var renglones = modelos.ListarMateriales(idModelo);
            var modelo = modelos.Obtener(idModelo);

            var cambios = new CambiosDeMateriales();
            cambios.Quitados.Add(new MaterialQuitado { IdMaterialModelo = renglones.Single(r => r.IdLote == loteA).IdMaterialModelo, Devolucion = Devolucion.ComoUsado(2m) });
            cambios.Cantidades.Add(new CambioDeCantidad { IdMaterialModelo = renglones.Single(r => r.IdLote == loteB).IdMaterialModelo, Cantidad = 3m });
            cambios.Agregados.Add(new LoteAsignado(loteC, 1.5m));
            modelos.Guardar(modelo, cambios);

            Assert.AreEqual(5m, DisponibleDe(loteA));
            Assert.AreEqual(2m, DisponibleDe(loteB));
            Assert.AreEqual(3.5m, DisponibleDe(loteC));
            var guardado = modelos.Obtener(idModelo);
            Assert.AreEqual(2, guardado.CantidadMateriales);
            Assert.AreEqual(3m * 10m + 1.5m * 10m, guardado.ValorMateriales);
        }

        [TestMethod]
        public void Guardar_SiAlgoFallaNoCambiaNada()
        {
            var (_, loteA) = CrearMaterialConLote(5m);
            var (_, loteCorto) = CrearMaterialConLote(1m, grupo: "Tela");
            int idModelo = CrearModelo(new LoteAsignado(loteA, 2m));
            var modelo = modelos.Obtener(idModelo);
            modelo.Nombre = "Otro nombre";

            var cambios = new CambiosDeMateriales();
            cambios.Quitados.Add(new MaterialQuitado { IdMaterialModelo = modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, Devolucion = Devolucion.ComoUsado(2m) });
            cambios.Agregados.Add(new LoteAsignado(loteCorto, 4m));

            Assert.ThrowsExactly<StockInsuficienteException>(() => modelos.Guardar(modelo, cambios));
            Assert.AreEqual("Piper J-3 Cub", modelos.Obtener(idModelo).Nombre);
            Assert.AreEqual(3m, DisponibleDe(loteA));
            Assert.AreEqual(1, modelos.ListarMateriales(idModelo).Count);
        }

        [TestMethod]
        public void Guardar_NoTocaRenglonesDeOtroModelo()
        {
            var (_, lote) = CrearMaterialConLote(5m);
            int otro = CrearModelo(new LoteAsignado(lote, 1m));
            int idModelo = CrearModelo();
            var cambios = new CambiosDeMateriales();
            cambios.Quitados.Add(new MaterialQuitado { IdMaterialModelo = modelos.ListarMateriales(otro).Single().IdMaterialModelo });

            Assert.ThrowsExactly<ReglaNegocioException>(() => modelos.Guardar(modelos.Obtener(idModelo), cambios));
            Assert.AreEqual(1, modelos.ListarMateriales(otro).Count);
        }

        [TestMethod]
        public void Eliminar_DevuelveSoloLoQueSeRecicla()
        {
            var (_, motor) = CrearMaterialConLote(3m, grupo: "Motor");
            var (_, servo) = CrearMaterialConLote(10m, grupo: "Servo");
            var (_, cola) = CrearMaterialConLote(2m, grupo: "Cola");
            int idModelo = CrearModelo(new LoteAsignado(motor, 1m), new LoteAsignado(servo, 4m), new LoteAsignado(cola, 1m));
            var renglones = modelos.ListarMateriales(idModelo);

            modelos.Eliminar(idModelo, new[]
            {
                new MaterialADevolver(renglones.Single(r => r.IdLote == motor).IdMaterialModelo, Devolucion.ComoUsado(1m)),
                new MaterialADevolver(renglones.Single(r => r.IdLote == servo).IdMaterialModelo, Devolucion.ComoUsado(2.5m))
            });

            Assert.IsNull(modelos.Obtener(idModelo));
            Assert.AreEqual(3m, DisponibleDe(motor));
            Assert.AreEqual(8.5m, DisponibleDe(servo));
            Assert.AreEqual(1m, DisponibleDe(cola));
        }

        [TestMethod]
        public void Eliminar_NoDevuelveMasDeLoUsadoYNoBorraNada()
        {
            var (_, lote) = CrearMaterialConLote(10m);
            int idModelo = CrearModelo(new LoteAsignado(lote, 4m));
            int idRenglon = modelos.ListarMateriales(idModelo).Single().IdMaterialModelo;

            Assert.ThrowsExactly<ReglaNegocioException>(() =>
                modelos.Eliminar(idModelo, new[] { new MaterialADevolver(idRenglon, Devolucion.ComoUsado(5m)) }));

            Assert.IsNotNull(modelos.Obtener(idModelo));
            Assert.AreEqual(6m, DisponibleDe(lote));
        }

        [TestMethod]
        public void Quitar_ReciclandoVuelveAlStockUsadoDelMismoLote()
        {
            var (_, lote) = CrearMaterialConLote(5m);
            int idModelo = CrearModelo(new LoteAsignado(lote, 2m));

            modelos.QuitarMaterial(modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, Devolucion.ComoUsado(2m));

            var l = new LoteRepositorio().Obtener(lote);
            Assert.AreEqual(3m, l.DisponibleNuevo);
            Assert.AreEqual(2m, l.DisponibleUsado);
        }

        [TestMethod]
        public void Quitar_ComoNuevoVuelveAlStockNuevo()
        {
            var (_, lote) = CrearMaterialConLote(5m);
            int idModelo = CrearModelo(new LoteAsignado(lote, 2m));

            modelos.QuitarMaterial(modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, Devolucion.ComoNuevo(2m));

            var l = new LoteRepositorio().Obtener(lote);
            Assert.AreEqual(5m, l.DisponibleNuevo);
            Assert.AreEqual(0m, l.DisponibleUsado);
        }

        [TestMethod]
        public void LoUsadoNoPuedeVolverComoNuevo()
        {
            var (_, lote) = CrearMaterialConLote(5m);
            int primero = CrearModelo(new LoteAsignado(lote, 2m));
            modelos.QuitarMaterial(modelos.ListarMateriales(primero).Single().IdMaterialModelo, Devolucion.ComoUsado(2m));

            int segundo = CrearModelo(new LoteAsignado(lote, 1m, EstadosMaterial.Usado));
            var renglon = modelos.ListarMateriales(segundo).Single();
            Assert.AreEqual(EstadosMaterial.Usado, renglon.Estado);
            Assert.AreEqual(1m, new LoteRepositorio().Obtener(lote).DisponibleUsado);

            Assert.ThrowsExactly<ReglaNegocioException>(() => modelos.QuitarMaterial(renglon.IdMaterialModelo, Devolucion.ComoNuevo(1m)));
        }

        [TestMethod]
        public void TomarDelStockUsadoSinUsadoDisponibleFalla()
        {
            var (_, lote) = CrearMaterialConLote(5m);

            Assert.ThrowsExactly<StockInsuficienteException>(() => CrearModelo(new LoteAsignado(lote, 1m, EstadosMaterial.Usado)));
        }

        [TestMethod]
        public void BajarCantidadDescartandoNoDevuelveNada()
        {
            var (_, lote) = CrearMaterialConLote(5m);
            int idModelo = CrearModelo(new LoteAsignado(lote, 3m));

            modelos.CambiarCantidad(modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, 1m, Devolucion.Nada);

            Assert.AreEqual(2m, DisponibleDe(lote));
            Assert.AreEqual(1m, modelos.ListarMateriales(idModelo).Single().Cantidad);
        }

        [TestMethod]
        public void MismoLoteNuevoYUsadoSonRenglonesDistintos()
        {
            var (_, lote) = CrearMaterialConLote(5m);
            int previo = CrearModelo(new LoteAsignado(lote, 2m));
            modelos.QuitarMaterial(modelos.ListarMateriales(previo).Single().IdMaterialModelo, Devolucion.ComoUsado(2m));

            int idModelo = CrearModelo(new LoteAsignado(lote, 1m), new LoteAsignado(lote, 1m, EstadosMaterial.Usado));

            var renglones = modelos.ListarMateriales(idModelo);
            Assert.AreEqual(2, renglones.Count);
            CollectionAssert.AreEquivalent(new[] { "N", "U" }, renglones.Select(r => r.Estado).ToList());
        }

        [TestMethod]
        public void Eliminar_CadaMaterialConSuDestino()
        {
            var (_, motor) = CrearMaterialConLote(3m, grupo: "Motor");
            var (_, servo) = CrearMaterialConLote(10m, grupo: "Servo");
            int idModelo = CrearModelo(new LoteAsignado(motor, 1m), new LoteAsignado(servo, 4m));
            var renglones = modelos.ListarMateriales(idModelo);

            modelos.Eliminar(idModelo, new[]
            {
                new MaterialADevolver(renglones.Single(r => r.IdLote == motor).IdMaterialModelo, Devolucion.ComoUsado(1m)),
                new MaterialADevolver(renglones.Single(r => r.IdLote == servo).IdMaterialModelo, Devolucion.ComoNuevo(3m))
            });

            var lotes = new LoteRepositorio();
            Assert.AreEqual(1m, lotes.Obtener(motor).DisponibleUsado);
            Assert.AreEqual(9m, lotes.Obtener(servo).DisponibleNuevo);
            Assert.AreEqual(0m, lotes.Obtener(servo).DisponibleUsado);
        }

        [TestMethod]
        public void Quitar_RepartidoEntreNuevoUsadoYDescartado()
        {
            var (_, alas) = CrearMaterialConLote(10m, grupo: "Ala");
            int idModelo = CrearModelo(new LoteAsignado(alas, 5m));

            // 5 piezas salen: 2 vuelven nuevas, 2 usadas y 1 se descarta
            modelos.QuitarMaterial(modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, new Devolucion(2m, 2m));

            var l = new LoteRepositorio().Obtener(alas);
            Assert.AreEqual(7m, l.DisponibleNuevo);
            Assert.AreEqual(2m, l.DisponibleUsado);
            Assert.AreEqual(1m, l.Consumido);
        }

        [TestMethod]
        public void BajarCantidad_RepartidoYNoMasDeLoQueSale()
        {
            var (_, lote) = CrearMaterialConLote(10m);
            int idModelo = CrearModelo(new LoteAsignado(lote, 5m));
            int idRenglon = modelos.ListarMateriales(idModelo).Single().IdMaterialModelo;

            Assert.ThrowsExactly<ReglaNegocioException>(() => modelos.CambiarCantidad(idRenglon, 3m, new Devolucion(2m, 1m)));

            modelos.CambiarCantidad(idRenglon, 3m, new Devolucion(1m, 1m));
            var l = new LoteRepositorio().Obtener(lote);
            Assert.AreEqual(6m, l.DisponibleNuevo);
            Assert.AreEqual(1m, l.DisponibleUsado);
        }

        [TestMethod]
        public void Actualizar_GuardaDatosYFecha()
        {
            int idModelo = CrearModelo();
            var modelo = modelos.Obtener(idModelo);
            modelo.Nombre = "Cessna 182";
            modelo.IdEstadoModelo = 3;
            modelo.Desde = new System.DateTime(2026, 10, 13);

            modelos.Actualizar(modelo);

            var guardado = modelos.Obtener(idModelo);
            Assert.AreEqual("Cessna 182", guardado.Nombre);
            Assert.AreEqual("En reparación", guardado.Estado);
            Assert.AreEqual(new System.DateTime(2026, 10, 13), guardado.Desde);
        }
    }
}
