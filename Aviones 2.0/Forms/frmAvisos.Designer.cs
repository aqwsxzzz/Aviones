namespace Aviones_2._0.Common
{
    partial class frmAvisos
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
            this.labTitulo1 = new System.Windows.Forms.Label();
            this.botCancela = new System.Windows.Forms.Button();
            this.botElimina = new System.Windows.Forms.Button();
            this.picboxRecuaElimina = new System.Windows.Forms.PictureBox();
            this.labTitulo2 = new System.Windows.Forms.Label();
            this.labTitulo3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaElimina)).BeginInit();
            this.SuspendLayout();
            // 
            // labTitulo1
            // 
            this.labTitulo1.AutoSize = true;
            this.labTitulo1.BackColor = System.Drawing.Color.DarkSalmon;
            this.labTitulo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitulo1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.labTitulo1.Location = new System.Drawing.Point(141, 23);
            this.labTitulo1.Name = "labTitulo1";
            this.labTitulo1.Size = new System.Drawing.Size(120, 24);
            this.labTitulo1.TabIndex = 7;
            this.labTitulo1.Text = "Está seguro?";
            // 
            // botCancela
            // 
            this.botCancela.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botCancela.Location = new System.Drawing.Point(206, 132);
            this.botCancela.Name = "botCancela";
            this.botCancela.Size = new System.Drawing.Size(124, 42);
            this.botCancela.TabIndex = 6;
            this.botCancela.Text = "CANCELAR";
            this.botCancela.UseVisualStyleBackColor = true;
            this.botCancela.Click += new System.EventHandler(this.botCancela_Click);
            // 
            // botElimina
            // 
            this.botElimina.BackColor = System.Drawing.SystemColors.HighlightText;
            this.botElimina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botElimina.Location = new System.Drawing.Point(57, 132);
            this.botElimina.Name = "botElimina";
            this.botElimina.Size = new System.Drawing.Size(124, 42);
            this.botElimina.TabIndex = 5;
            this.botElimina.Text = "ELIMINAR";
            this.botElimina.UseVisualStyleBackColor = false;
            // 
            // picboxRecuaElimina
            // 
            this.picboxRecuaElimina.BackColor = System.Drawing.Color.DarkSalmon;
            this.picboxRecuaElimina.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picboxRecuaElimina.Location = new System.Drawing.Point(5, 7);
            this.picboxRecuaElimina.Name = "picboxRecuaElimina";
            this.picboxRecuaElimina.Size = new System.Drawing.Size(378, 210);
            this.picboxRecuaElimina.TabIndex = 4;
            this.picboxRecuaElimina.TabStop = false;
            // 
            // labTitulo2
            // 
            this.labTitulo2.AutoSize = true;
            this.labTitulo2.BackColor = System.Drawing.Color.DarkSalmon;
            this.labTitulo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitulo2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.labTitulo2.Location = new System.Drawing.Point(141, 51);
            this.labTitulo2.Name = "labTitulo2";
            this.labTitulo2.Size = new System.Drawing.Size(20, 24);
            this.labTitulo2.TabIndex = 8;
            this.labTitulo2.Text = "2";
            // 
            // labTitulo3
            // 
            this.labTitulo3.AutoSize = true;
            this.labTitulo3.BackColor = System.Drawing.Color.DarkSalmon;
            this.labTitulo3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitulo3.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.labTitulo3.Location = new System.Drawing.Point(141, 84);
            this.labTitulo3.Name = "labTitulo3";
            this.labTitulo3.Size = new System.Drawing.Size(20, 24);
            this.labTitulo3.TabIndex = 9;
            this.labTitulo3.Text = "3";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Location = new System.Drawing.Point(313, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(51, 59);
            this.panel1.TabIndex = 10;
            this.panel1.Visible = false;
            // 
            // frmAvisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(388, 223);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labTitulo1);
            this.Controls.Add(this.labTitulo3);
            this.Controls.Add(this.labTitulo2);
            this.Controls.Add(this.botCancela);
            this.Controls.Add(this.botElimina);
            this.Controls.Add(this.picboxRecuaElimina);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAvisos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Avusos";
            this.Shown += new System.EventHandler(this.frmAvisos_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaElimina)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labTitulo1;
        public System.Windows.Forms.Button botCancela;
        public System.Windows.Forms.Button botElimina;
        public System.Windows.Forms.PictureBox picboxRecuaElimina;
        public System.Windows.Forms.Label labTitulo2;
        public System.Windows.Forms.Label labTitulo3;
        private System.Windows.Forms.Panel panel1;
    }
}