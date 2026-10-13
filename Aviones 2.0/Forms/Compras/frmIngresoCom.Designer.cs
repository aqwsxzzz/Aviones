namespace Aviones_2._0.Forms.Compras
{
    partial class frmIngresoCom
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
            this.lisFamiliaSelCom = new System.Windows.Forms.ListBox();
            this.botListaFamiCom = new System.Windows.Forms.Button();
            this.panFamiliaSelCom = new System.Windows.Forms.Panel();
            this.labListadoFamiliaCom = new System.Windows.Forms.Label();
            this.botSalirFamiliasCom = new System.Windows.Forms.Button();
            this.picFotoCom = new System.Windows.Forms.PictureBox();
            this.panCamtSolicitada = new System.Windows.Forms.Panel();
            this.texCantSolicitada = new System.Windows.Forms.TextBox();
            this.labCantSolicitada = new System.Windows.Forms.Label();
            this.botAceptarCompraCom = new System.Windows.Forms.Button();
            this.panTotalPedido = new System.Windows.Forms.Panel();
            this.labValorPedido = new System.Windows.Forms.Label();
            this.labTituloTotalPedido = new System.Windows.Forms.Label();
            this.panRecuadroTitulo = new System.Windows.Forms.Panel();
            this.panRecuadroTitulo1 = new System.Windows.Forms.Panel();
            this.panRecuaFamilia = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picFotoCom)).BeginInit();
            this.panCamtSolicitada.SuspendLayout();
            this.panTotalPedido.SuspendLayout();
            this.SuspendLayout();
            // 
            // lisFamiliaSelCom
            // 
            this.lisFamiliaSelCom.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lisFamiliaSelCom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lisFamiliaSelCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lisFamiliaSelCom.FormattingEnabled = true;
            this.lisFamiliaSelCom.ItemHeight = 24;
            this.lisFamiliaSelCom.Location = new System.Drawing.Point(69, 110);
            this.lisFamiliaSelCom.Name = "lisFamiliaSelCom";
            this.lisFamiliaSelCom.Size = new System.Drawing.Size(218, 192);
            this.lisFamiliaSelCom.TabIndex = 0;
            this.lisFamiliaSelCom.Visible = false;
            this.lisFamiliaSelCom.SelectedIndexChanged += new System.EventHandler(this.lisboxFamiliaSelcom_SelectedIndexChanged);
            // 
            // botListaFamiCom
            // 
            this.botListaFamiCom.BackColor = System.Drawing.Color.Cyan;
            this.botListaFamiCom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.botListaFamiCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botListaFamiCom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botListaFamiCom.Location = new System.Drawing.Point(626, 244);
            this.botListaFamiCom.Name = "botListaFamiCom";
            this.botListaFamiCom.Size = new System.Drawing.Size(236, 40);
            this.botListaFamiCom.TabIndex = 2;
            this.botListaFamiCom.Text = "MATERIALES";
            this.botListaFamiCom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botListaFamiCom.UseVisualStyleBackColor = false;
            this.botListaFamiCom.Click += new System.EventHandler(this.botListaFamiCom_Click);
            // 
            // panFamiliaSelCom
            // 
            this.panFamiliaSelCom.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panFamiliaSelCom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panFamiliaSelCom.Location = new System.Drawing.Point(350, 70);
            this.panFamiliaSelCom.Name = "panFamiliaSelCom";
            this.panFamiliaSelCom.Size = new System.Drawing.Size(261, 289);
            this.panFamiliaSelCom.TabIndex = 3;
            this.panFamiliaSelCom.Visible = false;
            // 
            // labListadoFamiliaCom
            // 
            this.labListadoFamiliaCom.AutoSize = true;
            this.labListadoFamiliaCom.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labListadoFamiliaCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labListadoFamiliaCom.Location = new System.Drawing.Point(527, 508);
            this.labListadoFamiliaCom.Name = "labListadoFamiliaCom";
            this.labListadoFamiliaCom.Size = new System.Drawing.Size(102, 24);
            this.labListadoFamiliaCom.TabIndex = 4;
            this.labListadoFamiliaCom.Text = "FAMILIAS";
            this.labListadoFamiliaCom.Visible = false;
            // 
            // botSalirFamiliasCom
            // 
            this.botSalirFamiliasCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botSalirFamiliasCom.Location = new System.Drawing.Point(1391, 820);
            this.botSalirFamiliasCom.Name = "botSalirFamiliasCom";
            this.botSalirFamiliasCom.Size = new System.Drawing.Size(157, 58);
            this.botSalirFamiliasCom.TabIndex = 5;
            this.botSalirFamiliasCom.Text = "CANCELAR";
            this.botSalirFamiliasCom.UseVisualStyleBackColor = true;
            this.botSalirFamiliasCom.Click += new System.EventHandler(this.botSalirFamiliasCom_Click);
            // 
            // picFotoCom
            // 
            this.picFotoCom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picFotoCom.Location = new System.Drawing.Point(65, 560);
            this.picFotoCom.Name = "picFotoCom";
            this.picFotoCom.Size = new System.Drawing.Size(218, 201);
            this.picFotoCom.TabIndex = 6;
            this.picFotoCom.TabStop = false;
            this.picFotoCom.Visible = false;
            // 
            // panCamtSolicitada
            // 
            this.panCamtSolicitada.BackColor = System.Drawing.Color.Silver;
            this.panCamtSolicitada.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panCamtSolicitada.Controls.Add(this.texCantSolicitada);
            this.panCamtSolicitada.Controls.Add(this.labCantSolicitada);
            this.panCamtSolicitada.Location = new System.Drawing.Point(1364, 372);
            this.panCamtSolicitada.Name = "panCamtSolicitada";
            this.panCamtSolicitada.Size = new System.Drawing.Size(186, 128);
            this.panCamtSolicitada.TabIndex = 7;
            this.panCamtSolicitada.Visible = false;
            // 
            // texCantSolicitada
            // 
            this.texCantSolicitada.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texCantSolicitada.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texCantSolicitada.Location = new System.Drawing.Point(43, 74);
            this.texCantSolicitada.Name = "texCantSolicitada";
            this.texCantSolicitada.Size = new System.Drawing.Size(102, 28);
            this.texCantSolicitada.TabIndex = 8;
            this.texCantSolicitada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.texCantSolicitada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.texCantSolicitada_KeyPress);
            // 
            // labCantSolicitada
            // 
            this.labCantSolicitada.AutoSize = true;
            this.labCantSolicitada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCantSolicitada.Location = new System.Drawing.Point(22, 28);
            this.labCantSolicitada.Name = "labCantSolicitada";
            this.labCantSolicitada.Size = new System.Drawing.Size(143, 16);
            this.labCantSolicitada.TabIndex = 8;
            this.labCantSolicitada.Text = "Cantidad Solicitada";
            // 
            // botAceptarCompraCom
            // 
            this.botAceptarCompraCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botAceptarCompraCom.Location = new System.Drawing.Point(1200, 820);
            this.botAceptarCompraCom.Name = "botAceptarCompraCom";
            this.botAceptarCompraCom.Size = new System.Drawing.Size(157, 58);
            this.botAceptarCompraCom.TabIndex = 8;
            this.botAceptarCompraCom.Text = "ACEPTAR";
            this.botAceptarCompraCom.UseVisualStyleBackColor = true;
            this.botAceptarCompraCom.Visible = false;
            // 
            // panTotalPedido
            // 
            this.panTotalPedido.BackColor = System.Drawing.Color.Silver;
            this.panTotalPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panTotalPedido.Controls.Add(this.labValorPedido);
            this.panTotalPedido.Controls.Add(this.labTituloTotalPedido);
            this.panTotalPedido.Location = new System.Drawing.Point(112, 817);
            this.panTotalPedido.Name = "panTotalPedido";
            this.panTotalPedido.Size = new System.Drawing.Size(346, 72);
            this.panTotalPedido.TabIndex = 9;
            this.panTotalPedido.Visible = false;
            // 
            // labValorPedido
            // 
            this.labValorPedido.AutoSize = true;
            this.labValorPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labValorPedido.Location = new System.Drawing.Point(231, 19);
            this.labValorPedido.Name = "labValorPedido";
            this.labValorPedido.Size = new System.Drawing.Size(85, 29);
            this.labValorPedido.TabIndex = 1;
            this.labValorPedido.Text = "label2";
            // 
            // labTituloTotalPedido
            // 
            this.labTituloTotalPedido.AutoSize = true;
            this.labTituloTotalPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTituloTotalPedido.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labTituloTotalPedido.Location = new System.Drawing.Point(30, 24);
            this.labTituloTotalPedido.Name = "labTituloTotalPedido";
            this.labTituloTotalPedido.Size = new System.Drawing.Size(146, 24);
            this.labTituloTotalPedido.TabIndex = 0;
            this.labTituloTotalPedido.Text = "Total del pedido";
            // 
            // panRecuadroTitulo
            // 
            this.panRecuadroTitulo.BackColor = System.Drawing.Color.LightGray;
            this.panRecuadroTitulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuadroTitulo.Location = new System.Drawing.Point(653, 317);
            this.panRecuadroTitulo.Name = "panRecuadroTitulo";
            this.panRecuadroTitulo.Size = new System.Drawing.Size(189, 127);
            this.panRecuadroTitulo.TabIndex = 10;
            this.panRecuadroTitulo.Visible = false;
            // 
            // panRecuadroTitulo1
            // 
            this.panRecuadroTitulo1.BackColor = System.Drawing.Color.Silver;
            this.panRecuadroTitulo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panRecuadroTitulo1.Location = new System.Drawing.Point(677, 512);
            this.panRecuadroTitulo1.Name = "panRecuadroTitulo1";
            this.panRecuadroTitulo1.Size = new System.Drawing.Size(197, 141);
            this.panRecuadroTitulo1.TabIndex = 11;
            this.panRecuadroTitulo1.Visible = false;
            // 
            // panRecuaFamilia
            // 
            this.panRecuaFamilia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaFamilia.Location = new System.Drawing.Point(677, 55);
            this.panRecuaFamilia.Name = "panRecuaFamilia";
            this.panRecuaFamilia.Size = new System.Drawing.Size(184, 150);
            this.panRecuaFamilia.TabIndex = 12;
            this.panRecuaFamilia.Visible = false;
            // 
            // frmIngresoCom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1753, 901);
            this.Controls.Add(this.panTotalPedido);
            this.Controls.Add(this.botAceptarCompraCom);
            this.Controls.Add(this.panCamtSolicitada);
            this.Controls.Add(this.picFotoCom);
            this.Controls.Add(this.botSalirFamiliasCom);
            this.Controls.Add(this.labListadoFamiliaCom);
            this.Controls.Add(this.lisFamiliaSelCom);
            this.Controls.Add(this.panFamiliaSelCom);
            this.Controls.Add(this.botListaFamiCom);
            this.Controls.Add(this.panRecuadroTitulo1);
            this.Controls.Add(this.panRecuadroTitulo);
            this.Controls.Add(this.panRecuaFamilia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmIngresoCom";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmIngresoCom";
            this.Load += new System.EventHandler(this.frmIngresoCom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFotoCom)).EndInit();
            this.panCamtSolicitada.ResumeLayout(false);
            this.panCamtSolicitada.PerformLayout();
            this.panTotalPedido.ResumeLayout(false);
            this.panTotalPedido.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lisFamiliaSelCom;
        private System.Windows.Forms.Button botListaFamiCom;
        private System.Windows.Forms.Panel panFamiliaSelCom;
        private System.Windows.Forms.Label labListadoFamiliaCom;
        private System.Windows.Forms.Button botSalirFamiliasCom;
        private System.Windows.Forms.PictureBox picFotoCom;
        private System.Windows.Forms.Panel panCamtSolicitada;
        private System.Windows.Forms.Label labCantSolicitada;
        private System.Windows.Forms.TextBox texCantSolicitada;
        private System.Windows.Forms.Button botAceptarCompraCom;
        private System.Windows.Forms.Panel panTotalPedido;
        private System.Windows.Forms.Label labValorPedido;
        private System.Windows.Forms.Label labTituloTotalPedido;
        private System.Windows.Forms.Panel panRecuadroTitulo;
        private System.Windows.Forms.Panel panRecuadroTitulo1;
        private System.Windows.Forms.Panel panRecuaFamilia;
    }
}