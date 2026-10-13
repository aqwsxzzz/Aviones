namespace Aviones_2._0.Forms.Materiales
{
    partial class frmListados
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
            this.labTituloListado = new System.Windows.Forms.Label();
            this.panRecuaTotalGral = new System.Windows.Forms.Panel();
            this.labValorTotalGeneral = new System.Windows.Forms.Label();
            this.labTituloTotalGeneral = new System.Windows.Forms.Label();
            this.botSalirListados = new System.Windows.Forms.Button();
            this.botListadoFamilia = new System.Windows.Forms.Button();
            this.lisFamListMat = new System.Windows.Forms.ListBox();
            this.texFamAEnviar = new System.Windows.Forms.TextBox();
            this.labTituloValorFamilia = new System.Windows.Forms.Label();
            this.labValorFamilia = new System.Windows.Forms.Label();
            this.botCancelarMat = new System.Windows.Forms.Button();
            this.panRecuaTotalFamilia = new System.Windows.Forms.Panel();
            this.panGrande = new System.Windows.Forms.Panel();
            this.panRecuaFamilia = new System.Windows.Forms.Panel();
            this.labListaFam = new System.Windows.Forms.Label();
            this.ritexFamAEnviar = new System.Windows.Forms.RichTextBox();
            this.panRecuaTotalGral.SuspendLayout();
            this.SuspendLayout();
            // 
            // labTituloListado
            // 
            this.labTituloListado.AutoSize = true;
            this.labTituloListado.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTituloListado.Location = new System.Drawing.Point(672, 40);
            this.labTituloListado.Name = "labTituloListado";
            this.labTituloListado.Size = new System.Drawing.Size(278, 33);
            this.labTituloListado.TabIndex = 0;
            this.labTituloListado.Text = "Listados generales";
            // 
            // panRecuaTotalGral
            // 
            this.panRecuaTotalGral.BackColor = System.Drawing.Color.SteelBlue;
            this.panRecuaTotalGral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaTotalGral.Controls.Add(this.labValorTotalGeneral);
            this.panRecuaTotalGral.Controls.Add(this.labTituloTotalGeneral);
            this.panRecuaTotalGral.Location = new System.Drawing.Point(623, 408);
            this.panRecuaTotalGral.Name = "panRecuaTotalGral";
            this.panRecuaTotalGral.Size = new System.Drawing.Size(396, 81);
            this.panRecuaTotalGral.TabIndex = 1;
            // 
            // labValorTotalGeneral
            // 
            this.labValorTotalGeneral.AutoSize = true;
            this.labValorTotalGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labValorTotalGeneral.ForeColor = System.Drawing.Color.Indigo;
            this.labValorTotalGeneral.Location = new System.Drawing.Point(257, 16);
            this.labValorTotalGeneral.Name = "labValorTotalGeneral";
            this.labValorTotalGeneral.Size = new System.Drawing.Size(98, 39);
            this.labValorTotalGeneral.TabIndex = 2;
            this.labValorTotalGeneral.Text = "Valor";
            // 
            // labTituloTotalGeneral
            // 
            this.labTituloTotalGeneral.AutoSize = true;
            this.labTituloTotalGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTituloTotalGeneral.Location = new System.Drawing.Point(48, 26);
            this.labTituloTotalGeneral.Name = "labTituloTotalGeneral";
            this.labTituloTotalGeneral.Size = new System.Drawing.Size(179, 25);
            this.labTituloTotalGeneral.TabIndex = 2;
            this.labTituloTotalGeneral.Text = "Total general US:";
            // 
            // botSalirListados
            // 
            this.botSalirListados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botSalirListados.Location = new System.Drawing.Point(1520, 782);
            this.botSalirListados.Name = "botSalirListados";
            this.botSalirListados.Size = new System.Drawing.Size(153, 61);
            this.botSalirListados.TabIndex = 2;
            this.botSalirListados.Text = "SALIR";
            this.botSalirListados.UseVisualStyleBackColor = true;
            this.botSalirListados.Click += new System.EventHandler(this.botSalirListados_Click);
            // 
            // botListadoFamilia
            // 
            this.botListadoFamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botListadoFamilia.Location = new System.Drawing.Point(1320, 782);
            this.botListadoFamilia.Name = "botListadoFamilia";
            this.botListadoFamilia.Size = new System.Drawing.Size(153, 61);
            this.botListadoFamilia.TabIndex = 3;
            this.botListadoFamilia.Text = "POR FAMILIA";
            this.botListadoFamilia.UseVisualStyleBackColor = true;
            this.botListadoFamilia.Click += new System.EventHandler(this.botListadoFamilia_Click);
            // 
            // lisFamListMat
            // 
            this.lisFamListMat.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lisFamListMat.FormattingEnabled = true;
            this.lisFamListMat.Location = new System.Drawing.Point(125, 419);
            this.lisFamListMat.Name = "lisFamListMat";
            this.lisFamListMat.Size = new System.Drawing.Size(123, 108);
            this.lisFamListMat.TabIndex = 4;
            this.lisFamListMat.SelectedIndexChanged += new System.EventHandler(this.lisFamListMat_SelectedIndexChanged);
            // 
            // texFamAEnviar
            // 
            this.texFamAEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.texFamAEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texFamAEnviar.Location = new System.Drawing.Point(100, 145);
            this.texFamAEnviar.Name = "texFamAEnviar";
            this.texFamAEnviar.Size = new System.Drawing.Size(185, 20);
            this.texFamAEnviar.TabIndex = 5;
            // 
            // labTituloValorFamilia
            // 
            this.labTituloValorFamilia.AutoSize = true;
            this.labTituloValorFamilia.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labTituloValorFamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTituloValorFamilia.Location = new System.Drawing.Point(834, 251);
            this.labTituloValorFamilia.Name = "labTituloValorFamilia";
            this.labTituloValorFamilia.Size = new System.Drawing.Size(197, 29);
            this.labTituloValorFamilia.TabIndex = 6;
            this.labTituloValorFamilia.Text = "Total               US:";
            this.labTituloValorFamilia.Visible = false;
            // 
            // labValorFamilia
            // 
            this.labValorFamilia.AutoSize = true;
            this.labValorFamilia.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labValorFamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labValorFamilia.Location = new System.Drawing.Point(1069, 245);
            this.labValorFamilia.Name = "labValorFamilia";
            this.labValorFamilia.Size = new System.Drawing.Size(83, 33);
            this.labValorFamilia.TabIndex = 7;
            this.labValorFamilia.Text = "Valor";
            this.labValorFamilia.Visible = false;
            // 
            // botCancelarMat
            // 
            this.botCancelarMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botCancelarMat.Location = new System.Drawing.Point(1160, 651);
            this.botCancelarMat.Name = "botCancelarMat";
            this.botCancelarMat.Size = new System.Drawing.Size(153, 51);
            this.botCancelarMat.TabIndex = 8;
            this.botCancelarMat.Text = "CANCELAR";
            this.botCancelarMat.UseVisualStyleBackColor = true;
            this.botCancelarMat.Click += new System.EventHandler(this.botCancelarMat_Click);
            // 
            // panRecuaTotalFamilia
            // 
            this.panRecuaTotalFamilia.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panRecuaTotalFamilia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaTotalFamilia.Location = new System.Drawing.Point(797, 222);
            this.panRecuaTotalFamilia.Name = "panRecuaTotalFamilia";
            this.panRecuaTotalFamilia.Size = new System.Drawing.Size(387, 83);
            this.panRecuaTotalFamilia.TabIndex = 9;
            // 
            // panGrande
            // 
            this.panGrande.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panGrande.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panGrande.Location = new System.Drawing.Point(1203, 461);
            this.panGrande.Name = "panGrande";
            this.panGrande.Size = new System.Drawing.Size(176, 149);
            this.panGrande.TabIndex = 10;
            this.panGrande.Visible = false;
            // 
            // panRecuaFamilia
            // 
            this.panRecuaFamilia.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panRecuaFamilia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaFamilia.Location = new System.Drawing.Point(706, 630);
            this.panRecuaFamilia.Name = "panRecuaFamilia";
            this.panRecuaFamilia.Size = new System.Drawing.Size(146, 100);
            this.panRecuaFamilia.TabIndex = 11;
            this.panRecuaFamilia.Visible = false;
            // 
            // labListaFam
            // 
            this.labListaFam.AutoSize = true;
            this.labListaFam.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.labListaFam.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labListaFam.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labListaFam.Location = new System.Drawing.Point(743, 564);
            this.labListaFam.Name = "labListaFam";
            this.labListaFam.Size = new System.Drawing.Size(109, 31);
            this.labListaFam.TabIndex = 12;
            this.labListaFam.Text = "Familas";
            this.labListaFam.Visible = false;
            // 
            // ritexFamAEnviar
            // 
            this.ritexFamAEnviar.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ritexFamAEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ritexFamAEnviar.Location = new System.Drawing.Point(1243, 94);
            this.ritexFamAEnviar.Name = "ritexFamAEnviar";
            this.ritexFamAEnviar.Size = new System.Drawing.Size(275, 40);
            this.ritexFamAEnviar.TabIndex = 13;
            this.ritexFamAEnviar.Text = "";
            this.ritexFamAEnviar.Visible = false;
            // 
            // frmListados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1685, 851);
            this.Controls.Add(this.ritexFamAEnviar);
            this.Controls.Add(this.labValorFamilia);
            this.Controls.Add(this.labTituloValorFamilia);
            this.Controls.Add(this.panRecuaTotalFamilia);
            this.Controls.Add(this.labListaFam);
            this.Controls.Add(this.lisFamListMat);
            this.Controls.Add(this.panRecuaFamilia);
            this.Controls.Add(this.panRecuaTotalGral);
            this.Controls.Add(this.botCancelarMat);
            this.Controls.Add(this.texFamAEnviar);
            this.Controls.Add(this.botListadoFamilia);
            this.Controls.Add(this.botSalirListados);
            this.Controls.Add(this.labTituloListado);
            this.Controls.Add(this.panGrande);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmListados";
            this.Text = "frnListados";
            this.Load += new System.EventHandler(this.frmListados_Load);
            this.panRecuaTotalGral.ResumeLayout(false);
            this.panRecuaTotalGral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTituloListado;
        private System.Windows.Forms.Panel panRecuaTotalGral;
        private System.Windows.Forms.Label labTituloTotalGeneral;
        private System.Windows.Forms.Label labValorTotalGeneral;
        public System.Windows.Forms.Button botSalirListados;
        private System.Windows.Forms.Button botListadoFamilia;
        private System.Windows.Forms.ListBox lisFamListMat;
        private System.Windows.Forms.TextBox texFamAEnviar;
        private System.Windows.Forms.Label labTituloValorFamilia;
        private System.Windows.Forms.Label labValorFamilia;
        private System.Windows.Forms.Button botCancelarMat;
        private System.Windows.Forms.Panel panRecuaTotalFamilia;
        private System.Windows.Forms.Panel panGrande;
        private System.Windows.Forms.Panel panRecuaFamilia;
        private System.Windows.Forms.Label labListaFam;
        private System.Windows.Forms.RichTextBox ritexFamAEnviar;
    }
}