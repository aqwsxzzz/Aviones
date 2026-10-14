using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.Forms.Modelos
{
    /// <summary>Lista de modelos (aviones) con la foto y los materiales del seleccionado.</summary>
    public partial class frmModelos : Form
    {
        private readonly ModeloRepositorio modelos = new ModeloRepositorio();
        private List<Modelo> todos = new List<Modelo>();
        private bool cargando;

        public frmModelos()
        {
            InitializeComponent();
            Estilo.Grilla(dgvModelos);
            Estilo.Grilla(dgvMateriales);
            dgvModelos.BackgroundColor = System.Drawing.Color.Linen;
            dgvMateriales.BackgroundColor = System.Drawing.Color.Linen;
            Estilo.Botones(botNuevo, botModificar, botEliminar, botImprimir, botImprimirMateriales);
            Estilo.BotonSecundario(botSalir);
        }

        private void frmModelos_Load(object sender, EventArgs e)
        {
            splPrincipal.SplitterDistance = Math.Max(400, (int)(splPrincipal.Width * 0.45));
            Mensajes.Intentar(this, () =>
            {
                var estados = new List<EstadoModelo> { new EstadoModelo { IdEstadoModelo = 0, Nombre = "(Todas)" } };
                estados.AddRange(new EstadoModeloRepositorio().Listar());
                cargando = true;
                cboEstado.DisplayMember = nameof(EstadoModelo.Nombre);
                cboEstado.ValueMember = nameof(EstadoModelo.IdEstadoModelo);
                cboEstado.DataSource = estados;
                cargando = false;
                CargarModelos();
            });
        }

        /// <summary>Lee los modelos de la base y aplica los filtros.</summary>
        private void CargarModelos(int? seleccionar = null)
        {
            int? idSeleccionado = seleccionar ?? ModeloSeleccionado()?.IdModelo;
            todos = modelos.Listar();
            MostrarFiltrados(idSeleccionado);
        }

        private void MostrarFiltrados(int? idSeleccionado)
        {
            int idEstado = cboEstado.SelectedValue is int id ? id : 0;
            string texto = txtBuscar.Text.Trim();
            var lista = todos
                .Where(m => idEstado == 0 || m.IdEstadoModelo == idEstado)
                .Where(m => texto.Length == 0
                            || (m.Nombre ?? "").IndexOf(texto, StringComparison.CurrentCultureIgnoreCase) >= 0
                            || (m.Alias ?? "").IndexOf(texto, StringComparison.CurrentCultureIgnoreCase) >= 0)
                .ToList();

            cargando = true;
            try
            {
                dgvModelos.DataSource = lista;
                dgvModelos.ClearSelection();
                var fila = dgvModelos.Rows.Cast<DataGridViewRow>()
                               .FirstOrDefault(r => ((Modelo)r.DataBoundItem).IdModelo == idSeleccionado)
                           ?? dgvModelos.Rows.Cast<DataGridViewRow>().FirstOrDefault();
                if (fila != null)
                {
                    dgvModelos.CurrentCell = fila.Cells[colNombre.Index];
                    fila.Selected = true;
                }
            }
            finally
            {
                cargando = false;
            }

            labResumen.Text = $"{lista.Count} modelos   ·   Valor total de materiales: $ {Numeros.Dinero(lista.Sum(m => m.ValorMateriales))}";
            MostrarDetalle();
        }

        private Modelo ModeloSeleccionado() =>
            dgvModelos.SelectedRows.Count > 0 ? dgvModelos.SelectedRows[0].DataBoundItem as Modelo : null;

        private void MostrarDetalle()
        {
            var m = ModeloSeleccionado();
            botModificar.Enabled = m != null;
            botEliminar.Enabled = m != null;
            botImprimirMateriales.Enabled = m != null;

            if (m == null)
            {
                dgvMateriales.DataSource = null;
                Imagenes.Mostrar(picFoto, null);
                labMateriales.Text = "Materiales";
                labTotal.Text = "";
                return;
            }

            Mensajes.Intentar(this, () =>
            {
                var materiales = modelos.ListarMateriales(m.IdModelo);
                dgvMateriales.DataSource = materiales;
                labMateriales.Text = $"Materiales de {m.Nombre}" + (string.IsNullOrEmpty(m.Alias) ? "" : $" ({m.Alias})");
                labTotal.Text = $"Total: $ {Numeros.Dinero(materiales.Sum(x => x.Subtotal))}";
                Imagenes.Mostrar(picFoto, Imagenes.DesdeBytes(modelos.ObtenerFoto(m.IdModelo)));
            });
        }

        private void Filtro_Changed(object sender, EventArgs e)
        {
            if (!cargando)
            {
                MostrarFiltrados(ModeloSeleccionado()?.IdModelo);
            }
        }

        private void dgvModelos_SelectionChanged(object sender, EventArgs e)
        {
            if (!cargando)
            {
                MostrarDetalle();
            }
        }

        private void dgvModelos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                botModificar_Click(sender, e);
            }
        }

        private void dgvModelos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                botModificar_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                botEliminar_Click(sender, e);
            }
        }

        private void picFoto_DoubleClick(object sender, EventArgs e)
        {
            var m = ModeloSeleccionado();
            if (m != null)
            {
                Imagenes.Ampliar(this, picFoto.Image, m.Nombre);
            }
        }

        private void botNuevo_Click(object sender, EventArgs e)
        {
            using (var editor = new frmModeloEditor(null))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    Mensajes.Intentar(this, () => CargarModelos(editor.IdModelo));
                }
            }
        }

        private void botModificar_Click(object sender, EventArgs e)
        {
            var m = ModeloSeleccionado();
            if (m == null)
            {
                return;
            }
            using (var editor = new frmModeloEditor(m.IdModelo))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    Mensajes.Intentar(this, () => CargarModelos(m.IdModelo));
                }
            }
        }

        private void botEliminar_Click(object sender, EventArgs e)
        {
            var m = ModeloSeleccionado();
            if (m == null)
            {
                return;
            }

            if (m.CantidadMateriales == 0)
            {
                if (Mensajes.Confirmar(this, $"¿Eliminar el modelo \"{m.Nombre}\"?", "Eliminar modelo"))
                {
                    Mensajes.Intentar(this, () =>
                    {
                        modelos.Eliminar(m.IdModelo, devolverAlStock: false);
                        CargarModelos();
                    });
                }
                return;
            }

            // Con materiales: se elige cuáles se reciclan y vuelven al stock
            using (var dialogo = new frmEliminarModelo(m))
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    Mensajes.Intentar(this, () => CargarModelos());
                }
            }
        }

        private void botImprimir_Click(object sender, EventArgs e)
        {
            ImpresionGrilla.VistaPrevia(this, dgvModelos, "Modelos", labResumen.Text);
        }

        private void botImprimirMateriales_Click(object sender, EventArgs e)
        {
            var m = ModeloSeleccionado();
            if (m != null)
            {
                ImpresionGrilla.VistaPrevia(this, dgvMateriales, labMateriales.Text, labTotal.Text);
            }
        }

        private void botSalir_Click(object sender, EventArgs e) => Close();

        private void frmModelos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
