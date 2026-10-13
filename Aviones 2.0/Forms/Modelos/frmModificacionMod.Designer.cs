namespace Aviones_2._0.Forms.Modelos
{
    partial class frmModificacionMod
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
            this.panRecuaPrincipal = new System.Windows.Forms.Panel();
            this.labValorTotal = new System.Windows.Forms.Label();
            this.texValorTotal = new System.Windows.Forms.TextBox();
            this.panRecuaNomAlias = new System.Windows.Forms.Panel();
            this.texModelo = new System.Windows.Forms.TextBox();
            this.texAlias = new System.Windows.Forms.TextBox();
            this.panRecuaCondiFecha = new System.Windows.Forms.Panel();
            this.texDesde = new System.Windows.Forms.TextBox();
            this.texEstado = new System.Windows.Forms.TextBox();
            this.panValorTotal = new System.Windows.Forms.Panel();
            this.labTitulo = new System.Windows.Forms.Label();
            this.botModificaFoto = new System.Windows.Forms.Button();
            this.botCancelar = new System.Windows.Forms.Button();
            this.botModificaMaterial = new System.Windows.Forms.Button();
            this.picFotoModi = new System.Windows.Forms.PictureBox();
            this.botConfirmar = new System.Windows.Forms.Button();
            this.datDesdeMod = new System.Windows.Forms.DateTimePicker();
            this.panRecuaPrincipal.SuspendLayout();
            this.panRecuaNomAlias.SuspendLayout();
            this.panRecuaCondiFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFotoModi)).BeginInit();
            this.SuspendLayout();
            // 
            // panRecuaPrincipal
            // 
            this.panRecuaPrincipal.BackColor = System.Drawing.Color.Bisque;
            this.panRecuaPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaPrincipal.Controls.Add(this.labValorTotal);
            this.panRecuaPrincipal.Controls.Add(this.texValorTotal);
            this.panRecuaPrincipal.Controls.Add(this.panRecuaNomAlias);
            this.panRecuaPrincipal.Controls.Add(this.panRecuaCondiFecha);
            this.panRecuaPrincipal.Controls.Add(this.panValorTotal);
            this.panRecuaPrincipal.Location = new System.Drawing.Point(23, 150);
            this.panRecuaPrincipal.Name = "panRecuaPrincipal";
            this.panRecuaPrincipal.Size = new System.Drawing.Size(1579, 639);
            this.panRecuaPrincipal.TabIndex = 0;
            // 
            // labValorTotal
            // 
            this.labValorTotal.AutoSize = true;
            this.labValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labValorTotal.Location = new System.Drawing.Point(1051, 529);
            this.labValorTotal.Name = "labValorTotal";
            this.labValorTotal.Size = new System.Drawing.Size(85, 20);
            this.labValorTotal.TabIndex = 6;
            this.labValorTotal.Text = "Valor total:";
            // 
            // texValorTotal
            // 
            this.texValorTotal.BackColor = System.Drawing.Color.Bisque;
            this.texValorTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.texValorTotal.Enabled = false;
            this.texValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texValorTotal.Location = new System.Drawing.Point(1036, 566);
            this.texValorTotal.Name = "texValorTotal";
            this.texValorTotal.Size = new System.Drawing.Size(117, 31);
            this.texValorTotal.TabIndex = 5;
            this.texValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panRecuaNomAlias
            // 
            this.panRecuaNomAlias.BackColor = System.Drawing.Color.Tan;
            this.panRecuaNomAlias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaNomAlias.Controls.Add(this.texModelo);
            this.panRecuaNomAlias.Controls.Add(this.texAlias);
            this.panRecuaNomAlias.Location = new System.Drawing.Point(476, 21);
            this.panRecuaNomAlias.Name = "panRecuaNomAlias";
            this.panRecuaNomAlias.Size = new System.Drawing.Size(577, 94);
            this.panRecuaNomAlias.TabIndex = 0;
            // 
            // texModelo
            // 
            this.texModelo.BackColor = System.Drawing.Color.Tan;
            this.texModelo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texModelo.Location = new System.Drawing.Point(79, 18);
            this.texModelo.Name = "texModelo";
            this.texModelo.Size = new System.Drawing.Size(183, 31);
            this.texModelo.TabIndex = 2;
            // 
            // texAlias
            // 
            this.texAlias.BackColor = System.Drawing.Color.Tan;
            this.texAlias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texAlias.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texAlias.Location = new System.Drawing.Point(340, 18);
            this.texAlias.Name = "texAlias";
            this.texAlias.Size = new System.Drawing.Size(183, 31);
            this.texAlias.TabIndex = 3;
            // 
            // panRecuaCondiFecha
            // 
            this.panRecuaCondiFecha.BackColor = System.Drawing.Color.Tan;
            this.panRecuaCondiFecha.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaCondiFecha.Controls.Add(this.texDesde);
            this.panRecuaCondiFecha.Controls.Add(this.texEstado);
            this.panRecuaCondiFecha.Location = new System.Drawing.Point(287, 515);
            this.panRecuaCondiFecha.Name = "panRecuaCondiFecha";
            this.panRecuaCondiFecha.Size = new System.Drawing.Size(577, 89);
            this.panRecuaCondiFecha.TabIndex = 1;
            // 
            // texDesde
            // 
            this.texDesde.BackColor = System.Drawing.Color.Tan;
            this.texDesde.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texDesde.Location = new System.Drawing.Point(369, 27);
            this.texDesde.Name = "texDesde";
            this.texDesde.Size = new System.Drawing.Size(183, 31);
            this.texDesde.TabIndex = 5;
            this.texDesde.Click += new System.EventHandler(this.texDesde_Click);
            // 
            // texEstado
            // 
            this.texEstado.BackColor = System.Drawing.Color.Tan;
            this.texEstado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texEstado.Location = new System.Drawing.Point(66, 26);
            this.texEstado.Name = "texEstado";
            this.texEstado.Size = new System.Drawing.Size(210, 31);
            this.texEstado.TabIndex = 4;
            this.texEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.texEstado.Click += new System.EventHandler(this.texEstado_Click);
            // 
            // panValorTotal
            // 
            this.panValorTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panValorTotal.Location = new System.Drawing.Point(1009, 515);
            this.panValorTotal.Name = "panValorTotal";
            this.panValorTotal.Size = new System.Drawing.Size(168, 89);
            this.panValorTotal.TabIndex = 7;
            // 
            // labTitulo
            // 
            this.labTitulo.AutoSize = true;
            this.labTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitulo.Location = new System.Drawing.Point(549, 49);
            this.labTitulo.Name = "labTitulo";
            this.labTitulo.Size = new System.Drawing.Size(475, 31);
            this.labTitulo.TabIndex = 1;
            this.labTitulo.Text = "Modificación de materiales por modelo";
            // 
            // botModificaFoto
            // 
            this.botModificaFoto.BackColor = System.Drawing.Color.DarkKhaki;
            this.botModificaFoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botModificaFoto.Location = new System.Drawing.Point(1313, 671);
            this.botModificaFoto.Name = "botModificaFoto";
            this.botModificaFoto.Size = new System.Drawing.Size(175, 60);
            this.botModificaFoto.TabIndex = 2;
            this.botModificaFoto.Text = "MODIFICAR FOTO";
            this.botModificaFoto.UseVisualStyleBackColor = false;
            this.botModificaFoto.Click += new System.EventHandler(this.botModificaFoto_Click_1);
            this.botModificaFoto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.botModificaFoto_KeyPress);
            // 
            // botCancelar
            // 
            this.botCancelar.BackColor = System.Drawing.Color.DarkKhaki;
            this.botCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botCancelar.Location = new System.Drawing.Point(1323, 808);
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(160, 60);
            this.botCancelar.TabIndex = 3;
            this.botCancelar.Text = "CANCELAR";
            this.botCancelar.UseVisualStyleBackColor = false;
            this.botCancelar.Click += new System.EventHandler(this.botCancelar_Click);
            // 
            // botModificaMaterial
            // 
            this.botModificaMaterial.BackColor = System.Drawing.Color.DarkKhaki;
            this.botModificaMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botModificaMaterial.Location = new System.Drawing.Point(1313, 570);
            this.botModificaMaterial.Name = "botModificaMaterial";
            this.botModificaMaterial.Size = new System.Drawing.Size(175, 60);
            this.botModificaMaterial.TabIndex = 4;
            this.botModificaMaterial.Text = "MODIFICAR MATERIAL";
            this.botModificaMaterial.UseVisualStyleBackColor = false;
            this.botModificaMaterial.Click += new System.EventHandler(this.botModificaMaterial_Click);
            // 
            // picFotoModi
            // 
            this.picFotoModi.BackColor = System.Drawing.Color.PapayaWhip;
            this.picFotoModi.Location = new System.Drawing.Point(1282, 175);
            this.picFotoModi.Name = "picFotoModi";
            this.picFotoModi.Size = new System.Drawing.Size(300, 300);
            this.picFotoModi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFotoModi.TabIndex = 4;
            this.picFotoModi.TabStop = false;
            // 
            // botConfirmar
            // 
            this.botConfirmar.BackColor = System.Drawing.Color.DarkKhaki;
            this.botConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botConfirmar.Location = new System.Drawing.Point(1131, 808);
            this.botConfirmar.Name = "botConfirmar";
            this.botConfirmar.Size = new System.Drawing.Size(160, 60);
            this.botConfirmar.TabIndex = 5;
            this.botConfirmar.Text = "CONFIRMAR";
            this.botConfirmar.UseVisualStyleBackColor = false;
            this.botConfirmar.Visible = false;
            this.botConfirmar.Click += new System.EventHandler(this.botConfirmar_Click);
            // 
            // datDesdeMod
            // 
            this.datDesdeMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datDesdeMod.Location = new System.Drawing.Point(649, 826);
            this.datDesdeMod.Name = "datDesdeMod";
            this.datDesdeMod.Size = new System.Drawing.Size(192, 26);
            this.datDesdeMod.TabIndex = 6;
            this.datDesdeMod.Visible = false;
            this.datDesdeMod.ValueChanged += new System.EventHandler(this.datDesdeMod_ValueChanged);
            // 
            // frmModificacionMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1750, 906);
            this.Controls.Add(this.datDesdeMod);
            this.Controls.Add(this.botConfirmar);
            this.Controls.Add(this.picFotoModi);
            this.Controls.Add(this.botModificaFoto);
            this.Controls.Add(this.botCancelar);
            this.Controls.Add(this.botModificaMaterial);
            this.Controls.Add(this.labTitulo);
            this.Controls.Add(this.panRecuaPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmModificacionMod";
            this.ShowIcon = false;
            this.Text = "frmModificacion";
            this.Load += new System.EventHandler(this.frmModificacion_Load);
            this.Shown += new System.EventHandler(this.frmModificacionMod_Shown);
            this.panRecuaPrincipal.ResumeLayout(false);
            this.panRecuaPrincipal.PerformLayout();
            this.panRecuaNomAlias.ResumeLayout(false);
            this.panRecuaNomAlias.PerformLayout();
            this.panRecuaCondiFecha.ResumeLayout(false);
            this.panRecuaCondiFecha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFotoModi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panRecuaNomAlias;
        private System.Windows.Forms.Label labTitulo;
        private System.Windows.Forms.TextBox texModelo;
        private System.Windows.Forms.TextBox texAlias;
        private System.Windows.Forms.Button botModificaFoto;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botModificaMaterial;
        private System.Windows.Forms.Label labValorTotal;
        private System.Windows.Forms.TextBox texValorTotal;
        private System.Windows.Forms.Panel panValorTotal;
        private System.Windows.Forms.PictureBox picFotoModi;
        public System.Windows.Forms.TextBox texEstado;
        public System.Windows.Forms.TextBox texDesde;
        public System.Windows.Forms.DateTimePicker datDesdeMod;
        public System.Windows.Forms.Panel panRecuaCondiFecha;
        public System.Windows.Forms.Panel panRecuaPrincipal;
        public System.Windows.Forms.Button botConfirmar;
    }
}