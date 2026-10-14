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
            this.flpRapidos = new System.Windows.Forms.FlowLayoutPanel();
            this.botTodoNuevo = new System.Windows.Forms.Button();
            this.botTodoUsado = new System.Windows.Forms.Button();
            this.botTodoDescarte = new System.Windows.Forms.Button();
            this.tlpReparto = new System.Windows.Forms.TableLayoutPanel();
            this.labNuevo = new System.Windows.Forms.Label();
            this.txtNuevo = new System.Windows.Forms.TextBox();
            this.labNuevoAyuda = new System.Windows.Forms.Label();
            this.labUsado = new System.Windows.Forms.Label();
            this.txtUsado = new System.Windows.Forms.TextBox();
            this.labUsadoAyuda = new System.Windows.Forms.Label();
            this.labDescarte = new System.Windows.Forms.Label();
            this.labDescarteCantidad = new System.Windows.Forms.Label();
            this.labDescarteAyuda = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botCancelar = new System.Windows.Forms.Button();
            this.botAceptar = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            this.flpRapidos.SuspendLayout();
            this.tlpReparto.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.AutoSize = true;
            this.tlpPrincipal.ColumnCount = 1;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 480F));
            this.tlpPrincipal.Controls.Add(this.labPregunta, 0, 0);
            this.tlpPrincipal.Controls.Add(this.labDetalle, 0, 1);
            this.tlpPrincipal.Controls.Add(this.flpRapidos, 0, 2);
            this.tlpPrincipal.Controls.Add(this.tlpReparto, 0, 3);
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
            this.tlpPrincipal.TabIndex = 0;
            //
            // labPregunta
            //
            this.labPregunta.AutoSize = true;
            this.labPregunta.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labPregunta.MaximumSize = new System.Drawing.Size(470, 0);
            this.labPregunta.Name = "labPregunta";
            this.labPregunta.Text = "¿Qué pasa con lo que se quita?";
            //
            // labDetalle
            //
            this.labDetalle.AutoSize = true;
            this.labDetalle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 10);
            this.labDetalle.MaximumSize = new System.Drawing.Size(470, 0);
            this.labDetalle.Name = "labDetalle";
            this.labDetalle.Text = "Material";
            //
            // flpRapidos
            //
            this.flpRapidos.AutoSize = true;
            this.flpRapidos.Controls.Add(this.botTodoNuevo);
            this.flpRapidos.Controls.Add(this.botTodoUsado);
            this.flpRapidos.Controls.Add(this.botTodoDescarte);
            this.flpRapidos.Name = "flpRapidos";
            this.flpRapidos.TabIndex = 0;
            //
            // botTodoNuevo
            //
            this.botTodoNuevo.Name = "botTodoNuevo";
            this.botTodoNuevo.Size = new System.Drawing.Size(150, 48);
            this.botTodoNuevo.TabIndex = 0;
            this.botTodoNuevo.Text = "TODO VUELVE\r\nNUEVO";
            this.botTodoNuevo.Click += new System.EventHandler(this.botTodoNuevo_Click);
            //
            // botTodoUsado
            //
            this.botTodoUsado.Name = "botTodoUsado";
            this.botTodoUsado.Size = new System.Drawing.Size(150, 48);
            this.botTodoUsado.TabIndex = 1;
            this.botTodoUsado.Text = "TODO VUELVE\r\nUSADO";
            this.botTodoUsado.Click += new System.EventHandler(this.botTodoUsado_Click);
            //
            // botTodoDescarte
            //
            this.botTodoDescarte.Name = "botTodoDescarte";
            this.botTodoDescarte.Size = new System.Drawing.Size(150, 48);
            this.botTodoDescarte.TabIndex = 2;
            this.botTodoDescarte.Text = "TODO SE\r\nDESCARTA";
            this.botTodoDescarte.Click += new System.EventHandler(this.botTodoDescarte_Click);
            //
            // tlpReparto
            //
            this.tlpReparto.AutoSize = true;
            this.tlpReparto.ColumnCount = 3;
            this.tlpReparto.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tlpReparto.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpReparto.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpReparto.Controls.Add(this.labNuevo, 0, 0);
            this.tlpReparto.Controls.Add(this.txtNuevo, 1, 0);
            this.tlpReparto.Controls.Add(this.labNuevoAyuda, 2, 0);
            this.tlpReparto.Controls.Add(this.labUsado, 0, 1);
            this.tlpReparto.Controls.Add(this.txtUsado, 1, 1);
            this.tlpReparto.Controls.Add(this.labUsadoAyuda, 2, 1);
            this.tlpReparto.Controls.Add(this.labDescarte, 0, 2);
            this.tlpReparto.Controls.Add(this.labDescarteCantidad, 1, 2);
            this.tlpReparto.Controls.Add(this.labDescarteAyuda, 2, 2);
            this.tlpReparto.Margin = new System.Windows.Forms.Padding(3, 14, 3, 3);
            this.tlpReparto.Name = "tlpReparto";
            this.tlpReparto.RowCount = 3;
            this.tlpReparto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpReparto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpReparto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpReparto.TabIndex = 1;
            //
            // labNuevo
            //
            this.labNuevo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labNuevo.AutoSize = true;
            this.labNuevo.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labNuevo.Name = "labNuevo";
            this.labNuevo.Text = "Vuelve como nuevo";
            //
            // txtNuevo
            //
            this.txtNuevo.Font = new System.Drawing.Font("Arial", 12F);
            this.txtNuevo.Name = "txtNuevo";
            this.txtNuevo.Size = new System.Drawing.Size(100, 26);
            this.txtNuevo.TabIndex = 0;
            this.txtNuevo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNuevo.TextChanged += new System.EventHandler(this.Cantidades_Changed);
            //
            // labNuevoAyuda
            //
            this.labNuevoAyuda.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labNuevoAyuda.AutoSize = true;
            this.labNuevoAyuda.ForeColor = System.Drawing.Color.DimGray;
            this.labNuevoAyuda.Name = "labNuevoAyuda";
            this.labNuevoAyuda.Text = "no se llegó a usar";
            //
            // labUsado
            //
            this.labUsado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labUsado.AutoSize = true;
            this.labUsado.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labUsado.Name = "labUsado";
            this.labUsado.Text = "Vuelve como usado";
            //
            // txtUsado
            //
            this.txtUsado.Font = new System.Drawing.Font("Arial", 12F);
            this.txtUsado.Name = "txtUsado";
            this.txtUsado.Size = new System.Drawing.Size(100, 26);
            this.txtUsado.TabIndex = 1;
            this.txtUsado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUsado.TextChanged += new System.EventHandler(this.Cantidades_Changed);
            //
            // labUsadoAyuda
            //
            this.labUsadoAyuda.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labUsadoAyuda.AutoSize = true;
            this.labUsadoAyuda.ForeColor = System.Drawing.Color.DimGray;
            this.labUsadoAyuda.Name = "labUsadoAyuda";
            this.labUsadoAyuda.Text = "se recicla";
            //
            // labDescarte
            //
            this.labDescarte.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDescarte.AutoSize = true;
            this.labDescarte.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labDescarte.Name = "labDescarte";
            this.labDescarte.Text = "Se descarta";
            //
            // labDescarteCantidad
            //
            this.labDescarteCantidad.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labDescarteCantidad.AutoSize = true;
            this.labDescarteCantidad.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labDescarteCantidad.ForeColor = System.Drawing.Color.Firebrick;
            this.labDescarteCantidad.Margin = new System.Windows.Forms.Padding(3, 6, 10, 6);
            this.labDescarteCantidad.Name = "labDescarteCantidad";
            this.labDescarteCantidad.Text = "0";
            //
            // labDescarteAyuda
            //
            this.labDescarteAyuda.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDescarteAyuda.AutoSize = true;
            this.labDescarteAyuda.ForeColor = System.Drawing.Color.DimGray;
            this.labDescarteAyuda.Name = "labDescarteAyuda";
            this.labDescarteAyuda.Text = "roto o consumido";
            //
            // flpBotones
            //
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botCancelar);
            this.flpBotones.Controls.Add(this.botAceptar);
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
            // botAceptar
            //
            this.botAceptar.Name = "botAceptar";
            this.botAceptar.Size = new System.Drawing.Size(130, 40);
            this.botAceptar.TabIndex = 0;
            this.botAceptar.Text = "ACEPTAR";
            this.botAceptar.Click += new System.EventHandler(this.botAceptar_Click);
            //
            // frmDestino
            //
            this.AcceptButton = this.botAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.CancelButton = this.botCancelar;
            this.ClientSize = new System.Drawing.Size(512, 360);
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
            this.flpRapidos.ResumeLayout(false);
            this.tlpReparto.ResumeLayout(false);
            this.tlpReparto.PerformLayout();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labPregunta;
        private System.Windows.Forms.Label labDetalle;
        private System.Windows.Forms.FlowLayoutPanel flpRapidos;
        private System.Windows.Forms.Button botTodoNuevo;
        private System.Windows.Forms.Button botTodoUsado;
        private System.Windows.Forms.Button botTodoDescarte;
        private System.Windows.Forms.TableLayoutPanel tlpReparto;
        private System.Windows.Forms.Label labNuevo;
        private System.Windows.Forms.TextBox txtNuevo;
        private System.Windows.Forms.Label labNuevoAyuda;
        private System.Windows.Forms.Label labUsado;
        private System.Windows.Forms.TextBox txtUsado;
        private System.Windows.Forms.Label labUsadoAyuda;
        private System.Windows.Forms.Label labDescarte;
        private System.Windows.Forms.Label labDescarteCantidad;
        private System.Windows.Forms.Label labDescarteAyuda;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botAceptar;
    }
}
