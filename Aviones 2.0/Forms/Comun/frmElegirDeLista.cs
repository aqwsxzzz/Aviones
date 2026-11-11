using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Aviones_2._0.Common;

namespace Aviones_2._0.Forms.Comun
{
    /// <summary>Elegir uno de una lista corta (por ejemplo, a qué vendedor se le cierra el pedido).</summary>
    public class frmElegirDeLista : Form
    {
        private readonly ListBox lista = new ListBox { Font = new Font("Arial", 12F), IntegralHeight = true, Margin = new Padding(0, 10, 0, 0) };
        private readonly Button botElegir = new Button { Text = "ELEGIR", Size = new Size(130, 40), Margin = new Padding(8, 0, 0, 0) };
        private readonly Button botCancelar = new Button { Text = "CANCELAR", Size = new Size(130, 40), Margin = new Padding(8, 0, 0, 0), DialogResult = DialogResult.Cancel };

        /// <summary>El elegido (0 el primero); -1 si se canceló.</summary>
        public int Elegido { get; private set; } = -1;

        public frmElegirDeLista(string titulo, string pregunta, IList<string> opciones)
        {
            Text = titulo;
            Font = new Font("Arial", 11F);
            BackColor = Estilo.Fondo;
            Estilo.ColorDeLaSeccion(this);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            foreach (var o in opciones)
            {
                lista.Items.Add(o);
            }
            lista.Size = new Size(420, lista.ItemHeight * System.Math.Min(System.Math.Max(opciones.Count, 3), 12) + 6);
            lista.SelectedIndexChanged += (s, e) => botElegir.Enabled = lista.SelectedIndex >= 0;
            lista.DoubleClick += (s, e) => Elegir();
            botElegir.Enabled = false;
            botElegir.Click += (s, e) => Elegir();
            Estilo.Botones(botElegir);
            Estilo.BotonSecundario(botCancelar);

            var botones = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill, Margin = new Padding(0, 16, 0, 0) };
            botones.Controls.Add(botCancelar);
            botones.Controls.Add(botElegir);

            var marco = new TableLayoutPanel { AutoSize = true, ColumnCount = 1, Padding = new Padding(20, 16, 20, 16) };
            marco.Controls.Add(new Label { Text = pregunta, AutoSize = true, Font = new Font("Arial", 11F, FontStyle.Bold) }, 0, 0);
            marco.Controls.Add(lista, 0, 1);
            marco.Controls.Add(botones, 0, 2);
            Controls.Add(marco);
            AcceptButton = botElegir;
            CancelButton = botCancelar;
            Shown += (s, e) => { lista.ClearSelected(); lista.Focus(); };
        }

        private void Elegir()
        {
            if (lista.SelectedIndex < 0)
            {
                return;
            }
            Elegido = lista.SelectedIndex;
            DialogResult = DialogResult.OK;
        }
    }
}
