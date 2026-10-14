using System;
using System.Drawing;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;

namespace Aviones_2._0.Forms.Modelos
{
    /// <summary>
    /// Pregunta qué pasa con material que sale de un modelo, pieza por pieza: cuántas vuelven como nuevas,
    /// cuántas como usadas, y el resto se descarta. No importa el motivo (error de carga, pieza rota,
    /// cambio de diseño): sólo en qué estado está cada pieza.
    /// </summary>
    public partial class frmDestino : Form
    {
        private readonly decimal saliente;
        private readonly bool puedeVolverNuevo;

        public Devolucion Devolucion { get; private set; }

        public frmDestino(string material, decimal saliente, string unidad, string estadoOrigen)
        {
            InitializeComponent();
            this.saliente = saliente;
            puedeVolverNuevo = estadoOrigen != EstadosMaterial.Usado;

            labDetalle.Text = $"Salen {Numeros.Cantidad(saliente)} {unidad} de {material}.";
            Estilo.Botones(botTodoNuevo, botTodoUsado, botTodoDescarte, botAceptar);
            botTodoDescarte.BackColor = Color.Firebrick;
            Estilo.BotonSecundario(botCancelar);

            if (!puedeVolverNuevo)
            {
                txtNuevo.Enabled = false;
                botTodoNuevo.Enabled = false;
                botTodoNuevo.BackColor = Color.Gray;
                labNuevoAyuda.Text = "no: salió del stock usado";
            }

            // Lo más común: se recicla todo
            Repartir(0m, saliente);
            Shown += (s, e) => { txtUsado.Focus(); txtUsado.SelectAll(); };
        }

        /// <summary>Muestra el diálogo; devuelve null si se cancela.</summary>
        public static Devolucion? Pedir(IWin32Window dueño, string material, decimal saliente, string unidad, string estadoOrigen)
        {
            using (var f = new frmDestino(material, saliente, unidad, estadoOrigen))
            {
                return f.ShowDialog(dueño) == DialogResult.OK ? f.Devolucion : (Devolucion?)null;
            }
        }

        private void Repartir(decimal nuevo, decimal usado)
        {
            txtNuevo.Text = Numeros.Cantidad(nuevo);
            txtUsado.Text = Numeros.Cantidad(usado);
        }

        private bool Leer(out decimal nuevo, out decimal usado)
        {
            nuevo = 0m;
            usado = 0m;
            bool okNuevo = string.IsNullOrWhiteSpace(txtNuevo.Text) || Numeros.TryLeer(txtNuevo.Text, out nuevo);
            bool okUsado = string.IsNullOrWhiteSpace(txtUsado.Text) || Numeros.TryLeer(txtUsado.Text, out usado);
            return okNuevo && okUsado && nuevo >= 0 && usado >= 0;
        }

        private void Cantidades_Changed(object sender, EventArgs e)
        {
            if (Leer(out decimal nuevo, out decimal usado) && nuevo + usado <= saliente)
            {
                labDescarteCantidad.Text = Numeros.Cantidad(saliente - nuevo - usado);
                labDescarteCantidad.ForeColor = Color.Firebrick;
                botAceptar.Enabled = true;
            }
            else
            {
                labDescarteCantidad.Text = "?";
                labDescarteCantidad.ForeColor = Color.Gray;
                botAceptar.Enabled = false;
            }
        }

        private void botTodoNuevo_Click(object sender, EventArgs e) => Repartir(saliente, 0m);

        private void botTodoUsado_Click(object sender, EventArgs e) => Repartir(0m, saliente);

        private void botTodoDescarte_Click(object sender, EventArgs e) => Repartir(0m, 0m);

        private void botAceptar_Click(object sender, EventArgs e)
        {
            if (!Leer(out decimal nuevo, out decimal usado) || nuevo + usado > saliente)
            {
                Mensajes.Aviso(this, $"Entre lo que vuelve nuevo y usado no puede pasar de {Numeros.Cantidad(saliente)}.");
                return;
            }
            if (nuevo > 0 && !puedeVolverNuevo)
            {
                Mensajes.Aviso(this, "Ese material salió del stock usado: no puede volver como nuevo.");
                return;
            }
            Devolucion = new Devolucion(nuevo, usado);
            DialogResult = DialogResult.OK;
        }
    }
}
