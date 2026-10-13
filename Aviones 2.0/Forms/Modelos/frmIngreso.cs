using Aviones_2._0.Common;
using Aviones_2._0.Forms.Materiales;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Aviones_2._0.Forms.Modelos
{
    public partial class frmIngreso : Form
    {
        private Componentes componentesGenericos;

        private frmCondicion frmCondicion;
        private frmModelos frmModelos;
        private funcModelos funcModelos;
        public DataGridView dgvMaterialesSeleccionadosFinal { get; set; }

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

        private byte[] imageByte;
        private string nuevoOModificacion = "Nuevo";
        public frmIngreso(frmModelos principal)
        {
            InitializeComponent();

            componentesGenericos = new Componentes();
            dgvMaterialesSeleccionadosFinal = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMaterialesSeleccionadosFinal);
            dgvMaterialesSeleccionadosFinal.Location = new System.Drawing.Point(200, 900);

            frmModelos = principal;
            funcModelos = new funcModelos();
        }

        private void frmIngreso_Load(object sender, EventArgs e)
        {
            texNombre.Text = "";
            texAlias.Text = "";
        }

        private void texNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                texNombre.Focus();
                texNombre.BackColor = Color.BurlyWood;
                texNombre.ForeColor = Color.Black;
                texNombre.BorderStyle = BorderStyle.None;

                labNombre.ForeColor = Color.Gray;
                labNombre.Font = new Font(labNombre.Font.FontFamily, 9);
                labAlias.Visible = true;
                texAlias.Visible = true;
                texAlias.Focus();
                texAlias.BorderStyle = BorderStyle.FixedSingle;
                texAlias.ForeColor = Color.Black;
                // texAlias.BackColor = Color.Linen;
            }
        }

        private void texAlias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                OpenFileDialog Dialogo = new OpenFileDialog();
                DialogResult resultado = Dialogo.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    picboxFotoMod.Image = Image.FromStream(Dialogo.OpenFile());
                    MemoryStream memoria = new MemoryStream();
                    picboxFotoMod.Image.Save(memoria, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageByte = memoria.ToArray();
                    picboxFotoMod.Visible = true;
                }

                labAlias.ForeColor = Color.Gray;
                labAlias.Font = new Font(labNombre.Font.FontFamily, 9);

                texAlias.BackColor = Color.BurlyWood;
                texAlias.BorderStyle = BorderStyle.None;

                panCajaGrande.Width = 1200;
                panCajaGrande.Location = new Point(260, 171);

                labNombre.Visible = false;
                labAlias.Visible = false;
                labAlias.ForeColor = Color.Gray;
                labAlias.Font = new Font(labNombre.Font.FontFamily, 9);

                texAlias.BackColor = Color.BurlyWood;
                texAlias.BorderStyle = BorderStyle.None;

                botAgregaMaterial.Visible = true;
                botAgregaMaterial.Focus();

                if (resultado == DialogResult.OK)
                {
                    picboxFotoMod.Image = Image.FromStream(Dialogo.OpenFile());
                    MemoryStream memoria = new MemoryStream();
                    picboxFotoMod.Image.Save(memoria, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageByte = memoria.ToArray();
                    picboxFotoMod.Visible = true;
                }
            }
        }

        private void texAlias_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void AbrirMaterialporModelo()
        {
            string nombre = texNombre.Text;  // Obtén el texto de texNombre
            string alias = texAlias.Text;    // Obtén el texto de texAlias
            string nuevoOModificacion = "Nuevo";
            frmMaterialporModelo frmMaterial = new frmMaterialporModelo(this, nombre, alias, nuevoOModificacion, null, frmModelos);  // Pasa ambos parámetros
            frmMaterial.ShowDialog(); // Muestra el formulario
        }

        private void botAgregaMaterial_Click(object sender, EventArgs e)
        {
            string nombreModelo = texNombre.Text;
            string aliasModelo = texAlias.Text;
            string nuevoOModificacion = "Nuevo";

            frmMaterialporModelo frmMaterialporModelo = new frmMaterialporModelo(this, nombreModelo, aliasModelo, nuevoOModificacion, null, frmModelos);
            frmModelos.NuevoOModificacion = "Nuevo";
            frmMaterialporModelo.StartPosition = FormStartPosition.Manual;
            frmMaterialporModelo.Location = new Point(200, 120);

            frmModelos.botEliminar.Visible = false;

            frmMaterialporModelo.Show();
        }

        private void botSalir_Click(object sender, EventArgs e)
        {
            frmCondicion = new frmCondicion(frmModelos, this, null, nuevoOModificacion);

            if (frmCondicion != null && !frmCondicion.IsDisposed)
            {
                frmCondicion.Close();
                frmCondicion = null;
            }

            this.Close();
        }

        private void texCondicion_Click(object sender, EventArgs e)
        {
            frmCondicion = new frmCondicion(frmModelos, this, null, nuevoOModificacion);
            frmCondicion.Location = new Point(590, 790);
            botGuardar.Visible = false;
            botSalir.Visible = false;

            panCajaEstadoFecha.Visible = false;
            frmCondicion.ShowDialog();
        }

        private void texDesde_Click(object sender, EventArgs e)
        {
            texDesde.Visible = false;
            datDesde.Visible = true;
            datDesde.BringToFront();
            datDesde.Location = new Point(860, 705);
            botGuardar.Visible = false;
            botSalir.Visible = false;
        }

        private void datDesde_ValueChanged(object sender, EventArgs e)
        {
            texDesde.Text = datDesde.Value.ToString("dd/MM/yyyy");
            texDesde.Visible = true;
            texDesde.BorderStyle = BorderStyle.None;
            texDesde.BackColor = Color.BurlyWood;
            datDesde.Visible = false;
            botSalir.Visible = true;
            botGuardar.Visible = true;
        }

        private void botGuardar_Click(object sender, EventArgs e)
        {
            if (frmModelos.NuevoOModificacion == "Nuevo")
            {
                string[] acopiosArray = ListaMateriales.ToArray();
                string[] cantidadArray = CantidadDeMaterial.ToArray();
                string[] cantidadDisponibleActualizadaArray = CantidadDisponibleActualizada.ToArray();
                funcModelos.GuardarModelos(texNombre, texAlias, texCondicionIngreso, texDesde, imageByte, acopiosArray, cantidadArray, cantidadDisponibleActualizadaArray);
                funcModelos.mostrarModelos(frmModelos.dgvModelos);
            }
            this.Close();
        }

        private void picboxFotoMod_Click(object sender, EventArgs e)
        {
            botAgregaMaterial.Visible = true;
        }
    }
}