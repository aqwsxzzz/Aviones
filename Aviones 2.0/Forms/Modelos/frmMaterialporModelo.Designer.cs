namespace Aviones_2._0.Forms.Modelos
{
    partial class frmMaterialporModelo
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

        private void InitializeComponent()
        {
            this.texFamiliaSel = new System.Windows.Forms.TextBox();
            this.panRecuaCantAsignada = new System.Windows.Forms.Panel();
            this.labCantAsignada = new System.Windows.Forms.Label();
            this.texCantidadMaterialSel = new System.Windows.Forms.TextBox();
            this.botBorrarMaterialSel = new System.Windows.Forms.Button();
            this.botAceptarSel = new System.Windows.Forms.Button();
            this.botSalirSel = new System.Windows.Forms.Button();
            this.texCantViejaSel = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labAlias = new System.Windows.Forms.Label();
            this.labModelo = new System.Windows.Forms.Label();
            this.lisboxFamiliasSel = new System.Windows.Forms.ListBox();
            this.panRecuaFamilia = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.picboxFotoModFinal = new System.Windows.Forms.PictureBox();
            this.panRecuaCantAsignada.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxFotoModFinal)).BeginInit();
            this.SuspendLayout();
            // 
            // texFamiliaSel
            // 
            this.texFamiliaSel.AcceptsTab = true;
            this.texFamiliaSel.BackColor = System.Drawing.Color.Tan;
            this.texFamiliaSel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texFamiliaSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texFamiliaSel.Location = new System.Drawing.Point(0, 143);
            this.texFamiliaSel.Name = "texFamiliaSel";
            this.texFamiliaSel.Size = new System.Drawing.Size(221, 28);
            this.texFamiliaSel.TabIndex = 0;
            this.texFamiliaSel.Text = "MATERIAL";
            this.texFamiliaSel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.texFamiliaSel.Click += new System.EventHandler(this.texFamiliaSel_Click);
            this.texFamiliaSel.Enter += new System.EventHandler(this.texFamiliaSel_Enter);
            // 
            // panRecuaCantAsignada
            // 
            this.panRecuaCantAsignada.BackColor = System.Drawing.Color.PeachPuff;
            this.panRecuaCantAsignada.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaCantAsignada.Controls.Add(this.labCantAsignada);
            this.panRecuaCantAsignada.Controls.Add(this.texCantidadMaterialSel);
            this.panRecuaCantAsignada.Location = new System.Drawing.Point(2, 544);
            this.panRecuaCantAsignada.Name = "panRecuaCantAsignada";
            this.panRecuaCantAsignada.Size = new System.Drawing.Size(178, 113);
            this.panRecuaCantAsignada.TabIndex = 2;
            this.panRecuaCantAsignada.Visible = false;
            // 
            // labCantAsignada
            // 
            this.labCantAsignada.AutoSize = true;
            this.labCantAsignada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCantAsignada.Location = new System.Drawing.Point(18, 23);
            this.labCantAsignada.Name = "labCantAsignada";
            this.labCantAsignada.Size = new System.Drawing.Size(138, 16);
            this.labCantAsignada.TabIndex = 4;
            this.labCantAsignada.Text = "Cantidad a asignar";
            // 
            // texCantidadMaterialSel
            // 
            this.texCantidadMaterialSel.BackColor = System.Drawing.Color.PeachPuff;
            this.texCantidadMaterialSel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texCantidadMaterialSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texCantidadMaterialSel.Location = new System.Drawing.Point(32, 57);
            this.texCantidadMaterialSel.Name = "texCantidadMaterialSel";
            this.texCantidadMaterialSel.Size = new System.Drawing.Size(94, 33);
            this.texCantidadMaterialSel.TabIndex = 3;
            this.texCantidadMaterialSel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.texCantidadMaterialSel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.texCantidadMaterialSel_KeyPress);
            // 
            // botBorrarMaterialSel
            // 
            this.botBorrarMaterialSel.BackColor = System.Drawing.Color.DarkKhaki;
            this.botBorrarMaterialSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botBorrarMaterialSel.ForeColor = System.Drawing.Color.Red;
            this.botBorrarMaterialSel.Location = new System.Drawing.Point(782, 836);
            this.botBorrarMaterialSel.Name = "botBorrarMaterialSel";
            this.botBorrarMaterialSel.Size = new System.Drawing.Size(160, 60);
            this.botBorrarMaterialSel.TabIndex = 3;
            this.botBorrarMaterialSel.Text = "BORRA MATERIAL";
            this.botBorrarMaterialSel.UseVisualStyleBackColor = false;
            this.botBorrarMaterialSel.Visible = false;
            this.botBorrarMaterialSel.Click += new System.EventHandler(this.botBorrarMaterialSel_Click);
            // 
            // botAceptarSel
            // 
            this.botAceptarSel.BackColor = System.Drawing.Color.DarkKhaki;
            this.botAceptarSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botAceptarSel.Location = new System.Drawing.Point(1162, 836);
            this.botAceptarSel.Name = "botAceptarSel";
            this.botAceptarSel.Size = new System.Drawing.Size(160, 60);
            this.botAceptarSel.TabIndex = 4;
            this.botAceptarSel.Text = "ACEPTAR";
            this.botAceptarSel.UseVisualStyleBackColor = false;
            this.botAceptarSel.Visible = false;
            this.botAceptarSel.Click += new System.EventHandler(this.botAceptarSel_Click);
            // 
            // botSalirSel
            // 
            this.botSalirSel.BackColor = System.Drawing.Color.DarkKhaki;
            this.botSalirSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botSalirSel.Location = new System.Drawing.Point(1358, 836);
            this.botSalirSel.Name = "botSalirSel";
            this.botSalirSel.Size = new System.Drawing.Size(160, 60);
            this.botSalirSel.TabIndex = 5;
            this.botSalirSel.Text = "SALIR";
            this.botSalirSel.UseVisualStyleBackColor = false;
            this.botSalirSel.Click += new System.EventHandler(this.botSalirSel_Click);
            // 
            // texCantViejaSel
            // 
            this.texCantViejaSel.Location = new System.Drawing.Point(118, 778);
            this.texCantViejaSel.Name = "texCantViejaSel";
            this.texCantViejaSel.Size = new System.Drawing.Size(103, 20);
            this.texCantViejaSel.TabIndex = 5;
            this.texCantViejaSel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.BurlyWood;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.labAlias);
            this.panel1.Controls.Add(this.labModelo);
            this.panel1.Location = new System.Drawing.Point(440, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 105);
            this.panel1.TabIndex = 6;
            // 
            // labAlias
            // 
            this.labAlias.AutoSize = true;
            this.labAlias.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labAlias.Location = new System.Drawing.Point(367, 33);
            this.labAlias.Name = "labAlias";
            this.labAlias.Size = new System.Drawing.Size(108, 37);
            this.labAlias.TabIndex = 1;
            this.labAlias.Text = "label2";
            // 
            // labModelo
            // 
            this.labModelo.AutoSize = true;
            this.labModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labModelo.Location = new System.Drawing.Point(64, 33);
            this.labModelo.Name = "labModelo";
            this.labModelo.Size = new System.Drawing.Size(106, 37);
            this.labModelo.TabIndex = 0;
            this.labModelo.Text = "label1";
            // 
            // lisboxFamiliasSel
            // 
            this.lisboxFamiliasSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lisboxFamiliasSel.FormattingEnabled = true;
            this.lisboxFamiliasSel.ItemHeight = 24;
            this.lisboxFamiliasSel.Location = new System.Drawing.Point(18, 309);
            this.lisboxFamiliasSel.Name = "lisboxFamiliasSel";
            this.lisboxFamiliasSel.Size = new System.Drawing.Size(203, 124);
            this.lisboxFamiliasSel.TabIndex = 7;
            this.lisboxFamiliasSel.Visible = false;
            this.lisboxFamiliasSel.Click += new System.EventHandler(this.lisboxFamiliasSel_Click);
            this.lisboxFamiliasSel.SelectedIndexChanged += new System.EventHandler(this.lisboxFamiliasSel_SelectedIndexChanged);
            // 
            // panRecuaFamilia
            // 
            this.panRecuaFamilia.BackColor = System.Drawing.Color.Tan;
            this.panRecuaFamilia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaFamilia.Location = new System.Drawing.Point(341, 255);
            this.panRecuaFamilia.Name = "panRecuaFamilia";
            this.panRecuaFamilia.Size = new System.Drawing.Size(100, 104);
            this.panRecuaFamilia.TabIndex = 8;
            this.panRecuaFamilia.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(48, 708);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(173, 20);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.Visible = false;
            // 
            // picFoto
            // 
            this.picFoto.BackColor = System.Drawing.Color.IndianRed;
            this.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picFoto.Location = new System.Drawing.Point(1447, 274);
            this.picFoto.Name = "picFoto";
            this.picFoto.Size = new System.Drawing.Size(198, 185);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFoto.TabIndex = 10;
            this.picFoto.TabStop = false;
            this.picFoto.Visible = false;
            // 
            // picboxFotoModFinal
            // 
            this.picboxFotoModFinal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picboxFotoModFinal.Location = new System.Drawing.Point(1420, 2);
            this.picboxFotoModFinal.Name = "picboxFotoModFinal";
            this.picboxFotoModFinal.Size = new System.Drawing.Size(192, 192);
            this.picboxFotoModFinal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picboxFotoModFinal.TabIndex = 11;
            this.picboxFotoModFinal.TabStop = false;
            // 
            // frmMaterialporModelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1707, 921);
            this.Controls.Add(this.picboxFotoModFinal);
            this.Controls.Add(this.picFoto);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lisboxFamiliasSel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.texCantViejaSel);
            this.Controls.Add(this.botSalirSel);
            this.Controls.Add(this.botAceptarSel);
            this.Controls.Add(this.botBorrarMaterialSel);
            this.Controls.Add(this.panRecuaCantAsignada);
            this.Controls.Add(this.texFamiliaSel);
            this.Controls.Add(this.panRecuaFamilia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMaterialporModelo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmMaterialporModelo";
            this.Load += new System.EventHandler(this.frmMaterialporModelo_Load);
            this.panRecuaCantAsignada.ResumeLayout(false);
            this.panRecuaCantAsignada.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxFotoModFinal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox texFamiliaSel;
        private System.Windows.Forms.Panel panRecuaCantAsignada;
        private System.Windows.Forms.Label labCantAsignada;
        private System.Windows.Forms.TextBox texCantidadMaterialSel;
        private System.Windows.Forms.Button botAceptarSel;
        private System.Windows.Forms.Button botSalirSel;
        private System.Windows.Forms.TextBox texCantViejaSel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labAlias;
        private System.Windows.Forms.Label labModelo;
        public System.Windows.Forms.ListBox lisboxFamiliasSel;
        public System.Windows.Forms.Panel panRecuaFamilia;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.PictureBox picboxFotoModFinal;
        public System.Windows.Forms.Button botBorrarMaterialSel;
    }
}