using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;
using Aviones_2._0.Forms.Comun;

namespace Aviones_2._0.Forms.Modelos
{
    /// <summary>
    /// Alta o modificación de un modelo con sus materiales.
    /// Todo se guarda junto al confirmar; si un lote no alcanza no se guarda nada.
    /// </summary>
    public partial class frmModeloEditor : Form
    {
        private const string EstadoInicial = "En construcción";

        private readonly ModeloRepositorio modelos = new ModeloRepositorio();
        private readonly LoteRepositorio lotes = new LoteRepositorio();
        private readonly int? idModelo;

        private readonly BindingList<FilaMaterial> filas = new BindingList<FilaMaterial>();
        private readonly List<MaterialQuitado> quitados = new List<MaterialQuitado>();
        private readonly List<FilaMaterial> filasQuitadas = new List<FilaMaterial>();

        private byte[] foto;
        private bool fotoCambiada;
        private bool modificado;
        private bool cargando = true;
        private Font negrita;

        /// <summary>El modelo guardado.</summary>
        public int IdModelo { get; private set; }

        /// <param name="idModelo">null para un modelo nuevo.</param>
        public frmModeloEditor(int? idModelo)
        {
            InitializeComponent();
            this.idModelo = idModelo;
            Estilo.Grilla(dgvMateriales);
            dgvMateriales.BackgroundColor = Color.Linen;
            Estilo.Botones(botAgregarMaterial, botCambiarCantidad, botQuitarMaterial, botGuardar);
            Estilo.BotonSecundario(botCancelar);
            Estilo.EnterAvanza(this);
            dgvMateriales.DataSource = filas;
            filas.ListChanged += (s, e) => ActualizarTotal();
        }

        private bool EsNuevo => !idModelo.HasValue;

        private void frmModeloEditor_Load(object sender, EventArgs e)
        {
            bool ok = Mensajes.Intentar(this, () =>
            {
                var estados = new EstadoModeloRepositorio().Listar();
                cboEstado.DisplayMember = nameof(EstadoModelo.Nombre);
                cboEstado.ValueMember = nameof(EstadoModelo.IdEstadoModelo);
                cboEstado.DataSource = estados;

                if (EsNuevo)
                {
                    Text = "Nuevo modelo";
                    cboEstado.SelectedItem = estados.FirstOrDefault(x => x.Nombre == EstadoInicial) ?? estados.FirstOrDefault();
                    dtpDesde.Value = DateTime.Today;
                    dtpDesde.Checked = true;
                }
                else
                {
                    var modelo = modelos.Obtener(idModelo.Value) ?? throw new ReglaNegocioException("El modelo ya no existe.");
                    Text = "Modificar modelo: " + modelo.Nombre;
                    txtNombre.Text = modelo.Nombre;
                    txtAlias.Text = modelo.Alias;
                    cboEstado.SelectedValue = modelo.IdEstadoModelo;
                    dtpDesde.Checked = modelo.Desde.HasValue;
                    if (modelo.Desde.HasValue) dtpDesde.Value = modelo.Desde.Value;

                    foreach (var m in modelos.ListarMateriales(idModelo.Value))
                    {
                        filas.Add(new FilaMaterial
                        {
                            IdMaterialModelo = m.IdMaterialModelo,
                            IdLote = m.IdLote,
                            IdMaterial = m.IdMaterial,
                            Familia = m.Familia,
                            Material = string.Join(" ", new[] { m.Grupo, m.Caracteristica, m.Color, m.Medidas }
                                .Where(s => !string.IsNullOrWhiteSpace(s))),
                            Fabricante = m.Fabricante,
                            Unidad = m.TipoUnidad,
                            Cantidad = m.Cantidad,
                            CantidadOriginal = m.Cantidad,
                            ValorUnitario = m.ValorUnitario
                        });
                    }

                    foto = modelos.ObtenerFoto(idModelo.Value);
                    Imagenes.Mostrar(picFoto, Imagenes.DesdeBytes(foto));
                }
            });

            if (!ok)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }
            dgvMateriales.ClearSelection();
            ActualizarBotones();
            ActualizarTotal();
            cargando = false;
            modificado = false;
            ActiveControl = txtNombre;
        }

