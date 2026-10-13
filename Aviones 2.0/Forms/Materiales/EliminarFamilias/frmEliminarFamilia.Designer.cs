namespace Aviones_2._0.Forms.Materiales.EliminarFamilias
{
    partial class frmEliminarFamilia
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
            this.listFamiliaElim = new System.Windows.Forms.ListBox();
            this.botEliminarElim = new System.Windows.Forms.Button();
            this.botCancelarElim = new System.Windows.Forms.Button();
            this.panRecuaFamilia = new System.Windows.Forms.Panel();
            this.botModificarElim = new System.Windows.Forms.Button();
            this.panelEmergente = new System.Windows.Forms.Panel();
            this.labNuevaFamilia = new System.Windows.Forms.Label();
            this.botCancelarNuevaFam = new System.Windows.Forms.Button();
            this.botAceptarNuevaFam = new System.Windows.Forms.Button();
            this.comboxNuevaFam = new System.Windows.Forms.ComboBox();
            this.labFamiliaElim = new System.Windows.Forms.Label();
            this.panRecuaElimina = new System.Windows.Forms.Panel();
            this.panelEmergente.SuspendLayout();
            this.SuspendLayout();
            // 
            // listFamiliaElim
            // 
            this.listFamiliaElim.BackColor = System.Drawing.Color.BurlyWood;
            this.listFamiliaElim.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listFamiliaElim.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listFamiliaElim.FormattingEnabled = true;
            this.listFamiliaElim.ItemHeight = 24;
            this.listFamiliaElim.Location = new System.Drawing.Point(34, 21);
            this.listFamiliaElim.Name = "listFamiliaElim";
            this.listFamiliaElim.Size = new System.Drawing.Size(205, 432);
            this.listFamiliaElim.Sorted = true;
            this.listFamiliaElim.TabIndex = 0;
            this.listFamiliaElim.SelectedIndexChanged += new System.EventHandler(this.listFamiliaElim_SelectedIndexChanged);
            // 
            // botEliminarElim
            // 
            this.botEliminarElim.BackColor = System.Drawing.Color.SteelBlue;
            this.botEliminarElim.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botEliminarElim.ForeColor = System.Drawing.Color.Red;
            this.botEliminarElim.Location = new System.Drawing.Point(1289, 749);
            this.botEliminarElim.Name = "botEliminarElim";
            this.botEliminarElim.Size = new System.Drawing.Size(150, 50);
            this.botEliminarElim.TabIndex = 1;
            this.botEliminarElim.Text = "Eliminar";
            this.botEliminarElim.UseVisualStyleBackColor = false;
            this.botEliminarElim.Visible = false;
            this.botEliminarElim.Click += new System.EventHandler(this.botEliminarElim_Click);
            // 
            // botCancelarElim
            // 
            this.botCancelarElim.BackColor = System.Drawing.Color.SteelBlue;
            this.botCancelarElim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botCancelarElim.Location = new System.Drawing.Point(1468, 749);
            this.botCancelarElim.Name = "botCancelarElim";
            this.botCancelarElim.Size = new System.Drawing.Size(150, 50);
            this.botCancelarElim.TabIndex = 2;
            this.botCancelarElim.Text = "Cancelar";
            this.botCancelarElim.UseVisualStyleBackColor = false;
            this.botCancelarElim.Click += new System.EventHandler(this.botCancelarElim_Click);
            // 
            // panRecuaFamilia
            // 
            this.panRecuaFamilia.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panRecuaFamilia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panRecuaFamilia.Location = new System.Drawing.Point(492, 150);
            this.panRecuaFamilia.Name = "panRecuaFamilia";
            this.panRecuaFamilia.Size = new System.Drawing.Size(200, 100);
            this.panRecuaFamilia.TabIndex = 3;
            // 
            // botModificarElim
            // 
            this.botModificarElim.BackColor = System.Drawing.Color.SteelBlue;
            this.botModificarElim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botModificarElim.Location = new System.Drawing.Point(1115, 750);
            this.botModificarElim.Name = "botModificarElim";
            this.botModificarElim.Size = new System.Drawing.Size(150, 50);
            this.botModificarElim.TabIndex = 4;
            this.botModificarElim.Text = "Modificar";
            this.botModificarElim.UseVisualStyleBackColor = false;
            this.botModificarElim.Visible = false;
            this.botModificarElim.Click += new System.EventHandler(this.botModificarElim_Click);
            // 
            // panelEmergente
            // 
            this.panelEmergente.BackColor = System.Drawing.Color.LightSlateGray;
            this.panelEmergente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEmergente.Controls.Add(this.labNuevaFamilia);
            this.panelEmergente.Controls.Add(this.botCancelarNuevaFam);
            this.panelEmergente.Controls.Add(this.botAceptarNuevaFam);
            this.panelEmergente.Controls.Add(this.comboxNuevaFam);
            this.panelEmergente.Location = new System.Drawing.Point(687, 311);
            this.panelEmergente.Name = "panelEmergente";
            this.panelEmergente.Size = new System.Drawing.Size(350, 199);
            this.panelEmergente.TabIndex = 5;
            this.panelEmergente.Visible = false;
            // 
            // labNuevaFamilia
            // 
            this.labNuevaFamilia.AutoSize = true;
            this.labNuevaFamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labNuevaFamilia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labNuevaFamilia.Location = new System.Drawing.Point(66, 21);
            this.labNuevaFamilia.Name = "labNuevaFamilia";
            this.labNuevaFamilia.Size = new System.Drawing.Size(227, 24);
            this.labNuevaFamilia.TabIndex = 3;
            this.labNuevaFamilia.Text = "Elige una nueva familia";
            // 
            // botCancelarNuevaFam
            // 
            this.botCancelarNuevaFam.Location = new System.Drawing.Point(188, 137);
            this.botCancelarNuevaFam.Name = "botCancelarNuevaFam";
            this.botCancelarNuevaFam.Size = new System.Drawing.Size(92, 32);
            this.botCancelarNuevaFam.TabIndex = 2;
            this.botCancelarNuevaFam.Text = "Cancelar";
            this.botCancelarNuevaFam.UseVisualStyleBackColor = true;
            this.botCancelarNuevaFam.Click += new System.EventHandler(this.botCancelarNuevaFam_Click);
            // 
            // botAceptarNuevaFam
            // 
            this.botAceptarNuevaFam.Location = new System.Drawing.Point(64, 137);
            this.botAceptarNuevaFam.Name = "botAceptarNuevaFam";
            this.botAceptarNuevaFam.Size = new System.Drawing.Size(91, 32);
            this.botAceptarNuevaFam.TabIndex = 1;
            this.botAceptarNuevaFam.Text = "Aceptar";
            this.botAceptarNuevaFam.UseVisualStyleBackColor = true;
            this.botAceptarNuevaFam.Click += new System.EventHandler(this.botAceptarNuevaFam_Click);
            // 
            // comboxNuevaFam
            // 
            this.comboxNuevaFam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboxNuevaFam.FormattingEnabled = true;
            this.comboxNuevaFam.Location = new System.Drawing.Point(113, 74);
            this.comboxNuevaFam.Name = "comboxNuevaFam";
            this.comboxNuevaFam.Size = new System.Drawing.Size(121, 24);
            this.comboxNuevaFam.Sorted = true;
            this.comboxNuevaFam.TabIndex = 0;
            this.comboxNuevaFam.DropDown += new System.EventHandler(this.comboxNuevaFam_DropDown);
            this.comboxNuevaFam.DropDownClosed += new System.EventHandler(this.comboxNuevaFam_DropDownClosed);
            // 
            // labFamiliaElim
            // 
            this.labFamiliaElim.AutoSize = true;
            this.labFamiliaElim.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labFamiliaElim.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFamiliaElim.Location = new System.Drawing.Point(1110, 83);
            this.labFamiliaElim.Name = "labFamiliaElim";
            this.labFamiliaElim.Size = new System.Drawing.Size(112, 29);
            this.labFamiliaElim.TabIndex = 6;
            this.labFamiliaElim.Text = "Familias";
            this.labFamiliaElim.Visible = false;
            // 
            // panRecuaElimina
            // 
            this.panRecuaElimina.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panRecuaElimina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panRecuaElimina.Location = new System.Drawing.Point(1195, 473);
            this.panRecuaElimina.Name = "panRecuaElimina";
            this.panRecuaElimina.Size = new System.Drawing.Size(253, 136);
            this.panRecuaElimina.TabIndex = 7;
            // 
            // frmEliminarFamilia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1840, 807);
            this.Controls.Add(this.labFamiliaElim);
            this.Controls.Add(this.panelEmergente);
            this.Controls.Add(this.botModificarElim);
            this.Controls.Add(this.panRecuaFamilia);
            this.Controls.Add(this.botCancelarElim);
            this.Controls.Add(this.botEliminarElim);
            this.Controls.Add(this.listFamiliaElim);
            this.Controls.Add(this.panRecuaElimina);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEliminarFamilia";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmEliminarFamilia";
            this.panelEmergente.ResumeLayout(false);
            this.panelEmergente.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listFamiliaElim;
        private System.Windows.Forms.Panel panRecuaFamilia;
        public System.Windows.Forms.Button botModificarElim;
        public System.Windows.Forms.Button botEliminarElim;
        public System.Windows.Forms.Button botCancelarElim;
        private System.Windows.Forms.Panel panelEmergente;
        private System.Windows.Forms.ComboBox comboxNuevaFam;
        private System.Windows.Forms.Button botCancelarNuevaFam;
        private System.Windows.Forms.Button botAceptarNuevaFam;
        private System.Windows.Forms.Label labNuevaFamilia;
        public System.Windows.Forms.Label labFamiliaElim;
        public System.Windows.Forms.Panel panRecuaElimina;
    }
}