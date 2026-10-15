using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Forms.Compras;
using Aviones_2._0.Forms.Materiales;
using Aviones_2._0.Forms.Modelos;

namespace Aviones_2._0
{
    public partial class PantallaPrincipal : Form
    {
        private Componentes componentesGenericosPrin;
        /// <summary>Foto de fondo elegida en esta computadora; null: fondo liso.</summary>
        private Image fondo;
        private static string ArchivoFondo => Path.Combine(Path.GetDirectoryName(Instalacion.Rutas.Datos), "fondo.jpg");

        public PantallaPrincipal()
        {
            InitializeComponent();
            Text = "Aviones " + Instalacion.Rutas.Version;

            //Procedimiento para que no parpadee la imagen de fondo
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            componentesGenericosPrin = new Componentes();
            this.FormBorderStyle = FormBorderStyle.None;

            // Pensado para 1920x1080; en pantallas más chicas los botones no se salen de los bordes
            var pantalla = Screen.PrimaryScreen.Bounds.Size;
            int izquierda = Math.Max(40, componentesGenericosPrin.puntoMedioHorizontal - 720);
            int arriba = Math.Max(40, componentesGenericosPrin.puntoMedioVertical - 375);
            Materiales.Location = new Point(izquierda, arriba);
            Modelos.Location = new Point(izquierda, arriba + 200);
            Compras.Location = new Point(izquierda, arriba + 400);
            Salir.Location = new Point(
                Math.Min(componentesGenericosPrin.puntoMedioHorizontal + 596, pantalla.Width - Salir.Width - 40),
                Math.Min(componentesGenericosPrin.puntoMedioVertical + 415, pantalla.Height - Salir.Height - 40));
            botFondo.Location = new Point(Salir.Left - botFondo.Width - 20, Salir.Bottom - botFondo.Height);
            CargarFondo();
        }

        protected override void OnResize(EventArgs e)  //Procedimiento para que no parpadee la imagen de fondo
        {
            {
                base.OnResize(e);
                this.Invalidate(); // Redibuja solo cuando el tamaño cambia
            }
        }

        private async void Materiales_Click(object sender, EventArgs e)
        {
            frmMateriales frmMateriales = new frmMateriales();

            frmMateriales.ShowDialog();
        }

        private void Modelos_Click(object sender, EventArgs e)
        {
            frmModelos frmModelos = new frmModelos();
            frmModelos.ShowDialog();
        }

        private void Compras_Click(object sender, EventArgs e)
        {
            using (var compras = new frmCompras())
            {
                compras.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Fondo

        // La foto de fondo no viene con el programa (es personal): cada uno elige la suya y queda en su computadora.

        private void CargarFondo()
        {
            var anterior = fondo;
            fondo = null;
            try
            {
                if (File.Exists(ArchivoFondo))
                {
                    fondo = Imagenes.DesdeBytes(File.ReadAllBytes(ArchivoFondo));
                }
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            anterior?.Dispose();
            pictureBox1.Invalidate();
        }

        private void botFondo_Click(object sender, EventArgs e)
        {
            if (fondo != null)
            {
                var respuesta = MessageBox.Show(this, "¿Elegir otra foto de fondo?\n\nSí: elegir otra foto.\nNo: dejar el fondo liso.",
                    "Fondo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Cancel)
                {
                    return;
                }
                if (respuesta == DialogResult.No)
                {
                    Mensajes.Intentar(this, () => File.Delete(ArchivoFondo));
                    CargarFondo();
                    return;
                }
            }
            byte[] foto = Imagenes.Elegir(this, 2560);
            if (foto != null && Mensajes.Intentar(this, () =>
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(ArchivoFondo));
                    File.WriteAllBytes(ArchivoFondo, foto);
                }))
            {
                CargarFondo();
            }
        }

        /// <summary>La foto cubre toda la pantalla sin deformarse (se recorta lo que sobra); sin foto, un degradé.</summary>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var area = pictureBox1.ClientRectangle;
            if (fondo == null)
            {
                using (var degrade = new LinearGradientBrush(area, Color.SteelBlue, Color.FromArgb(18, 40, 66), LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(degrade, area);
                }
                return;
            }
            float escala = Math.Max((float)area.Width / fondo.Width, (float)area.Height / fondo.Height);
            float ancho = fondo.Width * escala, alto = fondo.Height * escala;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImage(fondo, (area.Width - ancho) / 2, (area.Height - alto) / 2, ancho, alto);
        }

        #endregion
    }
}