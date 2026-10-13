using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aviones_2._0.Forms.Modelos
{
    public partial class frmCondicion : Form
    {
        private string Dato;
        private frmModelos frmModelos;
        private frmIngreso frmIngreso;
        private frmModificacionMod frmModificacionMod;
        private bool _isInitializing = true; // Variable para ignorar eventos durante la inicialización

        string nuevoOModificacion;
        public frmCondicion(frmModelos principal, frmIngreso ingreso, frmModificacionMod modificacion, string nuevoOModificacion)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmCondicion_Load);

            //frmModelos = principal;
            frmIngreso = ingreso;
            frmModificacionMod = modificacion;

            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;

            radbotActivo.CheckedChanged += RadioButton_CheckedChanged;
            radbotReparacion.CheckedChanged += RadioButton_CheckedChanged;
            radbotConstruccion.CheckedChanged += RadioButton_CheckedChanged;
            radbotInactivo.CheckedChanged += RadioButton_CheckedChanged;
            this.nuevoOModificacion = nuevoOModificacion;
            frmModelos = principal;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            MessageBox.Show(frmModelos.NuevoOModificacion);
            string valorActualizadoNuevooModificacion = frmModelos.NuevoOModificacion;
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                texCondicion.Text = rb.Text;
                texCondicion.Visible = false;

                radbotActivo.Visible = false;
                radbotReparacion.Visible = false;
                radbotConstruccion.Visible = false;
                radbotInactivo.Visible = false;
                if (frmModelos.NuevoOModificacion == "Nuevo")
                {
                    if (frmIngreso != null)
                    {
                        frmIngreso.texCondicionIngreso.Text = rb.Text;

                        // 🔹 Quitar selección de texto
                        frmIngreso.texCondicionIngreso.SelectionStart = frmIngreso.texCondicionIngreso.Text.Length;
                        frmIngreso.texCondicionIngreso.SelectionLength = 0;

                        // 🔹 Forzar un refresh visual
                        frmIngreso.texCondicionIngreso.Refresh();

                        // 🔹 Opción alternativa: quitar foco y devolverlo
                        frmIngreso.ActiveControl = null;
                        frmIngreso.texCondicionIngreso.Focus();

                        frmIngreso.texCondicionIngreso.Visible = true;
                        frmIngreso.texCondicionIngreso.Font = new Font(frmIngreso.texCondicionIngreso.Font.FontFamily, 18);
                        frmIngreso.texCondicionIngreso.BorderStyle = BorderStyle.None;
                        frmIngreso.texDesde.Visible = false;
                        frmIngreso.datDesde.Location = new Point(870, 705);
                        frmIngreso.datDesde.BringToFront();

                        frmIngreso.panCajaEstadoFecha.Visible = true;
                        frmIngreso.panCajaEstadoFecha.Location = new Point(550, 680);
                        frmIngreso.panCajaEstadoFecha.BringToFront();

                        if (frmIngreso.datDesde != null)
                        {
                            frmIngreso.datDesde.Visible = true;
                        }

                    }
                }
                else if (nuevoOModificacion == "Modificacion")
                {
                    frmModificacionMod.texEstado.Text = rb.Text;

                    // 🔹 Quitar selección de texto
                    frmModificacionMod.texEstado.SelectionStart = frmModificacionMod.texEstado.Text.Length;
                    frmModificacionMod.texEstado.SelectionLength = 0;

                    // 🔹 Forzar un refresh visual
                    frmModificacionMod.texEstado.Refresh();

                    // 🔹 Opción alternativa: quitar foco y devolverlo
                    frmModificacionMod.ActiveControl = null;
                    frmModificacionMod.texEstado.Focus();

                    frmModificacionMod.texEstado.Visible = true;
                    frmModificacionMod.texEstado.Font = new Font(frmModificacionMod.texEstado.Font.FontFamily, 18);
                    frmModificacionMod.texEstado.BorderStyle = BorderStyle.None;
                    frmModificacionMod.texDesde.Visible = false;
                    frmModificacionMod.datDesdeMod.Location = new Point(650, 699);
                    frmModificacionMod.datDesdeMod.BringToFront();

                    frmModificacionMod.panRecuaCondiFecha.Visible = true;

                    if (frmModificacionMod.datDesdeMod != null)
                    {
                        frmModificacionMod.datDesdeMod.Visible = true;
                    }

                }
                this.Close();
            }
        }

        private void frmCondicion_Load(object sender, EventArgs e)
        {
            _isInitializing = true;


            texCondicion.Visible = false; // Oculta el TextBox al inicio
            texCondicion.Text = "";
            texCondicion.Location = new Point(250, 55);

            // Desactivamos temporalmente AutoCheck para evitar selección automática
            radbotActivo.AutoCheck = false;
            radbotReparacion.AutoCheck = false;
            radbotConstruccion.AutoCheck = false;
            radbotInactivo.AutoCheck = false;

            radbotActivo.Visible = true;
            radbotReparacion.Visible = true;
            radbotConstruccion.Visible = true;
            radbotInactivo.Visible = true;

            // Quita la selección de los RadioButton
            radbotActivo.Checked = false;
            radbotReparacion.Checked = false;
            radbotConstruccion.Checked = false;
            radbotInactivo.Checked = false;

            // Reactivamos AutoCheck para que el usuario pueda seleccionarlos normalmente
            radbotActivo.AutoCheck = true;
            radbotReparacion.AutoCheck = true;
            radbotConstruccion.AutoCheck = true;
            radbotInactivo.AutoCheck = true;

            /*frmIngreso.texCondicionIngreso.Invalidate();
            frmIngreso.texCondicionIngreso.Update();*/

            botCancelar.Visible = true;

            // Quita el foco de cualquier control para evitar que alguno se seleccione automáticamente
            this.ActiveControl = null;

            _isInitializing = false;
        }

        public string DevolverDato
        {
            get { return Dato; }
            set { Dato = value; }
        }

        private void texCondicion_Click(object sender, EventArgs e)  //No está trabajando
        {
            texCondicion.Visible = false;

            radbotActivo.Visible = true;
            radbotConstruccion.Visible = true;
            radbotInactivo.Visible = true;
            radbotReparacion.Visible = true;

            // Asegurar que el panel en frmIngreso esté visible
            if (frmIngreso != null)
            {
                frmIngreso.panCajaEstadoFecha.Visible = true;
            }
        }

        private void botCancelar_Click(object sender, EventArgs e)
        {
            frmIngreso.texCondicionIngreso.Visible = true;
            frmIngreso.panCajaEstadoFecha.Visible = true;
            frmIngreso.panCajaEstadoFecha.BringToFront();
            frmIngreso.botGuardar.Visible = true;
            frmIngreso.botSalir.Visible = true;
            frmIngreso.texDesde.Visible = false;
            frmIngreso.texCondicionIngreso.Text = "Asignar condición";
            frmIngreso.texCondicionIngreso.BorderStyle = BorderStyle.None;

            this.Close();
        }

        private void frmCondicion_Leave(object sender, EventArgs e)
        {
        }

        private void frmCondicion_Shown(object sender, EventArgs e)
        {
        }

        private void panCondicion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radbotInactivo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radbotActivo_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radbotReparacion_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}