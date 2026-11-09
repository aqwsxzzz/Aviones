using System;
using System.Drawing;
using System.Windows.Forms;
using Aviones_2._0.Common;

namespace Aviones_2._0.Forms.Comun
{
    /// <summary>
    /// Ventana de mensajes del programa, en lugar de la de Windows: letra más grande y en negrita,
    /// y el tono de la pantalla que la llama pero más fuerte, para que llame la atención.
    /// Sin la barra blanca de Windows: el título va adentro, con borde en relieve y sombra.
    /// </summary>
    public class frmMensaje : Form
    {
        public enum Tipo { Aviso, Error, Pregunta, Informacion }

        /// <summary>El botón elegido (0 el primero); -1 si se cerró sin elegir.</summary>
        public int Elegido { get; private set; } = -1;

        private Button botonPorDefecto;

        public frmMensaje(Color fondo, string titulo, string texto, Tipo tipo, string[] botones, int porDefecto, int cancelar)
        {
            Text = titulo;
            Font = new Font("Arial", 12F);
            BackColor = Estilo.Resaltado(fondo);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            KeyPreview = true;

            var icono = new PictureBox
            {
                Image = IconoDe(tipo).ToBitmap(),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Size = new Size(48, 48),
                Margin = new Padding(0, 0, 16, 0)
            };

            // Un error inesperado trae todo el detalle: va en un cuadro con barra para leerlo entero
            Control cuerpo;
            if (texto.Length > 600)
            {
                cuerpo = new TextBox
                {
                    Text = texto.Replace("\r\n", "\n").Replace("\n", Environment.NewLine),
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Vertical,
                    Size = new Size(620, 300),
                    Font = new Font("Arial", 10F),
                    BackColor = Color.White
                };
            }
            else
            {
                cuerpo = new Label
                {
                    Text = texto,
                    AutoSize = true,
                    MaximumSize = new Size(560, 0),
                    Margin = new Padding(0, 10, 0, 0),
                    Font = new Font("Arial", 12F, FontStyle.Bold)
                };
            }

            var contenido = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = new Padding(0) };
            contenido.Controls.Add(icono);
            contenido.Controls.Add(cuerpo);

            var fila = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 20, 0, 0)
            };
            for (int i = botones.Length - 1; i >= 0; i--)
            {
                int indice = i;
                var boton = new Button
                {
                    Text = botones[i],
                    AutoSize = true,
                    MinimumSize = new Size(120, 40),
                    Margin = new Padding(8, 0, 0, 0)
                };
                Estilo.Botones(boton);
                if (i == cancelar && botones.Length > 1)
                {
                    Estilo.BotonSecundario(boton);
                }
                boton.Click += (s, e) => { Elegido = indice; DialogResult = DialogResult.OK; };
                fila.Controls.Add(boton);
                if (i == porDefecto)
                {
                    AcceptButton = boton;
                    botonPorDefecto = boton;
                }
            }

            var encabezado = new Label
            {
                Text = titulo,
                AutoSize = true,
                Font = new Font("Arial", 13F, FontStyle.Bold | FontStyle.Underline),
                Margin = new Padding(0, 0, 0, 14)
            };

            var marco = new TableLayoutPanel { AutoSize = true, ColumnCount = 1, Padding = new Padding(26, 18, 26, 20) };
            marco.Controls.Add(encabezado, 0, 0);
            marco.Controls.Add(contenido, 0, 1);
            marco.Controls.Add(fila, 0, 2);
            Controls.Add(marco);

            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape && cancelar >= 0)
                {
                    Elegido = cancelar;
                    DialogResult = DialogResult.Cancel;
                }
            };
        }

        /// <summary>Sombra alrededor de la ventana, para que se despegue de la pantalla de atrás.</summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                var cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        /// <summary>Borde en relieve: claro arriba y a la izquierda, oscuro abajo y a la derecha.</summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var r = ClientRectangle;
            using (var contorno = new Pen(Color.FromArgb(70, 70, 70)))
            using (var luz = new Pen(ControlPaint.Light(BackColor, 0.6f), 3))
            using (var sombra = new Pen(ControlPaint.Dark(BackColor, 0.25f), 3))
            {
                e.Graphics.DrawLine(luz, 2, 2, r.Right - 3, 2);
                e.Graphics.DrawLine(luz, 2, 2, 2, r.Bottom - 3);
                e.Graphics.DrawLine(sombra, r.Right - 3, 2, r.Right - 3, r.Bottom - 3);
                e.Graphics.DrawLine(sombra, 2, r.Bottom - 3, r.Right - 3, r.Bottom - 3);
                e.Graphics.DrawRectangle(contorno, 0, 0, r.Width - 1, r.Height - 1);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            botonPorDefecto?.Focus();   // recién visible se le puede dar el foco
        }

        private static Icon IconoDe(Tipo tipo)
        {
            switch (tipo)
            {
                case Tipo.Error: return SystemIcons.Error;
                case Tipo.Pregunta: return SystemIcons.Question;
                case Tipo.Informacion: return SystemIcons.Information;
                default: return SystemIcons.Warning;
            }
        }
    }
}
