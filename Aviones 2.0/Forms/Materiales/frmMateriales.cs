using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;
using Aviones_2._0.Forms.Comun;
using Aviones_2._0.Forms.Compras;

namespace Aviones_2._0.Forms.Materiales
{
    /// <summary>
    /// Lista de todos los materiales con su stock, filtros, y el detalle del seleccionado:
    /// foto, lotes y en qué modelos se usa.
    /// </summary>
    public partial class frmMateriales : Form
    {
        private readonly MaterialRepositorio materiales = new MaterialRepositorio();
        private readonly LoteRepositorio lotes = new LoteRepositorio();
        private readonly FamiliaRepositorio familias = new FamiliaRepositorio();
        private readonly TipoUnidadRepositorio tipos = new TipoUnidadRepositorio();

        /// <summary>Evita recargar mientras se llenan los controles por código.</summary>
        private bool cargando;

        public frmMateriales()
        {
            InitializeComponent();
            Estilo.Grilla(dgvMateriales);
            Estilo.Grilla(dgvLotes);
            Estilo.Grilla(dgvUsos);
            Estilo.Botones(botNuevo, botModificar, botEliminar, botComprar, botFamilias, botTipos, botInforme, botImprimir,
                botAgregarLote, botModificarLote, botEliminarLote);
            Estilo.BotonSecundario(botSalir);
            dgvLotes.SelectionChanged += (s, e) => ActualizarBotonesLote();
            OrdenGrilla.Habilitar(dgvMateriales, dgvLotes, dgvUsos);
        }

        private void frmMateriales_Load(object sender, EventArgs e)
        {
            splPrincipal.SplitterDistance = Math.Max(400, (int)(splPrincipal.Width * 0.62));
            Mensajes.Intentar(this, () =>
            {
                CargarFamilias();
                CargarMateriales();
            });
        }

        #region Carga

        private void CargarFamilias()
        {
            int actual = cboFamilia.SelectedValue is int id ? id : 0;
            var lista = new List<Familia> { new Familia { IdFamilia = 0, Nombre = "(Todas)" } };
            lista.AddRange(familias.Listar());

            cargando = true;
            try
            {
                cboFamilia.DisplayMember = nameof(Familia.Nombre);
                cboFamilia.ValueMember = nameof(Familia.IdFamilia);
                cboFamilia.DataSource = lista;
                cboFamilia.SelectedValue = lista.Any(f => f.IdFamilia == actual) ? actual : 0;
            }
            finally
            {
                cargando = false;
            }
        }

        private FiltroMateriales FiltroActual() => new FiltroMateriales
        {
            IdFamilia = cboFamilia.SelectedValue is int id && id > 0 ? id : (int?)null,
            Texto = txtBuscar.Text,
            SoloSinStock = chkSinStock.Checked,
            SoloAComprar = chkAComprar.Checked
        };

        /// <summary>Recarga la lista manteniendo (o eligiendo) el material seleccionado.</summary>
        private void CargarMateriales(int? seleccionar = null)
        {
            int? idSeleccionado = seleccionar ?? MaterialSeleccionado()?.IdMaterial;
            var lista = materiales.ListarStock(FiltroActual());

            cargando = true;
            try
            {
                dgvMateriales.DataSource = OrdenGrilla.Aplicar(dgvMateriales, lista);
                dgvMateriales.ClearSelection();
                var fila = dgvMateriales.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => ((MaterialStock)r.DataBoundItem).IdMaterial == idSeleccionado);
                if (fila == null && dgvMateriales.Rows.Count > 0 && seleccionar == null)
                {
                    fila = dgvMateriales.Rows[0];
                }
                if (fila != null)
                {
                    dgvMateriales.CurrentCell = fila.Cells[colGrupo.Index];
                    fila.Selected = true;
                }
            }
            finally
            {
                cargando = false;
            }

            int sinStock = lista.Count(m => m.Disponible == 0);
            decimal valor = lista.Sum(m => m.ValorDisponible);
            decimal usado = lista.Sum(m => m.DisponibleUsado);
            labResumen.Text = $"{lista.Count} materiales   ·   {sinStock} sin stock   ·   "
                + (usado > 0 ? $"{lista.Count(m => m.DisponibleUsado > 0)} con stock usado   ·   " : "")
                + $"Valor disponible: $ {Numeros.Dinero(valor)}";

            MostrarDetalle();
        }

        private MaterialStock MaterialSeleccionado() =>
            dgvMateriales.SelectedRows.Count > 0 ? dgvMateriales.SelectedRows[0].DataBoundItem as MaterialStock : null;

