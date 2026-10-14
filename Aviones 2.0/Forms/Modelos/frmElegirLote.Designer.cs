namespace Aviones_2._0.Forms.Modelos
{
    partial class frmElegirLote
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
            this.flpFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.labFamilia = new System.Windows.Forms.Label();
            this.cboFamilia = new System.Windows.Forms.ComboBox();
            this.labBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvMateriales = new System.Windows.Forms.DataGridView();
            this.colFamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGrupo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCaracteristica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMedidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labLotes = new System.Windows.Forms.Label();
            this.dgvLotes = new System.Windows.Forms.DataGridView();
            this.colLoteFabricante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteUbicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteDisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpAgregar = new System.Windows.Forms.FlowLayoutPanel();
            this.labCantidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.botAgregar = new System.Windows.Forms.Button();
            this.labEstado = new System.Windows.Forms.Label();
            this.botListo = new System.Windows.Forms.Button();
            this.timBuscar = new System.Windows.Forms.Timer();
            this.tlpPrincipal.SuspendLayout();
            this.flpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLotes)).BeginInit();
            this.flpAgregar.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 2;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.Controls.Add(this.flpFiltros, 0, 0);
            this.tlpPrincipal.Controls.Add(this.dgvMateriales, 0, 1);
            this.tlpPrincipal.Controls.Add(this.labLotes, 0, 2);
            this.tlpPrincipal.Controls.Add(this.dgvLotes, 0, 3);
            this.tlpPrincipal.Controls.Add(this.flpAgregar, 0, 4);
            this.tlpPrincipal.Controls.Add(this.botListo, 1, 4);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(12);
            this.tlpPrincipal.RowCount = 5;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.SetColumnSpan(this.flpFiltros, 2);
            this.tlpPrincipal.SetColumnSpan(this.dgvMateriales, 2);
            this.tlpPrincipal.SetColumnSpan(this.labLotes, 2);
            this.tlpPrincipal.SetColumnSpan(this.dgvLotes, 2);
            this.tlpPrincipal.TabIndex = 0;
            //
            // flpFiltros
            //
            this.flpFiltros.AutoSize = true;
            this.flpFiltros.Controls.Add(this.labFamilia);
            this.flpFiltros.Controls.Add(this.cboFamilia);
            this.flpFiltros.Controls.Add(this.labBuscar);
            this.flpFiltros.Controls.Add(this.txtBuscar);
            this.flpFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFiltros.Name = "flpFiltros";
            this.flpFiltros.TabIndex = 0;
            //
            // labFamilia
            //
            this.labFamilia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFamilia.AutoSize = true;
            this.labFamilia.Name = "labFamilia";
            this.labFamilia.Text = "Familia:";
            //
            // cboFamilia
            //
            this.cboFamilia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFamilia.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.cboFamilia.Name = "cboFamilia";
            this.cboFamilia.Size = new System.Drawing.Size(220, 24);
            this.cboFamilia.TabIndex = 0;
            this.cboFamilia.SelectedIndexChanged += new System.EventHandler(this.Filtro_Changed);
            //
            // labBuscar
            //
            this.labBuscar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labBuscar.AutoSize = true;
            this.labBuscar.Name = "labBuscar";
            this.labBuscar.Text = "Buscar:";
            //
            // txtBuscar
            //
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(300, 23);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            //
            // dgvMateriales
            //
            this.dgvMateriales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFamilia,
            this.colGrupo,
            this.colCaracteristica,
            this.colColor,
            this.colMedidas,
            this.colUnidad,
            this.colDisponible});
            this.dgvMateriales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMateriales.Name = "dgvMateriales";
            this.dgvMateriales.TabIndex = 1;
            this.dgvMateriales.SelectionChanged += new System.EventHandler(this.dgvMateriales_SelectionChanged);
            //
            // colFamilia
            //
            this.colFamilia.DataPropertyName = "Familia";
            this.colFamilia.HeaderText = "Familia";
            this.colFamilia.Name = "colFamilia";
            //
            // colGrupo
            //
            this.colGrupo.DataPropertyName = "Grupo";
            this.colGrupo.HeaderText = "Grupo";
            this.colGrupo.Name = "colGrupo";
            //
            // colCaracteristica
            //
            this.colCaracteristica.DataPropertyName = "Caracteristica";
            this.colCaracteristica.FillWeight = 150F;
            this.colCaracteristica.HeaderText = "Característica";
            this.colCaracteristica.Name = "colCaracteristica";
            //
            // colColor
            //
            this.colColor.DataPropertyName = "Color";
            this.colColor.HeaderText = "Color";
            this.colColor.Name = "colColor";
            //
            // colMedidas
            //
            this.colMedidas.DataPropertyName = "Medidas";
            this.colMedidas.HeaderText = "Medidas";
            this.colMedidas.Name = "colMedidas";
            //
            // colUnidad
            //
            this.colUnidad.DataPropertyName = "TipoUnidad";
            this.colUnidad.FillWeight = 70F;
            this.colUnidad.HeaderText = "Unidad";
            this.colUnidad.Name = "colUnidad";
            //
            // colDisponible
            //
            this.colDisponible.DataPropertyName = "Disponible";
            this.colDisponible.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDisponible.DefaultCellStyle.Format = "0.###";
            this.colDisponible.FillWeight = 70F;
            this.colDisponible.HeaderText = "Disponible";
            this.colDisponible.Name = "colDisponible";
            //
            // labLotes
            //
            this.labLotes.AutoSize = true;
            this.labLotes.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labLotes.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.labLotes.Name = "labLotes";
            this.labLotes.Text = "Lotes con stock";
            //
            // dgvLotes
            //
            this.dgvLotes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLoteFabricante,
            this.colLoteUbicacion,
            this.colLoteDisponible,
            this.colLoteValor,
            this.colLoteFecha});
            this.dgvLotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLotes.Name = "dgvLotes";
            this.dgvLotes.TabIndex = 2;
            //
            // colLoteFabricante
            //
            this.colLoteFabricante.DataPropertyName = "Fabricante";
            this.colLoteFabricante.HeaderText = "Fabricante";
            this.colLoteFabricante.Name = "colLoteFabricante";
            //
            // colLoteUbicacion
            //
            this.colLoteUbicacion.DataPropertyName = "Ubicacion";
            this.colLoteUbicacion.HeaderText = "Ubicación";
            this.colLoteUbicacion.Name = "colLoteUbicacion";
            //
            // colLoteDisponible
            //
            this.colLoteDisponible.DataPropertyName = "Disponible";
            this.colLoteDisponible.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLoteDisponible.DefaultCellStyle.Format = "0.###";
            this.colLoteDisponible.HeaderText = "Disponible";
            this.colLoteDisponible.Name = "colLoteDisponible";
            //
            // colLoteValor
            //
            this.colLoteValor.DataPropertyName = "ValorUnitario";
            this.colLoteValor.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLoteValor.DefaultCellStyle.Format = "N2";
            this.colLoteValor.HeaderText = "Valor unit.";
            this.colLoteValor.Name = "colLoteValor";
            //
            // colLoteFecha
            //
            this.colLoteFecha.DataPropertyName = "FechaIngreso";
            this.colLoteFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            this.colLoteFecha.HeaderText = "Ingreso";
            this.colLoteFecha.Name = "colLoteFecha";
            //
            // flpAgregar
            //
            this.flpAgregar.AutoSize = true;
            this.flpAgregar.Controls.Add(this.labCantidad);
            this.flpAgregar.Controls.Add(this.txtCantidad);
            this.flpAgregar.Controls.Add(this.botAgregar);
            this.flpAgregar.Controls.Add(this.labEstado);
            this.flpAgregar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAgregar.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.flpAgregar.Name = "flpAgregar";
            this.flpAgregar.TabIndex = 3;
            //
            // labCantidad
            //
            this.labCantidad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCantidad.AutoSize = true;
            this.labCantidad.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labCantidad.Name = "labCantidad";
            this.labCantidad.Text = "Cantidad a usar:";
            //
            // txtCantidad
            //
            this.txtCantidad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCantidad.Font = new System.Drawing.Font("Arial", 12F);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(110, 26);
            this.txtCantidad.TabIndex = 0;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCantidad_KeyDown);
            //
            // botAgregar
            //
            this.botAgregar.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.botAgregar.Name = "botAgregar";
            this.botAgregar.Size = new System.Drawing.Size(200, 40);
            this.botAgregar.TabIndex = 1;
            this.botAgregar.Text = "AGREGAR AL MODELO";
            this.botAgregar.Click += new System.EventHandler(this.botAgregar_Click);
            //
            // labEstado
            //
            this.labEstado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labEstado.AutoSize = true;
            this.labEstado.ForeColor = System.Drawing.Color.DarkGreen;
            this.labEstado.Margin = new System.Windows.Forms.Padding(16, 0, 3, 0);
            this.labEstado.Name = "labEstado";
            this.labEstado.Text = "";
            //
            // botListo
            //
            this.botListo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.botListo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.botListo.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.botListo.Name = "botListo";
            this.botListo.Size = new System.Drawing.Size(140, 40);
            this.botListo.TabIndex = 4;
            this.botListo.Text = "LISTO";
            //
            // timBuscar
            //
            this.timBuscar.Interval = 300;
            this.timBuscar.Tick += new System.EventHandler(this.timBuscar_Tick);
            //
            // frmElegirLote
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.CancelButton = this.botListo;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "frmElegirLote";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar materiales al modelo";
            this.Load += new System.EventHandler(this.frmElegirLote_Load);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.flpFiltros.ResumeLayout(false);
            this.flpFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLotes)).EndInit();
            this.flpAgregar.ResumeLayout(false);
            this.flpAgregar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.FlowLayoutPanel flpFiltros;
        private System.Windows.Forms.Label labFamilia;
        private System.Windows.Forms.ComboBox cboFamilia;
        private System.Windows.Forms.Label labBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvMateriales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFamilia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGrupo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCaracteristica;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMedidas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisponible;
        private System.Windows.Forms.Label labLotes;
        private System.Windows.Forms.DataGridView dgvLotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteFabricante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteUbicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteDisponible;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteFecha;
        private System.Windows.Forms.FlowLayoutPanel flpAgregar;
        private System.Windows.Forms.Label labCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button botAgregar;
        private System.Windows.Forms.Label labEstado;
        private System.Windows.Forms.Button botListo;
        private System.Windows.Forms.Timer timBuscar;
    }
}
