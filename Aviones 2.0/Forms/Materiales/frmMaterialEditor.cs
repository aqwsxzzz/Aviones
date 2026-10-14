using System;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;
using Aviones_2._0.Forms.Comun;

namespace Aviones_2._0.Forms.Materiales
{
    /// <summary>Alta o modificación de un material. En el alta se puede cargar también el primer lote.</summary>
    public partial class frmMaterialEditor : Form
    {
        private readonly MaterialRepositorio materiales = new MaterialRepositorio();
        private readonly LoteRepositorio lotes = new LoteRepositorio();
        private readonly TipoUnidadRepositorio tipos = new TipoUnidadRepositorio();

        private readonly int? idMaterial;
        private readonly int? idFamiliaSugerida;
        private byte[] imagen;
        private bool imagenCambiada;

        /// <summary>El material guardado.</summary>
        public int IdMaterial { get; private set; }

        /// <param name="idMaterial">null para un material nuevo.</param>
        /// <param name="idFamiliaSugerida">Familia preseleccionada en el alta.</param>
        public frmMaterialEditor(int? idMaterial, int? idFamiliaSugerida)
        {
            InitializeComponent();
            this.idMaterial = idMaterial;
            this.idFamiliaSugerida = idFamiliaSugerida;
            Estilo.Botones(botGuardar);
            Estilo.BotonSecundario(botCancelar);
            Estilo.EnterAvanza(this);
        }

        private bool EsNuevo => !idMaterial.HasValue;

        private void frmMaterialEditor_Load(object sender, EventArgs e)
        {
            bool ok = Mensajes.Intentar(this, () =>
            {
                cboFamilia.DisplayMember = nameof(Familia.Nombre);
                cboFamilia.ValueMember = nameof(Familia.IdFamilia);
                cboFamilia.DataSource = new FamiliaRepositorio().Listar();
                CargarUnidades(null);

                txtGrupo.AutoCompleteCustomSource.AddRange(materiales.ListarGrupos().ToArray());
                txtFabricante.AutoCompleteCustomSource.AddRange(lotes.ListarFabricantes().ToArray());
                txtUbicacion.AutoCompleteCustomSource.AddRange(lotes.ListarUbicaciones().ToArray());

                if (EsNuevo)
                {
                    Text = "Nuevo material";
                    cboFamilia.SelectedValue = idFamiliaSugerida ?? -1;
                    if (idFamiliaSugerida == null) cboFamilia.SelectedIndex = -1;
                    cboUnidad.SelectedIndex = -1;
                    dtpFecha.Value = DateTime.Today;
                }
                else
                {
                    Text = "Modificar material";
                    grpLote.Visible = false;
                    MostrarMaterial(materiales.Obtener(idMaterial.Value)
                        ?? throw new ReglaNegocioException("El material ya no existe."));
                    imagen = materiales.ObtenerImagen(idMaterial.Value);
                    Imagenes.Mostrar(picImagen, Imagenes.DesdeBytes(imagen));
                }
            });

            if (!ok)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }
            ActiveControl = EsNuevo && cboFamilia.SelectedIndex < 0 ? (Control)cboFamilia : txtGrupo;
        }

        private void MostrarMaterial(Material m)
        {
            cboFamilia.SelectedValue = m.IdFamilia;
            txtGrupo.Text = m.Grupo;
            txtCaracteristica.Text = m.Caracteristica;
            txtColor.Text = m.Color;
            txtMedidas.Text = m.Medidas;
            txtCodigo.Text = m.Codigo;
            radUsado.Checked = m.Estado == EstadosMaterial.Usado;
            radNuevo.Checked = !radUsado.Checked;
            cboUnidad.SelectedValue = m.IdTipoUnidad;
            txtAComprar.Text = Numeros.Cantidad(m.CantidadAComprar);
        }

        private void CargarUnidades(int? seleccionar)
        {
            cboUnidad.DisplayMember = nameof(TipoUnidad.Nombre);
            cboUnidad.ValueMember = nameof(TipoUnidad.IdTipoUnidad);
            cboUnidad.DataSource = tipos.Listar();
            if (seleccionar.HasValue)
            {
                cboUnidad.SelectedValue = seleccionar.Value;
            }
        }

