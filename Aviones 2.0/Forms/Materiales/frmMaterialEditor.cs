using System;
using System.Collections.Generic;
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
        /// <summary>Lugar máximo para la foto; la caja se ajusta a la forma de cada imagen.</summary>
        private readonly System.Drawing.Size fotoMaxima;
        private readonly MaterialRepositorio materiales = new MaterialRepositorio();
        private readonly TipoUnidadRepositorio tipos = new TipoUnidadRepositorio();
        private readonly FamiliaRepositorio familias = new FamiliaRepositorio();

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
            Imagenes.MarcarSinImagen(picImagen);
            selFabricante.Configurar(ListaRepositorio.Fabricantes(), "Fabricantes");
            selUbicacion.Configurar(ListaRepositorio.Ubicaciones(), "Ubicaciones");
            fotoMaxima = picImagen.Size;
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
                CargarFamilias(null);
                CargarUnidades(null);

                txtGrupo.AutoCompleteCustomSource.AddRange(materiales.ListarGrupos().ToArray());
                txtCaracteristica.AutoCompleteCustomSource.AddRange(materiales.ListarCaracteristicas().ToArray());
                txtColor.AutoCompleteCustomSource.AddRange(materiales.ListarColores().ToArray());
                txtMedidas.AutoCompleteCustomSource.AddRange(materiales.ListarMedidas().ToArray());
                selFabricante.Cargar(null);
                selUbicacion.Cargar(null);

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
                    Imagenes.Mostrar(picImagen, Imagenes.DesdeBytes(imagen), fotoMaxima);
                }
            });

            if (!ok)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }
            ActiveControl = EsNuevo && cboFamilia.SelectedIndex < 0 ? (Control)cboFamilia : txtGrupo;
        }

        private void frmMaterialEditor_Shown(object sender, EventArgs e)
        {
            // Material nuevo sin familia: la lista ya abierta para elegirla
            if (EsNuevo && cboFamilia.SelectedIndex < 0 && cboFamilia.Items.Count > 0)
            {
                cboFamilia.Focus();
                cboFamilia.DroppedDown = true;
            }
        }

        private void MostrarMaterial(Material m)
        {
            cboFamilia.SelectedValue = m.IdFamilia;
            txtGrupo.Text = m.Grupo;
            txtCaracteristica.Text = m.Caracteristica;
            txtColor.Text = m.Color;
            txtMedidas.Text = m.Medidas;
            txtCodigo.Text = m.Codigo;
            cboUnidad.SelectedValue = m.IdTipoUnidad;
            txtAComprar.Text = Numeros.Cantidad(m.StockMinimo);
            chkSeRepone.Checked = m.SeRepone;
            txtNota.Text = m.Nota;
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

        private void CargarFamilias(int? seleccionar)
        {
            cboFamilia.DisplayMember = nameof(Familia.Nombre);
            cboFamilia.ValueMember = nameof(Familia.IdFamilia);
            cboFamilia.DataSource = familias.Listar();
            if (seleccionar.HasValue)
            {
                cboFamilia.SelectedValue = seleccionar.Value;
            }
        }

        private void botFamilias_Click(object sender, EventArgs e)
        {
            int? actual = cboFamilia.SelectedValue as int?;
            using (var editor = new frmListaEditable("Familias",
                () => familias.Listar().Select(f => new ItemLista(f.IdFamilia, f.Nombre)),
                nombre => familias.Crear(nombre),
                (id, nombre) => familias.Renombrar(id, nombre),
                id => familias.Eliminar(id)))
            {
                editor.ShowDialog(this);
                Mensajes.Intentar(this, () => CargarFamilias(editor.UltimoCreado ?? actual));
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
                Imagenes.Mostrar(picImagen, Imagenes.DesdeBytes(imagen), fotoMaxima);
            }
        }

        private void botQuitarImagen_Click(object sender, EventArgs e)
        {
            imagen = null;
            imagenCambiada = true;
            Imagenes.Mostrar(picImagen, null, fotoMaxima);
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
            // Stock mínimo: vacío usa el mínimo general de Compras. Si no se repone no se usa, pero se guarda por si vuelve a reponerse.
            bool minimoValido = Numeros.TryLeerOpcional(txtAComprar.Text, out decimal? stockMinimo) && !(stockMinimo < 0);
            if (!minimoValido && !chkSeRepone.Checked)
            {
                stockMinimo = null;
            }
            else if (!minimoValido)
            {
                Invalido(txtAComprar, "El stock mínimo tiene que ser un número (o vacío para usar el general)."); 
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
                    IdFabricante = selFabricante.Valor,
                    IdUbicacion = selUbicacion.Valor,
                    Cantidad = cantidad,
                    CompradoUsado = radUsado.Checked,
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
                IdTipoUnidad = idTipoUnidad,
                StockMinimo = stockMinimo,
                SeRepone = chkSeRepone.Checked,
                Nota = Limpio(txtNota.Text)
            };

            if (!ConfirmarSiEstaRepetido(material))
            {
                return;
            }

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

        /// <summary>
        /// Avisa si ya hay un material igual. Una compra nueva de algo que ya existe
        /// conviene cargarla como lote del material existente.
        /// </summary>
        private bool ConfirmarSiEstaRepetido(Material material)
        {
            List<MaterialStock> iguales = null;
            if (!Mensajes.Intentar(this, () => iguales = materiales.BuscarIguales(material)))
            {
                return false;
            }
            if (iguales.Count == 0)
            {
                return true;
            }

            var igual = iguales[0];
            string descripcion = string.Join(" ", new[] { igual.Familia, igual.Grupo, igual.Caracteristica, igual.Color, igual.Medidas, igual.Codigo }
                .Where(t => !string.IsNullOrWhiteSpace(t)));
            string pregunta = $"Ya existe un material igual:\n\n    {descripcion}\n"
                + $"    ({igual.CantidadLotes} lotes, {Numeros.Cantidad(igual.Disponible)} {igual.TipoUnidad} disponibles)\n\n"
                + (EsNuevo
                    ? "Si es una compra nueva, conviene cancelar y usar \"Agregar lote\" en ese material.\n\n¿Crear otro material igual de todos modos?"
                    : "¿Guardar igual? Van a quedar dos materiales con la misma descripción.");
            return Mensajes.Confirmar(this, pregunta, "Material repetido");
        }

        private void chkSeRepone_CheckedChanged(object sender, EventArgs e) => txtAComprar.Enabled = chkSeRepone.Checked;

        private void Invalido(Control control, string mensaje)
        {
            Mensajes.Aviso(this, mensaje);
            control.Focus();
        }

        private static string Limpio(string texto) => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
    }
}
