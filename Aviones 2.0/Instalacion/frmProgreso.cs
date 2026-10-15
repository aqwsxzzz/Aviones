using System.Drawing;
using System.Windows.Forms;

namespace Aviones_2._0.Instalacion
{
    /// <summary>Ventanita con un mensaje y una barra, mientras se descarga o instala algo.</summary>
    public class frmProgreso : Form
    {
        private readonly Label labMensaje;
        private readonly ProgressBar barra;

        public frmProgreso(string titulo)
        {
            Text = titulo;
            Font = new Font("Arial", 10F);
            BackColor = Color.LightSteelBlue;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(460, 110);
            ShowInTaskbar = true;

            labMensaje = new Label { Location = new Point(16, 16), Size = new Size(428, 40), Text = titulo };
            barra = new ProgressBar { Location = new Point(16, 64), Size = new Size(428, 24), Style = ProgressBarStyle.Marquee };
            Controls.Add(labMensaje);
            Controls.Add(barra);
        }

        public void Mensaje(string texto)
        {
            labMensaje.Text = texto;
            Application.DoEvents();
        }

        /// <param name="porcentaje">null: sin porcentaje (barra en movimiento).</param>
        public void Avance(int? porcentaje)
        {
            if (porcentaje.HasValue)
            {
                barra.Style = ProgressBarStyle.Continuous;
                barra.Value = System.Math.Max(0, System.Math.Min(100, porcentaje.Value));
            }
            else
            {
                barra.Style = ProgressBarStyle.Marquee;
            }
            Application.DoEvents();
        }
    }
}
