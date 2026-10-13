using Aviones_2._0.Common;
using Aviones_2._0.Forms.Materiales;
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

namespace Aviones_2._0.Forms.Modelos
{
    public partial class frmModificacionMod : Form
    {
        private Componentes componentesGenericos;

        public DataGridView dgvMateriales { get; set; }
        public DataGridView dgvModelos { get; set; }

        private frmModelos frmModelos;
        private funcModelos funcModelos;
        private frmCondicion frmCondicion;

        private byte[] imageByte;
            string nombreModeloMod;
            string nombreAliasMod; 
        private frmIngreso frmIngreso;
        private string nuevoOModificacion = "Modificacion";

        private List<string> ColumnasDatosAModificar = new List<string>();
        private List<string> DatosAModificar = new List<string>();

        //Nuevo Modelo
        public List<string> ListaMateriales = new List<string>();

        public List<string> CantidadDeMaterial = new List<string>();
        public List<string> CantidadDisponibleActualizada = new List<string>();

        //Modificacion de cantidad de material ya usado en un modelo.
        public List<string> NuevaCantidadxModelo = new List<string>();

        public List<string> texCantidadVieja = new List<string>();
        public List<string> IdMaterial = new List<string>();

        //Eliminacion de material reciclable
        public List<string> CantidadUsadaDeMaterialMod = new List<string>();

        public List<string> IdMaterialReciclableAEliminarMod = new List<string>();
        public List<string> IdAcopioDeMaterialReciblableAEliminarMod = new List<string>();

        //Eliminacion de material no reciclable
        public List<string> IdMaterialNoReciclableAEliminarMod = new List<string>();


        public frmModificacionMod(frmModelos frmModelosParent, frmIngreso frmIngresoProp, string nombreModelo, string nombreAlias)
        {
            InitializeComponent();
            nombreModeloMod = nombreModelo;
            nombreAliasMod = nombreAlias;
            frmIngreso = frmIngresoProp;

            frmModelos = frmModelosParent;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(180, 120);

            componentesGenericos = new Componentes();
            dgvMateriales = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMateriales);
            dgvMateriales.Columns["Disponible"].HeaderText = "Seleccinada";
            dgvMateriales.Columns["Disponible"].DataPropertyName = "CantidadxModelo";
            dgvMateriales.Columns["Disponible"].Name = "CantidadSeleccionada";

            dgvModelos = Componentes.CrearDgvModelos();
            this.Controls.Add(dgvModelos);
            dgvModelos.Visible = false;

            funcModelos = new funcModelos();

            frmCondicion = new frmCondicion(frmModelos, frmIngreso, this, nuevoOModificacion);

            funcModelos.BuscarFotoPorIdModelo(frmModelos.dgvModelos.CurrentRow.Cells[4].Value.ToString(), picFotoModi);
        }

