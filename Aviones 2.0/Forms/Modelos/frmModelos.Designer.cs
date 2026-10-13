namespace Aviones_2._0.Forms.Modelos
{
    partial class frmModelos
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
            this.Salir = new System.Windows.Forms.Button();
            this.TituloPincipal = new System.Windows.Forms.Label();
            this.botIngresar = new System.Windows.Forms.Button();
            this.botModificar = new System.Windows.Forms.Button();
            this.botEliminar = new System.Windows.Forms.Button();
            this.botListados = new System.Windows.Forms.Button();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.panIngresoBorra = new System.Windows.Forms.Panel();
            this.botCancelar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxMod = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMod)).BeginInit();
            this.SuspendLayout();
            // 
            // Salir
            // 
            this.Salir.BackColor = System.Drawing.Color.DarkKhaki;
            this.Salir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Salir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Salir.Location = new System.Drawing.Point(1503, 714);
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(160, 60);
            this.Salir.TabIndex = 0;
            this.Salir.Text = "SALIR";
            this.Salir.UseVisualStyleBackColor = false;
            this.Salir.Click += new System.EventHandler(this.Salir_Click_1);
            // 
            // TituloPincipal
            // 
            this.TituloPincipal.AutoSize = true;
            this.TituloPincipal.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TituloPincipal.Location = new System.Drawing.Point(878, 85);
            this.TituloPincipal.Name = "TituloPincipal";
            this.TituloPincipal.Size = new System.Drawing.Size(162, 33);
            this.TituloPincipal.TabIndex = 2;
            this.TituloPincipal.Text = "MODELOS";
            // 
            // botIngresar
            // 
            this.botIngresar.BackColor = System.Drawing.Color.BurlyWood;
            this.botIngresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botIngresar.Location = new System.Drawing.Point(601, 714);
            this.botIngresar.Name = "botIngresar";
            this.botIngresar.Size = new System.Drawing.Size(160, 59);
            this.botIngresar.TabIndex = 3;
            this.botIngresar.Text = "INGRESAR / BORRAR";
            this.botIngresar.UseVisualStyleBackColor = false;
            this.botIngresar.Click += new System.EventHandler(this.botIngresar_Click);
            // 
            // botModificar
            // 
            this.botModificar.BackColor = System.Drawing.Color.SeaShell;
            this.botModificar.Enabled = false;
            this.botModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botModificar.ForeColor = System.Drawing.Color.Black;
            this.botModificar.Location = new System.Drawing.Point(781, 714);
            this.botModificar.Name = "botModificar";
            this.botModificar.Size = new System.Drawing.Size(160, 59);
            this.botModificar.TabIndex = 4;
            this.botModificar.Text = "MODIFICAR";
            this.botModificar.UseVisualStyleBackColor = false;
            this.botModificar.Click += new System.EventHandler(this.botModificar_Click);
            // 
            // botEliminar
            // 
            this.botEliminar.BackColor = System.Drawing.Color.SeaShell;
            this.botEliminar.Enabled = false;
            this.botEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botEliminar.ForeColor = System.Drawing.Color.Black;
            this.botEliminar.Location = new System.Drawing.Point(961, 714);
            this.botEliminar.Name = "botEliminar";
            this.botEliminar.Size = new System.Drawing.Size(160, 59);
            this.botEliminar.TabIndex = 5;
            this.botEliminar.Text = "ELIMINAR";
            this.botEliminar.UseVisualStyleBackColor = false;
            // 
            // botListados
            // 
            this.botListados.BackColor = System.Drawing.Color.BurlyWood;
            this.botListados.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botListados.ForeColor = System.Drawing.Color.Black;
            this.botListados.Location = new System.Drawing.Point(1140, 714);
            this.botListados.Name = "botListados";
            this.botListados.Size = new System.Drawing.Size(160, 59);
            this.botListados.TabIndex = 6;
            this.botListados.Text = "LISTADOS";
            this.botListados.UseVisualStyleBackColor = false;
            // 
            // panIngresoBorra
            // 
            this.panIngresoBorra.AccessibleRole = System.Windows.Forms.AccessibleRole.PageTabList;
            this.panIngresoBorra.BackColor = System.Drawing.Color.DarkSalmon;
            this.panIngresoBorra.Location = new System.Drawing.Point(216, 532);
            this.panIngresoBorra.Name = "panIngresoBorra";
            this.panIngresoBorra.Size = new System.Drawing.Size(170, 109);
            this.panIngresoBorra.TabIndex = 7;
            this.panIngresoBorra.Visible = false;
            // 
            // botCancelar
            // 
            this.botCancelar.BackColor = System.Drawing.Color.DarkKhaki;
            this.botCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botCancelar.Location = new System.Drawing.Point(1503, 626);
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(159, 59);
            this.botCancelar.TabIndex = 8;
            this.botCancelar.Text = "CANCELAR";
            this.botCancelar.UseVisualStyleBackColor = false;
            this.botCancelar.Visible = false;
            this.botCancelar.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Bisque;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBoxMod);
            this.panel2.Location = new System.Drawing.Point(351, 282);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1216, 482);
            this.panel2.TabIndex = 9;
            // 
            // pictureBoxMod
            // 
            this.pictureBoxMod.Location = new System.Drawing.Point(933, 61);
            this.pictureBoxMod.Name = "pictureBoxMod";
            this.pictureBoxMod.Size = new System.Drawing.Size(237, 205);
            this.pictureBoxMod.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMod.TabIndex = 10;
            this.pictureBoxMod.TabStop = false;
            // 
            // frmModelos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1801, 849);
            this.Controls.Add(this.botCancelar);
            this.Controls.Add(this.panIngresoBorra);
            this.Controls.Add(this.botListados);
            this.Controls.Add(this.botEliminar);
            this.Controls.Add(this.botModificar);
            this.Controls.Add(this.botIngresar);
            this.Controls.Add(this.TituloPincipal);
            this.Controls.Add(this.Salir);
            this.Controls.Add(this.panel2);
            this.Name = "frmModelos";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmModelos_Shown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TituloPincipal;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.Panel panIngresoBorra;
        public System.Windows.Forms.Button botIngresar;
        public System.Windows.Forms.Button botModificar;
        public System.Windows.Forms.Button botEliminar;
        public System.Windows.Forms.Button botListados;
        public System.Windows.Forms.Button Salir;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBoxMod;
    }
}