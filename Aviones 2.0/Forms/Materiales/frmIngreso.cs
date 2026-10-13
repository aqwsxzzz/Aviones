using Aviones_2._0.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Aviones_2._0.Forms.Materiales;
using System.Globalization;

namespace Aviones_2._0.Forms.Materiales
{
    public partial class frmIngreso : Form
    {
        private Componentes componentesGenericos;
        private frmMateriales frmMateriales;
        private funcMateriales funciones;
        private string[] arrayTextBoxes = new string[] { "texGrupo", "texCaracteristica", "texColor", "texMedidas", "texCodigo", "texFabricante", "gruboxEstado", "texUbicacion", "texCantidad", "texTipo", "texValor" };

        public DataGridView dgvMaterialesEnExistencia { get; set; }

        private byte[] imageByte;
        private bool esperandoConfirmacionEstado = false;

        public frmIngreso(frmMateriales principal)
        {
            InitializeComponent();
            frmMateriales = principal;
            componentesGenericos = new Componentes();
            funciones = new funcMateriales();

            dgvMaterialesEnExistencia = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMaterialesEnExistencia);

            dgvMaterialesEnExistencia.CellDoubleClick += new DataGridViewCellEventHandler(dgvMaterialesEnExistencia_CellDoubleClick);
            dgvMaterialesEnExistencia.CellClick += new DataGridViewCellEventHandler(dgvMaterialesEnExistencia_CellContentClick);

            this.Controls.Add(texFamilia);
            texFamilia.BorderStyle = BorderStyle.None;
            texFamilia.Location = new Point(255, 7);

            // Use default cursor to avoid the pointer disappearing on some systems
            this.Cursor = Cursors.Default;
            this.Location = new Point(270, 445);
            this.FormBorderStyle = FormBorderStyle.None;

            this.Shown += new EventHandler(frmIngreso_Shown);

            // Manejar teclas en radios de estado para confirmar con Enter
            try
            {
                radbotNuevo.KeyDown += RadbotEstado_KeyDown;
                radbotUsado.KeyDown += RadbotEstado_KeyDown;
            }
            catch { }

            // Asociar el mismo manejador de eventos a varios TextBox
            AsociarEventosTextBox(texGrupo, texMedidas, texValor, texCantidad, texCaracteristica, texTipo, texUbicacion, texFabricante, texCodigo, texEstado, texColor);

            texGrupo.KeyPress += TextBox_KeyPress;
            texCaracteristica.KeyPress += TextBox_KeyPress;
            texColor.KeyPress += TextBox_KeyPress;
            texMedidas.KeyPress += TextBox_KeyPress;
            texCodigo.KeyPress += TextBox_KeyPress;
            texEstado.KeyPress += TextBox_KeyPress;
            texFabricante.KeyPress += TextBox_KeyPress;
            // Asegurar comportamiento también al salir del campo Fabricante (por tab o click)
            texUbicacion.KeyPress += TextBox_KeyPress;
            texCantidad.KeyPress += TextBox_KeyPress;
            texTipo.KeyPress += TextBox_KeyPress;
            texValor.KeyPress += TextBox_KeyPress;

            texGrupo.Height = 33;
        }

        private void frmIngreso_Load(object sender, EventArgs e)
        {
            texFamilia.Visible = true;
            texFamilia.Text = frmMateriales.listFamiliaMat.SelectedItem.ToString();

            funciones.mostrarTipo(comboxTipoDeUnidades);
            comboxTipoDeUnidades.Items.Add("AGREGAR TIPO");
            // Mostrar cursor si la combobox se despliega
            comboxTipoDeUnidades.DropDown += comboxTipoDeUnidades_DropDown;
            funciones.MaterialesEnExistencia(dgvMaterialesEnExistencia, texFamilia.Text);
            // Estado no debe mostrarse hasta que el usuario confirme en Fabricante
            texEstado.Visible = false;
            texEstado.Text = "";
            texEstado.Location = new Point(793, 749);
            gruboxEstado.Visible = false;
            comboxTipoDeUnidades.Location = new Point(1160, 718);

            texCaracteristica.BackColor = Color.CadetBlue;
            texColor.BackColor = Color.CadetBlue;
            texMedidas.BackColor = Color.CadetBlue;
            texCodigo.BackColor = Color.CadetBlue;
            texEstado.BackColor = Color.CadetBlue;
            texFabricante.BackColor = Color.CadetBlue;
            texUbicacion.BackColor = Color.CadetBlue;
            texCantidad.BackColor = Color.CadetBlue;
            texTipo.BackColor = Color.CadetBlue;
            texValor.BackColor = Color.CadetBlue;

            dgvMaterialesEnExistencia.Size = new Size(1200, 544);
            dgvMaterialesEnExistencia.Columns["Familia"].Visible = false;
            dgvMaterialesEnExistencia.Columns[9].Visible = false;
            dgvMaterialesEnExistencia.Columns[10].Visible = false;
            dgvMaterialesEnExistencia.Columns[12].Visible = false;
            dgvMaterialesEnExistencia.Columns[13].Visible = false;
            dgvMaterialesEnExistencia.BackgroundColor = Color.LightSteelBlue;
            dgvMaterialesEnExistencia.Location = new Point(255, 40);
        }

