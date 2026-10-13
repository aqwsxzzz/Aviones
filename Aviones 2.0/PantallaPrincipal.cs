using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public PantallaPrincipal()
        {
            InitializeComponent();

            //Procedimiento para que no parpadee la imagen de fondo
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            componentesGenericosPrin = new Componentes();
            this.FormBorderStyle = FormBorderStyle.None;

            Materiales.Location = new Point((componentesGenericosPrin.puntoMedioHorizontal - 720), (componentesGenericosPrin.puntoMedioVertical - 375));
            Modelos.Location = new Point((componentesGenericosPrin.puntoMedioHorizontal - 720), (componentesGenericosPrin.puntoMedioVertical - 175));
            Compras.Location = new Point((componentesGenericosPrin.puntoMedioHorizontal - 720), (componentesGenericosPrin.puntoMedioVertical + 25));
            Salir.Location = new Point((componentesGenericosPrin.puntoMedioHorizontal + 596), (componentesGenericosPrin.puntoMedioVertical + 415));
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
            frmComprasCom frmCompras = new frmComprasCom();
            frmCompras.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}