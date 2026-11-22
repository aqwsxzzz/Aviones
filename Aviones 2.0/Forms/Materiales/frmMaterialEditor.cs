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
        private Sugerencias sugGrupo, sugCaracteristica, sugColor, sugMedidas;

        /// <summary>Se eligió la familia (Materiales filtra su listado con ella).</summary>
        public event Action<int> FamiliaElegida;

        /// <summary>
        /// Sin la parte de "Primer ingreso": para un material que se va a pedir (desde Compras), que todavía
        /// no está en el inventario. Su stock entra cuando se recibe el pedido.
        /// </summary>
        public bool SinPrimerIngreso { get; set; }

        /// <summary>El material guardado.</summary>
        public int IdMaterial { get; private set; }

        /// <param name="idMaterial">null para un material nuevo.</param>
        /// <param name="idFamiliaSugerida">Familia preseleccionada en el alta.</param>
        public frmMaterialEditor(int? idMaterial, int? idFamiliaSugerida)
        {
            InitializeComponent();
            EnColumna();
            Estilo.ColorDeLaSeccion(this);
            selFabricante.Configurar(ListaRepositorio.Fabricantes(), "Fabricantes");
            selUbicacion.Configurar(ListaRepositorio.Ubicaciones(), "Ubicaciones");
            fotoMaxima = picImagen.Size;
            this.idMaterial = idMaterial;
            this.idFamiliaSugerida = idFamiliaSugerida;
            Estilo.Botones(botGuardar);
            Estilo.BotonSecundario(botCancelar);

            KeyPreview = true;
            KeyDown += EnterAvanza;
            // Elegida la familia, se sigue con el grupo
            cboFamilia.SelectionChangeCommitted += (s, e) =>
            {
                if (!cboFamilia.DroppedDown) FamiliaCambiada();
            };
            cboFamilia.DropDownClosed += (s, e) => FamiliaCambiada();
            // Abierta al costado de Materiales (no como ventana que bloquea), Cancelar la cierra
            botCancelar.Click += (s, e) => { if (!Modal) Close(); };
        }

        private int? familiaAvisada;
        private Label labFoto;

        private void FamiliaCambiada()
        {
            if (!(cboFamilia.SelectedValue is int id))
            {
                return;
            }
            if (id != familiaAvisada)
            {
                familiaAvisada = id;
                FamiliaElegida?.Invoke(id);
            }
            BeginInvoke((Action)(() => txtGrupo.Select()));
        }

        /// <summary>
        /// Todo en una sola columna, angosta y alta, para que entre al costado del listado de Materiales:
        /// la foto debajo de Unidad y el primer ingreso con un dato por renglón.
        /// </summary>
        private void EnColumna()
        {
            tlpPrincipal.SuspendLayout();
            tlpLote.SuspendLayout();

            tlpPrincipal.SetRowSpan(flpImagen, 1);
            tlpPrincipal.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 390F);
            tlpPrincipal.ColumnStyles.RemoveAt(2);
            tlpPrincipal.ColumnCount = 2;
            // Desde Stock mínimo todo baja un renglón, para dejar lugar a la foto
            const int filaFoto = 8;
            foreach (Control c in tlpPrincipal.Controls)
            {
                int fila = tlpPrincipal.GetRow(c);
                if (c != flpImagen && fila >= filaFoto)
                {
                    tlpPrincipal.SetRow(c, fila + 1);
                }
            }
            tlpPrincipal.RowCount++;
            tlpPrincipal.RowStyles.Insert(filaFoto, new RowStyle(SizeType.AutoSize));
            tlpPrincipal.SetCellPosition(flpImagen, new TableLayoutPanelCellPosition(1, filaFoto));
            labFoto = new Label { Text = "Foto", AutoSize = true, Margin = new Padding(3, 8, 3, 3) };
            tlpPrincipal.Controls.Add(labFoto, 0, filaFoto);
            tlpPrincipal.SetColumnSpan(grpLote, 2);
            tlpPrincipal.SetColumnSpan(flpBotones, 2);
            // Con Tab, la foto después de Unidad
            flpImagen.TabIndex = 8;
            flpMinimo.TabIndex = 9;
            grpLote.TabIndex = 10;
            flpBotones.TabIndex = 11;

            // La foto a la izquierda y sus botones al lado
            flpImagen.FlowDirection = FlowDirection.LeftToRight;
            flpImagen.Margin = new Padding(3, 6, 3, 3);
            picImagen.Size = new System.Drawing.Size(200, 150);
            var botonesFoto = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false, Margin = new Padding(10, 0, 0, 0) };
            foreach (var b in new[] { botCargarImagen, botQuitarImagen })
            {
                flpImagen.Controls.Remove(b);
                b.Width = 160;
                botonesFoto.Controls.Add(b);
            }
            flpImagen.Controls.Add(botonesFoto);

            // Primer ingreso: título y dato, un renglón cada uno
            tlpLote.ColumnStyles.Clear();
            tlpLote.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            tlpLote.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLote.ColumnCount = 2;
            tlpLote.RowStyles.Clear();
            var orden = new (Control Titulo, Control Dato)[]
            {
                (labFabricante, selFabricante), (labUbicacion, selUbicacion), (labCantidad, txtCantidad),
                (labValor, txtValor), (labFecha, dtpFecha), (labEstado, flpEstado)
            };
            tlpLote.RowCount = orden.Length;
            for (int i = 0; i < orden.Length; i++)
            {
                tlpLote.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tlpLote.SetCellPosition(orden[i].Titulo, new TableLayoutPanelCellPosition(0, i));
                tlpLote.SetCellPosition(orden[i].Dato, new TableLayoutPanelCellPosition(1, i));
                orden[i].Dato.TabIndex = i;
            }
            labUbicacion.Margin = labValor.Margin = labEstado.Margin = labFabricante.Margin;
            // La familia y su botón entran en un renglón
            cboFamilia.Width = 230;

            tlpLote.ResumeLayout();
            tlpPrincipal.ResumeLayout();
        }

        /// <summary>Lo que se recorre con Enter, en este orden. Lo que está oculto o apagado se saltea.</summary>
        private Control[] OrdenEnter => new Control[]
        {
            cboFamilia, txtGrupo, txtCaracteristica, txtColor, txtMedidas, txtCodigo, txtNota, cboUnidad,
            txtAComprar, selFabricante, selUbicacion, txtCantidad, txtValor, dtpFecha, flpEstado, botGuardar
        };

        /// <summary>
        /// Enter pasa al campo siguiente (también desde Nota; Mayúsculas+Enter hace un renglón nuevo).
        /// En un botón, Enter lo toca. Con una sugerencia marcada, Enter la elige y sigue.
        /// </summary>
        private void EnterAvanza(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Shift || e.Control)
            {
                return;
            }
            foreach (var sug in new[] { sugGrupo, sugCaracteristica, sugColor, sugMedidas })
            {
                if (sug != null && sug.HayElegida)
                {
                    sug.Aceptar();
                }
            }
            var orden = OrdenEnter;
            int actual = Array.FindIndex(orden, c => c.ContainsFocus);
            if (actual < 0 || ActiveControl is Button
                || (orden[actual] is SelectorLista selector && selector.ActiveControl is Button)
                || (orden[actual] is ComboBox combo && combo.DroppedDown))
            {
                return;
            }
            e.Handled = e.SuppressKeyPress = true;
            IrDespuesDe(orden[actual]);
        }

        /// <summary>Pasa al primer campo visible y encendido después del indicado.</summary>
        private void IrDespuesDe(Control campo)
        {
            var orden = OrdenEnter;
            for (int i = Array.IndexOf(orden, campo) + 1; i < orden.Length; i++)
            {
                var c = orden[i];
                if (!c.Visible || !c.Enabled)
                {
                    continue;
                }
                if (c == flpEstado)
                {
                    (radUsado.Checked ? radUsado : radNuevo).Select();
                }
                else if (c is SelectorLista)
                {
                    c.SelectNextControl(null, true, true, true, false);
                }
                else
                {
                    c.Select();
                }
                return;
            }
        }

        /// <summary>Esc con sugerencias abiertas sólo las cierra (no cancela la pantalla).</summary>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                foreach (var sug in new[] { sugGrupo, sugCaracteristica, sugColor, sugMedidas })
                {
                    if (sug != null && sug.Abierta)
                    {
                        sug.Cerrar();
                        return true;
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Copia al alta un dato de otro material (doble clic en esa casilla del listado de Materiales)
        /// y sigue con el campo siguiente. Sólo los datos que describen al material.
        /// </summary>
        public void Copiar(MaterialStock origen, string propiedad)
        {
            Control campo;
            switch (propiedad)
            {
                case nameof(MaterialStock.Familia):
                    cboFamilia.SelectedValue = origen.IdFamilia;
                    campo = cboFamilia;
                    break;
                case nameof(MaterialStock.Grupo):
                    sugGrupo.Poner(origen.Grupo);
                    campo = txtGrupo;
                    break;
                case nameof(MaterialStock.Caracteristica):
                    sugCaracteristica.Poner(origen.Caracteristica);
                    campo = txtCaracteristica;
                    break;
                case nameof(MaterialStock.Color):
                    sugColor.Poner(origen.Color);
                    campo = txtColor;
                    break;
                case nameof(MaterialStock.Medidas):
                    sugMedidas.Poner(origen.Medidas);
                    campo = txtMedidas;
                    break;
                case nameof(MaterialStock.Codigo):
                    txtCodigo.Text = origen.Codigo;
                    campo = txtCodigo;
                    break;
                case nameof(MaterialStock.TipoUnidad):
                    cboUnidad.SelectedValue = origen.IdTipoUnidad;
                    campo = cboUnidad;
                    break;
                default:
                    return;
            }
            Activate();
            IrDespuesDe(campo);
        }

        private bool EsNuevo => !idMaterial.HasValue;

        private void frmMaterialEditor_Load(object sender, EventArgs e)
        {
            bool ok = Mensajes.Intentar(this, () =>
            {
                CargarFamilias(null);
                CargarUnidades(null);

                // Lo ya escrito en otros materiales, en una lista con letra grande
                sugGrupo = new Sugerencias(txtGrupo, materiales.ListarGrupos());
                sugCaracteristica = new Sugerencias(txtCaracteristica, materiales.ListarCaracteristicas());
                sugColor = new Sugerencias(txtColor, materiales.ListarColores());
                sugMedidas = new Sugerencias(txtMedidas, materiales.ListarMedidas());
                selFabricante.Cargar(null);
                selUbicacion.Cargar(null);

                if (EsNuevo)
                {
                    Text = "Nuevo material";
                    grpLote.Visible = !SinPrimerIngreso;
                    // Un material que todavía no se compró: nota, unidad, foto y mínimo se completan al recibirlo
                    foreach (var c in new Control[] { labNota, txtNota, labUnidad, flpUnidad, labFoto, flpImagen, labAComprar, flpMinimo })
                    {
                        c.Visible = !SinPrimerIngreso;
                    }
                    cboFamilia.SelectedValue = idFamiliaSugerida ?? -1;
                    if (idFamiliaSugerida == null) cboFamilia.SelectedIndex = -1;
                    // Casi todo se cuenta en unidades: viene elegida
                    int unidades = cboUnidad.FindStringExact("Unidades");
                    cboUnidad.SelectedIndex = unidades >= 0 || cboUnidad.Items.Count == 0 || !SinPrimerIngreso ? unidades : 0;
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
                if (!Modal) BeginInvoke((Action)Close);
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
            if (EsNuevo && grpLote.Visible && !string.IsNullOrWhiteSpace(txtCantidad.Text))
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
                if (!Modal) Close();
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
