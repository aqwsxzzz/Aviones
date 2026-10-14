namespace Aviones_2._0.Forms.Comun
{
    partial class frmListaEditable
    {
        private System.ComponentModel.IContainer components = null;

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
            this.tlpPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.flpAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.labNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.botAgregar = new System.Windows.Forms.Button();
            this.botRenombrar = new System.Windows.Forms.Button();
            this.botEliminar = new System.Windows.Forms.Button();
            this.botCerrar = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            this.flpAcciones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 2;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.Controls.Add(this.lstItems, 0, 0);
            this.tlpPrincipal.Controls.Add(this.flpAcciones, 1, 0);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(12);
            this.tlpPrincipal.RowCount = 1;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.TabIndex = 0;
            //
            // lstItems
            //
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.Font = new System.Drawing.Font("Arial", 11F);
            this.lstItems.IntegralHeight = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.TabIndex = 1;
            this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
            //
            // flpAcciones
            //
            this.flpAcciones.AutoSize = true;
            this.flpAcciones.Controls.Add(this.labNombre);
            this.flpAcciones.Controls.Add(this.txtNombre);
            this.flpAcciones.Controls.Add(this.botAgregar);
            this.flpAcciones.Controls.Add(this.botRenombrar);
            this.flpAcciones.Controls.Add(this.botEliminar);
            this.flpAcciones.Controls.Add(this.botCerrar);
            this.flpAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAcciones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAcciones.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.flpAcciones.Name = "flpAcciones";
            this.flpAcciones.TabIndex = 0;
            //
            // labNombre
            //
            this.labNombre.AutoSize = true;
            this.labNombre.Name = "labNombre";
            this.labNombre.Text = "Nombre";
            //
            // txtNombre
            //
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.txtNombre.MaxLength = 60;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(200, 23);
            this.txtNombre.TabIndex = 0;
            //
            // botAgregar
            //
            this.botAgregar.Name = "botAgregar";
            this.botAgregar.Size = new System.Drawing.Size(200, 38);
            this.botAgregar.TabIndex = 1;
            this.botAgregar.Text = "AGREGAR";
            this.botAgregar.Click += new System.EventHandler(this.botAgregar_Click);
            //
            // botRenombrar
            //
            this.botRenombrar.Name = "botRenombrar";
            this.botRenombrar.Size = new System.Drawing.Size(200, 38);
            this.botRenombrar.TabIndex = 2;
            this.botRenombrar.Text = "CAMBIAR NOMBRE";
            this.botRenombrar.Click += new System.EventHandler(this.botRenombrar_Click);
            //
            // botEliminar
            //
            this.botEliminar.Name = "botEliminar";
            this.botEliminar.Size = new System.Drawing.Size(200, 38);
            this.botEliminar.TabIndex = 3;
            this.botEliminar.Text = "ELIMINAR";
            this.botEliminar.Click += new System.EventHandler(this.botEliminar_Click);
            //
            // botCerrar
            //
            this.botCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCerrar.Margin = new System.Windows.Forms.Padding(3, 24, 3, 3);
            this.botCerrar.Name = "botCerrar";
            this.botCerrar.Size = new System.Drawing.Size(200, 38);
            this.botCerrar.TabIndex = 4;
            this.botCerrar.Text = "CERRAR";
            //
            // frmListaEditable
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.CancelButton = this.botCerrar;
            this.ClientSize = new System.Drawing.Size(560, 440);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListaEditable";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lista";
            this.Load += new System.EventHandler(this.frmListaEditable_Load);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.flpAcciones.ResumeLayout(false);
            this.flpAcciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.FlowLayoutPanel flpAcciones;
        private System.Windows.Forms.Label labNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button botAgregar;
        private System.Windows.Forms.Button botRenombrar;
        private System.Windows.Forms.Button botEliminar;
        private System.Windows.Forms.Button botCerrar;
    }
}