        private Lote LoteSeleccionado() =>
            dgvLotes.SelectedRows.Count > 0 ? dgvLotes.SelectedRows[0].DataBoundItem as Lote : null;

        private void MostrarDetalle()
        {
            var m = MaterialSeleccionado();
            bool hay = m != null;
            botModificar.Enabled = hay;
            botEliminar.Enabled = hay;
            botComprar.Enabled = hay;
            botAgregarLote.Enabled = hay;

            if (!hay)
            {
                dgvLotes.DataSource = null;
                dgvUsos.DataSource = null;
                Imagenes.Mostrar(picFoto, null);
                labLotes.Text = "Lotes (ingresos)";
                labUsos.Text = "Usado en modelos";
            }
            else
            {
                Mensajes.Intentar(this, () =>
                {
                    var listaLotes = lotes.ListarPorMaterial(m.IdMaterial);
                    dgvLotes.DataSource = OrdenGrilla.Aplicar(dgvLotes, listaLotes);
                    labLotes.Text = $"Lotes de {Descripcion(m)} ({listaLotes.Count})";

                    var usos = materiales.ListarUsos(m.IdMaterial);
                    dgvUsos.DataSource = OrdenGrilla.Aplicar(dgvUsos, usos);
                    labUsos.Text = usos.Count == 0 ? "No se usa en ningún modelo" : $"Usado en {usos.Select(u => u.IdModelo).Distinct().Count()} modelos";

                    Imagenes.Mostrar(picFoto, Imagenes.DesdeBytes(materiales.ObtenerImagen(m.IdMaterial)));
                });
            }

            ActualizarBotonesLote();
        }

        private void ActualizarBotonesLote()
        {
            bool hayLote = LoteSeleccionado() != null;
            botModificarLote.Enabled = hayLote;
            botEliminarLote.Enabled = hayLote;
        }

        private static string Descripcion(MaterialStock m) =>
            string.Join(" ", new[] { m.Grupo, m.Caracteristica, m.Medidas }.Where(s => !string.IsNullOrWhiteSpace(s)));

        #endregion

        #region Filtros

        private void Filtro_Changed(object sender, EventArgs e)
        {
            if (!cargando)
            {
                Mensajes.Intentar(this, () => CargarMateriales());
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Espera a que se deje de escribir para no consultar en cada tecla
            timBuscar.Stop();
            timBuscar.Start();
        }

        private void timBuscar_Tick(object sender, EventArgs e)
        {
            timBuscar.Stop();
            Filtro_Changed(sender, e);
        }

        private void botLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
            Mensajes.Intentar(this, () => CargarMateriales());
        }

        private void LimpiarFiltros()
        {
            cargando = true;
            try
            {
                cboFamilia.SelectedValue = 0;
                txtBuscar.Clear();
                chkSinStock.Checked = false;
                chkAComprar.Checked = false;
                timBuscar.Stop();
            }
            finally
            {
                cargando = false;
            }
        }

        #endregion

        #region Grilla

        private void dgvMateriales_SelectionChanged(object sender, EventArgs e)
        {
            if (!cargando)
            {
                MostrarDetalle();
            }
        }

