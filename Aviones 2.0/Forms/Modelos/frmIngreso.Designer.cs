namespace Aviones_2._0.Forms.Modelos
{
    partial class frmIngreso
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
            this.panCajaGrande = new System.Windows.Forms.Panel();
            this.picboxFotoMod = new System.Windows.Forms.PictureBox();
            this.botAgregaMaterial = new System.Windows.Forms.Button();
            this.panCajaTextos = new System.Windows.Forms.Panel();
            this.labAlias = new System.Windows.Forms.Label();
            this.labNombre = new System.Windows.Forms.Label();
            this.texAlias = new System.Windows.Forms.TextBox();
            this.texNombre = new System.Windows.Forms.TextBox();
            this.botGuardar = new System.Windows.Forms.Button();
            this.labIngreso = new System.Windows.Forms.Label();
            this.botSalir = new System.Windows.Forms.Button();
            this.panCajaEstadoFecha = new System.Windows.Forms.Panel();
            this.texDesde = new System.Windows.Forms.TextBox();
            this.texCondicionIngreso = new System.Windows.Forms.TextBox();
            this.datDesde = new System.Windows.Forms.DateTimePicker();
            this.panCajaGrande.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxFotoMod)).BeginInit();
            this.panCajaTextos.SuspendLayout();
            this.panCajaEstadoFecha.SuspendLayout();
            this.SuspendLayout();
            // 
            // panCajaGrande
            // 
            this.panCajaGrande.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(180)))));
            this.panCajaGrande.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panCajaGrande.Controls.Add(this.picboxFotoMod);
            this.panCajaGrande.Controls.Add(this.botAgregaMaterial);
            this.panCajaGrande.Controls.Add(this.panCajaTextos);
            this.panCajaGrande.Location = new System.Drawing.Point(393, 171);
            this.panCajaGrande.Name = "panCajaGrande";
            this.panCajaGrande.Size = new System.Drawing.Size(987, 460);
            this.panCajaGrande.TabIndex = 0;
            // 
            // picboxFotoMod
            // 
            this.picboxFotoMod.Location = new System.Drawing.Point(817, 58);
            this.picboxFotoMod.Name = "picboxFotoMod";
            this.picboxFotoMod.Size = new System.Drawing.Size(367, 367);
            this.picboxFotoMod.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picboxFotoMod.TabIndex = 5;
            this.picboxFotoMod.TabStop = false;
            this.picboxFotoMod.Visible = false;
            this.picboxFotoMod.Click += new System.EventHandler(this.picboxFotoMod_Click);
            // 
            // botAgregaMaterial
            // 
            this.botAgregaMaterial.BackColor = System.Drawing.Color.Sienna;
            this.botAgregaMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botAgregaMaterial.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.botAgregaMaterial.Location = new System.Drawing.Point(430, 338);
            this.botAgregaMaterial.Name = "botAgregaMaterial";
            this.botAgregaMaterial.Size = new System.Drawing.Size(184, 48);
            this.botAgregaMaterial.TabIndex = 1;
            this.botAgregaMaterial.Text = "AGREGAR MATERIAL";
            this.botAgregaMaterial.UseVisualStyleBackColor = false;
            this.botAgregaMaterial.Visible = false;
            this.botAgregaMaterial.Click += new System.EventHandler(this.botAgregaMaterial_Click);
            // 
            // panCajaTextos
            // 
            this.panCajaTextos.BackColor = System.Drawing.Color.BurlyWood;
            this.panCajaTextos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panCajaTextos.Controls.Add(this.labAlias);
            this.panCajaTextos.Controls.Add(this.labNombre);
            this.panCajaTextos.Controls.Add(this.texAlias);
            this.panCajaTextos.Controls.Add(this.texNombre);
            this.panCajaTextos.Location = new System.Drawing.Point(168, 153);
            this.panCajaTextos.Name = "panCajaTextos";
            this.panCajaTextos.Size = new System.Drawing.Size(643, 137);
            this.panCajaTextos.TabIndex = 0;
            // 
            // labAlias
            // 
            this.labAlias.AutoSize = true;
            this.labAlias.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labAlias.ForeColor = System.Drawing.Color.Black;
            this.labAlias.Location = new System.Drawing.Point(340, 24);
            this.labAlias.Name = "labAlias";
            this.labAlias.Size = new System.Drawing.Size(50, 24);
            this.labAlias.TabIndex = 3;
            this.labAlias.Text = "Alias";
            this.labAlias.Visible = false;
            // 
            // labNombre
            // 
            this.labNombre.AutoSize = true;
            this.labNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labNombre.ForeColor = System.Drawing.Color.Black;
            this.labNombre.Location = new System.Drawing.Point(109, 24);
            this.labNombre.Name = "labNombre";
            this.labNombre.Size = new System.Drawing.Size(79, 24);
            this.labNombre.TabIndex = 2;
            this.labNombre.Text = "Nombre";
            // 
            // texAlias
            // 
            this.texAlias.BackColor = System.Drawing.Color.BurlyWood;
            this.texAlias.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texAlias.Location = new System.Drawing.Point(340, 49);
            this.texAlias.MaxLength = 13;
            this.texAlias.Name = "texAlias";
            this.texAlias.Size = new System.Drawing.Size(198, 38);
            this.texAlias.TabIndex = 1;
            this.texAlias.Visible = false;
            this.texAlias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.texAlias_KeyDown);
            this.texAlias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.texAlias_KeyPress);
            // 
            // texNombre
            // 
            this.texNombre.BackColor = System.Drawing.Color.BurlyWood;
            this.texNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texNombre.Location = new System.Drawing.Point(112, 49);
            this.texNombre.MaxLength = 13;
            this.texNombre.Name = "texNombre";
            this.texNombre.Size = new System.Drawing.Size(198, 38);
            this.texNombre.TabIndex = 0;
            this.texNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.texNombre_KeyPress);
            // 
            // botGuardar
            // 
            this.botGuardar.BackColor = System.Drawing.Color.DarkKhaki;
            this.botGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botGuardar.Location = new System.Drawing.Point(1259, 826);
            this.botGuardar.Name = "botGuardar";
            this.botGuardar.Size = new System.Drawing.Size(160, 60);
            this.botGuardar.TabIndex = 2;
            this.botGuardar.Text = "GUARDAR";
            this.botGuardar.UseVisualStyleBackColor = false;
            this.botGuardar.Visible = false;
            this.botGuardar.Click += new System.EventHandler(this.botGuardar_Click);
            // 
            // labIngreso
            // 
            this.labIngreso.AutoSize = true;
            this.labIngreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labIngreso.Location = new System.Drawing.Point(743, 38);
            this.labIngreso.Name = "labIngreso";
            this.labIngreso.Size = new System.Drawing.Size(237, 31);
            this.labIngreso.TabIndex = 1;
            this.labIngreso.Text = "Ingreso de modelo";
            // 
            // botSalir
            // 
            this.botSalir.BackColor = System.Drawing.Color.DarkKhaki;
            this.botSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botSalir.Location = new System.Drawing.Point(1457, 826);
            this.botSalir.Name = "botSalir";
            this.botSalir.Size = new System.Drawing.Size(160, 60);
            this.botSalir.TabIndex = 2;
            this.botSalir.Text = "SALIR";
            this.botSalir.UseVisualStyleBackColor = false;
            this.botSalir.Click += new System.EventHandler(this.botSalir_Click);
            // 
            // panCajaEstadoFecha
            // 
            this.panCajaEstadoFecha.BackColor = System.Drawing.Color.BurlyWood;
            this.panCajaEstadoFecha.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panCajaEstadoFecha.Controls.Add(this.texDesde);
            this.panCajaEstadoFecha.Controls.Add(this.texCondicionIngreso);
            this.panCajaEstadoFecha.Location = new System.Drawing.Point(553, 666);
            this.panCajaEstadoFecha.Name = "panCajaEstadoFecha";
            this.panCajaEstadoFecha.Size = new System.Drawing.Size(672, 95);
            this.panCajaEstadoFecha.TabIndex = 3;
            this.panCajaEstadoFecha.Visible = false;
            // 
            // texDesde
            // 
            this.texDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texDesde.Location = new System.Drawing.Point(392, 18);
            this.texDesde.Name = "texDesde";
            this.texDesde.Size = new System.Drawing.Size(249, 35);
            this.texDesde.TabIndex = 1;
            this.texDesde.Visible = false;
            this.texDesde.Click += new System.EventHandler(this.texDesde_Click);
            // 
            // texCondicionIngreso
            // 
            this.texCondicionIngreso.BackColor = System.Drawing.Color.BurlyWood;
            this.texCondicionIngreso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.texCondicionIngreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texCondicionIngreso.Location = new System.Drawing.Point(75, 18);
            this.texCondicionIngreso.Name = "texCondicionIngreso";
            this.texCondicionIngreso.Size = new System.Drawing.Size(168, 29);
            this.texCondicionIngreso.TabIndex = 0;
            this.texCondicionIngreso.Visible = false;
            this.texCondicionIngreso.Click += new System.EventHandler(this.texCondicion_Click);
            // 
            // datDesde
            // 
            this.datDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datDesde.Location = new System.Drawing.Point(947, 767);
            this.datDesde.Name = "datDesde";
            this.datDesde.Size = new System.Drawing.Size(249, 22);
            this.datDesde.TabIndex = 4;
            this.datDesde.Visible = false;
            this.datDesde.ValueChanged += new System.EventHandler(this.datDesde_ValueChanged);
            // 
            // frmIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1793, 885);
            this.Controls.Add(this.datDesde);
            this.Controls.Add(this.panCajaEstadoFecha);
            this.Controls.Add(this.botGuardar);
            this.Controls.Add(this.botSalir);
            this.Controls.Add(this.labIngreso);
            this.Controls.Add(this.panCajaGrande);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmIngreso";
            this.ShowIcon = false;
            this.Text = "frmIngreso";
            this.Load += new System.EventHandler(this.frmIngreso_Load);
            this.panCajaGrande.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picboxFotoMod)).EndInit();
            this.panCajaTextos.ResumeLayout(false);
            this.panCajaTextos.PerformLayout();
            this.panCajaEstadoFecha.ResumeLayout(false);
            this.panCajaEstadoFecha.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button botAgregaMaterial;
        public System.Windows.Forms.Button botGuardar;
        public System.Windows.Forms.Panel panCajaTextos;
        public System.Windows.Forms.Panel panCajaGrande;
        public System.Windows.Forms.TextBox texAlias;
        public System.Windows.Forms.TextBox texNombre;
        public System.Windows.Forms.Label labAlias;
        public System.Windows.Forms.Label labNombre;
        public System.Windows.Forms.Panel panCajaEstadoFecha;
        public System.Windows.Forms.TextBox texDesde;
        public System.Windows.Forms.TextBox texCondicionIngreso;
        public System.Windows.Forms.Button botSalir;
        public System.Windows.Forms.Label labIngreso;
        public System.Windows.Forms.DateTimePicker datDesde;
        public System.Windows.Forms.PictureBox picboxFotoMod;
    }
}