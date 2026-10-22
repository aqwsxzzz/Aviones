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
        public void Pedidos_PrecioEstimadoReemplazaAlUltimo()
        {
            int servo = CrearMaterial("Servo", 2m, precio: 3.5m);
            int conPrecio = compras.CrearPedido(servo, 10m, null, precioEstimado: 4m);
            int sinPrecio = compras.CrearPedido(servo, 2m, null);

            Assert.AreEqual(40m, compras.Obtener(conPrecio).CostoEstimado);
            Assert.AreEqual(7m, compras.Obtener(sinPrecio).CostoEstimado);
            Assert.AreEqual(3.5m, compras.UltimoPrecio(servo));

            compras.ModificarPedido(conPrecio, 10m, null, precioEstimado: null);
            Assert.AreEqual(35m, compras.Obtener(conPrecio).CostoEstimado);
            Assert.ThrowsExactly<ReglaNegocioException>(() => compras.ModificarPedido(conPrecio, 10m, null, -1m));
        }

        [TestMethod]
        public void Pedidos_CantidadTieneQueSerPositiva()
        {
            int servo = CrearMaterial("Servo", 2m);
            Assert.ThrowsExactly<ReglaNegocioException>(() => compras.CrearPedido(servo, 0m, null));
        }

        private static Lote Recibido(decimal cantidad, decimal valor = 12m) =>
            new Lote { Cantidad = cantidad, ValorUnitario = valor, IdFabricante = IdDe(ListaLoteRepositorio.Fabricantes(), "Hitec") };

        private static decimal? Valor(string sql, object datos)
        {
            using (var cn = Db.Abrir())
            {
                return Dapper.SqlMapper.ExecuteScalar<decimal?>(cn, sql, datos);
            }
        }

        [TestMethod]
        public void Recibir_TodoCierraElPedidoEIngresaUnLote()
        {
            int servo = CrearMaterial("Servo", 2m);
            int id = compras.CrearPedido(servo, 10m, null);

            int idLote = compras.Recibir(id, Recibido(10m), esperarResto: false);

            var pedido = compras.Obtener(id);
            Assert.AreEqual(EstadosPedido.Comprado, pedido.Estado);
            Assert.AreEqual(10m, pedido.CantidadRecibida);
            Assert.AreEqual(idLote, pedido.IdLote);
            Assert.IsNotNull(pedido.FechaCierre);
            Assert.AreEqual(0, compras.ListarPendientes().Count);

            var lote = new LoteRepositorio().Obtener(idLote);
            Assert.AreEqual(servo, lote.IdMaterial);
            Assert.AreEqual(10m, lote.DisponibleNuevo);
            Assert.AreEqual(12m, lote.ValorUnitario);
            Assert.AreEqual(12m, materiales.ObtenerStock(servo).Disponible);
        }

        [TestMethod]
        public void Recibir_MenosYSeEsperaElResto()
        {
            int servo = CrearMaterial("Servo", 2m);
            int id = compras.CrearPedido(servo, 10m, "para el Cub");

            int idLote = compras.Recibir(id, Recibido(4m), esperarResto: true);

            var pendiente = compras.ListarPendientes().Single();
            Assert.AreEqual(id, pendiente.IdPedido);
            Assert.AreEqual(6m, pendiente.Cantidad);
            Assert.AreEqual(6m, materiales.ObtenerStock(servo).PedidoPendiente);
            Assert.AreEqual(6m, materiales.ObtenerStock(servo).Disponible);
            // Lo recibido queda como pedido cerrado aparte, con la misma nota
            Assert.AreEqual(4m, Valor("SELECT CantidadRecibida FROM dbo.PedidosCompra WHERE Estado = 'C' AND IdLote = @idLote AND Nota = 'para el Cub'", new { idLote }));

            compras.Recibir(id, Recibido(6m), esperarResto: true);
            Assert.AreEqual(0, compras.ListarPendientes().Count);
            Assert.AreEqual(12m, materiales.ObtenerStock(servo).Disponible);
        }

        [TestMethod]
        public void Recibir_MenosYSeCierraElPedido()
        {
            int servo = CrearMaterial("Servo", 2m);
            int id = compras.CrearPedido(servo, 10m, null);

            compras.Recibir(id, Recibido(5m), esperarResto: false);

            var pedido = compras.Obtener(id);
            Assert.AreEqual(EstadosPedido.Comprado, pedido.Estado);
            Assert.AreEqual(10m, pedido.Cantidad);
            Assert.AreEqual(5m, pedido.CantidadRecibida);
            Assert.AreEqual(0m, materiales.ObtenerStock(servo).PedidoPendiente);
            Assert.AreEqual(7m, materiales.ObtenerStock(servo).Disponible);
        }

        [TestMethod]
        public void Recibir_MasDeLoPedidoCierra()
        {
            int servo = CrearMaterial("Servo", 2m);
            int id = compras.CrearPedido(servo, 10m, null);

            compras.Recibir(id, Recibido(12m), esperarResto: true);

            Assert.AreEqual(12m, compras.Obtener(id).CantidadRecibida);
            Assert.AreEqual(0, compras.ListarPendientes().Count);
        }

        [TestMethod]
        public void Recibir_UsadoEntraComoUsado()
        {
            int servo = CrearMaterial("Servo", 2m);
            int id = compras.CrearPedido(servo, 3m, null);
            var recibido = Recibido(3m);
            recibido.CompradoUsado = true;

            int idLote = compras.Recibir(id, recibido, esperarResto: false);

            Assert.AreEqual(3m, new LoteRepositorio().Obtener(idLote).DisponibleUsado);
        }

        [TestMethod]
        public void Recibir_SoloPedidosPendientesYCantidadPositiva()
        {
            int servo = CrearMaterial("Servo", 2m);
            int id = compras.CrearPedido(servo, 10m, null);

            Assert.ThrowsExactly<ReglaNegocioException>(() => compras.Recibir(id, Recibido(0m), false));
            compras.Recibir(id, Recibido(10m), false);
            Assert.ThrowsExactly<ReglaNegocioException>(() => compras.Recibir(id, Recibido(10m), false));
            Assert.ThrowsExactly<ReglaNegocioException>(() => compras.ModificarPedido(id, 3m, null));
            // Nada se ingresó de más
            Assert.AreEqual(12m, materiales.ObtenerStock(servo).Disponible);
        }

        [TestMethod]
        public void Recibido_SeBorraElLoteOElMaterial()
        {
            int servo = CrearMaterial("Servo", 2m);
            int id = compras.CrearPedido(servo, 10m, null);
            int idLote = compras.Recibir(id, Recibido(10m), false);

            new LoteRepositorio().Eliminar(idLote);
            Assert.IsNull(compras.Obtener(id).IdLote);
            Assert.AreEqual(10m, compras.Obtener(id).CantidadRecibida);

            materiales.Eliminar(servo);
            Assert.IsNull(compras.Obtener(id));
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
