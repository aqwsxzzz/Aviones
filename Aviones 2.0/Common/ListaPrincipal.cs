using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Aviones_2._0.Common
{
    /// <summary>
    /// El listado principal de Materiales y Modelos, dentro del lado izquierdo de un SplitContainer:
    /// centrado en la pantalla mientras no hay nada elegido (con su ancho de siempre), a la izquierda con
    /// el detalle al lado cuando se elige algo. Termina siempre en un renglón entero, y los filtros
    /// y el resumen arrancan donde arranca el listado.
    /// </summary>
    public static class ListaPrincipal
    {
        /// <summary>El margen izquierdo de cada control alineado, antes de correrlo.</summary>
        private static readonly Dictionary<Control, int> margenes = new Dictionary<Control, int>();

        public static void Acomodar(SplitContainer division, DataGridView lista, int ancho, bool centrada, params Control[] alineados)
        {
            division.Panel2Collapsed = centrada;
            var panel = division.Panel1;
            int alto = AltoEnRenglonesEnteros(lista, panel.ClientSize.Height);
            if (centrada)
            {
                int w = Math.Min(ancho, panel.ClientSize.Width);
                lista.Dock = DockStyle.None;
                panel.Padding = Padding.Empty;
                lista.Bounds = new Rectangle((panel.ClientSize.Width - w) / 2, 0, w, alto);
            }
            else
            {
                if (lista.Dock != DockStyle.Fill)
                {
                    lista.Dock = DockStyle.Fill;
                }
                // Lo que sobra abajo queda vacío, para que no se vea medio renglón cortado
                var relleno = new Padding(0, 0, 0, Math.Max(0, panel.ClientSize.Height - alto));
                if (panel.Padding != relleno)
                {
                    panel.Padding = relleno;
                }
            }

            int corrimiento = lista.Dock == DockStyle.Fill ? 0 : lista.Left;
            foreach (var c in alineados)
            {
                if (!margenes.TryGetValue(c, out int original))
                {
                    margenes[c] = original = c.Margin.Left;
                    c.Disposed += (s, e) => margenes.Remove(c);
                }
                if (c.Margin.Left != original + corrimiento)
                {
                    c.Margin = new Padding(original + corrimiento, c.Margin.Top, c.Margin.Right, c.Margin.Bottom);
                }
            }
        }

        /// <summary>El alto que entra en el lugar disponible terminando en un renglón entero.</summary>
        public static int AltoEnRenglonesEnteros(DataGridView lista, int disponible)
        {
            int renglon = lista.Rows.Count > 0 ? lista.Rows[0].Height : lista.RowTemplate.Height;
            int encabezado = lista.ColumnHeadersVisible ? lista.ColumnHeadersHeight : 0;
            int barra = lista.Controls.OfType<HScrollBar>().Any(b => b.Visible) ? SystemInformation.HorizontalScrollBarHeight : 0;
            int bordes = lista.BorderStyle == BorderStyle.None ? 0 : 2;
            int fijo = encabezado + barra + bordes;
            int renglones = Math.Max(1, (disponible - fijo) / Math.Max(1, renglon));
            return Math.Min(disponible, fijo + renglones * renglon);
        }
    }
}
