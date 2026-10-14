namespace Aviones_2._0.Forms.Compras
{
    partial class frmPedido
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tlpPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.labMaterial = new System.Windows.Forms.Label();
            this.labStock = new System.Windows.Forms.Label();
            this.labCantidad = new System.Windows.Forms.Label();
            this.flpCantidad = new System.Windows.Forms.FlowLayoutPanel();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.labUnidad = new System.Windows.Forms.Label();
            this.labNota = new System.Windows.Forms.Label();
            this.txtNota = new System.Windows.Forms.TextBox();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botCancelar = new System.Windows.Forms.Button();
            this.botGuardar = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            this.flpCantidad.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.AutoSize = true;
            this.tlpPrincipal.ColumnCount = 2;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 380F));
            this.tlpPrincipal.Controls.Add(this.labMaterial, 0, 0);
            this.tlpPrincipal.Controls.Add(this.labStock, 0, 1);
            this.tlpPrincipal.Controls.Add(this.labCantidad, 0, 2);
            this.tlpPrincipal.Controls.Add(this.flpCantidad, 1, 2);
            this.tlpPrincipal.Controls.Add(this.labNota, 0, 3);
            this.tlpPrincipal.Controls.Add(this.txtNota, 1, 3);
            this.tlpPrincipal.Controls.Add(this.flpBotones, 0, 4);
            this.tlpPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(16);
            this.tlpPrincipal.RowCount = 5;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.SetColumnSpan(this.labMaterial, 2);
            this.tlpPrincipal.SetColumnSpan(this.labStock, 2);
            this.tlpPrincipal.SetColumnSpan(this.flpBotones, 2);
            this.tlpPrincipal.TabIndex = 0;
            //
            // labMaterial
            //
            this.labMaterial.AutoSize = true;
            this.labMaterial.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labMaterial.MaximumSize = new System.Drawing.Size(490, 0);
            this.labMaterial.Name = "labMaterial";
            this.labMaterial.Text = "Material";
            //
            // labStock
            //
            this.labStock.AutoSize = true;
            this.labStock.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labStock.Margin = new System.Windows.Forms.Padding(3, 6, 3, 14);
            this.labStock.MaximumSize = new System.Drawing.Size(490, 0);
            this.labStock.Name = "labStock";
            this.labStock.Text = "Stock";
            //
            // labCantidad
            //
            this.labCantidad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCantidad.AutoSize = true;
            this.labCantidad.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labCantidad.Name = "labCantidad";
            this.labCantidad.Text = "Comprar *";
            //
            // flpCantidad
            //
            this.flpCantidad.AutoSize = true;
            this.flpCantidad.Controls.Add(this.txtCantidad);
            this.flpCantidad.Controls.Add(this.labUnidad);
            this.flpCantidad.Margin = new System.Windows.Forms.Padding(0);
            this.flpCantidad.Name = "flpCantidad";
            this.flpCantidad.TabIndex = 0;
            //
            // txtCantidad
            //
            this.txtCantidad.Font = new System.Drawing.Font("Arial", 12F);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(110, 26);
            this.txtCantidad.TabIndex = 0;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // labUnidad
            //
            this.labUnidad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labUnidad.AutoSize = true;
            this.labUnidad.Name = "labUnidad";
            this.labUnidad.Text = "Unidades";
            //
            // labNota
            //
            this.labNota.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labNota.AutoSize = true;
            this.labNota.Name = "labNota";
            this.labNota.Text = "Nota";
            //
            // txtNota
            //
            this.txtNota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNota.MaxLength = 200;
            this.txtNota.Name = "txtNota";
            this.txtNota.TabIndex = 1;
            //
            // flpBotones
            //
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botCancelar);
            this.flpBotones.Controls.Add(this.botGuardar);
            this.flpBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Margin = new System.Windows.Forms.Padding(3, 14, 3, 3);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 2;
            //
            // botCancelar
            //
            this.botCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(130, 40);
            this.botCancelar.TabIndex = 1;
            this.botCancelar.Text = "CANCELAR";
            //
            // botGuardar
            //
            this.botGuardar.Name = "botGuardar";
            this.botGuardar.Size = new System.Drawing.Size(150, 40);
            this.botGuardar.TabIndex = 0;
            this.botGuardar.Text = "AGREGAR PEDIDO";
            this.botGuardar.Click += new System.EventHandler(this.botGuardar_Click);
            //
            // frmPedido
            //
            this.AcceptButton = this.botGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.CancelButton = this.botCancelar;
            this.ClientSize = new System.Drawing.Size(532, 260);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPedido";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pedido de compra";
            this.Load += new System.EventHandler(this.frmPedido_Load);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.flpCantidad.ResumeLayout(false);
            this.flpCantidad.PerformLayout();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labMaterial;
        private System.Windows.Forms.Label labStock;
        private System.Windows.Forms.Label labCantidad;
        private System.Windows.Forms.FlowLayoutPanel flpCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label labUnidad;
        private System.Windows.Forms.Label labNota;
        private System.Windows.Forms.TextBox txtNota;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botGuardar;
    }
}
