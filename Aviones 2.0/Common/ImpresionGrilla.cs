using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Aviones_2._0.Common
{
    /// <summary>
    /// Imprime las columnas visibles de una grilla, con vista previa, repitiendo los títulos en cada página.
    /// Cada columna toma el ancho de su contenido (y de su título en un renglón); la hoja va vertical
    /// y sólo se pone apaisada si no entra. Al mandar a la impresora, vuelve a la pantalla que llamó.
    /// </summary>
    public class ImpresionGrilla
    {
        private readonly DataGridView grilla;
        private readonly string titulo;
        private readonly string pie;
        private readonly Font fuenteTitulo = new Font("Arial", 14F, FontStyle.Bold);
        private readonly Font fuenteEncabezado = new Font("Arial", 8.5F, FontStyle.Bold);
        private readonly Font fuente = new Font("Arial", 8.5F);
        private List<DataGridViewColumn> columnas;
        /// <summary>Ancho natural de cada columna, en centésimas de pulgada (la unidad de la impresión).</summary>
        private float[] naturales;
        private int filaActual;
        private int pagina;

        /// <summary>Espacio a cada lado del texto en una celda.</summary>
        private const float Relleno = 4f;

        private ImpresionGrilla(DataGridView grilla, string titulo, string pie)
        {
            this.grilla = grilla;
            this.titulo = titulo;
            this.pie = pie;
        }

        /// <summary>Muestra la vista previa. Devuelve si se mandó a la impresora.</summary>
        public static bool VistaPrevia(IWin32Window dueño, DataGridView grilla, string titulo, string pie = null)
        {
            if (grilla.Rows.Count == 0)
            {
                Mensajes.Aviso(dueño, "No hay nada para imprimir.");
                return false;
            }
            var impresion = new ImpresionGrilla(grilla, titulo, pie);
            bool impreso = false;
            using (var documento = new PrintDocument { DocumentName = titulo })
            using (var vista = new PrintPreviewDialog { Document = documento, WindowState = FormWindowState.Maximized })
            {
                impresion.Medir();
                documento.DefaultPageSettings.Landscape = impresion.AnchoNatural > AnchoUtil(documento.DefaultPageSettings, false);
                documento.BeginPrint += (s, e) => impresion.Empezar();
                documento.PrintPage += impresion.ImprimirPagina;
                documento.EndPrint += (s, e) =>
                {
                    // La vista previa también "imprime" para dibujarse; sólo la impresora cierra la ventana
                    if (e.PrintAction == PrintAction.PrintToPrinter && !e.Cancel)
                    {
                        impreso = true;
                        vista.BeginInvoke((Action)vista.Close);
                    }
                };
                vista.ShowDialog(dueño);
            }
            return impreso;
        }

        private float AnchoNatural => naturales.Sum();

        private static float AnchoUtil(PageSettings hoja, bool apaisada)
        {
            try
            {
                var tamaño = hoja.PaperSize;
                float ancho = apaisada ? tamaño.Height : tamaño.Width;
                return ancho - hoja.Margins.Left - hoja.Margins.Right;
            }
            catch (InvalidPrinterException)
            {
                return apaisada ? 900f : 650f;   // sin impresora: una hoja carta
            }
        }

        /// <summary>El ancho que necesita cada columna para su título (en un renglón) y su contenido más largo.</summary>
        private void Medir()
        {
            columnas = grilla.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).OrderBy(c => c.DisplayIndex).ToList();
            naturales = new float[columnas.Count];
            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                g.PageUnit = GraphicsUnit.Display;   // centésimas de pulgada, como la impresora
                for (int i = 0; i < columnas.Count; i++)
                {
                    float ancho = g.MeasureString(columnas[i].HeaderText.Trim(), fuenteEncabezado).Width;
                    foreach (DataGridViewRow fila in grilla.Rows)
                    {
                        string valor = fila.Cells[columnas[i].Index].FormattedValue?.ToString() ?? "";
                        ancho = Math.Max(ancho, g.MeasureString(valor, fuente).Width);
                    }
                    naturales[i] = (float)Math.Ceiling(ancho + 2 * Relleno + 2);
                }
            }
        }

        private void Empezar()
        {
            filaActual = 0;
            pagina = 0;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            pagina++;
            var area = e.MarginBounds;
            var g = e.Graphics;
            float y = area.Top;

            string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            g.DrawString(titulo, fuenteTitulo, Brushes.Black, area.Left, y);
            g.DrawString(fecha, fuente, Brushes.Black, area.Right - g.MeasureString(fecha, fuente).Width, y + 4);
            y += fuenteTitulo.GetHeight(g) + 8;

            // Del ancho de su contenido; si no entra ni apaisado, se achican todas por igual
            float escala = Math.Min(1f, area.Width / AnchoNatural);
            float[] anchos = naturales.Select(a => a * escala).ToArray();
            bool titulosEnDosRenglones = escala < 1f;
            float altoEncabezado = fuenteEncabezado.GetHeight(g) * (titulosEnDosRenglones ? 2 : 1) + 8;
            float altoFila = fuente.GetHeight(g) + 6;

            using (var formato = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, LineAlignment = StringAlignment.Center })
            using (var lapiz = new Pen(Color.Gray, 0.5f))
            {
                if (!titulosEnDosRenglones)
                {
                    formato.FormatFlags |= StringFormatFlags.NoWrap;
                }
                float x = area.Left;
                for (int i = 0; i < columnas.Count; i++)
                {
                    var celda = new RectangleF(x, y, anchos[i], altoEncabezado);
                    g.FillRectangle(Brushes.Gainsboro, celda);
                    g.DrawRectangle(lapiz, celda.X, celda.Y, celda.Width, celda.Height);
                    formato.Alignment = EsNumerica(columnas[i]) ? StringAlignment.Far : StringAlignment.Near;
                    g.DrawString(columnas[i].HeaderText.Trim(), fuenteEncabezado, Brushes.Black, Achicar(celda), formato);
                    x += anchos[i];
                }
                y += altoEncabezado;

                formato.FormatFlags |= StringFormatFlags.NoWrap;
                float limite = area.Bottom - altoFila;
                while (filaActual < grilla.Rows.Count && y <= limite)
                {
                    var fila = grilla.Rows[filaActual];
                    x = area.Left;
                    for (int i = 0; i < columnas.Count; i++)
                    {
                        var celda = new RectangleF(x, y, anchos[i], altoFila);
                        g.DrawRectangle(lapiz, celda.X, celda.Y, celda.Width, celda.Height);
                        var valor = fila.Cells[columnas[i].Index].FormattedValue?.ToString() ?? "";
                        formato.Alignment = EsNumerica(columnas[i]) ? StringAlignment.Far : StringAlignment.Near;
                        g.DrawString(valor, fuente, Brushes.Black, Achicar(celda), formato);
                        x += anchos[i];
                    }
                    y += altoFila;
                    filaActual++;
                }
            }

            string textoPie = (pie != null ? pie + "    " : "") + "Página " + pagina;
            g.DrawString(textoPie, fuente, Brushes.Black, area.Left, area.Bottom + 6);

            e.HasMorePages = filaActual < grilla.Rows.Count;
        }

        private static RectangleF Achicar(RectangleF r) => new RectangleF(r.X + Relleno, r.Y, r.Width - 2 * Relleno, r.Height);

        private static bool EsNumerica(DataGridViewColumn c) =>
            c.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight;
    }
}
