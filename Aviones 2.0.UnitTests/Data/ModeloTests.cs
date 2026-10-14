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

            modelos.CambiarCantidad(idRenglon, 3.5m);
            Assert.AreEqual(1.5m, DisponibleDe(idLote));

            modelos.CambiarCantidad(idRenglon, 0.5m);
            Assert.AreEqual(4.5m, DisponibleDe(idLote));
        }

        [TestMethod]
        public void QuitarMaterial_ReciclableVuelveAlStock()
        {
            var (_, idLote) = CrearMaterialConLote(5m);
            int idModelo = CrearModelo(new LoteAsignado(idLote, 2m));

            modelos.QuitarMaterial(modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, devolverAlStock: true);

            Assert.AreEqual(5m, DisponibleDe(idLote));
            Assert.AreEqual(0, modelos.ListarMateriales(idModelo).Count);
        }

        [TestMethod]
        public void QuitarMaterial_NoReciclableSeDaPorConsumido()
        {
            var (_, idLote) = CrearMaterialConLote(5m);
            int idModelo = CrearModelo(new LoteAsignado(idLote, 2m));

            modelos.QuitarMaterial(modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, devolverAlStock: false);

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
            cambios.Quitados.Add(new MaterialQuitado { IdMaterialModelo = renglones.Single(r => r.IdLote == loteA).IdMaterialModelo, DevolverAlStock = true });
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
            cambios.Quitados.Add(new MaterialQuitado { IdMaterialModelo = modelos.ListarMateriales(idModelo).Single().IdMaterialModelo, DevolverAlStock = true });
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
                new MaterialADevolver(renglones.Single(r => r.IdLote == motor).IdMaterialModelo, 1m),
                new MaterialADevolver(renglones.Single(r => r.IdLote == servo).IdMaterialModelo, 2.5m)
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
                modelos.Eliminar(idModelo, new[] { new MaterialADevolver(idRenglon, 5m) }));

            Assert.IsNotNull(modelos.Obtener(idModelo));
            Assert.AreEqual(6m, DisponibleDe(lote));
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
