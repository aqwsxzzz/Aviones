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

        public static void BotonSecundario(Button b)
        {
            Botones(b);
            b.BackColor = BotonSalir;
        }
    }
}
