using System.Linq;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.UnitTests.Data
{
    [TestClass]
    public class CompraTests : BaseDePrueba
    {
        private readonly CompraRepositorio compras = new CompraRepositorio();
        private readonly MaterialRepositorio materiales = new MaterialRepositorio();

        private int CrearMaterial(string grupo, decimal stock, decimal? minimo = null, decimal precio = 10m)
        {
            var (id, _) = CrearMaterialConLote(stock, precio, grupo: grupo);
            if (minimo.HasValue)
            {
                var m = materiales.Obtener(id);
                m.StockMinimo = minimo;
                materiales.Actualizar(m);
            }
            return id;
        }

        [TestMethod]
        public void StockCritico_UsaElMinimoPropioOElGeneral()
        {
            int servo = CrearMaterial("Servo", 3m, minimo: 4m);     // 3 <= 4: crítico
            CrearMaterial("Motor", 3m, minimo: 2m);                   // 3 > 2: no
            int cola = CrearMaterial("Cola", 1m);                     // sin mínimo: 1 <= general 1
            CrearMaterial("Lija", 5m);                                // 5 > 1: no

            var criticos = compras.ListarStockCritico(minimoGeneral: 1m);

            CollectionAssert.AreEquivalent(new[] { servo, cola }, criticos.Select(c => c.IdMaterial).ToList());
            Assert.AreEqual(4m, criticos.Single(c => c.IdMaterial == servo).MinimoAplicado);
            Assert.AreEqual(1m, criticos.Single(c => c.IdMaterial == cola).MinimoAplicado);
        }

        [TestMethod]
        public void StockCritico_ConMinimoGeneralMasAlto()
        {
            CrearMaterial("Lija", 5m);

            Assert.AreEqual(0, compras.ListarStockCritico(1m).Count);
            Assert.AreEqual(1, compras.ListarStockCritico(5m).Count);
        }

        [TestMethod]
        public void StockCritico_MinimoCeroSoloSiNoQuedaNada()
        {
            int herramienta = CrearMaterial("Pistola de calor", 1m, minimo: 0m);
            Assert.AreEqual(0, compras.ListarStockCritico(3m).Count);

            var lote = new LoteRepositorio().ListarPorMaterial(herramienta).Single();
            new ModeloRepositorio().Crear(new Modelo { Nombre = "Cub", IdEstadoModelo = EstadoActivo }, new[] { new LoteAsignado(lote.IdLote, 1m) });
            Assert.AreEqual(1, compras.ListarStockCritico(3m).Count);
        }

        [TestMethod]
        public void Pedidos_SeCreanListanYSumanAlMaterial()
        {
            int servo = CrearMaterial("Servo", 2m, precio: 3.5m);
            compras.CrearPedido(servo, 10m, "  para el Mustang ");
            compras.CrearPedido(servo, 2m, null);

            var pendientes = compras.ListarPendientes();
            Assert.AreEqual(2, pendientes.Count);
            var primero = pendientes.First();
            Assert.AreEqual("para el Mustang", primero.Nota);
            Assert.AreEqual(2m, primero.Disponible);
            Assert.AreEqual(3.5m, primero.UltimoPrecio);
            Assert.AreEqual(35m, primero.CostoEstimado);

            var fila = materiales.ListarStock(new FiltroMateriales { SoloAComprar = true }).Single();
            Assert.AreEqual(12m, fila.PedidoPendiente);
        }

        [TestMethod]
        public void Pedidos_SeModificanYSeEliminan()
        {
            int servo = CrearMaterial("Servo", 2m);
            int id = compras.CrearPedido(servo, 10m, null);

            compras.ModificarPedido(id, 6m, "mejor 6");
            Assert.AreEqual(6m, compras.Obtener(id).Cantidad);
            Assert.AreEqual("mejor 6", compras.Obtener(id).Nota);

            compras.EliminarPedido(id);
            Assert.AreEqual(0, compras.ListarPendientes().Count);
            Assert.AreEqual(0m, materiales.ListarStock().Single().PedidoPendiente);
        }

        [TestMethod]
        public void Pedidos_CantidadTieneQueSerPositiva()
        {
            int servo = CrearMaterial("Servo", 2m);
            Assert.ThrowsExactly<ReglaNegocioException>(() => compras.CrearPedido(servo, 0m, null));
        }

        [TestMethod]
        public void MinimoGeneral_SeGuardaYSeLee()
        {
            var config = new ConfiguracionRepositorio();
            Assert.AreEqual(1m, config.ObtenerStockMinimoGeneral());

            config.GuardarStockMinimoGeneral(2.5m);

            Assert.AreEqual(2.5m, config.ObtenerStockMinimoGeneral());
        }
    }
}
