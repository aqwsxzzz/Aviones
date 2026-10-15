namespace Aviones_2._0
{
    partial class PantallaPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Materiales = new System.Windows.Forms.Button();
            this.Modelos = new System.Windows.Forms.Button();
            this.Compras = new System.Windows.Forms.Button();
            this.Salir = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.botFondo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Materiales
            // 
            this.Materiales.BackColor = System.Drawing.Color.CadetBlue;
            this.Materiales.Font = new System.Drawing.Font("Adobe Fangsong Std R", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Materiales.Location = new System.Drawing.Point(241, 124);
            this.Materiales.Margin = new System.Windows.Forms.Padding(4);
            this.Materiales.Name = "Materiales";
            this.Materiales.Size = new System.Drawing.Size(230, 111);
            this.Materiales.TabIndex = 0;
            this.Materiales.Text = "MATERIALES";
            this.Materiales.UseVisualStyleBackColor = false;
            this.Materiales.Click += new System.EventHandler(this.Materiales_Click);
            // 
            // Modelos
            // 
            this.Modelos.BackColor = System.Drawing.Color.CadetBlue;
            this.Modelos.Font = new System.Drawing.Font("Adobe Fangsong Std R", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Modelos.Location = new System.Drawing.Point(241, 311);
            this.Modelos.Margin = new System.Windows.Forms.Padding(4);
            this.Modelos.Name = "Modelos";
            this.Modelos.Size = new System.Drawing.Size(230, 111);
            this.Modelos.TabIndex = 1;
            this.Modelos.Text = "MODELOS";
            this.Modelos.UseVisualStyleBackColor = false;
            this.Modelos.Click += new System.EventHandler(this.Modelos_Click);
            // 
            // Compras
            // 
            this.Compras.BackColor = System.Drawing.Color.CadetBlue;
            this.Compras.Font = new System.Drawing.Font("Adobe Fangsong Std R", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Compras.Location = new System.Drawing.Point(241, 508);
            this.Compras.Margin = new System.Windows.Forms.Padding(4);
            this.Compras.Name = "Compras";
            this.Compras.Size = new System.Drawing.Size(230, 111);
            this.Compras.TabIndex = 2;
            this.Compras.Text = "COMPRAS";
            this.Compras.UseVisualStyleBackColor = false;
            this.Compras.Click += new System.EventHandler(this.Compras_Click);
            // 
            // Salir
            // 
            this.Salir.BackColor = System.Drawing.Color.DarkKhaki;
            this.Salir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Salir.FlatAppearance.BorderSize = 0;
            this.Salir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Salir.Location = new System.Drawing.Point(1376, 711);
            this.Salir.Margin = new System.Windows.Forms.Padding(4);
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(160, 60);
            this.Salir.TabIndex = 3;
            this.Salir.Text = "SALIR DEL SISTEMA";
            this.Salir.UseVisualStyleBackColor = false;
            this.Salir.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // botFondo
            // 
            this.botFondo.BackColor = System.Drawing.Color.Gainsboro;
            this.botFondo.Font = new System.Drawing.Font("Arial", 9F);
            this.botFondo.Margin = new System.Windows.Forms.Padding(4);
            this.botFondo.Name = "botFondo";
            this.botFondo.Size = new System.Drawing.Size(130, 34);
            this.botFondo.TabIndex = 5;
            this.botFondo.Text = "Cambiar fondo...";
            this.botFondo.UseVisualStyleBackColor = false;
            this.botFondo.Click += new System.EventHandler(this.botFondo_Click);
            // 
            // PantallaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1884, 1061);
            this.Controls.Add(this.botFondo);
            this.Controls.Add(this.Salir);
            this.Controls.Add(this.Compras);
            this.Controls.Add(this.Modelos);
            this.Controls.Add(this.Materiales);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PantallaPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Materiales;
        private System.Windows.Forms.Button Modelos;
        private System.Windows.Forms.Button Compras;
        private System.Windows.Forms.Button Salir;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button botFondo;
    }
}

