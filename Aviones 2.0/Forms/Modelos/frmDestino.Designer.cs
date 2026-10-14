namespace Aviones_2._0.Forms.Modelos
{
    partial class frmDestino
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
            this.labDetalle = new System.Windows.Forms.Label();
            this.botNuevo = new System.Windows.Forms.Button();
            this.botUsado = new System.Windows.Forms.Button();
            this.botDescartar = new System.Windows.Forms.Button();
            this.botCancelar = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.AutoSize = true;
            this.tlpPrincipal.ColumnCount = 1;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.tlpPrincipal.Controls.Add(this.labPregunta, 0, 0);
            this.tlpPrincipal.Controls.Add(this.labDetalle, 0, 1);
            this.tlpPrincipal.Controls.Add(this.botNuevo, 0, 2);
            this.tlpPrincipal.Controls.Add(this.botUsado, 0, 3);
            this.tlpPrincipal.Controls.Add(this.botDescartar, 0, 4);
            this.tlpPrincipal.Controls.Add(this.botCancelar, 0, 5);
            this.tlpPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(16);
            this.tlpPrincipal.RowCount = 6;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.TabIndex = 0;
            //
            // labPregunta
            //
            this.labPregunta.AutoSize = true;
            this.labPregunta.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labPregunta.MaximumSize = new System.Drawing.Size(410, 0);
            this.labPregunta.Name = "labPregunta";
            this.labPregunta.Text = "¿Qué pasa con lo que se quita?";
            //
            // labDetalle
            //
            this.labDetalle.AutoSize = true;
            this.labDetalle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 12);
            this.labDetalle.MaximumSize = new System.Drawing.Size(410, 0);
            this.labDetalle.Name = "labDetalle";
            this.labDetalle.Text = "Material";
            //
            // botNuevo
            //
            this.botNuevo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.botNuevo.Name = "botNuevo";
            this.botNuevo.Size = new System.Drawing.Size(410, 52);
            this.botNuevo.TabIndex = 0;
            this.botNuevo.Text = "VUELVE COMO NUEVO\r\n(no se llegó a usar)";
            this.botNuevo.Click += new System.EventHandler(this.botNuevo_Click);
            //
            // botUsado
            //
            this.botUsado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.botUsado.Name = "botUsado";
            this.botUsado.Size = new System.Drawing.Size(410, 52);
            this.botUsado.TabIndex = 1;
            this.botUsado.Text = "VUELVE COMO USADO\r\n(se recicla)";
            this.botUsado.Click += new System.EventHandler(this.botUsado_Click);
            //
            // botDescartar
            //
            this.botDescartar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.botDescartar.Name = "botDescartar";
            this.botDescartar.Size = new System.Drawing.Size(410, 52);
            this.botDescartar.TabIndex = 2;
            this.botDescartar.Text = "SE DESCARTA\r\n(roto o consumido)";
            this.botDescartar.Click += new System.EventHandler(this.botDescartar_Click);
            //
            // botCancelar
            //
            this.botCancelar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.botCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCancelar.Margin = new System.Windows.Forms.Padding(3, 14, 3, 3);
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(130, 38);
            this.botCancelar.TabIndex = 3;
            this.botCancelar.Text = "CANCELAR";
            //
            // frmDestino
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.CancelButton = this.botCancelar;
            this.ClientSize = new System.Drawing.Size(452, 360);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDestino";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Material que sale del modelo";
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labPregunta;
        private System.Windows.Forms.Label labDetalle;
        private System.Windows.Forms.Button botNuevo;
        private System.Windows.Forms.Button botUsado;
        private System.Windows.Forms.Button botDescartar;
        private System.Windows.Forms.Button botCancelar;
    }
}