        private void frmIngreso_LocationChanged(object sender, EventArgs e)
        {
            this.Location = new Point(103, 170);
        }

        private void botCancelar_Click(object sender, EventArgs e)
        {
            frmMateriales.labIngresoMat.Location = new Point(903, 280);
            frmMateriales.labIngresoMat.BackColor = Color.FromArgb(153, 180, 209);
            frmMateriales.labIngresoMat.Text = "Ingresos";
            frmMateriales.labFamiliaMat.Visible = false;

            texGrupo.Text = " ";
            texCaracteristica.Text = " ";
            texColor.Text = " ";
            texMedidas.Text = " ";
            texCodigo.Text = " ";
            texFabricante.Text = " ";
            texEstado.Text = " ";
            texUbicacion.Text = " ";
            texCantidad.Text = " ";
            texTipo.Text = " ";
            texValor.Text = " ";

            frmMateriales.botIngresarFam.Visible = false;
            frmMateriales.botIngresarFam.Visible = true;
            frmMateriales.botCancelarMat.Visible = true;
            frmMateriales.botCancelarMat.Visible = false;
            frmMateriales.botRegresa.Visible = true;
            frmMateriales.botFamilias.Visible = true;
            frmMateriales.botIngresarFam.Visible = false;
            frmMateriales.botRegistros.Visible = true;

            frmMateriales.picboxFotoMat.Visible = false;
            frmMateriales.picboxRecuaTituloMat.Visible = false;
            frmMateriales.picboxRecuadroGrande.Visible = true;
            frmMateriales.picboxRecuadroGrande.Location = new Point(725, 420);
            frmMateriales.picboxRecuadroGrande.Height = 245;
            frmMateriales.picboxRecuadroGrande.BackColor = Color.CornflowerBlue;
            frmMateriales.picboxRecuadroGrande.BorderStyle = BorderStyle.Fixed3D;

            frmMateriales.listFamiliaMat.Visible = false;

            frmMateriales.panFamiliasUnidades.Visible = true;

            frmMateriales.picboxRecuadroGrande.Visible = false;
            comboxTipoDeUnidades.Visible = false;

            this.Close();
        }

        private void botCargarFoto_Click(object sender, EventArgs e)
        {
            {
                dgvMaterialesEnExistencia.Columns[7].Width = 25;
                frmMateriales.picboxFotoMat.Location = new Point(1560, 50);
                OpenFileDialog Dialogo = new OpenFileDialog();
                DialogResult resultado = Dialogo.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    frmMateriales.picboxFotoMat.Image = Image.FromStream(Dialogo.OpenFile());
                    MemoryStream memoria = new MemoryStream();
                    frmMateriales.picboxFotoMat.Image.Save(memoria, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageByte = memoria.ToArray();
                    frmMateriales.picboxFotoMat.Visible = true;
                    frmMateriales.picboxRecuadroGrande.Visible = false;
                }
                botAceptar.Focus();
                this.Location = new Point(360, 400);
            }
        }