        #region Materiales

        /// <summary>
        /// Cuánto de un lote toman los cambios todavía sin guardar
        /// (positivo: se va a descontar; negativo: se va a devolver).
        /// </summary>
        private decimal UsoPendiente(int idLote) =>
            filas.Where(f => f.IdLote == idLote).Sum(f => f.Cantidad - f.CantidadOriginal)
            - quitados.Where(q => q.DevolverAlStock).Join(filasQuitadas.Where(f => f.IdLote == idLote),
                q => q.IdMaterialModelo, f => f.IdMaterialModelo.Value, (q, f) => f.CantidadOriginal).Sum();

        private decimal UsoPendienteMaterial(int idMaterial) =>
            filas.Where(f => f.IdMaterial == idMaterial).Select(f => f.IdLote)
                .Concat(filasQuitadas.Where(f => f.IdMaterial == idMaterial).Select(f => f.IdLote))
                .Distinct().Sum(UsoPendiente);

        private FilaMaterial FilaSeleccionada() =>
            dgvMateriales.SelectedRows.Count > 0 ? dgvMateriales.SelectedRows[0].DataBoundItem as FilaMaterial : null;

        private void AgregarFila(FilaMaterial nueva)
        {
            var existente = filas.FirstOrDefault(f => f.IdLote == nueva.IdLote);
            if (existente != null)
            {
                existente.Cantidad += nueva.Cantidad;
                filas.ResetItem(filas.IndexOf(existente));
            }
            else
            {
                filas.Add(nueva);
            }
            modificado = true;
        }

        private void botAgregarMaterial_Click(object sender, EventArgs e)
        {
            using (var picker = new frmElegirLote(UsoPendiente, UsoPendienteMaterial, AgregarFila))
            {
                picker.ShowDialog(this);
            }
            ActualizarBotones();
        }

        private void botCambiarCantidad_Click(object sender, EventArgs e)
        {
            var fila = FilaSeleccionada();
            if (fila == null)
            {
                return;
            }
            Lote lote = null;
            if (!Mensajes.Intentar(this, () => lote = lotes.Obtener(fila.IdLote)) || lote == null)
            {
                return;
            }
            decimal maximo = fila.Cantidad + lote.Disponible - UsoPendiente(fila.IdLote);
            var nueva = frmCantidad.Pedir(this, "Cambiar cantidad",
                $"¿Cuánto de \"{fila.Material}\" usa el modelo? ({fila.Unidad})", fila.Cantidad, maximo);
            if (nueva.HasValue && nueva.Value != fila.Cantidad)
            {
                fila.Cantidad = nueva.Value;
                filas.ResetItem(filas.IndexOf(fila));
                modificado = true;
            }
        }

