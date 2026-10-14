using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Aviones_2._0.Common
{
    /// <summary>
    /// Imprime las columnas visibles de una grilla en hoja apaisada, con vista previa,
    /// repitiendo los títulos en cada página.
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
        private int filaActual;
        private int pagina;

        private ImpresionGrilla(DataGridView grilla, string titulo, string pie)
        {
            this.grilla = grilla;
            this.titulo = titulo;
            this.pie = pie;
        }

        public static void VistaPrevia(IWin32Window dueño, DataGridView grilla, string titulo, string pie = null)
        {
            if (grilla.Rows.Count == 0)
            {
                Mensajes.Aviso(dueño, "No hay nada para imprimir.");
                return;
            }
            var impresion = new ImpresionGrilla(grilla, titulo, pie);
            using (var documento = new PrintDocument { DocumentName = titulo })
            using (var vista = new PrintPreviewDialog { Document = documento, WindowState = FormWindowState.Maximized })
            {
                documento.DefaultPageSettings.Landscape = true;
                documento.BeginPrint += (s, e) => impresion.Empezar();
                documento.PrintPage += impresion.ImprimirPagina;
                vista.ShowDialog(dueño);
            }
        }

        private void Empezar()
        {
            columnas = grilla.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).OrderBy(c => c.DisplayIndex).ToList();
            filaActual = 0;
            pagina = 0;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            pagina++;
            var area = e.MarginBounds;
            var g = e.Graphics;
            float y = area.Top;

            g.DrawString(titulo, fuenteTitulo, Brushes.Black, area.Left, y);
            g.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fuente, Brushes.Black,
                area.Right - g.MeasureString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fuente).Width, y + 4);
            y += fuenteTitulo.GetHeight(g) + 8;

            // Anchos proporcionales a los de pantalla, ajustados al ancho de la hoja
            float total = columnas.Sum(c => (float)c.Width);
            float[] anchos = columnas.Select(c => c.Width / total * area.Width).ToArray();
            float altoEncabezado = fuenteEncabezado.GetHeight(g) * 2 + 6;
            float altoFila = fuente.GetHeight(g) + 6;

            using (var formato = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, LineAlignment = StringAlignment.Center })
            using (var lapiz = new Pen(Color.Gray, 0.5f))
            {
                float x = area.Left;
                for (int i = 0; i < columnas.Count; i++)
                {
                    var celda = new RectangleF(x, y, anchos[i], altoEncabezado);
                    g.FillRectangle(Brushes.Gainsboro, celda);
                    g.DrawRectangle(lapiz, celda.X, celda.Y, celda.Width, celda.Height);
                    g.DrawString(columnas[i].HeaderText.Trim(), fuenteEncabezado, Brushes.Black, Achicar(celda), formato);
                    x += anchos[i];
                }
                y += altoEncabezado;

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

        private static RectangleF Achicar(RectangleF r) => new RectangleF(r.X + 3, r.Y, r.Width - 6, r.Height);

        private static bool EsNumerica(DataGridViewColumn c) =>
            c.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight;
    }
}
