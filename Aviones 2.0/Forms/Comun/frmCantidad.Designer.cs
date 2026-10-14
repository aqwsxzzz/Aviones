namespace Aviones_2._0.Forms.Comun
{
    partial class frmCantidad
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
            this.labPregunta = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.labAyuda = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botCancelar = new System.Windows.Forms.Button();
            this.botAceptar = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.AutoSize = true;
            this.tlpPrincipal.ColumnCount = 1;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 380F));
            this.tlpPrincipal.Controls.Add(this.labPregunta, 0, 0);
            this.tlpPrincipal.Controls.Add(this.txtCantidad, 0, 1);
            this.tlpPrincipal.Controls.Add(this.labAyuda, 0, 2);
            this.tlpPrincipal.Controls.Add(this.flpBotones, 0, 3);
            this.tlpPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(16);
            this.tlpPrincipal.RowCount = 4;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.TabIndex = 0;
            //
            // labPregunta
            //
            this.labPregunta.AutoSize = true;
            this.labPregunta.MaximumSize = new System.Drawing.Size(370, 0);
            this.labPregunta.Name = "labPregunta";
            this.labPregunta.Text = "Cantidad";
            //
            // txtCantidad
            //
            this.txtCantidad.Font = new System.Drawing.Font("Arial", 14F);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(150, 29);
            this.txtCantidad.TabIndex = 0;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // labAyuda
            //
            this.labAyuda.AutoSize = true;
            this.labAyuda.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labAyuda.MaximumSize = new System.Drawing.Size(370, 0);
            this.labAyuda.Name = "labAyuda";
            this.labAyuda.Text = "";
            //
            // flpBotones
            //
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botCancelar);
            this.flpBotones.Controls.Add(this.botAceptar);
            this.flpBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 1;
            //
            // botCancelar
            //
            this.botCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(120, 38);
            this.botCancelar.TabIndex = 1;
            this.botCancelar.Text = "CANCELAR";
            //
            // botAceptar
            //
            this.botAceptar.Name = "botAceptar";
            this.botAceptar.Size = new System.Drawing.Size(120, 38);
            this.botAceptar.TabIndex = 0;
            this.botAceptar.Text = "ACEPTAR";
            this.botAceptar.Click += new System.EventHandler(this.botAceptar_Click);
            //
            // frmCantidad
            //
            this.AcceptButton = this.botAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.CancelButton = this.botCancelar;
            this.ClientSize = new System.Drawing.Size(412, 200);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCantidad";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cantidad";
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labPregunta;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label labAyuda;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botAceptar;
    }
}
