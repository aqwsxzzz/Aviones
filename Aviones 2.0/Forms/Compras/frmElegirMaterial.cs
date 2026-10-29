using System;
using System.Drawing;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;
using Aviones_2._0.Forms.Materiales;

namespace Aviones_2._0.Forms.Compras
{
    /// <summary>
    /// Elegir cualquier material para pedirlo (no sólo los del stock crítico).
    /// Si todavía no está cargado, se crea desde acá con "MATERIAL NUEVO...".
    /// </summary>
    public class frmElegirMaterial : Form
    {
        private readonly MaterialRepositorio materiales = new MaterialRepositorio();
        private readonly TextBox txtBuscar = new TextBox { Width = 320 };
        private readonly DataGridView dgv = new DataGridView { Dock = DockStyle.Fill };
        private readonly Button botElegir = new Button { Text = "ELEGIR", Size = new Size(140, 40), Margin = new Padding(6) };
        private readonly Button botNuevo = new Button { Text = "MATERIAL NUEVO...", Size = new Size(190, 40), Margin = new Padding(6) };
        private readonly Button botCancelar = new Button { Text = "CANCELAR", Size = new Size(140, 40), Margin = new Padding(6), DialogResult = DialogResult.Cancel };

        /// <summary>El material elegido (o el recién creado).</summary>
        public int IdMaterial { get; private set; }

        public frmElegirMaterial()
        {
            Text = "Agregar a la lista de compras";
            Font = new Font("Arial", 10F);
            BackColor = Color.Honeydew;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1000, 620);
            MinimumSize = new Size(700, 400);
            ShowInTaskbar = false;
            MinimizeBox = false;

            Estilo.Grilla(dgv);
            dgv.BackgroundColor = BackColor;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.AddRange(
                Columna(nameof(MaterialStock.Familia), "Familia", 90),
                Columna(nameof(MaterialStock.Descripcion), "Material", 260),
                Columna(nameof(MaterialStock.Codigo), "Código", 80),
                Columna(nameof(MaterialStock.TipoUnidad), "Unidad", 70),
                Columna(nameof(MaterialStock.Disponible), "Hay", 55, "0.###"),
                Columna(nameof(MaterialStock.PedidoPendiente), "Ya pedido", 70, "0.###"));
            dgv.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Elegir(); };
            dgv.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.Handled = true; Elegir(); } };
            OrdenGrilla.Habilitar(dgv);

            var arriba = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(12, 10, 12, 6) };
            arriba.Controls.Add(new Label { Text = "Buscar:", AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 6, 3, 3) });
            arriba.Controls.Add(txtBuscar);
            arriba.Controls.Add(new Label
            {
                Text = "Elija el material, o cree uno nuevo si todavía no está cargado.",
                AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(20, 6, 3, 3), ForeColor = Color.DimGray
            });
            txtBuscar.TextChanged += (s, e) => Cargar();
            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Down) { dgv.Focus(); e.Handled = true; }
                else if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Elegir(); }
            };

            var abajo = new FlowLayoutPanel { Dock = DockStyle.Bottom, AutoSize = true, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(12, 6, 12, 6) };
            abajo.Controls.Add(botCancelar);
            abajo.Controls.Add(botElegir);
            abajo.Controls.Add(botNuevo);
            Estilo.Botones(botElegir, botNuevo);
            Estilo.BotonSecundario(botCancelar);
            botElegir.Click += (s, e) => Elegir();
            botNuevo.Click += (s, e) => CrearNuevo();

            var medio = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12, 0, 12, 0) };
            medio.Controls.Add(dgv);
            Controls.Add(medio);
            Controls.Add(arriba);
            Controls.Add(abajo);
            CancelButton = botCancelar;
            Load += (s, e) => { Cargar(); ActiveControl = txtBuscar; };
        }

        private static DataGridViewTextBoxColumn Columna(string propiedad, string titulo, float peso, string formato = null)
        {
            var c = new DataGridViewTextBoxColumn { DataPropertyName = propiedad, HeaderText = titulo, FillWeight = peso, Name = "col" + propiedad };
            if (formato != null)
            {
                c.DefaultCellStyle.Format = formato;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            return c;
        }

        private void Cargar()
        {
            Mensajes.Intentar(this, () =>
            {
                var lista = materiales.ListarStock(new FiltroMateriales { Texto = txtBuscar.Text });
                dgv.DataSource = OrdenGrilla.Aplicar(dgv, lista);
            });
            botElegir.Enabled = dgv.Rows.Count > 0;
        }

        private void Elegir()
        {
            if (!(dgv.CurrentRow?.DataBoundItem is MaterialStock m))
            {
                return;
            }
            if (!m.SeRepone && !Mensajes.Confirmar(this,
                    "Este material está marcado como que no se repone (no se vuelve a comprar).\n\n¿Agregarlo a la lista de compras igual?", "Comprar"))
            {
                return;
            }
            IdMaterial = m.IdMaterial;
            DialogResult = DialogResult.OK;
        }

        private void CrearNuevo()
        {
            using (var editor = new frmMaterialEditor(null, null))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    IdMaterial = editor.IdMaterial;
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
