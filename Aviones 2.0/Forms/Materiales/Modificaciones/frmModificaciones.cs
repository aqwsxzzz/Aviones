using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aviones_2._0.Common;

namespace Aviones_2._0.Forms.Materiales.Modificaciones
{
    public partial class frmModificaciones : Form
    {
        private frmMateriales mate;
        private Componentes componentesGenericos;
        private funcMateriales funciones;

        private byte[] imageByte;
        private string[] arrayTextBoxes = new string[] { "texGrupo", "texCaracteristica", "texColor", "texMedidas", "texCodigo", "texFabricante", "texEstado", "texUbicacion", "texCantidad", "texTipo", "texValor" };

        private List<string> ListaNuevoValorMats = new List<string>();
        private List<string> ListaNombreColumnasMats = new List<string>();
        private List<string> ListaNuevoValorAcopio = new List<string>();
        private List<string> ListaNombreColumnasAcopio = new List<string>();
        private int contador = 0;
        public string cantOriginal = "";

        public frmModificaciones(frmMateriales mate, Componentes componentes)
        {
            InitializeComponent();

            this.mate = mate;
            componentesGenericos = new Componentes();
            funciones = new funcMateriales();

            funciones.mostrarTipo(comboxTipoDeUnidades);
            comboxTipoDeUnidades.Items.Add("Agregar Tipo");

            texFamilia.Text = mate.dgvMateriales.CurrentRow.Cells[0].Value.ToString();
            texGrupo.Text = mate.dgvMateriales.CurrentRow.Cells[1].Value.ToString();
            texCaracteristica.Text = mate.dgvMateriales.CurrentRow.Cells[2].Value.ToString();
            texColor.Text = mate.dgvMateriales.CurrentRow.Cells[3].Value.ToString();
            texMedidas.Text = mate.dgvMateriales.CurrentRow.Cells[4].Value.ToString();
            texCodigo.Text = mate.dgvMateriales.CurrentRow.Cells[5].Value.ToString();
            texFabricante.Text = mate.dgvMateriales.CurrentRow.Cells[6].Value.ToString();
            texEstado.Text = mate.dgvMateriales.CurrentRow.Cells[7].Value.ToString();
            texUbicacion.Text = mate.dgvMateriales.CurrentRow.Cells[8].Value.ToString();
            texCantidad.Text = mate.dgvMateriales.CurrentRow.Cells[9].Value.ToString();
            texDisponible.Text = mate.dgvMateriales.CurrentRow.Cells[10].Value.ToString();
            texTipo.Text = mate.dgvMateriales.CurrentRow.Cells[11].Value.ToString();
            texValor.Text = mate.dgvMateriales.CurrentRow.Cells[12].Value.ToString();

            texGrupo.Focus();
            AsociarEventosTextBox(texGrupo, texCaracteristica, texColor, texMedidas, texCodigo, texFabricante, texEstado, texUbicacion, texCantidad, texTipo, texValor);
            NextFocusTextBoxes(texGrupo, texCaracteristica, texColor, texMedidas, texCodigo, texFabricante, texEstado, texUbicacion, texCantidad, texTipo, texValor);

            ListaNombreColumnasAcopio.Clear();
            ListaNombreColumnasMats.Clear();
            ListaNuevoValorAcopio.Clear();
            ListaNuevoValorMats.Clear();

            comboxTipoDeUnidades.Location = new Point(708, 440);
        }

        private void botCancelarModifica_Click(object sender, EventArgs e)
        {
            mate.labTituloDgv.Visible = true;
            mate.labTituloDgv.Location = new Point(1467, 147);

            mate.botIngresar.Enabled = true;
            mate.botIngresar.Visible = true;
            mate.botIngresar.Location = new Point(423, 956);
            mate.botIngresar.BackColor = Color.CadetBlue;
            mate.botBuscar.Enabled = true;
            mate.botBuscar.Visible = true;
            mate.botBuscar.Location = new Point(601, 956);
            mate.botBuscar.BackColor = Color.CadetBlue;
            mate.botModifica.Visible = true;
            mate.botModifica.Enabled = false;
            mate.botModifica.Location = new Point(779, 956);
            mate.botModifica.FlatStyle = FlatStyle.Flat;
            mate.botModifica.BackColor = Color.Transparent;
            mate.botEliminar.Visible = true;
            mate.botEliminar.Enabled = false;
            mate.botEliminar.Location = new Point(957, 956);
            mate.botEliminar.BackColor = Color.Transparent;
            mate.botEliminar.FlatStyle = FlatStyle.Flat;
            mate.botInforma.Enabled = true;
            mate.botInforma.Visible = true;
            mate.botInforma.Location = new Point(1135, 956);
            mate.botInforma.BackColor = Color.CadetBlue;
            mate.botImprimir.Visible = true;
            mate.botImprimir.Location = new Point(1313, 956);
            mate.botSalir.Visible = true;
            mate.botSalir.Location = new Point(1556, 950);

            mate.picboxFotoMat.Visible = false;
            mate.picboxRecuadroTitulodgv.Visible = true;
            mate.picboxRecuadroTitulodgv.Location = new Point(1433, 127);

            mate.dgvMateriales.Visible = true;
            mate.dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mate.dgvMateriales.Location = new Point(204, 190);
            mate.dgvMateriales.Size = new Size(1510, 745);
            mate.dgvMateriales.EnableHeadersVisualStyles = false; // Deshabilitar estilos visuales
            mate.dgvMateriales.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ButtonShadow;
            mate.dgvMateriales.DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
            mate.dgvMateriales.ClearSelection();
            mate.dgvMateriales.CurrentCell = null;
            mate.dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mate.dgvMateriales.MultiSelect = false;
            mate.dgvMateriales.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;


            this.Close();
        }

