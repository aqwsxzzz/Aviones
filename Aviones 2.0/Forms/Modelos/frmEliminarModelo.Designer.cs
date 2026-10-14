namespace Aviones_2._0.Forms.Modelos
{
    partial class frmEliminarModelo
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
            this.labTitulo = new System.Windows.Forms.Label();
            this.labExplicacion = new System.Windows.Forms.Label();
            this.dgvMateriales = new System.Windows.Forms.DataGridView();
            this.colFamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFabricante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCondicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVuelveNuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVuelveUsado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescarta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpMarcar = new System.Windows.Forms.FlowLayoutPanel();
            this.botTodos = new System.Windows.Forms.Button();
            this.botNinguno = new System.Windows.Forms.Button();
            this.labResumen = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botCancelar = new System.Windows.Forms.Button();
            this.botEliminar = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).BeginInit();
            this.flpMarcar.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 1;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.Controls.Add(this.labTitulo, 0, 0);
            this.tlpPrincipal.Controls.Add(this.labExplicacion, 0, 1);
            this.tlpPrincipal.Controls.Add(this.dgvMateriales, 0, 2);
            this.tlpPrincipal.Controls.Add(this.flpMarcar, 0, 3);
            this.tlpPrincipal.Controls.Add(this.flpBotones, 0, 4);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(16);
            this.tlpPrincipal.RowCount = 5;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.TabIndex = 0;
            //
            // labTitulo
            //
            this.labTitulo.AutoSize = true;
            this.labTitulo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.labTitulo.Name = "labTitulo";
            this.labTitulo.Text = "Eliminar modelo";
            //
            // labExplicacion
            //
            this.labExplicacion.AutoSize = true;
            this.labExplicacion.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.labExplicacion.Name = "labExplicacion";
            this.labExplicacion.Text = "Para cada material escriba cuánto vuelve al stock como nuevo (no se llegó a usar) y cuánto como usado (se recicla).\r\nLo que no vuelve se descarta.";
            //
            // dgvMateriales
            //
            this.dgvMateriales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFamilia,
            this.colMaterial,
            this.colFabricante,
            this.colCondicion,
            this.colUsado,
            this.colVuelveNuevo,
            this.colVuelveUsado,
            this.colDescarta,
            this.colUnidad});
            this.dgvMateriales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMateriales.Name = "dgvMateriales";
            this.dgvMateriales.TabIndex = 0;
            this.dgvMateriales.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvMateriales_CellBeginEdit);
            this.dgvMateriales.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMateriales_CellFormatting);
            this.dgvMateriales.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dgvMateriales_CellParsing);
            this.dgvMateriales.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvMateriales_CellValidating);
            this.dgvMateriales.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMateriales_CellValueChanged);
            this.dgvMateriales.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMateriales_DataError);
            //
            // colFamilia
            //
            this.colFamilia.DataPropertyName = "Familia";
            this.colFamilia.FillWeight = 80F;
            this.colFamilia.HeaderText = "Familia";
            this.colFamilia.Name = "colFamilia";
            this.colFamilia.ReadOnly = true;
            //
            // colMaterial
            //
            this.colMaterial.DataPropertyName = "Material";
            this.colMaterial.FillWeight = 200F;
            this.colMaterial.HeaderText = "Material";
            this.colMaterial.Name = "colMaterial";
            this.colMaterial.ReadOnly = true;
            //
            // colFabricante
            //
            this.colFabricante.DataPropertyName = "Fabricante";
            this.colFabricante.HeaderText = "Fabricante (lote)";
            this.colFabricante.Name = "colFabricante";
            this.colFabricante.ReadOnly = true;
            //
            // colCondicion
            //
            this.colCondicion.DataPropertyName = "Condicion";
            this.colCondicion.FillWeight = 50F;
            this.colCondicion.HeaderText = "Salió";
            this.colCondicion.Name = "colCondicion";
            this.colCondicion.ReadOnly = true;
            //
            // colUsado
            //
            this.colUsado.DataPropertyName = "Usado";
            this.colUsado.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colUsado.DefaultCellStyle.Format = "0.###";
            this.colUsado.FillWeight = 50F;
            this.colUsado.HeaderText = "En el modelo";
            this.colUsado.Name = "colUsado";
            this.colUsado.ReadOnly = true;
            //
            // colVuelveNuevo
            //
            this.colVuelveNuevo.DataPropertyName = "VuelveNuevo";
            this.colVuelveNuevo.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colVuelveNuevo.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.colVuelveNuevo.DefaultCellStyle.Format = "0.###";
            this.colVuelveNuevo.FillWeight = 60F;
            this.colVuelveNuevo.HeaderText = "Vuelve nuevo";
            this.colVuelveNuevo.Name = "colVuelveNuevo";
            //
            // colVuelveUsado
            //
            this.colVuelveUsado.DataPropertyName = "VuelveUsado";
            this.colVuelveUsado.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colVuelveUsado.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.colVuelveUsado.DefaultCellStyle.Format = "0.###";
            this.colVuelveUsado.FillWeight = 60F;
            this.colVuelveUsado.HeaderText = "Vuelve usado";
            this.colVuelveUsado.Name = "colVuelveUsado";
            //
            // colDescarta
            //
            this.colDescarta.DataPropertyName = "SeDescarta";
            this.colDescarta.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDescarta.DefaultCellStyle.Format = "0.###";
            this.colDescarta.DefaultCellStyle.ForeColor = System.Drawing.Color.Firebrick;
            this.colDescarta.FillWeight = 60F;
            this.colDescarta.HeaderText = "Se descarta";
            this.colDescarta.Name = "colDescarta";
            this.colDescarta.ReadOnly = true;
            //
            // colUnidad
            //
            this.colUnidad.DataPropertyName = "Unidad";
            this.colUnidad.FillWeight = 60F;
            this.colUnidad.HeaderText = "Unidad";
            this.colUnidad.Name = "colUnidad";
            this.colUnidad.ReadOnly = true;
            //
            // flpMarcar
            //
            this.flpMarcar.AutoSize = true;
            this.flpMarcar.Controls.Add(this.botTodos);
            this.flpMarcar.Controls.Add(this.botNinguno);
            this.flpMarcar.Controls.Add(this.labResumen);
            this.flpMarcar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMarcar.Name = "flpMarcar";
            this.flpMarcar.TabIndex = 1;
            //
            // botTodos
            //
            this.botTodos.AutoSize = true;
            this.botTodos.Name = "botTodos";
            this.botTodos.TabIndex = 0;
            this.botTodos.Text = "Todo vuelve como usado";
            this.botTodos.Click += new System.EventHandler(this.botTodos_Click);
            //
            // botNinguno
            //
            this.botNinguno.AutoSize = true;
            this.botNinguno.Name = "botNinguno";
            this.botNinguno.TabIndex = 1;
            this.botNinguno.Text = "Todo se descarta";
            this.botNinguno.Click += new System.EventHandler(this.botNinguno_Click);
            //
            // labResumen
            //
            this.labResumen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labResumen.AutoSize = true;
            this.labResumen.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labResumen.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.labResumen.Name = "labResumen";
            this.labResumen.Text = "";
            //
            // flpBotones
            //
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botCancelar);
            this.flpBotones.Controls.Add(this.botEliminar);
            this.flpBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 2;
            //
            // botCancelar
            //
            this.botCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCancelar.Margin = new System.Windows.Forms.Padding(6);
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(130, 42);
            this.botCancelar.TabIndex = 1;
            this.botCancelar.Text = "CANCELAR";
            //
            // botEliminar
            //
            this.botEliminar.Margin = new System.Windows.Forms.Padding(6);
            this.botEliminar.Name = "botEliminar";
            this.botEliminar.Size = new System.Drawing.Size(200, 42);
            this.botEliminar.TabIndex = 0;
            this.botEliminar.Text = "ELIMINAR MODELO";
            this.botEliminar.Click += new System.EventHandler(this.botEliminar_Click);
            //
            // frmEliminarModelo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.CancelButton = this.botCancelar;
            this.ClientSize = new System.Drawing.Size(1120, 620);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 420);
            this.Name = "frmEliminarModelo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Eliminar modelo";
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).EndInit();
            this.flpMarcar.ResumeLayout(false);
            this.flpMarcar.PerformLayout();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labTitulo;
        private System.Windows.Forms.Label labExplicacion;
        private System.Windows.Forms.DataGridView dgvMateriales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFamilia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFabricante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCondicion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVuelveNuevo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVuelveUsado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescarta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnidad;
        private System.Windows.Forms.FlowLayoutPanel flpMarcar;
        private System.Windows.Forms.Button botTodos;
        private System.Windows.Forms.Button botNinguno;
        private System.Windows.Forms.Label labResumen;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botEliminar;
    }
}
