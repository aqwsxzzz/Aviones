using System;
using System.Windows.Forms;
using Aviones_2._0.Common;

namespace Aviones_2._0.Forms.Comun
{
    /// <summary>Pide una cantidad mayor que cero, con un máximo opcional.</summary>
    public partial class frmCantidad : Form
    {
        private readonly decimal? maximo;

        public decimal Cantidad { get; private set; }

        public frmCantidad(string titulo, string pregunta, decimal? actual, decimal? maximo)
        {
            InitializeComponent();
            Text = titulo;
            labPregunta.Text = pregunta;
            this.maximo = maximo;
            txtCantidad.Text = actual.HasValue ? Numeros.Cantidad(actual.Value) : "";
            labAyuda.Text = maximo.HasValue ? $"Máximo disponible: {Numeros.Cantidad(maximo.Value)}" : "";
            Estilo.Botones(botAceptar);
            Estilo.BotonSecundario(botCancelar);
            Shown += (s, e) => { txtCantidad.Focus(); txtCantidad.SelectAll(); };
        }

        /// <summary>Muestra el diálogo; devuelve null si se cancela.</summary>
        public static decimal? Pedir(IWin32Window dueño, string titulo, string pregunta, decimal? actual = null, decimal? maximo = null)
        {
            using (var f = new frmCantidad(titulo, pregunta, actual, maximo))
            {
                return f.ShowDialog(dueño) == DialogResult.OK ? f.Cantidad : (decimal?)null;
            }
        }

        private void botAceptar_Click(object sender, EventArgs e)
        {
            if (!Numeros.TryLeer(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                Mensajes.Aviso(this, "Escriba un número mayor que cero.");
                txtCantidad.Focus();
                return;
            }
            if (maximo.HasValue && cantidad > maximo.Value)
            {
                Mensajes.Aviso(this, $"No alcanza: hay {Numeros.Cantidad(maximo.Value)} disponibles.");
                txtCantidad.Focus();
                return;
            }
            Cantidad = cantidad;
            DialogResult = DialogResult.OK;
        }
    }
}