        private void dgvMateriales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !(dgvMateriales.Rows[e.RowIndex].DataBoundItem is MaterialStock m))
            {
                return;
            }
            if (m.Disponible == 0)
            {
                e.CellStyle.ForeColor = Estilo.SinStock;
            }
        }

        private void dgvMateriales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                botModificar_Click(sender, e);
            }
        }

        private void dgvMateriales_KeyDown(object sender, KeyEventArgs e)
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
            var m = MaterialSeleccionado();
            if (m != null)
            {
                Imagenes.Ampliar(this, picFoto.Image, Descripcion(m));
            }
        }

        #endregion

        #region Materiales

        private void botNuevo_Click(object sender, EventArgs e)
        {
            int? familia = cboFamilia.SelectedValue is int id && id > 0 ? id : (int?)null;
            using (var editor = new frmMaterialEditor(null, familia))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    LimpiarFiltros();
                    Mensajes.Intentar(this, () => CargarMateriales(editor.IdMaterial));
                }
            }
        }

        private void botModificar_Click(object sender, EventArgs e)
        {
            var m = MaterialSeleccionado();
            if (m == null)
            {
                return;
            }
            using (var editor = new frmMaterialEditor(m.IdMaterial, null))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    Mensajes.Intentar(this, () => CargarMateriales(m.IdMaterial));
                }
            }
        }

        private void botEliminar_Click(object sender, EventArgs e)
        {
            var m = MaterialSeleccionado();
            if (m == null)
            {
                return;
            }

            List<UsoDeMaterial> usos = null;
            if (!Mensajes.Intentar(this, () => usos = materiales.ListarUsos(m.IdMaterial)))
            {
                return;
            }

            string pregunta = usos.Count == 0
                ? $"¿Eliminar \"{Descripcion(m)}\" y sus {m.CantidadLotes} lotes?"
                : $"\"{Descripcion(m)}\" se usa en: {string.Join(", ", usos.Select(u => u.Modelo).Distinct())}.\n\n"
                  + "Si lo elimina también se quita de esos modelos. ¿Eliminar igual?";

            if (Mensajes.Confirmar(this, pregunta, "Eliminar material"))
            {
                Mensajes.Intentar(this, () =>
                {
                    materiales.Eliminar(m.IdMaterial);
                    CargarMateriales();
                });
            }
        }

        private void botComprar_Click(object sender, EventArgs e)
        {
            var m = MaterialSeleccionado();
            if (m == null)
            {
                return;
            }
            using (var dialogo = new frmPedido(m.IdMaterial))
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    Mensajes.Intentar(this, () => CargarMateriales(m.IdMaterial));
                }
            }
        }

        #endregion

        #region Lotes

        private void botAgregarLote_Click(object sender, EventArgs e)
        {
            var m = MaterialSeleccionado();
            if (m == null)
            {
                return;
            }
            using (var editor = new frmLoteEditor(m.IdMaterial, Descripcion(m), null))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    Mensajes.Intentar(this, () => CargarMateriales(m.IdMaterial));
                }
            }
        }

        private void botModificarLote_Click(object sender, EventArgs e)
        {
            var m = MaterialSeleccionado();
            var lote = LoteSeleccionado();
            if (m == null || lote == null)
            {
                return;
            }
            using (var editor = new frmLoteEditor(m.IdMaterial, Descripcion(m), lote))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    Mensajes.Intentar(this, () => CargarMateriales(m.IdMaterial));
                }
            }
        }

        private void dgvLotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                botModificarLote_Click(sender, e);
            }
        }

        private void botEliminarLote_Click(object sender, EventArgs e)
        {
            var m = MaterialSeleccionado();
            var lote = LoteSeleccionado();
            if (m == null || lote == null)
            {
                return;
            }
            string pregunta = $"¿Eliminar el lote de {Numeros.Cantidad(lote.Cantidad)} " +
                              $"({lote.Fabricante}, {lote.Ubicacion}) de \"{Descripcion(m)}\"?";
            if (Mensajes.Confirmar(this, pregunta, "Eliminar lote"))
            {
                Mensajes.Intentar(this, () =>
                {
                    lotes.Eliminar(lote.IdLote);
                    CargarMateriales(m.IdMaterial);
                });
            }
        }

        #endregion

        #region Familias, unidades, informe, impresión

        private void botFamilias_Click(object sender, EventArgs e)
        {
            using (var editor = new frmListaEditable("Familias",
                () => familias.Listar().Select(f => new ItemLista(f.IdFamilia, f.Nombre)),
                nombre => familias.Crear(nombre),
                (id, nombre) => familias.Renombrar(id, nombre),
                id => familias.Eliminar(id)))
            {
                editor.ShowDialog(this);
            }
            Mensajes.Intentar(this, () =>
            {
                CargarFamilias();
                CargarMateriales();
            });
        }

        private void botTipos_Click(object sender, EventArgs e)
        {
            using (var editor = new frmListaEditable("Unidades de medida",
                () => tipos.Listar().Select(t => new ItemLista(t.IdTipoUnidad, t.Nombre)),
                nombre => tipos.Crear(nombre),
                (id, nombre) => tipos.Renombrar(id, nombre),
                id => tipos.Eliminar(id)))
            {
                editor.ShowDialog(this);
            }
            Mensajes.Intentar(this, () => CargarMateriales());
        }

        private void botInforme_Click(object sender, EventArgs e)
        {
            List<MaterialStock> todos = null;
            if (Mensajes.Intentar(this, () => todos = materiales.ListarStock()))
            {
                using (var informe = new frmInformeValor(todos))
                {
                    informe.ShowDialog(this);
                }
            }
        }

        private void botImprimir_Click(object sender, EventArgs e)
        {
            string titulo = "Materiales";
            if (cboFamilia.SelectedValue is int id && id > 0)
            {
                titulo += " - " + cboFamilia.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                titulo += $" - \"{txtBuscar.Text.Trim()}\"";
            }
            ImpresionGrilla.VistaPrevia(this, dgvMateriales, titulo, labResumen.Text);
        }

        #endregion

        private void botSalir_Click(object sender, EventArgs e) => Close();

        private void frmMateriales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
