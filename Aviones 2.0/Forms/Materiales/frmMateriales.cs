using System;
using System.Collections.Generic;
using System.Drawing;
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
            Imagenes.MarcarSinImagen(picFoto);
            Estilo.Grilla(dgvMateriales);
            Estilo.Grilla(dgvLotes);
            Estilo.Grilla(dgvUsos);
            Estilo.Botones(botNuevo, botModificar, botEliminar, botComprar, botInforme, botImprimir,
                botAgregarLote, botModificarLote, botEliminarLote);
            Estilo.BotonSecundario(botSalir);
            dgvLotes.SelectionChanged += (s, e) => ActualizarBotonesLote();
            OrdenGrilla.Habilitar(dgvMateriales, dgvLotes, dgvUsos);
        }

        private void frmMateriales_Load(object sender, EventArgs e)
        {
            splPrincipal.SplitterDistance = Math.Max(400, (int)(splPrincipal.Width * 0.72));
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
                + (lista.Any(m => !m.SeRepone) ? $"{lista.Count(m => !m.SeRepone)} no se reponen (en cursiva)   ·   " : "")
                + $"Valor: $ {Numeros.Dinero(valor)}";

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
                MostrarFoto(null);
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

                    MostrarFoto(Imagenes.DesdeBytes(materiales.ObtenerImagen(m.IdMaterial)));
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
            if (!m.SeRepone)
            {
                e.CellStyle.Font = Cursiva(e.CellStyle.Font);
            }
        }

        private readonly Dictionary<Font, Font> cursivas = new Dictionary<Font, Font>();

        /// <summary>La misma letra en cursiva, creada una sola vez.</summary>
        private Font Cursiva(Font letra)
        {
            if (!cursivas.TryGetValue(letra, out var cursiva))
            {
                cursivas[letra] = cursiva = new Font(letra, letra.Style | FontStyle.Italic);
            }
            return cursiva;
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

        private void MostrarFoto(Image foto)
        {
            Imagenes.Mostrar(picFoto, foto);
            AjustarFoto();
        }

        /// <summary>La foto toma la forma de la imagen dentro de su lugar; sin foto lo ocupa entero con "Sin imagen".</summary>
        private void AjustarFoto()
        {
            Imagenes.AjustarCaja(picFoto, new Size(
                tlpDetalle.ClientSize.Width - picFoto.Margin.Horizontal,
                (int)tlpDetalle.RowStyles[0].Height - picFoto.Margin.Vertical));
        }

        private void tlpDetalle_SizeChanged(object sender, EventArgs e) => AjustarFoto();

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
                bool guardado = editor.ShowDialog(this) == DialogResult.OK;
                Mensajes.Intentar(this, () =>
                {
                    CargarFamilias();
                    if (guardado)
                    {
                        LimpiarFiltros();
                        CargarMateriales(editor.IdMaterial);
                    }
                    else
                    {
                        CargarMateriales();
                    }
                });
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
                editor.ShowDialog(this);
                Mensajes.Intentar(this, () =>
                {
                    CargarFamilias();
                    CargarMateriales(m.IdMaterial);
                });
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
            if (!m.SeRepone && !Mensajes.Confirmar(this,
                    "Este material está marcado como que no se repone (no se vuelve a comprar).\n\n¿Agregarlo a la lista de compras igual?", "Comprar"))
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

        #region Informe e impresión

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
