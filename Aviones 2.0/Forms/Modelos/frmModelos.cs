using Aviones_2._0.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aviones_2._0.Forms.Modelos
{
    public partial class frmModelos : Form
    {
        public Componentes componentesGenericos;
        public DataGridView dgvMateriales { get; set; }
        public DataGridView dgvModelos { get; set; }
        public frmModificacionMod frmModificacionMod;
        public string NuevoOModificacion = "";

        public frmIngreso frmIngreso;

        private List<string> ColumnasDatosAModificar = new List<string>();
        private List<string> DatosAModificar = new List<string>();

        //Id de modelo en modificacion
        public string IdModeloEnModificacion = "";

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

        private int lastRowIndex = -1;
        private funcModelos funciones;

        public frmModelos()
        {
            InitializeComponent();

            componentesGenericos = new Componentes();

            this.FormBorderStyle = FormBorderStyle.None;

            dgvModelos = Componentes.CrearDgvModelos();
            


            dgvModelos.Location = new Point(658, 320);
            dgvModelos.Size = new Size(600, 350);
            dgvModelos.BackgroundColor = System.Drawing.Color.Bisque;
            // Asegurar color de fondo de celdas consistente
            dgvModelos.DefaultCellStyle.BackColor = dgvModelos.BackgroundColor;
            dgvModelos.EnableHeadersVisualStyles = false;
            dgvModelos.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            // Hacer que cuando el encabezado quede seleccionado use mismo fondo que las celdas
            dgvModelos.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvModelos.DefaultCellStyle.BackColor;
            dgvModelos.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgvModelos.ColumnHeadersDefaultCellStyle.ForeColor;
            // Selección de filas
            dgvModelos.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dgvModelos.DefaultCellStyle.SelectionForeColor = Color.White;

            funciones = new funcModelos();
            this.Controls.Add(dgvModelos);
            funciones.mostrarModelos(dgvModelos);

            dgvModelos.Columns[4].Visible = false;
           // dgvModelos.BackgroundColor = System.Drawing.Color.SeaShell;
            dgvModelos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvModelos.BringToFront();

            botIngresar.Location = new Point(612, 955);
            botModificar.Location = new Point(791, 955);
            botEliminar.Location = new Point(970, 955);
            botListados.Location = new Point(1149, 955);
            Salir.Location = new Point(1557, 955);


            dgvModelos.CellClick += new DataGridViewCellEventHandler(dgvModelos_CellClick);
            dgvModelos.CellMouseEnter += new DataGridViewCellEventHandler(dgvModelos_CellMouseEnter);
        }

        private void botIngresar_Click(object sender, EventArgs e)
        {
            NuevoOModificacion = "Nuevo";
            frmIngreso = new frmIngreso(this);
            frmIngreso.StartPosition = FormStartPosition.Manual;
            frmIngreso.Location = new Point(100, 130);
            frmIngreso.Show();
        }

        private void dgvModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener IdModelo de la fila clicada de forma segura usando e.RowIndex
                var fila = dgvModelos.Rows[e.RowIndex];
                string IdModelo = null;
                if (fila.Cells.Count > 4 && fila.Cells[4].Value != null)
                {
                    IdModelo = fila.Cells[4].Value.ToString();
                }

                // Intentar establecer una celda válida como CurrentCell para preservar apariencia de selección (azul)
                try
                {
                    DataGridViewCell firstVisibleCell = null;
                    for (int i = 0; i < fila.Cells.Count; i++)
                    {
                        var c = fila.Cells[i];
                        if (c.Visible)
                        {
                            firstVisibleCell = c;
                            break;
                        }
                    }
                    if (firstVisibleCell != null)
                    {
                        dgvModelos.CurrentCell = firstVisibleCell;
                    }
                }
                catch
                {
                    // Ignorar errores del control al cambiar CurrentCell
                }

                // botIngresar.Enabled = false;
                botIngresar.ForeColor = Color.Peru;
                botIngresar.BackColor = Color.White;

                botModificar.Enabled = true;
                botModificar.BackColor = Color.BurlyWood;
                botModificar.ForeColor = Color.Black;

                botEliminar.Enabled = true;
                botEliminar.BackColor = Color.BurlyWood;

                botListados.ForeColor = Color.Peru;
                botListados.BackColor = Color.White;

                botCancelar.Visible = true;
                botCancelar.Location = new Point(1558, 955);
            }
        }

        private void dgvModelos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // asegurarse de no estar en el header
            {
                // Obtener el valor de la columna con la ruta o nombre de la imagen de forma segura
                var fila = dgvModelos.Rows[e.RowIndex];
                if (fila.Cells.Count > 4 && fila.Cells[4].Value != null)
                {
                    funciones.BuscarFotoPorIdModelo(fila.Cells[4].Value.ToString(), pictureBoxMod);
                }
                else
                {
                    // Si no hay id, limpiar o no actualizar la imagen
                    pictureBoxMod.Image = null;
                }

            }
        }

        private void botModificar_Click(object sender, EventArgs e)
        {
            if (dgvModelos.CurrentRow != null)
            {
                NuevoOModificacion = "Modificacion";
                string nombreModelo = Convert.ToString(dgvModelos.CurrentRow.Cells["Modelo"].Value);
                string nombreAlias = Convert.ToString(dgvModelos.CurrentRow.Cells["Alias"].Value);

                frmModificacionMod = new frmModificacionMod(this, this.frmIngreso, nombreModelo, nombreAlias);

                frmModificacionMod.ShowDialog();
            }
        }

        private void Salir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                botIngresar.Enabled = true;
                botIngresar.BackColor = Color.BurlyWood;
                botIngresar.ForeColor = Color.Black;

                botModificar.Enabled = false;
                botModificar.BackColor = Color.White;
                botModificar.ForeColor = Color.Peru;

                botEliminar.Enabled = false;
                botEliminar.BackColor = Color.White;
                botEliminar.ForeColor = Color.Peru;

                botListados.Enabled = false;
                botListados.BackColor = Color.BurlyWood;
                botListados.ForeColor = Color.Peru;

                botCancelar.Visible = false;
            }
        }

        private void frmModelos_Shown(object sender, EventArgs e)
        {
            if (dgvModelos != null)
            {
                // Para evitar que se entre en modo edición (y aparezca el azul por defecto)
                dgvModelos.ReadOnly = true;
                // o: dgvModelos.EditMode = DataGridViewEditMode.EditProgrammatically;

                // Selección de fila completa, sin multiselección
                dgvModelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvModelos.MultiSelect = false;

                // Colores de la fila seleccionada
                dgvModelos.DefaultCellStyle.SelectionBackColor = Color.Tan;   // Fondo de la fila seleccionada
                dgvModelos.DefaultCellStyle.SelectionForeColor = Color.Black; // Texto de la fila seleccionada

                // (Opcional) Color de fondo normal de todas las filas
                dgvModelos.DefaultCellStyle.BackColor = Color.White;

                // Desactiva el estilo visual de encabezados si quieres un color personalizado allí
                dgvModelos.EnableHeadersVisualStyles = false;
                dgvModelos.ColumnHeadersDefaultCellStyle.BackColor = Color.Peru;
                dgvModelos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvModelos.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Peru;
                dgvModelos.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

                // Limpia cualquier selección previa
                dgvModelos.ClearSelection();
                dgvModelos.CurrentCell = null;

                // ¡Listo! Sin más eventos extra
                dgvModelos.Refresh();
            }
        }


    }
}