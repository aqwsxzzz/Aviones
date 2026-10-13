namespace Aviones_2._0.Forms.Compras
{
    partial class frmFaltanteCom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.botCancelaFaltaCom = new System.Windows.Forms.Button();
            this.botAceptaFaltaCom = new System.Windows.Forms.Button();
            this.labTituloDgvMateriales = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.picFotoFaltante = new System.Windows.Forms.PictureBox();
            this.texCantSolicitadaFal = new System.Windows.Forms.TextBox();
            this.labCantSolicitadaFal = new System.Windows.Forms.Label();
            this.labTituloTotalPedidoFal = new System.Windows.Forms.Label();
            this.labValorPedidoFal = new System.Windows.Forms.Label();
            this.panTotalPedidoFal = new System.Windows.Forms.Panel();
            this.labTitulodgvMaterialesacomprar = new System.Windows.Forms.Label();
            this.picCamtSolicitadaFal = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picFotoFaltante)).BeginInit();
            this.panTotalPedidoFal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamtSolicitadaFal)).BeginInit();
            this.SuspendLayout();
            // 
            // botCancelaFaltaCom
            // 
            this.botCancelaFaltaCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botCancelaFaltaCom.Location = new System.Drawing.Point(1016, 543);
            this.botCancelaFaltaCom.Name = "botCancelaFaltaCom";
            this.botCancelaFaltaCom.Size = new System.Drawing.Size(155, 59);
            this.botCancelaFaltaCom.TabIndex = 0;
            this.botCancelaFaltaCom.Text = "CANCELAR";
            this.botCancelaFaltaCom.UseVisualStyleBackColor = true;
            this.botCancelaFaltaCom.Click += new System.EventHandler(this.button1_Click);
            // 
            // botAceptaFaltaCom
            // 
            this.botAceptaFaltaCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botAceptaFaltaCom.Location = new System.Drawing.Point(843, 544);
            this.botAceptaFaltaCom.Name = "botAceptaFaltaCom";
            this.botAceptaFaltaCom.Size = new System.Drawing.Size(155, 59);
            this.botAceptaFaltaCom.TabIndex = 1;
            this.botAceptaFaltaCom.Text = "ACEPTAR";
            this.botAceptaFaltaCom.UseVisualStyleBackColor = true;
            this.botAceptaFaltaCom.Visible = false;
            // 
            // labTituloDgvMateriales
            // 
            this.labTituloDgvMateriales.AutoSize = true;
            this.labTituloDgvMateriales.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTituloDgvMateriales.ForeColor = System.Drawing.Color.White;
            this.labTituloDgvMateriales.Location = new System.Drawing.Point(424, 11);
            this.labTituloDgvMateriales.Name = "labTituloDgvMateriales";
            this.labTituloDgvMateriales.Size = new System.Drawing.Size(514, 25);
            this.labTituloDgvMateriales.TabIndex = 2;
            this.labTituloDgvMateriales.Text = "Listados de materiales con existencias mínimas";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            // 
            // picFotoFaltante
            // 
            this.picFotoFaltante.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFotoFaltante.Location = new System.Drawing.Point(47, 143);
            this.picFotoFaltante.Name = "picFotoFaltante";
            this.picFotoFaltante.Size = new System.Drawing.Size(218, 196);
            this.picFotoFaltante.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFotoFaltante.TabIndex = 3;
            this.picFotoFaltante.TabStop = false;
            this.picFotoFaltante.Visible = false;
            // 
            // texCantSolicitadaFal
            // 
            this.texCantSolicitadaFal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texCantSolicitadaFal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texCantSolicitadaFal.Location = new System.Drawing.Point(642, 378);
            this.texCantSolicitadaFal.Name = "texCantSolicitadaFal";
            this.texCantSolicitadaFal.Size = new System.Drawing.Size(102, 28);
            this.texCantSolicitadaFal.TabIndex = 4;
            this.texCantSolicitadaFal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.texCantSolicitadaFal.Visible = false;
            this.texCantSolicitadaFal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.texCantSolicitadaFal_KeyPress);
            // 
            // labCantSolicitadaFal
            // 
            this.labCantSolicitadaFal.AutoSize = true;
            this.labCantSolicitadaFal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCantSolicitadaFal.Location = new System.Drawing.Point(605, 319);
            this.labCantSolicitadaFal.Name = "labCantSolicitadaFal";
            this.labCantSolicitadaFal.Size = new System.Drawing.Size(162, 20);
            this.labCantSolicitadaFal.TabIndex = 0;
            this.labCantSolicitadaFal.Text = "Cantidad solicitada";
            this.labCantSolicitadaFal.Visible = false;
            // 
            // labTituloTotalPedidoFal
            // 
            this.labTituloTotalPedidoFal.AutoSize = true;
            this.labTituloTotalPedidoFal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTituloTotalPedidoFal.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labTituloTotalPedidoFal.Location = new System.Drawing.Point(64, 27);
            this.labTituloTotalPedidoFal.Name = "labTituloTotalPedidoFal";
            this.labTituloTotalPedidoFal.Size = new System.Drawing.Size(146, 24);
            this.labTituloTotalPedidoFal.TabIndex = 6;
            this.labTituloTotalPedidoFal.Text = "Total del pedido";
            // 
            // labValorPedidoFal
            // 
            this.labValorPedidoFal.AutoSize = true;
            this.labValorPedidoFal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labValorPedidoFal.Location = new System.Drawing.Point(288, 23);
            this.labValorPedidoFal.Name = "labValorPedidoFal";
            this.labValorPedidoFal.Size = new System.Drawing.Size(85, 29);
            this.labValorPedidoFal.TabIndex = 7;
            this.labValorPedidoFal.Text = "label2";
            // 
            // panTotalPedidoFal
            // 
            this.panTotalPedidoFal.BackColor = System.Drawing.Color.Silver;
            this.panTotalPedidoFal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panTotalPedidoFal.Controls.Add(this.labValorPedidoFal);
            this.panTotalPedidoFal.Controls.Add(this.labTituloTotalPedidoFal);
            this.panTotalPedidoFal.Location = new System.Drawing.Point(179, 485);
            this.panTotalPedidoFal.Name = "panTotalPedidoFal";
            this.panTotalPedidoFal.Size = new System.Drawing.Size(430, 81);
            this.panTotalPedidoFal.TabIndex = 8;
            this.panTotalPedidoFal.Visible = false;
            // 
            // labTitulodgvMaterialesacomprar
            // 
            this.labTitulodgvMaterialesacomprar.AutoSize = true;
            this.labTitulodgvMaterialesacomprar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitulodgvMaterialesacomprar.ForeColor = System.Drawing.Color.White;
            this.labTitulodgvMaterialesacomprar.Location = new System.Drawing.Point(424, 74);
            this.labTitulodgvMaterialesacomprar.Name = "labTitulodgvMaterialesacomprar";
            this.labTitulodgvMaterialesacomprar.Size = new System.Drawing.Size(229, 25);
            this.labTitulodgvMaterialesacomprar.TabIndex = 9;
            this.labTitulodgvMaterialesacomprar.Text = "Materiales a adquirir";
            // 
            // picCamtSolicitadaFal
            // 
            this.picCamtSolicitadaFal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picCamtSolicitadaFal.Location = new System.Drawing.Point(826, 302);
            this.picCamtSolicitadaFal.Name = "picCamtSolicitadaFal";
            this.picCamtSolicitadaFal.Size = new System.Drawing.Size(186, 128);
            this.picCamtSolicitadaFal.TabIndex = 10;
            this.picCamtSolicitadaFal.TabStop = false;
            this.picCamtSolicitadaFal.Visible = false;
            // 
            // frmFaltanteCom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1411, 629);
            this.Controls.Add(this.picCamtSolicitadaFal);
            this.Controls.Add(this.labCantSolicitadaFal);
            this.Controls.Add(this.texCantSolicitadaFal);
            this.Controls.Add(this.labTitulodgvMaterialesacomprar);
            this.Controls.Add(this.picFotoFaltante);
            this.Controls.Add(this.labTituloDgvMateriales);
            this.Controls.Add(this.botAceptaFaltaCom);
            this.Controls.Add(this.botCancelaFaltaCom);
            this.Controls.Add(this.panTotalPedidoFal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmFaltanteCom";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmFaltanteCom";
            this.Load += new System.EventHandler(this.frmFaltanteCom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFotoFaltante)).EndInit();
            this.panTotalPedidoFal.ResumeLayout(false);
            this.panTotalPedidoFal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamtSolicitadaFal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botCancelaFaltaCom;
        private System.Windows.Forms.Button botAceptaFaltaCom;
        private System.Windows.Forms.Label labTituloDgvMateriales;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox picFotoFaltante;
        private System.Windows.Forms.TextBox texCantSolicitadaFal;
        private System.Windows.Forms.Label labCantSolicitadaFal;
        private System.Windows.Forms.Label labTituloTotalPedidoFal;
        private System.Windows.Forms.Label labValorPedidoFal;
        private System.Windows.Forms.Panel panTotalPedidoFal;
        private System.Windows.Forms.Label labTitulodgvMaterialesacomprar;
        private System.Windows.Forms.PictureBox picCamtSolicitadaFal;
    }
}