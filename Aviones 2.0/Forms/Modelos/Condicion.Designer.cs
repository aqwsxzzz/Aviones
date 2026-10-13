namespace Aviones_2._0.Forms.Modelos
{
    partial class frmCondicion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCondicion));
            this.texCondicion = new System.Windows.Forms.TextBox();
            this.botCancelar = new System.Windows.Forms.Button();
            this.radbotActivo = new System.Windows.Forms.RadioButton();
            this.radbotReparacion = new System.Windows.Forms.RadioButton();
            this.radbotConstruccion = new System.Windows.Forms.RadioButton();
            this.radbotInactivo = new System.Windows.Forms.RadioButton();
            this.panCondicion = new System.Windows.Forms.Panel();
            this.panCondicion.SuspendLayout();
            this.SuspendLayout();
            // 
            // texCondicion
            // 
            resources.ApplyResources(this.texCondicion, "texCondicion");
            this.texCondicion.Name = "texCondicion";
            this.texCondicion.Click += new System.EventHandler(this.texCondicion_Click);
            // 
            // botCancelar
            // 
            this.botCancelar.BackColor = System.Drawing.Color.DarkKhaki;
            resources.ApplyResources(this.botCancelar, "botCancelar");
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.UseVisualStyleBackColor = false;
            this.botCancelar.Click += new System.EventHandler(this.botCancelar_Click);
            // 
            // radbotActivo
            // 
            resources.ApplyResources(this.radbotActivo, "radbotActivo");
            this.radbotActivo.Name = "radbotActivo";
            this.radbotActivo.TabStop = true;
            this.radbotActivo.UseVisualStyleBackColor = true;
            this.radbotActivo.CheckedChanged += new System.EventHandler(this.radbotActivo_CheckedChanged);
            // 
            // radbotReparacion
            // 
            resources.ApplyResources(this.radbotReparacion, "radbotReparacion");
            this.radbotReparacion.Name = "radbotReparacion";
            this.radbotReparacion.TabStop = true;
            this.radbotReparacion.UseVisualStyleBackColor = true;
            this.radbotReparacion.CheckedChanged += new System.EventHandler(this.radbotReparacion_CheckedChanged);
            // 
            // radbotConstruccion
            // 
            resources.ApplyResources(this.radbotConstruccion, "radbotConstruccion");
            this.radbotConstruccion.Name = "radbotConstruccion";
            this.radbotConstruccion.TabStop = true;
            this.radbotConstruccion.UseVisualStyleBackColor = true;
            // 
            // radbotInactivo
            // 
            resources.ApplyResources(this.radbotInactivo, "radbotInactivo");
            this.radbotInactivo.Name = "radbotInactivo";
            this.radbotInactivo.TabStop = true;
            this.radbotInactivo.UseVisualStyleBackColor = true;
            this.radbotInactivo.CheckedChanged += new System.EventHandler(this.radbotInactivo_CheckedChanged);
            // 
            // panCondicion
            // 
            this.panCondicion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panCondicion.Controls.Add(this.radbotReparacion);
            this.panCondicion.Controls.Add(this.radbotInactivo);
            this.panCondicion.Controls.Add(this.radbotActivo);
            this.panCondicion.Controls.Add(this.radbotConstruccion);
            resources.ApplyResources(this.panCondicion, "panCondicion");
            this.panCondicion.Name = "panCondicion";
            // 
            // frmCondicion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(180)))));
            this.ControlBox = false;
            this.Controls.Add(this.botCancelar);
            this.Controls.Add(this.texCondicion);
            this.Controls.Add(this.panCondicion);
            this.Name = "frmCondicion";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmCondicion_Load);
            this.Shown += new System.EventHandler(this.frmCondicion_Shown);
            this.Leave += new System.EventHandler(this.frmCondicion_Leave);
            this.panCondicion.ResumeLayout(false);
            this.panCondicion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TextBox texCondicion;
        private System.Windows.Forms.Button botCancelar;
        public System.Windows.Forms.RadioButton radbotActivo;
        public System.Windows.Forms.RadioButton radbotReparacion;
        public System.Windows.Forms.RadioButton radbotConstruccion;
        public System.Windows.Forms.RadioButton radbotInactivo;
        private System.Windows.Forms.Panel panCondicion;
    }
}