        public void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvMateriales.Rows)
            {
                if (row.Cells["Valor"].Value != null && row.Cells["CantidadSeleccionada"].Value != null)
                {
                    decimal valorUnitario;
                    decimal cantidadSeleccionada;

                    bool esValidoValorUnitario = decimal.TryParse(row.Cells["Valor"].Value.ToString(), out valorUnitario);
                    bool esValidoCantidad = decimal.TryParse(row.Cells["CantidadSeleccionada"].Value.ToString(), out cantidadSeleccionada);

                    if (esValidoValorUnitario && esValidoCantidad)
                    {
                        total += valorUnitario * cantidadSeleccionada;
                    }
                }
            }

            texValorTotal.Text = total.ToString("N2"); // Formatea con dos decimales
            texValorTotal.Visible = true;
            texValorTotal.BorderStyle = BorderStyle.None;
        }

        private void frmModificacion_Load(object sender, EventArgs e)
        {
            texModelo.Text = frmModelos.dgvModelos.CurrentRow.Cells[0].Value.ToString();
            texAlias.Text = frmModelos.dgvModelos.CurrentRow.Cells[1].Value.ToString();
            texEstado.Text = frmModelos.dgvModelos.CurrentRow.Cells[2].Value.ToString();
            texDesde.Text = frmModelos.dgvModelos.CurrentRow.Cells[3].Value.ToString();

            dgvMateriales.Visible = true;
            dgvMateriales.Location = new Point(43, 280);
            dgvMateriales.Size = new Size(1200, 340);
            dgvMateriales.Columns[7].Visible = false;
            dgvMateriales.Columns[8].Visible = false;
            dgvMateriales.Columns[9].Visible = false;
            dgvMateriales.Columns[13].Visible = false;
            dgvMateriales.EnableHeadersVisualStyles = false; //habilita el cambio de color de los títulos
            dgvMateriales.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 192, 128);
            dgvMateriales.BackgroundColor = System.Drawing.Color.Bisque;
            dgvMateriales.BringToFront();

            // Llamar a la función para mostrar materiales del modelo
            string idModelo = frmModelos.dgvModelos.CurrentRow.Cells[4].Value.ToString();
            if(dgvMateriales.Rows.Count == 0)
            {
            funcModelos.mostrarMaterialesxModelo(dgvMateriales, idModelo, texEstado);
            }
            this.BeginInvoke(new Action(() => botCancelar.Focus()));
        }

        private void botCancelar_Click(object sender, EventArgs e)
        {
            frmModelos.botIngresar.Enabled = true;
            frmModelos.botIngresar.ForeColor = Color.Black;
            frmModelos.botIngresar.BackColor = Color.Peru;

            frmModelos.botModificar.Enabled = false;
            frmModelos.botModificar.ForeColor = Color.SandyBrown;
            frmModelos.botModificar.BackColor = Color.SeaShell;

            frmModelos.botEliminar.Enabled = false;
            frmModelos.botEliminar.ForeColor = Color.SandyBrown;
            frmModelos.botEliminar.BackColor = Color.SeaShell;

            frmModelos.botListados.Enabled = true;
            frmModelos.botListados.ForeColor = Color.Black;
            frmModelos.botListados.BackColor = Color.Peru;

            frmModelos.dgvModelos.ClearSelection();

            this.Close();
        }

        private void botModificaMaterial_Click(object sender, EventArgs e)
        {
            string nuevoOModificacion = "Modificacion";
            frmMaterialporModelo frmMaterialporModelo = new frmMaterialporModelo(frmIngreso, nombreModeloMod, nombreAliasMod, nuevoOModificacion, this, frmModelos);

            frmMaterialporModelo.Location = new Point(190, 120);
            frmMaterialporModelo.Show();
            foreach (DataGridViewRow row in dgvMateriales.Rows)
            {
                if (!row.IsNewRow) // Evita copiar la fila vacía de edición
                {
                    int rowIndex = frmMaterialporModelo.dgvMaterialesSeleccionadosLocal.Rows.Add();
                    for (int columnIndex = 0; columnIndex < dgvMateriales.Columns.Count; columnIndex++)
                    {
                        if (columnIndex < frmMaterialporModelo.dgvMaterialesSeleccionadosLocal.Columns.Count) // Evita error de índice
                        {
                            frmMaterialporModelo.dgvMaterialesSeleccionadosLocal.Rows[rowIndex].Cells[columnIndex].Value = row.Cells[columnIndex].Value;
                        }
                    }
                }
            }
            frmMaterialporModelo.dgvMaterialesSeleccionadosLocal.Visible = true;
            frmMaterialporModelo.dgvMaterialesSeleccionadosLocal.Location = new Point(210, 510);
            frmMaterialporModelo.botBorrarMaterialSel.Visible = true;
        }

        private void botModificaFoto_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void botModificaFoto_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog Dialogo = new OpenFileDialog())
            {
                // Solo permite seleccionar imágenes
                Dialogo.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                Dialogo.Title = "Seleccionar una imagen";

                if (Dialogo.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Cargar imagen sin bloquear el archivo
                        byte[] imageData = File.ReadAllBytes(Dialogo.FileName);
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            picFotoModi.Image = Image.FromStream(ms);
                        }

                        // Guardar la imagen en un array de bytes para su uso posterior
                        imageByte = imageData;

                        // Asegurar que el PictureBox sea visible
                        picFotoModi.Visible = true;
                        botConfirmar.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void botModificaFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog Dialogo = new OpenFileDialog())
            {
                Dialogo.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                Dialogo.Title = "Seleccionar una imagen";

                if (Dialogo.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Hacer una copia de la imagen para evitar bloqueos
                        using (Image imagenOriginal = Image.FromFile(Dialogo.FileName))
                        {
                            picFotoModi.Image = new Bitmap(imagenOriginal); // Copia de la imagen
                        }

                        // Guardar la imagen en un array de bytes
                        using (MemoryStream memoria = new MemoryStream())
                        {
                            picFotoModi.Image.Save(memoria, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imageByte = memoria.ToArray();
                        }

                        picFotoModi.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void texEstado_Click(object sender, EventArgs e)
        {
            frmCondicion.Location = new Point(490, 755);
            panValorTotal.Visible = false;
            texValorTotal.Visible = false;
            labValorTotal.Visible = false;

            frmCondicion.ShowDialog();
        }

        private void texDesde_Click(object sender, EventArgs e)
        {
            datDesdeMod.Visible = true;
            texDesde.Visible = false;
            datDesdeMod.Location = new Point(650, 700);
            datDesdeMod.Format = DateTimePickerFormat.Custom;
            datDesdeMod.CustomFormat = "dd MMMM yyyy";
        }

        private void datDesdeMod_ValueChanged(object sender, EventArgs e)
        {
            texDesde.Text = datDesdeMod.Value.ToString("dd/MM/yyyy");
            texDesde.Visible = true;
            texDesde.BorderStyle = BorderStyle.None;
            texDesde.BackColor = Color.BurlyWood;
            datDesdeMod.Visible = false;
        }

        private void frmModificacionMod_Shown(object sender, EventArgs e)
        {
            if (dgvMateriales != null)
            {
                // Para evitar que se entre en modo edición (y aparezca el azul por defecto)
                dgvMateriales.ReadOnly = true;
                // o: dgvModelos.EditMode = DataGridViewEditMode.EditProgrammatically;

                // Selección de fila completa, sin multiselección
                dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMateriales.MultiSelect = false;

                // Deshabilita la interacción con el DGV
                dgvMateriales.Enabled = false;
                dgvMateriales.TabStop = false;

                // Colores de la fila seleccionada
                dgvMateriales.DefaultCellStyle.SelectionBackColor = Color.Tan;   // Fondo de la fila seleccionada
                dgvMateriales.DefaultCellStyle.SelectionForeColor = Color.Black; // Texto de la fila seleccionada

                // (Opcional) Color de fondo normal de todas las filas
                dgvMateriales.DefaultCellStyle.BackColor = Color.White;

                // Desactiva el estilo visual de encabezados si quieres un color personalizado allí
                dgvMateriales.EnableHeadersVisualStyles = false;
                dgvMateriales.ColumnHeadersDefaultCellStyle.BackColor = Color.Peru;
                dgvMateriales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvMateriales.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Peru;
                dgvMateriales.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

                // Limpia cualquier selección previa
                dgvMateriales.ClearSelection();
                dgvMateriales.CurrentCell = null;

                // Bloquea eventos de clic y teclado
                dgvMateriales.CellEnter += (s, ev) => dgvMateriales.ClearSelection();
                dgvMateriales.SelectionChanged += (s, ev) => dgvMateriales.ClearSelection();

                // ¡Listo! Sin más eventos extra
                dgvMateriales.Refresh();

                CalcularTotal();
            }
        }

        private void botConfirmar_Click(object sender, EventArgs e)
        {
            string[] Columnas = ColumnasDatosAModificar.ToArray();
            string[] Datos = DatosAModificar.ToArray();
            Modelos.funcModelos funcModelos = new Modelos.funcModelos();
            funcModelos.ModificarModelo(Columnas, Datos, frmModelos.dgvModelos.CurrentRow.Cells[4].Value.ToString(), imageByte);
            if (ListaMateriales.Count > 0)
            {
                funcModelos.GuardarMaterialesXModeloEnModificacion(ListaMateriales, CantidadDeMaterial, CantidadDisponibleActualizada, frmModelos.dgvModelos.CurrentRow.Cells[4].Value.ToString());
            }

            if (IdMaterialReciclableAEliminarMod.Count > 0)
            {
                for (int i = 0; i < IdMaterialReciclableAEliminarMod.Count; i++)
                {
                    funcModelos.DevolverMaterialReciclable(IdMaterialReciclableAEliminarMod[i], CantidadUsadaDeMaterialMod[i], IdAcopioDeMaterialReciblableAEliminarMod[i]);
                }
            }
            if (IdMaterialNoReciclableAEliminarMod.Count > 0)
            {
                for (int i = 0; i < IdMaterialNoReciclableAEliminarMod.Count; i++)
                {
                    string Cantidad = "0";
                    string IdAcopio = "No Reciclable";
                    funcModelos.DevolverMaterialReciclable(IdMaterialNoReciclableAEliminarMod[i], Cantidad, IdAcopio);
                }
            }
            this.Close();
        }
    }
}