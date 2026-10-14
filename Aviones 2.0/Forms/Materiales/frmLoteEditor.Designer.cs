namespace Aviones_2._0.Forms.Materiales
{
    partial class frmLoteEditor
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
            this.labFabricante = new System.Windows.Forms.Label();
            this.txtFabricante = new System.Windows.Forms.TextBox();
            this.labUbicacion = new System.Windows.Forms.Label();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.labCantidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.labValor = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.labFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.labUso = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botCancelar = new System.Windows.Forms.Button();
            this.botGuardar = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 2;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.Controls.Add(this.labMaterial, 0, 0);
            this.tlpPrincipal.Controls.Add(this.labFabricante, 0, 1);
            this.tlpPrincipal.Controls.Add(this.txtFabricante, 1, 1);
            this.tlpPrincipal.Controls.Add(this.labUbicacion, 0, 2);
            this.tlpPrincipal.Controls.Add(this.txtUbicacion, 1, 2);
            this.tlpPrincipal.Controls.Add(this.labCantidad, 0, 3);
            this.tlpPrincipal.Controls.Add(this.txtCantidad, 1, 3);
            this.tlpPrincipal.Controls.Add(this.labValor, 0, 4);
            this.tlpPrincipal.Controls.Add(this.txtValor, 1, 4);
            this.tlpPrincipal.Controls.Add(this.labFecha, 0, 5);
            this.tlpPrincipal.Controls.Add(this.dtpFecha, 1, 5);
            this.tlpPrincipal.Controls.Add(this.labUso, 0, 6);
            this.tlpPrincipal.Controls.Add(this.flpBotones, 0, 8);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(16);
            this.tlpPrincipal.RowCount = 9;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.SetColumnSpan(this.labMaterial, 2);
            this.tlpPrincipal.SetColumnSpan(this.labUso, 2);
            this.tlpPrincipal.SetColumnSpan(this.flpBotones, 2);
            this.tlpPrincipal.TabIndex = 0;
            //
            // labMaterial
            //
            this.labMaterial.AutoSize = true;
            this.labMaterial.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labMaterial.Margin = new System.Windows.Forms.Padding(3, 0, 3, 12);
            this.labMaterial.Name = "labMaterial";
            this.labMaterial.Text = "Material";
            //
            // labFabricante
            //
            this.labFabricante.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFabricante.AutoSize = true;
            this.labFabricante.Name = "labFabricante";
            this.labFabricante.Text = "Fabricante";
            //
            // txtFabricante
            //
            this.txtFabricante.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFabricante.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFabricante.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFabricante.MaxLength = 100;
            this.txtFabricante.Name = "txtFabricante";
            this.txtFabricante.TabIndex = 0;
            //
            // labUbicacion
            //
            this.labUbicacion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labUbicacion.AutoSize = true;
            this.labUbicacion.Name = "labUbicacion";
            this.labUbicacion.Text = "Ubicación";
            //
            // txtUbicacion
            //
            this.txtUbicacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUbicacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUbicacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUbicacion.MaxLength = 100;
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.TabIndex = 1;
            //
            // labCantidad
            //
            this.labCantidad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCantidad.AutoSize = true;
            this.labCantidad.Name = "labCantidad";
            this.labCantidad.Text = "Cantidad *";
            //
            // txtCantidad
            //
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(120, 23);
            this.txtCantidad.TabIndex = 2;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // labValor
            //
            this.labValor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labValor.AutoSize = true;
            this.labValor.Name = "labValor";
            this.labValor.Text = "Valor unitario";
            //
            // txtValor
            //
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(120, 23);
            this.txtValor.TabIndex = 3;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // labFecha
            //
            this.labFecha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFecha.AutoSize = true;
            this.labFecha.Name = "labFecha";
            this.labFecha.Text = "Fecha ingreso";
            //
            // dtpFecha
            //
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(120, 23);
            this.dtpFecha.TabIndex = 4;
            //
            // labUso
            //
            this.labUso.AutoSize = true;
            this.labUso.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labUso.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.labUso.Name = "labUso";
            this.labUso.Text = "";
            //
            // flpBotones
            //
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botCancelar);
            this.flpBotones.Controls.Add(this.botGuardar);
            this.flpBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 5;
            //
            // botCancelar
            //
            this.botCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCancelar.Margin = new System.Windows.Forms.Padding(6);
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(130, 40);
            this.botCancelar.TabIndex = 1;
            this.botCancelar.Text = "CANCELAR";
            //
            // botGuardar
            //
            this.botGuardar.Margin = new System.Windows.Forms.Padding(6);
            this.botGuardar.Name = "botGuardar";
            this.botGuardar.Size = new System.Drawing.Size(130, 40);
            this.botGuardar.TabIndex = 0;
            this.botGuardar.Text = "GUARDAR";
            this.botGuardar.Click += new System.EventHandler(this.botGuardar_Click);
            //
            // frmLoteEditor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.CancelButton = this.botCancelar;
            this.ClientSize = new System.Drawing.Size(520, 400);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoteEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lote";
            this.Load += new System.EventHandler(this.frmLoteEditor_Load);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labMaterial;
        private System.Windows.Forms.Label labFabricante;
        private System.Windows.Forms.TextBox txtFabricante;
        private System.Windows.Forms.Label labUbicacion;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.Label labCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label labValor;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label labFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label labUso;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botGuardar;
    }
}