        private void dgvMateriales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                botCambiarCantidad_Click(sender, e);
            }
        }

        private void botQuitarMaterial_Click(object sender, EventArgs e)
        {
            var fila = FilaSeleccionada();
            if (fila == null)
            {
                return;
            }
            if (!fila.EsNueva)
            {
                var respuesta = MessageBox.Show(this,
                    $"¿\"{fila.Material}\" se recicla?\n\n" +
                    "Sí: vuelve al stock.\nNo: se da por consumido.",
                    "Quitar material", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                if (respuesta == DialogResult.Cancel)
                {
                    return;
                }
                quitados.Add(new MaterialQuitado
                {
                    IdMaterialModelo = fila.IdMaterialModelo.Value,
                    DevolverAlStock = respuesta == DialogResult.Yes
                });
                filasQuitadas.Add(fila);
            }
            filas.Remove(fila);
            modificado = true;
            ActualizarBotones();
        }

        private void dgvMateriales_SelectionChanged(object sender, EventArgs e) => ActualizarBotones();

        private void dgvMateriales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !(dgvMateriales.Rows[e.RowIndex].DataBoundItem is FilaMaterial f))
            {
                return;
            }
            if (f.EsNueva)
            {
                e.CellStyle.BackColor = Color.Honeydew;
            }
            if (e.ColumnIndex == colCantidad.Index && f.Cantidad != f.CantidadOriginal)
            {
                e.CellStyle.Font = negrita ?? (negrita = new Font(dgvMateriales.DefaultCellStyle.Font, FontStyle.Bold));
            }
        }

        private void ActualizarBotones()
        {
            bool hay = FilaSeleccionada() != null;
            botCambiarCantidad.Enabled = hay;
            botQuitarMaterial.Enabled = hay;
        }

        private void ActualizarTotal()
        {
            labTotal.Text = $"Total materiales: $ {Numeros.Dinero(filas.Sum(f => f.Subtotal))}";
            labMateriales.Text = $"Materiales del modelo ({filas.Count})";
        }

        #endregion

        #region Foto y datos

        private void botCargarFoto_Click(object sender, EventArgs e)
        {
            var nueva = Imagenes.Elegir(this);
            if (nueva != null)
            {
                foto = nueva;
                fotoCambiada = true;
                modificado = true;
                Imagenes.Mostrar(picFoto, Imagenes.DesdeBytes(foto));
            }
        }

        private void botQuitarFoto_Click(object sender, EventArgs e)
        {
            foto = null;
            fotoCambiada = true;
            modificado = true;
            Imagenes.Mostrar(picFoto, null);
        }

        private void Datos_Changed(object sender, EventArgs e)
        {
            if (!cargando)
            {
                modificado = true;
            }
        }

        #endregion

        private void botGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                Mensajes.Aviso(this, "Escriba el nombre del modelo.");
                txtNombre.Focus();
                return;
            }
            if (!(cboEstado.SelectedValue is int idEstado))
            {
                Mensajes.Aviso(this, "Elija la condición del modelo.");
                cboEstado.Focus();
                return;
            }

            var modelo = new Modelo
            {
                IdModelo = idModelo ?? 0,
                Nombre = txtNombre.Text.Trim(),
                Alias = string.IsNullOrWhiteSpace(txtAlias.Text) ? null : txtAlias.Text.Trim(),
                IdEstadoModelo = idEstado,
                Desde = dtpDesde.Checked ? dtpDesde.Value.Date : (DateTime?)null
            };

            bool guardado = Mensajes.Intentar(this, () =>
            {
                if (EsNuevo)
                {
                    IdModelo = modelos.Crear(modelo, filas.Select(f => new LoteAsignado(f.IdLote, f.Cantidad)));
                }
                else
                {
                    var cambios = new CambiosDeMateriales();
                    cambios.Quitados.AddRange(quitados);
                    cambios.Cantidades.AddRange(filas
                        .Where(f => !f.EsNueva && f.Cantidad != f.CantidadOriginal)
                        .Select(f => new CambioDeCantidad { IdMaterialModelo = f.IdMaterialModelo.Value, Cantidad = f.Cantidad }));
                    cambios.Agregados.AddRange(filas.Where(f => f.EsNueva).Select(f => new LoteAsignado(f.IdLote, f.Cantidad)));
                    modelos.Guardar(modelo, cambios);
                    IdModelo = modelo.IdModelo;
                }
                if (fotoCambiada)
                {
                    modelos.GuardarFoto(IdModelo, foto);
                }
            });

            if (guardado)
            {
                modificado = false;
                DialogResult = DialogResult.OK;
            }
        }

        private void frmModeloEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK && modificado
                && !Mensajes.Confirmar(this, "Hay cambios sin guardar. ¿Descartarlos?", "Cancelar"))
            {
                e.Cancel = true;
            }
        }
    }
}