        private void botAceptar_Click(object sender, EventArgs e)
        {
            if (texEstado.Text == "" || texValor.Text == "" || texCantidad.Text == "")
            {
                MessageBox.Show("Estado, Valor y Cantidad son obgligatorios.");
            }
            else
            {
                funciones.guardarMateriales(texFamilia, texGrupo, texCaracteristica, texMedidas, texColor,
                texCodigo, texTipo, texUbicacion, texFabricante, texCantidad, texValor, texEstado, imageByte);
                funciones.mostrarMateriales(frmMateriales.dgvMateriales);

                frmMateriales.labTituloPrincipal.Location = new Point(855, 85);

                frmMateriales.botCancelarMat.Visible = true;

                frmMateriales.picboxFotoMat.Visible = false;
                frmMateriales.picboxRecuaTituloMat.Visible = false;

                frmMateriales.picboxRecuaMat.Visible = true;

                frmMateriales.listFamiliaMat.Visible = true;
                frmMateriales.listFamiliaMat.Focus();

                texFamilia.Visible = true;
                texFamilia.Location = new Point(255, 17);

                texGrupo.Text = "";
                texCaracteristica.Text = "";
                texMedidas.Text = "";
                texColor.Text = "";
                texCodigo.Text = "";
                texTipo.Text = "";
                texUbicacion.Text = "";
                texFabricante.Text = "";
                texCantidad.Text = "";
                texValor.Text = "";
                texEstado.Visible = false;

                gruboxEstado.Visible = true;
                radbotNuevo.Checked = false;
                radbotUsado.Checked = false;
                comboxTipoDeUnidades.Visible = false;

                this.Close();
            }
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
            textBox.BackColor = Color.White;
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            // Acciones a realizar cuando un TextBox pierde el foco
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "texValor" && texValor.Text != "" && texEstado.Text != "" && texCantidad.Text != "")
            {
                botAceptar.Visible = true;
            }
            textBox.BackColor = System.Drawing.Color.CadetBlue;
        }

        private void dgvMaterialesEnExistencia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string texBoxSinCorregir = "tex" + dgvMaterialesEnExistencia.Columns[e.ColumnIndex].Name;
            string texBoxAEncontrar = texBoxSinCorregir.Replace(" ", "");
            string dato = dgvMaterialesEnExistencia.CurrentCell.Value.ToString();
            TextBox foundTextBox = Controls.Find(texBoxAEncontrar, true).FirstOrDefault() as TextBox;

            if (foundTextBox != null)
                foundTextBox.Text = dato;

            string[] texBoxNames = { "texCaracteristica", "texMedidas", "texCodigo", "texFabricante", "texEstado", "texUbicacion", "texCantidad", "texValor" };
            int column = e.ColumnIndex;
            string textToFocus = dgvMaterialesEnExistencia.Columns[column].Name;
            TextBox textFocus = Controls.Find(textToFocus, true).FirstOrDefault() as TextBox;
            foundTextBox.Focus();
        }

        private void dgvMaterialesEnExistencia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string texBoxSinCorregir = "tex" + dgvMaterialesEnExistencia.Columns[e.ColumnIndex].Name;
                string texBoxAEncontrar = texBoxSinCorregir.Replace(" ", "");
                string dato = dgvMaterialesEnExistencia.CurrentCell.Value.ToString();
                TextBox foundTextBox = Controls.Find(texBoxAEncontrar, true).FirstOrDefault() as TextBox;

                if (foundTextBox != null)
                    foundTextBox.Text = dato;

                string[] texBoxNames = { "texCaracteristica", "texMedidas", "texCodigo", "texFabricante", "texEstado", "texUbicacion", "texCantidad", "texValor" };
                int column = e.ColumnIndex;
                string textToFocus = dgvMaterialesEnExistencia.Columns[column].Name;
                TextBox textFocus = Controls.Find(textToFocus, true).FirstOrDefault() as TextBox;
                foundTextBox.Focus();
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                for (int i = 0; i < arrayTextBoxes.Length; i++)
                {
                    if (textBox.Name == arrayTextBoxes[i] && textBox.Name != "texValor" && textBox.Name != "texFabricante" && textBox.Name != "texCantidad")
                    {
                        // If the current box is texTipo and the user entered a new type, add it to the combo box
                        if (textBox.Name == "texTipo")
                        {
                            string nuevoTipo = texTipo.Text?.Trim();
                            if (!string.IsNullOrEmpty(nuevoTipo))
                            {
                                // ensure combo contains the new type (insert before AGREGAR TIPO entry)
                                int idxAgregar = comboxTipoDeUnidades.Items.IndexOf("AGREGAR TIPO");
                                bool existe = comboxTipoDeUnidades.Items.Cast<object>().Any(x => string.Equals(x.ToString(), nuevoTipo, StringComparison.OrdinalIgnoreCase));
                                if (!existe)
                                {
                                    if (idxAgregar >= 0)
                                        comboxTipoDeUnidades.Items.Insert(idxAgregar, nuevoTipo);
                                    else
                                        comboxTipoDeUnidades.Items.Add(nuevoTipo);
                                }
                            }

                            // Move focus to next logical field
                            string nextTextBoxName = arrayTextBoxes[i + 1];
                            TextBox textBoxToFOcus = Controls.Find(nextTextBoxName, true).FirstOrDefault() as TextBox;
                            textBoxToFOcus?.Focus();
                        }
                        else
                        {
                            string nextTextBoxName = arrayTextBoxes[i + 1];
                            TextBox textBoxToFOcus = Controls.Find(nextTextBoxName, true).FirstOrDefault() as TextBox;
                            textBoxToFOcus.Focus();

                        }
                    }
                    else if (textBox.Name == "texValor")
                    {
                        botCargarFoto.Focus();
                    }
                    else if (textBox.Name == "texFabricante")
                    {
                        texEstado.Visible = false;

                        // Mostrar controles
                        radbotNuevo.Visible = true;
                        radbotUsado.Visible = true;

                        gruboxEstado.Visible = true;
                        gruboxEstado.Enabled = true;
                        gruboxEstado.BringToFront();

                        radbotNuevo.Enabled = true;
                        radbotUsado.Enabled = true;

                        // Dejar Nuevo seleccionado por defecto SIN disparar click
                        radbotNuevo.Checked = true;

                        // Diferir el foco para evitar que el Enter active el radio
                        BeginInvoke(new Action(() =>
                        {
                            radbotNuevo.Focus();
                        }));

                        e.Handled = true;
                    }
                    else if (textBox.Name == "texCantidad")
                    {
                        texTipo.Visible = false;
                        // Asegurar que el cursor del mouse esté visible antes de mostrar controles
                        try { Cursor.Show(); this.Cursor = Cursors.Default; } catch { }
                        // Si no hay tipos definidos, abrir directamente el textbox para agregar uno
                        if (comboxTipoDeUnidades.Items.Count == 1 && comboxTipoDeUnidades.Items[0].ToString() == "AGREGAR TIPO")
                        {
                            comboxTipoDeUnidades.Visible = false;
                            texTipo.Visible = true;
                            texTipo.Text = string.Empty;
                            texTipo.Focus();
                        }
                        else
                        {
                            comboxTipoDeUnidades.Visible = true;
                            comboxTipoDeUnidades.DroppedDown = true;
                            comboxTipoDeUnidades.Font = new Font(comboxTipoDeUnidades.Font.FontFamily, 10, FontStyle.Bold);
                        }
                    }
                // Evitar el pitido del sistema al procesar Enter
                e.Handled = true;
                }
            }
        }

        private void radbotNuevo_Click(object sender, EventArgs e)
        {
            texEstado.Text = "Nuevo";
            texEstado.Visible = true;
            gruboxEstado.Visible = false;
            texEstado.Location = new Point(795, 750);
            texEstado.BorderStyle = BorderStyle.None;
            texEstado.BringToFront();
            texEstado.Width = 100;
            texUbicacion.Focus();
        }

        private void radbotUsado_Click(object sender, EventArgs e)
        {
            texEstado.Text = "Usado";
            texEstado.Visible = true;
            gruboxEstado.Visible = false;
            texEstado.Location = new Point(795, 750);
            texEstado.BringToFront();
            texEstado.Width = 100;
            texUbicacion.Focus();
        }

        private void frmIngreso_Shown(object sender, EventArgs e)
        {
            dgvMaterialesEnExistencia.ClearSelection();
            dgvMaterialesEnExistencia.CurrentCell = null;
            dgvMaterialesEnExistencia.EnableHeadersVisualStyles = false; // Deshabilitar estilos visuales
            dgvMaterialesEnExistencia.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ButtonShadow;
            // Asegurar cursor visible al mostrarse el formulario
            try { Cursor.Show(); this.Cursor = Cursors.Default; Application.DoEvents(); } catch { }
        }

        private void texEstado_Click(object sender, EventArgs e)
        {
            gruboxEstado.Visible = true;
            radbotNuevo.Visible = true;
            radbotUsado.Visible = true;
            texEstado.Visible = false;
        }

        private void texTipo_Click(object sender, EventArgs e)
        {
            // Asegurar cursor visible
            try { Cursor.Show(); this.Cursor = Cursors.Default; Application.DoEvents(); } catch { }
            comboxTipoDeUnidades.Visible = true;
            comboxTipoDeUnidades.DroppedDown = true;
            texTipo.Visible = false;
        }

        private void comboxTipoDeUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxTipoDeUnidades.SelectedIndex >= 0 && comboxTipoDeUnidades.SelectedItem.ToString() != "AGREGAR TIPO")
            {
                texTipo.Text = comboxTipoDeUnidades.SelectedItem.ToString();
                texTipo.Visible = true;
                comboxTipoDeUnidades.Visible = false;

                texValor.Focus();
                try { Cursor.Show(); this.Cursor = Cursors.Default; } catch { }
                try { Application.DoEvents(); } catch { }
            }
            else if (comboxTipoDeUnidades.SelectedIndex >= 0 && comboxTipoDeUnidades.SelectedItem.ToString() == "AGREGAR TIPO")
            {
                texTipo.Visible = true;
                texTipo.Text = "";
                texTipo.Focus();
                comboxTipoDeUnidades.Visible = false;
                try { Cursor.Show(); this.Cursor = Cursors.Default; } catch { }
                try { Application.DoEvents(); } catch { }
            }
            Cursor.Show();
        }

        private void comboxTipoDeUnidades_DropDown(object sender, EventArgs e)
        {
            try { Cursor.Show(); this.Cursor = Cursors.Default; Application.DoEvents(); } catch { }
        }

        private void RadbotEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (!esperandoConfirmacionEstado) return;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    texEstado.Text = radbotNuevo.Checked ? "Nuevo" : "Usado";
                    texEstado.Visible = true;
                    gruboxEstado.Visible = false;
                    //texUbicacion.Focus();
                }
                catch { }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}