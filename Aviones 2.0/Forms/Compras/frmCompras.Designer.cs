namespace Aviones_2._0.Forms.Compras
{
    partial class frmComprasCom
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
            this.botSalirCom = new System.Windows.Forms.Button();
            this.TituloPincipal = new System.Windows.Forms.Label();
            this.botIngresoCom = new System.Windows.Forms.Button();
            this.botModificaCom = new System.Windows.Forms.Button();
            this.botEliminaCom = new System.Windows.Forms.Button();
            this.labAvisoCompra = new System.Windows.Forms.Label();
            this.panAvisoCompra = new System.Windows.Forms.Panel();
            this.panGrande = new System.Windows.Forms.Panel();
            this.botFaltanteCom = new System.Windows.Forms.Button();
            this.pnaRecuaBotones = new System.Windows.Forms.Panel();
            this.panAvisoCompra.SuspendLayout();
            this.pnaRecuaBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // botSalirCom
            // 
            this.botSalirCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botSalirCom.Location = new System.Drawing.Point(78, 756);
            this.botSalirCom.Name = "botSalirCom";
            this.botSalirCom.Size = new System.Drawing.Size(160, 60);
            this.botSalirCom.TabIndex = 0;
            this.botSalirCom.Text = "SALIR";
            this.botSalirCom.UseVisualStyleBackColor = true;
            this.botSalirCom.Click += new System.EventHandler(this.Salir_Click);
            // 
            // TituloPincipal
            // 
            this.TituloPincipal.AutoSize = true;
            this.TituloPincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TituloPincipal.Location = new System.Drawing.Point(875, 85);
            this.TituloPincipal.Name = "TituloPincipal";
            this.TituloPincipal.Size = new System.Drawing.Size(161, 33);
            this.TituloPincipal.TabIndex = 3;
            this.TituloPincipal.Text = "COMPRAS";
            // 
            // botIngresoCom
            // 
            this.botIngresoCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botIngresoCom.Location = new System.Drawing.Point(35, 10);
            this.botIngresoCom.Name = "botIngresoCom";
            this.botIngresoCom.Size = new System.Drawing.Size(167, 60);
            this.botIngresoCom.TabIndex = 4;
            this.botIngresoCom.Text = "INGRESAR PRODUCTO";
            this.botIngresoCom.UseVisualStyleBackColor = true;
            this.botIngresoCom.Click += new System.EventHandler(this.botIngresoCom_Click);
            // 
            // botModificaCom
            // 
            this.botModificaCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botModificaCom.Location = new System.Drawing.Point(35, 109);
            this.botModificaCom.Name = "botModificaCom";
            this.botModificaCom.Size = new System.Drawing.Size(167, 60);
            this.botModificaCom.TabIndex = 5;
            this.botModificaCom.Text = "MODIFICAR ";
            this.botModificaCom.UseVisualStyleBackColor = true;
            this.botModificaCom.Click += new System.EventHandler(this.botModificaCom_Click);
            // 
            // botEliminaCom
            // 
            this.botEliminaCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botEliminaCom.Location = new System.Drawing.Point(35, 213);
            this.botEliminaCom.Name = "botEliminaCom";
            this.botEliminaCom.Size = new System.Drawing.Size(167, 60);
            this.botEliminaCom.TabIndex = 9;
            this.botEliminaCom.Text = "ELIMINAR";
            this.botEliminaCom.UseVisualStyleBackColor = true;
            this.botEliminaCom.Click += new System.EventHandler(this.botEliminaCom_Click);
            // 
            // labAvisoCompra
            // 
            this.labAvisoCompra.AutoSize = true;
            this.labAvisoCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labAvisoCompra.Location = new System.Drawing.Point(51, 37);
            this.labAvisoCompra.Name = "labAvisoCompra";
            this.labAvisoCompra.Size = new System.Drawing.Size(542, 29);
            this.labAvisoCompra.TabIndex = 10;
            this.labAvisoCompra.Text = "Distintas opciones de manejo de las compras\r\n";
            // 
            // panAvisoCompra
            // 
            this.panAvisoCompra.BackColor = System.Drawing.Color.SkyBlue;
            this.panAvisoCompra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panAvisoCompra.Controls.Add(this.labAvisoCompra);
            this.panAvisoCompra.Location = new System.Drawing.Point(635, 374);
            this.panAvisoCompra.Name = "panAvisoCompra";
            this.panAvisoCompra.Size = new System.Drawing.Size(652, 110);
            this.panAvisoCompra.TabIndex = 11;
            // 
            // panGrande
            // 
            this.panGrande.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panGrande.Location = new System.Drawing.Point(305, 144);
            this.panGrande.Name = "panGrande";
            this.panGrande.Size = new System.Drawing.Size(1480, 572);
            this.panGrande.TabIndex = 12;
            this.panGrande.Visible = false;
            // 
            // botFaltanteCom
            // 
            this.botFaltanteCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botFaltanteCom.Location = new System.Drawing.Point(35, 312);
            this.botFaltanteCom.Name = "botFaltanteCom";
            this.botFaltanteCom.Size = new System.Drawing.Size(167, 60);
            this.botFaltanteCom.TabIndex = 8;
            this.botFaltanteCom.Text = "FALTANTES";
            this.botFaltanteCom.UseVisualStyleBackColor = true;
            this.botFaltanteCom.Click += new System.EventHandler(this.botFaltanteCom_Click);
            // 
            // pnaRecuaBotones
            // 
            this.pnaRecuaBotones.BackColor = System.Drawing.Color.SteelBlue;
            this.pnaRecuaBotones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnaRecuaBotones.Controls.Add(this.botEliminaCom);
            this.pnaRecuaBotones.Controls.Add(this.botFaltanteCom);
            this.pnaRecuaBotones.Controls.Add(this.botModificaCom);
            this.pnaRecuaBotones.Controls.Add(this.botIngresoCom);
            this.pnaRecuaBotones.Location = new System.Drawing.Point(28, 148);
            this.pnaRecuaBotones.Name = "pnaRecuaBotones";
            this.pnaRecuaBotones.Size = new System.Drawing.Size(245, 384);
            this.pnaRecuaBotones.TabIndex = 13;
            // 
            // frmComprasCom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1809, 895);
            this.Controls.Add(this.pnaRecuaBotones);
            this.Controls.Add(this.TituloPincipal);
            this.Controls.Add(this.botSalirCom);
            this.Controls.Add(this.panAvisoCompra);
            this.Controls.Add(this.panGrande);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmComprasCom";
            this.Text = "frmCompras";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmComprasCom_Load);
            this.panAvisoCompra.ResumeLayout(false);
            this.panAvisoCompra.PerformLayout();
            this.pnaRecuaBotones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TituloPincipal;
        public System.Windows.Forms.Button botIngresoCom;
        public System.Windows.Forms.Button botModificaCom;
        public System.Windows.Forms.Button botEliminaCom;
        public System.Windows.Forms.Label labAvisoCompra;
        public System.Windows.Forms.Button botSalirCom;
        public System.Windows.Forms.Panel panAvisoCompra;
        public System.Windows.Forms.Panel panGrande;
        public System.Windows.Forms.Button botFaltanteCom;
        private System.Windows.Forms.Panel pnaRecuaBotones;
    }
}