        private void AsociarEventosTextBox(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.Enter += TextBox_Enter;
                textBox.Leave += TextBox_Leave;
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            // Acciones a realizar cuando un TextBox obtiene el foco

            TextBox textBox = sender as TextBox;
            /*if (textBox.Name == "texEstado")
            {
                texEstado.Visible = false;
                radbotNuevo.Visible = true;
                radbotUsado.Visible = true;
                gruboxEstado.Visible = true;
            }
            else
            {*/
            textBox.BorderStyle = BorderStyle.Fixed3D;
            textBox.BackColor = Color.White;
            //}
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string texBoxName = textBox.Name;
            string columnName = texBoxName.Substring(3);
            contador = 0;

            if (texBoxName == "texValor")
            {
                int valorI = 0;
                if (ListaNombreColumnasAcopio.Count() > 0)
                {
                    for (int i = 0; i < ListaNombreColumnasAcopio.Count(); i++)
                    {
                        if (ListaNombreColumnasAcopio[i] == columnName)
                        {
                            contador = 1;
                            valorI = i;
                        }
                    }
                }
                if (contador == 1 && mate.dgvMateriales.CurrentRow.Cells[12].Value.ToString() != textBox.Text)
                {
                    ListaNuevoValorAcopio[valorI] = textBox.Text;
                }
                else
                {
                    if (mate.dgvMateriales.CurrentRow.Cells[12].Value.ToString() != textBox.Text)
                    {
                        botGuardar.Visible = true;
                        ListaNombreColumnasAcopio.Add(columnName);
                        ListaNuevoValorAcopio.Add(textBox.Text);
                    }
                }
            }
            else if (texBoxName == "texCantidad")
            {
                int valorI = 0;
                if (ListaNombreColumnasAcopio.Count() > 0)
                {
                    for (int i = 0; i < ListaNombreColumnasAcopio.Count(); i++)
                    {
                        if (ListaNombreColumnasAcopio[i] == columnName)
                        {
                            contador = 1;
                            valorI = i;
                        }
                    }
                }
                if (contador == 1 && mate.dgvMateriales.CurrentRow.Cells[9].Value.ToString() != textBox.Text)
                {
                    ListaNuevoValorAcopio[valorI] = textBox.Text;
                }
                else
                {
                    if (mate.dgvMateriales.CurrentRow.Cells[9].Value.ToString() != textBox.Text)
                    {
                        botGuardar.Visible = true;
                        ListaNombreColumnasAcopio.Add(columnName);
                        ListaNuevoValorAcopio.Add(textBox.Text);
                    }
                }
            }
            else if (texBoxName == "texTipo")
            {
                for (int i = 0; i < ListaNombreColumnasMats.Count(); i++)
                {
                    if (ListaNombreColumnasMats[i].ToString() == "Tipo")
                    {
                        ListaNuevoValorMats[i] = comboxTipoDeUnidades.SelectedItem.ToString();
                    }
                    else
                    {
                        ListaNombreColumnasMats.Add("Tipo");
                        ListaNuevoValorMats.Add(comboxTipoDeUnidades.SelectedItem.ToString());
                    }
                }
                botGuardar.Visible = true;
            }
            else
            {
                foreach (DataGridViewCell cell in mate.dgvMateriales.CurrentRow.Cells)
                {
                    int columnIndex = cell.ColumnIndex;
                    if (mate.dgvMateriales.Columns[columnIndex].Name == columnName)
                    {
                        if (mate.dgvMateriales.CurrentRow.Cells[columnIndex].Value.ToString() != textBox.Text)
                        {
                            if (columnName == "Ubicacion" || columnName == "Fabricante")
                            {
                                int valorI = 0;
                                if (ListaNombreColumnasAcopio.Count() > 0)
                                {
                                    for (int i = 0; i < ListaNombreColumnasAcopio.Count(); i++)
                                    {
                                        if (ListaNombreColumnasAcopio[i] == columnName)
                                        {
                                            contador = 1;
                                            valorI = i;
                                        }
                                    }
                                }
                                if (contador == 1)
                                {
                                    ListaNuevoValorAcopio[valorI] = textBox.Text;
                                }
                                else
                                {
                                    botGuardar.Visible = true;
                                    ListaNombreColumnasAcopio.Add(columnName);
                                    ListaNuevoValorAcopio.Add(textBox.Text);
                                }
                            }
                            else
                            {
                                int valorI = 0;
                                if (ListaNombreColumnasMats.Count() > 0)
                                {
                                    for (int i = 0; i < ListaNombreColumnasMats.Count(); i++)
                                    {
                                        if (ListaNombreColumnasMats[i] == columnName)
                                        {
                                            contador = 1;
                                            valorI = i;
                                        }
                                    }
                                }
                                if (contador == 1)
                                {
                                    ListaNuevoValorMats[valorI] = textBox.Text;
                                }
                                else
                                {
                                    botGuardar.Visible = true;
                                    ListaNombreColumnasMats.Add(columnName);
                                    ListaNuevoValorMats.Add(textBox.Text);
                                }
                            }
                        }
                    }
                }
            }
            textBox.BackColor = System.Drawing.Color.LightSteelBlue;
            textBox.BorderStyle = BorderStyle.None;
        }

