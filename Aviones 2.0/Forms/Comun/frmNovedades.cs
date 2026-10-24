using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Aviones_2._0.Common;

namespace Aviones_2._0.Forms.Comun
{
    /// <summary>Muestra las novedades: por cada cambio, lo pedido, lo hecho y en qué quedó distinto.</summary>
    public class frmNovedades : Form
    {
        private readonly CheckBox chkNoMostrar = new CheckBox
        {
            Text = "No volver a mostrar estas novedades",
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(3, 10, 3, 3)
        };

        /// <summary>Se marcó "No volver a mostrar".</summary>
        public bool NoVolverAMostrar => chkNoMostrar.Checked;

        public frmNovedades(IEnumerable<VersionNovedades> versiones, bool conCasilla)
        {
            Text = "Novedades de Aviones";
            Font = new Font("Arial", 10F);
            BackColor = Color.LightSteelBlue;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(900, 680);
            MinimumSize = new Size(600, 400);
            ShowInTaskbar = false;
            MinimizeBox = false;
            KeyPreview = true;

            var navegador = new WebBrowser
            {
                Dock = DockStyle.Fill,
                IsWebBrowserContextMenuEnabled = false,
                WebBrowserShortcutsEnabled = false,
                ScriptErrorsSuppressed = true,
                DocumentText = Html(versiones)
            };
            navegador.Navigating += (s, e) => { if (e.Url.ToString() != "about:blank") e.Cancel = true; };

            var cerrar = new Button { Text = "CERRAR", Size = new Size(140, 40), Margin = new Padding(6), DialogResult = DialogResult.OK };
            Estilo.Botones(cerrar);
            chkNoMostrar.Visible = conCasilla;

            var pie = new TableLayoutPanel { Dock = DockStyle.Bottom, AutoSize = true, ColumnCount = 2, Padding = new Padding(12, 6, 12, 6) };
            pie.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pie.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            pie.Controls.Add(chkNoMostrar, 0, 0);
            pie.Controls.Add(cerrar, 1, 0);

            Controls.Add(navegador);
            Controls.Add(pie);
            AcceptButton = cerrar;
            CancelButton = cerrar;
        }

        private static string Html(IEnumerable<VersionNovedades> versiones)
        {
            string E(string texto) => WebUtility.HtmlEncode(texto ?? "");
            var html = new StringBuilder(@"<!DOCTYPE html><html><head><meta charset='utf-8'><meta http-equiv='X-UA-Compatible' content='IE=edge'>
<style>
body { font-family: Arial, sans-serif; font-size: 15px; background: #eef2f8; color: #1f2933; margin: 18px 24px; }
h1 { font-size: 22px; margin: 0 0 4px 0; }
h2 { font-size: 18px; margin: 26px 0 10px 0; color: #2f4f4f; border-bottom: 2px solid #5f9ea0; padding-bottom: 4px; }
.intro { color: #52606d; margin-bottom: 8px; }
.cambio { background: #ffffff; border: 1px solid #c5d0de; border-radius: 6px; padding: 10px 14px; margin: 0 0 12px 0; }
.fila { margin: 4px 0; }
.etiqueta { display: inline-block; min-width: 92px; font-weight: bold; }
.pedido .etiqueta { color: #52606d; }
.hecho .etiqueta { color: #2e7d32; }
.distinto { background: #fff4e5; border-left: 4px solid #c85500; padding: 6px 10px; margin-top: 8px; }
.distinto .etiqueta { color: #c85500; display: block; }
</style></head><body>
<h1>Novedades</h1>
<div class='intro'>Por cada cambio: lo que pediste, lo que se hizo y, si quedó distinto a lo pedido, por qué.</div>");
            foreach (var version in versiones)
            {
                html.Append($"<h2>Versión {E(version.Version.ToString())}</h2>");
                foreach (var cambio in version.Cambios)
                {
                    html.Append("<div class='cambio'>");
                    html.Append($"<div class='fila pedido'><span class='etiqueta'>Pediste:</span> {E(cambio.Pedido)}</div>");
                    if (!string.IsNullOrEmpty(cambio.Hecho))
                    {
                        html.Append($"<div class='fila hecho'><span class='etiqueta'>Se hizo:</span> {E(cambio.Hecho)}</div>");
                    }
                    if (!string.IsNullOrEmpty(cambio.Diferencia))
                    {
                        html.Append($"<div class='distinto'><span class='etiqueta'>Distinto a lo pedido</span>{E(cambio.Diferencia)}</div>");
                    }
                    html.Append("</div>");
                }
            }
            return html.Append("</body></html>").ToString();
        }
    }
}
