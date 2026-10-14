using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.Forms.Modelos
{
    /// <summary>
    /// Elige materiales con stock para un modelo: material, lote y cantidad.
    /// Queda abierto para agregar varios; cada uno se entrega con <c>agregar</c>.
    /// </summary>
    public partial class frmElegirLote : Form
    {
        /// <summary>Un lote como se muestra: con lo disponible descontando lo que ya se agregó sin guardar.</summary>
        public class LoteVista
        {
            public Lote Lote { get; set; }
            public string Fabricante => Lote.Fabricante;
            public string Ubicacion => Lote.Ubicacion;
            public decimal Disponible { get; set; }
            public decimal ValorUnitario => Lote.ValorUnitario;
            public DateTime FechaIngreso => Lote.FechaIngreso;
        }

        private readonly MaterialRepositorio materiales = new MaterialRepositorio();
        private readonly LoteRepositorio lotes = new LoteRepositorio();
        private readonly Func<int, decimal> usoPendiente;
        private readonly Func<int, decimal> usoPendienteMaterial;
        private readonly Action<FilaMaterial> agregar;
        private List<MaterialStock> conStock = new List<MaterialStock>();
        private bool cargando;

        /// <param name="usoPendiente">Cuánto de un lote ya está tomado en la edición sin guardar.</param>
        /// <param name="usoPendienteMaterial">Lo mismo, sumado por material.</param>
        /// <param name="agregar">Recibe cada material elegido.</param>
        public frmElegirLote(Func<int, decimal> usoPendiente, Func<int, decimal> usoPendienteMaterial, Action<FilaMaterial> agregar)
        {
            InitializeComponent();
            this.usoPendiente = usoPendiente;
            this.usoPendienteMaterial = usoPendienteMaterial;
            this.agregar = agregar;
            Estilo.Grilla(dgvMateriales);
            Estilo.Grilla(dgvLotes);
            dgvMateriales.BackgroundColor = System.Drawing.Color.Linen;
            dgvLotes.BackgroundColor = System.Drawing.Color.Linen;
            Estilo.Botones(botAgregar);
            Estilo.BotonSecundario(botListo);
        }

        private void frmElegirLote_Load(object sender, EventArgs e)
        {
            Mensajes.Intentar(this, () =>
            {
                var familias = new List<Familia> { new Familia { IdFamilia = 0, Nombre = "(Todas)" } };
                familias.AddRange(new FamiliaRepositorio().Listar());
                cargando = true;
                cboFamilia.DisplayMember = nameof(Familia.Nombre);
                cboFamilia.ValueMember = nameof(Familia.IdFamilia);
                cboFamilia.DataSource = familias;
                cargando = false;
                CargarMateriales();
            });
            ActiveControl = txtBuscar;
        }

        private void CargarMateriales()
        {
            var filtro = new FiltroMateriales
            {
                IdFamilia = cboFamilia.SelectedValue is int id && id > 0 ? id : (int?)null,
                Texto = txtBuscar.Text
            };
            conStock = materiales.ListarStock(filtro).ToList();
            foreach (var m in conStock)
            {
                m.Disponible = Math.Max(0, m.Disponible - usoPendienteMaterial(m.IdMaterial));
            }
            conStock = conStock.Where(m => m.Disponible > 0).ToList();
            dgvMateriales.DataSource = conStock;
            CargarLotes();
        }

        private MaterialStock MaterialSeleccionado() =>
            dgvMateriales.SelectedRows.Count > 0 ? dgvMateriales.SelectedRows[0].DataBoundItem as MaterialStock : null;

        private LoteVista LoteSeleccionado() =>
            dgvLotes.SelectedRows.Count > 0 ? dgvLotes.SelectedRows[0].DataBoundItem as LoteVista : null;

        private void CargarLotes()
        {
            var m = MaterialSeleccionado();
            if (m == null)
            {
                dgvLotes.DataSource = null;
                labLotes.Text = "Lotes con stock";
                return;
            }
            var vistas = lotes.ListarPorMaterial(m.IdMaterial, soloConStock: true)
                .Select(l => new LoteVista { Lote = l, Disponible = l.Disponible - usoPendiente(l.IdLote) })
                .Where(v => v.Disponible > 0)
                .ToList();
            dgvLotes.DataSource = vistas;
            labLotes.Text = vistas.Count == 1
                ? $"Lote de {Descripcion(m)}"
                : $"Lotes de {Descripcion(m)} ({vistas.Count}): elija de cuál se usa";
        }

        private static string Descripcion(MaterialStock m) =>
            string.Join(" ", new[] { m.Grupo, m.Caracteristica, m.Color, m.Medidas }.Where(s => !string.IsNullOrWhiteSpace(s)));

        private void dgvMateriales_SelectionChanged(object sender, EventArgs e)
        {
            if (!cargando)
            {
                Mensajes.Intentar(this, CargarLotes);
            }
        }

        private void Filtro_Changed(object sender, EventArgs e)
        {
            if (!cargando)
            {
                Mensajes.Intentar(this, CargarMateriales);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            timBuscar.Stop();
            timBuscar.Start();
        }

        private void timBuscar_Tick(object sender, EventArgs e)
        {
            timBuscar.Stop();
            Filtro_Changed(sender, e);
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                botAgregar_Click(sender, e);
            }
        }

        private void botAgregar_Click(object sender, EventArgs e)
        {
            var m = MaterialSeleccionado();
            var vista = LoteSeleccionado();
            if (m == null || vista == null)
            {
                Mensajes.Aviso(this, "Elija un material y el lote del que se usa.");
                return;
            }
            if (!Numeros.TryLeer(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                Mensajes.Aviso(this, "Escriba la cantidad a usar (mayor que cero).");
                txtCantidad.Focus();
                return;
            }
            if (cantidad > vista.Disponible)
            {
                Mensajes.Aviso(this, $"La cantidad supera lo disponible en ese lote ({Numeros.Cantidad(vista.Disponible)}).");
                txtCantidad.Focus();
                return;
            }

            agregar(new FilaMaterial
            {
                IdLote = vista.Lote.IdLote,
                IdMaterial = m.IdMaterial,
                Familia = m.Familia,
                Material = Descripcion(m),
                Fabricante = vista.Fabricante,
                Unidad = m.TipoUnidad,
                Cantidad = cantidad,
                ValorUnitario = vista.ValorUnitario
            });

            labEstado.Text = $"Agregado: {Numeros.Cantidad(cantidad)} {m.TipoUnidad} de {Descripcion(m)}";
            txtCantidad.Clear();

            int idMaterial = m.IdMaterial;
            Mensajes.Intentar(this, () =>
            {
                cargando = true;
                CargarMateriales();
                cargando = false;
                var fila = dgvMateriales.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => ((MaterialStock)r.DataBoundItem).IdMaterial == idMaterial);
                if (fila != null)
                {
                    dgvMateriales.CurrentCell = fila.Cells[colGrupo.Index];
                }
                CargarLotes();
            });
            txtBuscar.Focus();
            txtBuscar.SelectAll();
        }
    }
}