        //
        private void NextFocusTextBoxes(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.KeyPress += TextBox_KeyPress;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            // Verifica si el carácter presionado no es una letra.
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                for (int i = 0; i < arrayTextBoxes.Length; i++)
                {
                    if (textBox.Name == arrayTextBoxes[i] && textBox.Name != "texValor")
                    {
                        string nextTextBoxName = arrayTextBoxes[i + 1];
                        TextBox textBoxToFOcus = Controls.Find(nextTextBoxName, true).FirstOrDefault() as TextBox;
                        textBoxToFOcus.Focus();
                    }
                    else if (textBox.Name == "texValor")
                    {
                        botFoto.Focus();
                        botGuardar.Visible = true;
                    }
                }
            }
        }

        private void botFoto_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog Dialogo = new OpenFileDialog();
                DialogResult resultado = Dialogo.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    mate.picboxFotoMat.Image = Image.FromStream(Dialogo.OpenFile());
                    MemoryStream memoria = new MemoryStream();
                    mate.picboxFotoMat.Image.Save(memoria, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageByte = memoria.ToArray();
                    mate.picboxFotoMat.Visible = true;
                    botFoto.Visible = false;
                }
                botGuardar.Visible = true;
                this.Location = new Point(263, 220);
            }
        }

        private void botGuardar_Click(object sender, EventArgs e)
        {
            string[] ListaNombreColumnasAcopioArray = ListaNombreColumnasAcopio.ToArray();
            string[] ListaNuevoValorAcopioArray = ListaNuevoValorAcopio.ToArray();
            string[] ListaNombreColumnasMatsArray = ListaNombreColumnasMats.ToArray();
            string[] ListaNuevoValorMatsArray = ListaNuevoValorMats.ToArray();
            string disponible = "";
            if (cantOriginal != texCantidad.Text)
            {
                disponible = (int.Parse(texCantidad.Text) - int.Parse(cantOriginal)).ToString();
            }

            // Comprobar que hay una fila seleccionada
            if (mate.dgvMateriales.CurrentRow == null)
            {
                MessageBox.Show("No hay ninguna fila seleccionada para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener IdMaterial de forma robusta: preferir columna por nombre si existe
            string idMaterial = string.Empty;
            try
            {
                var dgv = mate.dgvMateriales;
                if (dgv.Columns.Contains("IdMaterial"))
                {
                    idMaterial = dgv.CurrentRow.Cells["IdMaterial"].Value?.ToString() ?? string.Empty;
                }
                else
                {
                    // si no existe el nombre, probar índices comunes (13, 14, 12)
                    int[] posibles = new int[] { 13, 14, 12 };
                    foreach (int idx in posibles)
                    {
                        if (dgv.CurrentRow.Cells.Count > idx)
                        {
                            var v = dgv.CurrentRow.Cells[idx].Value;
                            if (v != null)
                            {
                                idMaterial = v.ToString();
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                idMaterial = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(idMaterial))
            {
                MessageBox.Show("No se pudo determinar el IdMaterial de la fila seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            funciones.ModificarMaterial(idMaterial, ListaNombreColumnasAcopioArray, ListaNuevoValorAcopioArray, ListaNombreColumnasMatsArray, ListaNuevoValorMatsArray, disponible, imageByte);

            // Determinar nombre de familia seguro para refrescar el grid
            string familiaParaRefrescar = null;
            try
            {
                if (mate.listFamiliaMat != null && mate.listFamiliaMat.SelectedItem != null)
                    familiaParaRefrescar = mate.listFamiliaMat.SelectedItem.ToString();
            }
            catch { }

            if (string.IsNullOrWhiteSpace(familiaParaRefrescar))
            {
                if (!string.IsNullOrWhiteSpace(texFamilia.Text)) familiaParaRefrescar = texFamilia.Text;
                else
                {
                    try { familiaParaRefrescar = mate.dgvMateriales.CurrentRow?.Cells[0]?.Value?.ToString(); } catch { familiaParaRefrescar = null; }
                }
            }

            if (!string.IsNullOrWhiteSpace(familiaParaRefrescar))
            {
                try { funciones.llenarDGVSeleccionStock(mate.dgvMateriales, familiaParaRefrescar); } catch { /* no interrumpir la UI si falla refresco */ }
            }

            mate.labTituloDgv.Visible = true;

            texFamilia.Visible = false;

            mate.botRegresa.Visible = true;

            mate.dgvMateriales.Visible = true;
            mate.dgvMateriales.Location = new Point(203, 190);

            mate.dgvMateriales.ClearSelection();
            mate.dgvMateriales.CurrentCell = null;
            mate.dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mate.dgvMateriales.MultiSelect = false;

            mate.picboxFotoMat.Visible = false;
            mate.picboxRecuadroTitulodgv.Visible = true;

            this.Close();
        }

        private void radbotNuevo_Click(object sender, EventArgs e)
        {
            texEstado.Text = "Nuevo"; // Asigna "N" al TextBox
            texEstado.Visible = true;
            gruboxEstados.Visible = false; // Oculta el GroupBox después de seleccionar una opción
            texUbicacion.Focus();
            if (texEstado.Text != mate.dgvMateriales.CurrentRow.Cells[7].Value.ToString())
            {
                botGuardar.Visible = true;
                ListaNombreColumnasMats.Add("Estado");
                ListaNuevoValorMats.Add("N");
            }
        }

        private void radbotUsado_Click(object sender, EventArgs e)
        {
            texEstado.Text = "Usado"; // Asigna "U" al TextBox
            texEstado.Visible = true;
            gruboxEstados.Visible = false; // Oculta el GroupBox después de seleccionar una opción
            texUbicacion.Focus();
            if (texEstado.Text != mate.dgvMateriales.CurrentRow.Cells[7].Value.ToString())
            {
                botGuardar.Visible = true;
                ListaNombreColumnasMats.Add("Estado");
                ListaNuevoValorMats.Add("U");
            }
        }

        private void texTipo_Click(object sender, EventArgs e)
        {
            comboxTipoDeUnidades.Visible = true;
            comboxTipoDeUnidades.DroppedDown = true;
            texTipo.Visible = false;
        }

        private void comboxTipoDeUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxTipoDeUnidades.SelectedIndex >= 0 && comboxTipoDeUnidades.SelectedItem.ToString() != "AGREGAR TIPO" && comboxTipoDeUnidades.SelectedItem.ToString() != texTipo.Text)
            {
                texTipo.Text = comboxTipoDeUnidades.SelectedItem.ToString();
                texTipo.Visible = true;
                comboxTipoDeUnidades.Visible = false;
            }
            else if (comboxTipoDeUnidades.SelectedIndex >= 0 && comboxTipoDeUnidades.SelectedItem.ToString() == "AGREGAR TIPO")
            {
                texTipo.Visible = true;
                texTipo.Text = "";
                texTipo.Focus();
                comboxTipoDeUnidades.Visible = false;
            }
        }

        private void texEstado_Click(object sender, EventArgs e)
        {
            gruboxEstados.Visible = true;
            gruboxEstados.Location = new Point(700, 270);
            texEstado.Visible = false;
        }

        private void frmModificaciones_Load(object sender, EventArgs e)
        {
            ListaNombreColumnasAcopio.Clear();
            ListaNombreColumnasMats.Clear();
            ListaNuevoValorAcopio.Clear();
            ListaNuevoValorMats.Clear();
        }
    }
}