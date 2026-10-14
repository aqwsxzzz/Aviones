using System.Linq;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.UnitTests.Data
{
    [TestClass]
    public class MaterialYLoteTests : BaseDePrueba
    {
        private readonly MaterialRepositorio materiales = new MaterialRepositorio();
        private readonly LoteRepositorio lotes = new LoteRepositorio();

        [TestMethod]
        public void ListarStock_SumaTodosLosLotesConDecimales()
        {
            var (idMaterial, _) = CrearMaterialConLote(3.6m, valor: 12m);
            lotes.Crear(new Lote { IdMaterial = idMaterial, Cantidad = 0.66m, ValorUnitario = 13.5m });

            var fila = materiales.ListarStock().Single();

            Assert.AreEqual(2, fila.CantidadLotes);
            Assert.AreEqual(4.26m, fila.Cantidad);
            Assert.AreEqual(4.26m, fila.Disponible);
            Assert.AreEqual(3.6m * 12m + 0.66m * 13.5m, fila.ValorDisponible);
        }

        [TestMethod]
        public void ListarStock_MaterialSinLotesApareceConCero()
        {
            materiales.Crear(new Material { IdFamilia = IdFamilia, Grupo = "Cinta", IdTipoUnidad = IdMetros });

            var fila = materiales.ListarStock(new FiltroMateriales { SoloSinStock = true }).Single();

            Assert.AreEqual("Cinta", fila.Grupo);
            Assert.AreEqual(0m, fila.Disponible);
        }

        [TestMethod]
        public void ListarStock_FiltraPorFabricanteDelLoteYAceptaApostrofes()
        {
            CrearMaterialConLote(1m, fabricante: "D'Agostino");
            CrearMaterialConLote(1m, grupo: "Tela", fabricante: "Solarfilm");

            var encontrados = materiales.ListarStock(new FiltroMateriales { Texto = "d'agost" });

            Assert.AreEqual(1, encontrados.Count);
            Assert.AreEqual("Plástico", encontrados[0].Grupo);
        }

        [TestMethod]
        public void Eliminar_BorraLotesYUsosEnModelos()
        {
            var (idMaterial, idLote) = CrearMaterialConLote(5m);
            int idModelo = new ModeloRepositorio().Crear(
                new Modelo { Nombre = "Cub", IdEstadoModelo = EstadoActivo }, new[] { new LoteAsignado(idLote, 2m) });

            materiales.Eliminar(idMaterial);

            Assert.IsNull(materiales.Obtener(idMaterial));
            Assert.IsNull(lotes.Obtener(idLote));
            Assert.AreEqual(0, new ModeloRepositorio().ListarMateriales(idModelo).Count);
        }

        [TestMethod]
        public void ListarUsos_DiceEnQueModelosSeUsa()
        {
            var (idMaterial, idLote) = CrearMaterialConLote(5m);
            new ModeloRepositorio().Crear(new Modelo { Nombre = "Cub", Alias = "Cubby", IdEstadoModelo = EstadoActivo },
                new[] { new LoteAsignado(idLote, 1.5m) });

            var uso = materiales.ListarUsos(idMaterial).Single();

            Assert.AreEqual("Cub", uso.Modelo);
            Assert.AreEqual(1.5m, uso.Cantidad);
        }

        [TestMethod]
        public void BuscarIguales_IgnoraMayusculasYEspaciosPeroNoSeEncuentraASiMismo()
        {
            var (idMaterial, _) = CrearMaterialConLote(1m);   // Plástico / Monokote
            var parecido = new Material
            {
                IdFamilia = IdFamilia, Grupo = "  plástico ", Caracteristica = "MONOKOTE", IdTipoUnidad = IdMetros
            };

            Assert.AreEqual(idMaterial, materiales.BuscarIguales(parecido).Single().IdMaterial);

            parecido.IdMaterial = idMaterial;   // al modificarse no es repetido de sí mismo
            Assert.AreEqual(0, materiales.BuscarIguales(parecido).Count);
        }

        [TestMethod]
        public void BuscarIguales_DistintoColorOFamiliaNoEsRepetido()
        {
            CrearMaterialConLote(1m);
            int otraFamilia = new FamiliaRepositorio().Crear("Adhesivos");

            Assert.AreEqual(0, materiales.BuscarIguales(new Material
                { IdFamilia = IdFamilia, Grupo = "Plástico", Caracteristica = "Monokote", Color = "Rojo", IdTipoUnidad = IdMetros }).Count);
            Assert.AreEqual(0, materiales.BuscarIguales(new Material
                { IdFamilia = otraFamilia, Grupo = "Plástico", Caracteristica = "Monokote", IdTipoUnidad = IdMetros }).Count);
        }

        [TestMethod]
        public void Crear_GuardaTextoSinEspaciosDeMas()
        {
            int id = materiales.Crear(new Material
                { IdFamilia = IdFamilia, Grupo = "  Balsa ", Caracteristica = "Tabla   1/16", Color = "  ", IdTipoUnidad = IdMetros });

            var guardado = materiales.Obtener(id);
            Assert.AreEqual("Balsa", guardado.Grupo);
            Assert.AreEqual("Tabla 1/16", guardado.Caracteristica);
            Assert.IsNull(guardado.Color);
        }

        [TestMethod]
        public void Imagen_SeGuardaYSeBorra()
        {
            var (idMaterial, _) = CrearMaterialConLote(1m);
            var bytes = new byte[] { 1, 2, 3 };

            materiales.GuardarImagen(idMaterial, bytes);
            CollectionAssert.AreEqual(bytes, materiales.ObtenerImagen(idMaterial));

            materiales.GuardarImagen(idMaterial, null);
            Assert.IsNull(materiales.ObtenerImagen(idMaterial));
        }

        [TestMethod]
        public void LoteActualizar_SubirCantidadSubeDisponible()
        {
            var (_, idLote) = CrearMaterialConLote(5m);
            new ModeloRepositorio().Crear(new Modelo { Nombre = "Cub", IdEstadoModelo = EstadoActivo }, new[] { new LoteAsignado(idLote, 2m) });

            var lote = lotes.Obtener(idLote);
            lote.Cantidad = 7m;
            lotes.Actualizar(lote);

            Assert.AreEqual(5m, DisponibleDe(idLote));
        }

        [TestMethod]
        public void LoteActualizar_NoPuedeBajarDeLoUsado()
        {
            var (_, idLote) = CrearMaterialConLote(5m);
            new ModeloRepositorio().Crear(new Modelo { Nombre = "Cub", IdEstadoModelo = EstadoActivo }, new[] { new LoteAsignado(idLote, 4m) });

            var lote = lotes.Obtener(idLote);
            lote.Cantidad = 3m;

            Assert.ThrowsExactly<ReglaNegocioException>(() => lotes.Actualizar(lote));
            Assert.AreEqual(1m, DisponibleDe(idLote));
        }

        [TestMethod]
        public void LoteEliminar_UsadoEnModeloNoSePuede()
        {
            var (_, idLote) = CrearMaterialConLote(5m);
            new ModeloRepositorio().Crear(new Modelo { Nombre = "Cub", IdEstadoModelo = EstadoActivo }, new[] { new LoteAsignado(idLote, 1m) });

            Assert.ThrowsExactly<ReglaNegocioException>(() => lotes.Eliminar(idLote));
        }

        [TestMethod]
        public void Familia_ConMaterialesNoSePuedeEliminarYNoSeRepite()
        {
            CrearMaterialConLote(1m);
            var familias = new FamiliaRepositorio();

            Assert.ThrowsExactly<ReglaNegocioException>(() => familias.Eliminar(IdFamilia));
            Assert.ThrowsExactly<ReglaNegocioException>(() => familias.Crear(" Revestimientos "));
        }
    }
}
