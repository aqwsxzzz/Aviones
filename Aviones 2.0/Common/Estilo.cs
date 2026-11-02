using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aviones_2._0.Common
{
    /// <summary>Colores y formato comunes, tomados de las pantallas originales.</summary>
    public static class Estilo
    {
        public static readonly Color Fondo = Color.LightSteelBlue;
        public static readonly Color Boton = Color.CadetBlue;
        public static readonly Color BotonSalir = Color.DarkSlateGray;
        public static readonly Color Encabezado = SystemColors.ButtonShadow;
        public static readonly Color Celda = Color.FromArgb(224, 224, 224);
        public static readonly Color Seleccion = Color.CornflowerBlue;
        public static readonly Color SinStock = Color.DimGray;

        public static void Grilla(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Fondo;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Encabezado;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Encabezado;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.BackColor = Celda;
            dgv.DefaultCellStyle.SelectionBackColor = Seleccion;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Arial", 10F);
        }

        public static void Botones(params Button[] botones)
        {
            foreach (var b in botones)
            {
                b.BackColor = Boton;
                b.ForeColor = Color.White;
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderColor = Color.DarkSlateGray;
                b.Font = new Font("Arial", 10F, FontStyle.Bold);
                b.UseVisualStyleBackColor = false;
            }
        }

        /// <summary>
        /// Enter pasa al campo siguiente, como en las pantallas originales.
        /// En botones Enter sigue haciendo clic.
        /// </summary>
        public static void EnterAvanza(Form form)
        {
            form.KeyPreview = true;
            form.KeyDown += (s, e) =>
            {
                var activo = form.ActiveControl;
                // Dentro de un control compuesto (como el selector de una lista), el campo es el de adentro
                while (activo is ContainerControl contenedor && contenedor.ActiveControl != null)
                {
                    activo = contenedor.ActiveControl;
                }
                bool esCampo = activo is TextBoxBase || activo is ComboBox || activo is DateTimePicker
                               || activo is RadioButton || activo is CheckBox;
                if (e.KeyCode == Keys.Enter && esCampo && !(activo is TextBoxBase t && t.Multiline))
                {
                    e.SuppressKeyPress = true;
                    form.SelectNextControl(activo, true, true, true, true);
                }
            };
        }

        /// <summary>Un total que tiene que verse: recuadrado y con letra más grande.</summary>
        public static void Total(Label total)
        {
            total.AutoSize = true;
            total.BorderStyle = BorderStyle.FixedSingle;
            total.BackColor = Color.FromArgb(250, 250, 250);
            total.Font = new Font("Arial", 13F, FontStyle.Bold);
            total.Padding = new Padding(10, 5, 10, 5);
            total.Anchor = AnchorStyles.Left;
        }

        /// <summary>
        /// La ventana toma el color de la que la abre, para que se vea en qué división se está
        /// (Materiales, Modelos o Compras). Las grillas con el fondo de antes también cambian.
        /// </summary>
        public static void ColorDeLaSeccion(Form ventana)
        {
            ventana.Load += (s, e) =>
            {
                // Al cargarse, la ventana activa todavía es la que la abrió
                var origen = ventana.Owner ?? Form.ActiveForm;
                if (origen == null || origen == ventana || origen.BackColor.A < 255)
                {
                    return;
                }
                Repintar(ventana, ventana.BackColor.ToArgb(), origen.BackColor);
                ventana.BackColor = origen.BackColor;
            };
        }

        private static void Repintar(Control padre, int anterior, Color nuevo)
        {
            foreach (Control c in padre.Controls)
            {
                if (c is DataGridView dgv)
                {
                    int fondo = dgv.BackgroundColor.ToArgb();
                    if (fondo == anterior || fondo == Fondo.ToArgb())
                    {
                        dgv.BackgroundColor = nuevo;
                    }
                }
                else if (c.BackColor.A == 255 && c.BackColor.ToArgb() == anterior)
                {
                    c.BackColor = nuevo;
                }
                Repintar(c, anterior, nuevo);
            }
        }

        /// <summary>
        /// El color de un aviso: el mismo tono que la división pero más fuerte
        /// (en Modelos un mostaza, en Materiales un azul acero, en Compras un verde), para que llame la atención.
        /// </summary>
        public static Color Resaltado(Color c)
        {
            if (c.IsEmpty || c.A < 255)
            {
                c = SystemColors.Control;
            }
            float h = c.GetHue(), s = c.GetSaturation();
            if (s < 0.05f)
            {
                return Color.FromArgb((int)(c.R * 0.8), (int)(c.G * 0.8), (int)(c.B * 0.8));   // gris: sólo más oscuro
            }
            return DeHsl(h, Math.Min(Math.Max(s, 0.45f), 0.65f), 0.70f);
        }

        private static Color DeHsl(float h, float s, float l)
        {
            float c = (1 - Math.Abs(2 * l - 1)) * s;
            float x = c * (1 - Math.Abs(h / 60f % 2 - 1));
            float m = l - c / 2;
            float r, g, b;
            if (h < 60) { r = c; g = x; b = 0; }
            else if (h < 120) { r = x; g = c; b = 0; }
            else if (h < 180) { r = 0; g = c; b = x; }
            else if (h < 240) { r = 0; g = x; b = c; }
            else if (h < 300) { r = x; g = 0; b = c; }
            else { r = c; g = 0; b = x; }
            return Color.FromArgb((int)Math.Round((r + m) * 255), (int)Math.Round((g + m) * 255), (int)Math.Round((b + m) * 255));
        }

        public static void BotonSecundario(Button b)
        {
            Botones(b);
            b.BackColor = BotonSalir;
        }
    }
}
