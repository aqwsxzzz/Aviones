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
        /// <summary>Materiales guardados que se quitaron en esta edición, con cuánto vuelve de cada uno.</summary>
        private readonly List<(FilaMaterial Fila, Devolucion Devolucion)> quitados = new List<(FilaMaterial, Devolucion)>();

        /// <summary>Lugar máximo para la foto; la caja se ajusta a la forma de cada imagen.</summary>
        private readonly Size fotoMaxima;
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
            Imagenes.MarcarSinImagen(picFoto);
            fotoMaxima = picFoto.Size;
            this.idModelo = idModelo;
            Estilo.Grilla(dgvMateriales);
            dgvMateriales.BackgroundColor = BackColor;
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
                            Estado = m.Estado,
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
                    Imagenes.Mostrar(picFoto, Imagenes.DesdeBytes(foto), fotoMaxima);
                }
            });

            if (!ok)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }
            // El primer material ya elegido: CAMBIAR CANTIDAD y QUITAR MATERIAL quedan listos para usar
            dgvMateriales.ClearSelection();
            if (dgvMateriales.Rows.Count > 0)
            {
                dgvMateriales.CurrentCell = dgvMateriales.Rows[0].Cells.Cast<DataGridViewCell>().First(c => c.Visible);
                dgvMateriales.Rows[0].Selected = true;
            }
            ActualizarBotones();
            ActualizarTotal();
            cargando = false;
            modificado = false;
            ActiveControl = txtNombre;
        }

        #region Materiales

        /// <summary>Cuánto de una devolución va al stock nuevo o al usado.</summary>
        private static decimal Parte(Devolucion d, string estado) =>
            estado == EstadosMaterial.Usado ? d.Usado : d.Nuevo;

        /// <summary>
        /// Cuánto toma un renglón del stock nuevo o usado de un lote con los cambios sin guardar
        /// (positivo: se va a descontar; negativo: va a volver).
        /// </summary>
        private static decimal Contribucion(FilaMaterial f, int idLote, string estado)
        {
            if (f.IdLote != idLote)
            {
                return 0m;
            }
            decimal c = 0m;
            if (f.Cantidad > f.CantidadOriginal && f.Estado == estado)
            {
                c += f.Cantidad - f.CantidadOriginal;
            }
            if (f.Cantidad < f.CantidadOriginal)
            {
                c -= Parte(f.DevolucionBaja, estado);
            }
            return c;
        }

        /// <summary>Cuánto del stock nuevo o usado de un lote toman todos los cambios sin guardar.</summary>
        private decimal UsoPendiente(int idLote, string estado) =>
            filas.Sum(f => Contribucion(f, idLote, estado))
            - quitados.Where(q => q.Fila.IdLote == idLote).Sum(q => Parte(q.Devolucion, estado));

        private decimal UsoPendienteMaterial(int idMaterial) =>
            filas.Where(f => f.IdMaterial == idMaterial).Select(f => f.IdLote)
                .Concat(quitados.Where(q => q.Fila.IdMaterial == idMaterial).Select(q => q.Fila.IdLote))
                .Distinct()
                .Sum(l => UsoPendiente(l, EstadosMaterial.Nuevo) + UsoPendiente(l, EstadosMaterial.Usado));

        private FilaMaterial FilaSeleccionada() =>
            dgvMateriales.SelectedRows.Count > 0 ? dgvMateriales.SelectedRows[0].DataBoundItem as FilaMaterial : null;

        private void AgregarFila(FilaMaterial nueva)
        {
            var existente = filas.FirstOrDefault(f => f.IdLote == nueva.IdLote && f.Estado == nueva.Estado);
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
            // Lo que puede llegar a usar: lo guardado más lo que queda en su stock (nuevo o usado)
            // sin contar lo que este mismo renglón ya toma o devuelve en esta edición.
            decimal disponible = fila.Estado == EstadosMaterial.Usado ? lote.DisponibleUsado : lote.DisponibleNuevo;
            decimal libre = disponible - (UsoPendiente(fila.IdLote, fila.Estado) - Contribucion(fila, fila.IdLote, fila.Estado));
            decimal maximo = fila.CantidadOriginal + libre;

            var nueva = frmCantidad.Pedir(this, "Cambiar cantidad",
                $"¿Cuánto de \"{fila.Material}\" usa el modelo? ({fila.Unidad}, {fila.Condicion.ToLower()})", fila.Cantidad, maximo);
            if (!nueva.HasValue || nueva.Value == fila.Cantidad)
            {
                return;
            }

            // Si queda menos de lo guardado, lo que se quita puede volver al stock (nuevo o usado) o descartarse
            if (!fila.EsNueva && nueva.Value < fila.CantidadOriginal)
            {
                var devolucion = frmDestino.Pedir(this, fila.Material, fila.CantidadOriginal - nueva.Value, fila.Unidad, fila.Estado);
                if (!devolucion.HasValue)
                {
                    return;
                }
                fila.DevolucionBaja = devolucion.Value;
            }
            else
            {
                fila.DevolucionBaja = Devolucion.Nada;
            }

            fila.Cantidad = nueva.Value;
            filas.ResetItem(filas.IndexOf(fila));
            modificado = true;
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
                var devolucion = frmDestino.Pedir(this, fila.Material, fila.CantidadOriginal, fila.Unidad, fila.Estado);
                if (!devolucion.HasValue)
                {
                    return;
                }
                quitados.Add((fila, devolucion.Value));
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
                Imagenes.Mostrar(picFoto, Imagenes.DesdeBytes(foto), fotoMaxima);
            }
        }

        private void botQuitarFoto_Click(object sender, EventArgs e)
        {
            foto = null;
            fotoCambiada = true;
            modificado = true;
            Imagenes.Mostrar(picFoto, null, fotoMaxima);
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
                    IdModelo = modelos.Crear(modelo, filas.Select(f => new LoteAsignado(f.IdLote, f.Cantidad, f.Estado)));
                }
                else
                {
                    var cambios = new CambiosDeMateriales();
                    cambios.Quitados.AddRange(quitados.Select(q =>
                        new MaterialQuitado { IdMaterialModelo = q.Fila.IdMaterialModelo.Value, Devolucion = q.Devolucion }));
                    // Primero las bajas (pueden devolver stock que usan las subas)
                    cambios.Cantidades.AddRange(filas
                        .Where(f => !f.EsNueva && f.Cantidad != f.CantidadOriginal)
                        .OrderBy(f => f.Cantidad > f.CantidadOriginal)
                        .Select(f => new CambioDeCantidad
                        {
                            IdMaterialModelo = f.IdMaterialModelo.Value,
                            Cantidad = f.Cantidad,
                            DevolucionSiBaja = f.Cantidad < f.CantidadOriginal ? f.DevolucionBaja : Devolucion.Nada
                        }));
                    cambios.Agregados.AddRange(filas.Where(f => f.EsNueva).Select(f => new LoteAsignado(f.IdLote, f.Cantidad, f.Estado)));
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
