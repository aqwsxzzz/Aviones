using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Aviones_2._0.Forms.Comun
{
    /// <summary>
    /// Lista de sugerencias debajo de un cuadro de texto (lo ya escrito en otros materiales), con letra grande.
    /// Reemplaza al autocompletar de Windows, cuya lista no deja cambiar la letra.
    /// Flechas para moverse, Enter (lo maneja la pantalla con <see cref="Aceptar"/>) o clic para elegir, Esc para cerrar.
    /// </summary>
    public class Sugerencias
    {
        private const int MaximoVisibles = 10;
        private readonly TextBox caja;
        private readonly List<string> opciones;
        private readonly ListBox lista = new ListBox
        {
            Visible = false,
            Font = new Font("Arial", 12F),
            IntegralHeight = false,
            TabStop = false,
            BorderStyle = BorderStyle.FixedSingle
        };
        /// <summary>El texto lo pone el programa, no el usuario: no se buscan sugerencias.</summary>
        private bool poniendo;

        public Sugerencias(TextBox caja, IEnumerable<string> opciones)
        {
            this.caja = caja;
            this.opciones = opciones.Where(o => !string.IsNullOrWhiteSpace(o)).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
            caja.AutoCompleteMode = AutoCompleteMode.None;
            caja.TextChanged += (s, e) => { if (!poniendo) Filtrar(); };
            caja.KeyDown += Caja_KeyDown;
            // Al ir a otro campo se cierra; si fue para hacer clic en la lista, el clic llega antes de cerrar
            caja.Leave += (s, e) => caja.BeginInvoke((Action)(() => { if (!lista.Focused) Cerrar(); }));
            lista.MouseClick += (s, e) =>
            {
                int i = lista.IndexFromPoint(e.Location);
                if (i >= 0)
                {
                    lista.SelectedIndex = i;
                    Aceptar();
                }
                caja.Focus();
            };
        }

        /// <summary>Hay una sugerencia marcada (con las flechas) lista para elegir con Enter.</summary>
        public bool HayElegida => lista.Visible && lista.SelectedIndex >= 0;

        public bool Abierta => lista.Visible;

        /// <summary>Pone un texto sin abrir sugerencias.</summary>
        public void Poner(string texto)
        {
            poniendo = true;
            caja.Text = texto ?? "";
            caja.SelectionStart = caja.TextLength;
            poniendo = false;
            Cerrar();
        }

        /// <summary>Pasa la sugerencia marcada al cuadro.</summary>
        public void Aceptar()
        {
            if (lista.SelectedIndex >= 0)
            {
                Poner((string)lista.SelectedItem);
            }
            Cerrar();
        }

        public void Cerrar() => lista.Visible = false;

        private void Filtrar()
        {
            string texto = caja.Text.Trim();
            // Sólo mientras se escribe en el cuadro (no cuando el programa le pone el texto)
            if (texto.Length == 0 || caja.FindForm()?.ActiveControl != caja)
            {
                Cerrar();
                return;
            }
            // Primero las que empiezan con lo escrito, después las que lo contienen
            var empiezan = opciones.Where(o => o.StartsWith(texto, StringComparison.CurrentCultureIgnoreCase));
            var contienen = opciones.Where(o => o.IndexOf(texto, StringComparison.CurrentCultureIgnoreCase) > 0);
            var encontradas = empiezan.Concat(contienen)
                .Where(o => !string.Equals(o, texto, StringComparison.CurrentCultureIgnoreCase))
                .Take(50).ToList();
            if (encontradas.Count == 0)
            {
                Cerrar();
                return;
            }

            var form = caja.FindForm();
            if (form == null)
            {
                return;
            }
            if (lista.Parent != form)
            {
                form.Controls.Add(lista);
            }
            lista.BeginUpdate();
            lista.Items.Clear();
            lista.Items.AddRange(encontradas.Cast<object>().ToArray());
            lista.EndUpdate();
            // Visible antes de medir: hasta que está en pantalla no sabe el alto de cada renglón
            lista.Visible = true;
            lista.BringToFront();
            var abajo = form.PointToClient(caja.Parent.PointToScreen(new Point(caja.Left, caja.Bottom)));
            int renglon = Math.Max(lista.ItemHeight, lista.Font.Height);
            lista.SetBounds(abajo.X, abajo.Y, caja.Width, renglon * Math.Min(encontradas.Count, MaximoVisibles) + 4);
            lista.SelectedIndex = -1;
        }

        private void Caja_KeyDown(object sender, KeyEventArgs e)
        {
            if (!lista.Visible)
            {
                return;
            }
            if (e.KeyCode == Keys.Down)
            {
                lista.SelectedIndex = Math.Min(lista.SelectedIndex + 1, lista.Items.Count - 1);
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                lista.SelectedIndex = Math.Max(lista.SelectedIndex - 1, -1);
                e.Handled = e.SuppressKeyPress = true;
            }
        }
    }
}