        private void botUnidades_Click(object sender, EventArgs e)
        {
            int? actual = cboUnidad.SelectedValue as int?;
            using (var editor = new frmListaEditable("Unidades de medida",
                () => tipos.Listar().Select(t => new ItemLista(t.IdTipoUnidad, t.Nombre)),
                nombre => tipos.Crear(nombre),
                (id, nombre) => tipos.Renombrar(id, nombre),
                id => tipos.Eliminar(id)))
            {
                editor.ShowDialog(this);
                Mensajes.Intentar(this, () => CargarUnidades(editor.UltimoCreado ?? actual));
            }
        }

        private void botCargarImagen_Click(object sender, EventArgs e)
        {
            var nueva = Imagenes.Elegir(this);
            if (nueva != null)
            {
                imagen = nueva;
                imagenCambiada = true;
                Imagenes.Mostrar(picImagen, Imagenes.DesdeBytes(imagen));
            }
        }

        private void botQuitarImagen_Click(object sender, EventArgs e)
        {
            imagen = null;
            imagenCambiada = true;
            Imagenes.Mostrar(picImagen, null);
        }

        private void botGuardar_Click(object sender, EventArgs e)
        {
            if (!(cboFamilia.SelectedValue is int idFamilia))
            {
                Invalido(cboFamilia, "Elija la familia.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtGrupo.Text))
            {
                Invalido(txtGrupo, "Escriba el grupo (por ejemplo Balsa, Servo, Batería).");
                return;
            }
            if (!(cboUnidad.SelectedValue is int idTipoUnidad))
            {
                Invalido(cboUnidad, "Elija la unidad de medida.");
                return;
            }
            if (!Numeros.TryLeerOpcional(txtAComprar.Text, out decimal? aComprar) || aComprar < 0)
            {
                Invalido(txtAComprar, "La cantidad a comprar tiene que ser un número.");
                return;
            }

            Lote primerLote = null;
            if (EsNuevo && !string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                if (!Numeros.TryLeer(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
                {
                    Invalido(txtCantidad, "La cantidad tiene que ser un número mayor que cero.");
                    return;
                }
                if (!Numeros.TryLeerOpcional(txtValor.Text, out decimal? valor) || valor < 0)
                {
                    Invalido(txtValor, "El valor unitario tiene que ser un número.");
                    return;
                }
                primerLote = new Lote
                {
                    Fabricante = Limpio(txtFabricante.Text),
                    Ubicacion = Limpio(txtUbicacion.Text),
                    Cantidad = cantidad,
                    ValorUnitario = valor ?? 0m,
                    FechaIngreso = dtpFecha.Value.Date
                };
            }

            var material = new Material
            {
                IdMaterial = idMaterial ?? 0,
                IdFamilia = idFamilia,
                Grupo = txtGrupo.Text.Trim(),
                Caracteristica = Limpio(txtCaracteristica.Text),
                Color = Limpio(txtColor.Text),
                Medidas = Limpio(txtMedidas.Text),
                Codigo = Limpio(txtCodigo.Text),
                Estado = radUsado.Checked ? EstadosMaterial.Usado : EstadosMaterial.Nuevo,
                IdTipoUnidad = idTipoUnidad,
                CantidadAComprar = aComprar
            };

            bool guardado = Mensajes.Intentar(this, () =>
            {
                if (EsNuevo)
                {
                    IdMaterial = materiales.Crear(material, primerLote);
                }
                else
                {
                    materiales.Actualizar(material);
                    IdMaterial = material.IdMaterial;
                }
                if (imagenCambiada || (EsNuevo && imagen != null))
                {
                    materiales.GuardarImagen(IdMaterial, imagen);
                }
            });

            if (guardado)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void Invalido(Control control, string mensaje)
        {
            Mensajes.Aviso(this, mensaje);
            control.Focus();
        }

        private static string Limpio(string texto) => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
    }
}
