using System.Drawing;
using System.Windows.Forms;
using Aviones_2._0.Common;

namespace Aviones_2._0.Forms.Comun
{
    /// <summary>Ventanita para escribir un texto largo (por ejemplo la nota de un material) que no entra en una casilla.</summary>
    public class frmTexto : Form
    {
        private readonly TextBox txt = new TextBox
        {
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Size = new Size(460, 140),
            Font = new Font("Arial", 12F),
            MaxLength = 500,
            AcceptsReturn = true
        };

        public string Texto => txt.Text;

        public frmTexto(string titulo, string texto)
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
            txt.Text = texto ?? "";

            var aceptar = new Button { Text = "ACEPTAR", Size = new Size(130, 40), Margin = new Padding(8, 0, 0, 0), DialogResult = DialogResult.OK };
            var cancelar = new Button { Text = "CANCELAR", Size = new Size(130, 40), Margin = new Padding(8, 0, 0, 0), DialogResult = DialogResult.Cancel };
            Estilo.Botones(aceptar);
            Estilo.BotonSecundario(cancelar);
            var botones = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill, Margin = new Padding(0, 14, 0, 0) };
            botones.Controls.Add(cancelar);
            botones.Controls.Add(aceptar);

            var marco = new TableLayoutPanel { AutoSize = true, ColumnCount = 1, Padding = new Padding(18, 14, 18, 14) };
            marco.Controls.Add(txt, 0, 0);
            marco.Controls.Add(botones, 0, 1);
            Controls.Add(marco);
            CancelButton = cancelar;
            Shown += (s, e) => { txt.Focus(); txt.SelectionStart = txt.TextLength; };
        }
    }
}
