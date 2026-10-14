using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Aviones_2._0.Common;

namespace Aviones_2._0.UnitTests.Common
{
    [TestClass]
    public class OrdenGrillaTests
    {
        public class Fila
        {
            public string Nombre { get; set; }
            public decimal Cantidad { get; set; }
        }

        private Form form = null!;
        private DataGridView dgv = null!;

        [TestInitialize]
        public void Crear()
        {
            dgv = new DataGridView { AutoGenerateColumns = false };
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", DataPropertyName = "Nombre" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCantidad", DataPropertyName = "Cantidad" });
            form = new Form();
            form.Controls.Add(dgv);
            form.CreateControl();
            OrdenGrilla.Habilitar(dgv);
            dgv.DataSource = new List<Fila>
            {
                new Fila { Nombre = "balsa", Cantidad = 3m },
                new Fila { Nombre = null, Cantidad = 1.5m },
                new Fila { Nombre = "Abedul", Cantidad = 10m }
            };
        }

        [TestCleanup]
        public void Cerrar() => form.Dispose();

        private void ClicEnTitulo(int columna)
        {
            var metodo = typeof(DataGridView).GetMethod("OnColumnHeaderMouseClick", BindingFlags.Instance | BindingFlags.NonPublic)!;
            metodo.Invoke(dgv, new object[] { new DataGridViewCellMouseEventArgs(columna, -1, 0, 0, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)) });
        }

        private List<Fila> Filas() => ((IEnumerable<Fila>)dgv.DataSource).ToList();

        [TestMethod]
        public void ClicOrdenaYOtroClicInvierte()
        {
            ClicEnTitulo(1);
            CollectionAssert.AreEqual(new[] { 1.5m, 3m, 10m }, Filas().Select(f => f.Cantidad).ToList());

            ClicEnTitulo(1);
            CollectionAssert.AreEqual(new[] { 10m, 3m, 1.5m }, Filas().Select(f => f.Cantidad).ToList());
            Assert.AreEqual(SortOrder.Descending, dgv.Columns[1].HeaderCell.SortGlyphDirection);
        }

        [TestMethod]
        public void TextoSinMayusculasYVaciosAlFinal()
        {
            ClicEnTitulo(0);
            CollectionAssert.AreEqual(new[] { "Abedul", "balsa", null }, Filas().Select(f => f.Nombre).ToList());
        }

        [TestMethod]
        public void AplicarMantieneElOrdenAlRecargar()
        {
            ClicEnTitulo(1);
            var recargados = new List<Fila> { new Fila { Cantidad = 7m }, new Fila { Cantidad = 2m } };

            var ordenados = OrdenGrilla.Aplicar(dgv, recargados);

            CollectionAssert.AreEqual(new[] { 2m, 7m }, ordenados.Select(f => f.Cantidad).ToList());
        }

        [TestMethod]
        public void SinClicNoCambiaElOrden()
        {
            var lista = new List<Fila> { new Fila { Cantidad = 7m }, new Fila { Cantidad = 2m } };
            Assert.AreSame(lista, OrdenGrilla.Aplicar(dgv, lista));
        }
    }
}
