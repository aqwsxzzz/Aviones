namespace Aviones_2._0.Forms.Materiales
{
    partial class frmInformeValor
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
            this.labTitulo = new System.Windows.Forms.Label();
            this.dgvFamilias = new System.Windows.Forms.DataGridView();
            this.colFamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMateriales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSinStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labTotal = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botCerrar = new System.Windows.Forms.Button();
            this.botImprimir = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFamilias)).BeginInit();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 1;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.Controls.Add(this.labTitulo, 0, 0);
            this.tlpPrincipal.Controls.Add(this.dgvFamilias, 0, 1);
            this.tlpPrincipal.Controls.Add(this.labTotal, 0, 2);
            this.tlpPrincipal.Controls.Add(this.flpBotones, 0, 3);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(16);
            this.tlpPrincipal.RowCount = 4;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.TabIndex = 0;
            //
            // labTitulo
            //
            this.labTitulo.AutoSize = true;
            this.labTitulo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.labTitulo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.labTitulo.Name = "labTitulo";
            this.labTitulo.Text = "Valor de los materiales por familia";
            //
            // dgvFamilias
            //
            this.dgvFamilias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFamilia,
            this.colMateriales,
            this.colSinStock,
            this.colValor});
            this.dgvFamilias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFamilias.Name = "dgvFamilias";
            this.dgvFamilias.TabIndex = 0;
            //
            // colFamilia
            //
            this.colFamilia.DataPropertyName = "Familia";
            this.colFamilia.FillWeight = 160F;
            this.colFamilia.HeaderText = "Familia";
            this.colFamilia.Name = "colFamilia";
            //
            // colMateriales
            //
            this.colMateriales.DataPropertyName = "Materiales";
            this.colMateriales.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colMateriales.FillWeight = 70F;
            this.colMateriales.HeaderText = "Materiales";
            this.colMateriales.Name = "colMateriales";
            //
            // colSinStock
            //
            this.colSinStock.DataPropertyName = "SinStock";
            this.colSinStock.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSinStock.FillWeight = 70F;
            this.colSinStock.HeaderText = "Sin stock";
            this.colSinStock.Name = "colSinStock";
            //
            // colValor
            //
            this.colValor.DataPropertyName = "Valor";
            this.colValor.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colValor.DefaultCellStyle.Format = "N2";
            this.colValor.HeaderText = "Valor";
            this.colValor.Name = "colValor";
            //
            // labTotal
            //
            this.labTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTotal.AutoSize = true;
            this.labTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labTotal.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.labTotal.Name = "labTotal";
            this.labTotal.Text = "Total";
            //
            // flpBotones
            //
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botCerrar);
            this.flpBotones.Controls.Add(this.botImprimir);
            this.flpBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 1;
            //
            // botCerrar
            //
            this.botCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCerrar.Margin = new System.Windows.Forms.Padding(6);
            this.botCerrar.Name = "botCerrar";
            this.botCerrar.Size = new System.Drawing.Size(130, 40);
            this.botCerrar.TabIndex = 1;
            this.botCerrar.Text = "CERRAR";
            //
            // botImprimir
            //
            this.botImprimir.Margin = new System.Windows.Forms.Padding(6);
            this.botImprimir.Name = "botImprimir";
            this.botImprimir.Size = new System.Drawing.Size(130, 40);
            this.botImprimir.TabIndex = 0;
            this.botImprimir.Text = "IMPRIMIR";
            this.botImprimir.Click += new System.EventHandler(this.botImprimir_Click);
            //
            // frmInformeValor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.CancelButton = this.botCerrar;
            this.ClientSize = new System.Drawing.Size(680, 560);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInformeValor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Informe de valor";
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFamilias)).EndInit();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labTitulo;
        private System.Windows.Forms.DataGridView dgvFamilias;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFamilia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMateriales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSinStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValor;
        private System.Windows.Forms.Label labTotal;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botCerrar;
        private System.Windows.Forms.Button botImprimir;
    }
}
