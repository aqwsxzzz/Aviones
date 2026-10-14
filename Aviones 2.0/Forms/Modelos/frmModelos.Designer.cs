namespace Aviones_2._0.Forms.Modelos
{
    partial class frmModelos
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
            this.flpFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.labEstado = new System.Windows.Forms.Label();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.labBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.splPrincipal = new System.Windows.Forms.SplitContainer();
            this.dgvModelos = new System.Windows.Forms.DataGridView();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidadMateriales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorMateriales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpDetalle = new System.Windows.Forms.TableLayoutPanel();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.labMateriales = new System.Windows.Forms.Label();
            this.dgvMateriales = new System.Windows.Forms.DataGridView();
            this.colMatFamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatGrupo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatCaracteristica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatMedidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatFabricante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpDetalleBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.labTotal = new System.Windows.Forms.Label();
            this.botImprimirMateriales = new System.Windows.Forms.Button();
            this.labResumen = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botNuevo = new System.Windows.Forms.Button();
            this.botModificar = new System.Windows.Forms.Button();
            this.botEliminar = new System.Windows.Forms.Button();
            this.botImprimir = new System.Windows.Forms.Button();
            this.botSalir = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            this.flpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splPrincipal)).BeginInit();
            this.splPrincipal.Panel1.SuspendLayout();
            this.splPrincipal.Panel2.SuspendLayout();
            this.splPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModelos)).BeginInit();
            this.tlpDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).BeginInit();
            this.flpDetalleBotones.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 1;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.Controls.Add(this.labTitulo, 0, 0);
            this.tlpPrincipal.Controls.Add(this.flpFiltros, 0, 1);
            this.tlpPrincipal.Controls.Add(this.splPrincipal, 0, 2);
            this.tlpPrincipal.Controls.Add(this.labResumen, 0, 3);
            this.tlpPrincipal.Controls.Add(this.flpBotones, 0, 4);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
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
            this.labTitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labTitulo.AutoSize = true;
            this.labTitulo.Font = new System.Drawing.Font("Arial", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.labTitulo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.labTitulo.Name = "labTitulo";
            this.labTitulo.Text = "Modelos";
            //
            // flpFiltros
            //
            this.flpFiltros.AutoSize = true;
            this.flpFiltros.Controls.Add(this.labEstado);
            this.flpFiltros.Controls.Add(this.cboEstado);
            this.flpFiltros.Controls.Add(this.labBuscar);
            this.flpFiltros.Controls.Add(this.txtBuscar);
            this.flpFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFiltros.Name = "flpFiltros";
            this.flpFiltros.TabIndex = 1;
            //
            // labEstado
            //
            this.labEstado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labEstado.AutoSize = true;
            this.labEstado.Name = "labEstado";
            this.labEstado.Text = "Condición:";
            //
            // cboEstado
            //
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(200, 24);
            this.cboEstado.TabIndex = 0;
            this.cboEstado.SelectedIndexChanged += new System.EventHandler(this.Filtro_Changed);
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
            this.txtBuscar.TextChanged += new System.EventHandler(this.Filtro_Changed);
            //
            // splPrincipal
            //
            this.splPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splPrincipal.Name = "splPrincipal";
            this.splPrincipal.Panel1.Controls.Add(this.dgvModelos);
            this.splPrincipal.Panel2.Controls.Add(this.tlpDetalle);
            this.splPrincipal.Panel2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.splPrincipal.Size = new System.Drawing.Size(1224, 560);
            this.splPrincipal.SplitterDistance = 560;
            this.splPrincipal.TabIndex = 2;
            //
            // dgvModelos
            //
            this.dgvModelos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNombre,
            this.colAlias,
            this.colEstado,
            this.colDesde,
            this.colCantidadMateriales,
            this.colValorMateriales});
            this.dgvModelos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvModelos.Name = "dgvModelos";
            this.dgvModelos.TabIndex = 0;
            this.dgvModelos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModelos_CellDoubleClick);
            this.dgvModelos.SelectionChanged += new System.EventHandler(this.dgvModelos_SelectionChanged);
            this.dgvModelos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvModelos_KeyDown);
            //
            // colNombre
            //
            this.colNombre.DataPropertyName = "Nombre";
            this.colNombre.FillWeight = 150F;
            this.colNombre.HeaderText = "Modelo";
            this.colNombre.MinimumWidth = 120;
            this.colNombre.Name = "colNombre";
            //
            // colAlias
            //
            this.colAlias.DataPropertyName = "Alias";
            this.colAlias.HeaderText = "Alias";
            this.colAlias.MinimumWidth = 80;
            this.colAlias.Name = "colAlias";
            //
            // colEstado
            //
            this.colEstado.DataPropertyName = "Estado";
            this.colEstado.HeaderText = "Condición";
            this.colEstado.MinimumWidth = 100;
            this.colEstado.Name = "colEstado";
            //
            // colDesde
            //
            this.colDesde.DataPropertyName = "Desde";
            this.colDesde.DefaultCellStyle.Format = "dd/MM/yyyy";
            this.colDesde.FillWeight = 70F;
            this.colDesde.HeaderText = "Desde";
            this.colDesde.MinimumWidth = 80;
            this.colDesde.Name = "colDesde";
            //
            // colCantidadMateriales
            //
            this.colCantidadMateriales.DataPropertyName = "CantidadMateriales";
            this.colCantidadMateriales.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCantidadMateriales.FillWeight = 60F;
            this.colCantidadMateriales.HeaderText = "Materiales";
            this.colCantidadMateriales.MinimumWidth = 80;
            this.colCantidadMateriales.Name = "colCantidadMateriales";
            //
            // colValorMateriales
            //
            this.colValorMateriales.DataPropertyName = "ValorMateriales";
            this.colValorMateriales.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colValorMateriales.DefaultCellStyle.Format = "N2";
            this.colValorMateriales.FillWeight = 70F;
            this.colValorMateriales.HeaderText = "Valor materiales";
            this.colValorMateriales.MinimumWidth = 90;
            this.colValorMateriales.Name = "colValorMateriales";
            //
            // tlpDetalle
            //
            this.tlpDetalle.ColumnCount = 1;
            this.tlpDetalle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDetalle.Controls.Add(this.picFoto, 0, 0);
            this.tlpDetalle.Controls.Add(this.labMateriales, 0, 1);
            this.tlpDetalle.Controls.Add(this.dgvMateriales, 0, 2);
            this.tlpDetalle.Controls.Add(this.flpDetalleBotones, 0, 3);
            this.tlpDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDetalle.Name = "tlpDetalle";
            this.tlpDetalle.RowCount = 4;
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDetalle.TabIndex = 0;
            //
            // picFoto
            //
            this.picFoto.BackColor = System.Drawing.Color.White;
            this.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picFoto.Name = "picFoto";
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFoto.TabStop = false;
            this.picFoto.DoubleClick += new System.EventHandler(this.picFoto_DoubleClick);
            //
            // labMateriales
            //
            this.labMateriales.AutoSize = true;
            this.labMateriales.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labMateriales.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.labMateriales.Name = "labMateriales";
            this.labMateriales.Text = "Materiales";
            //
            // dgvMateriales
            //
            this.dgvMateriales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMatFamilia,
            this.colMatGrupo,
            this.colMatCaracteristica,
            this.colMatMedidas,
            this.colMatFabricante,
            this.colMatCantidad,
            this.colMatUnidad,
            this.colMatSubtotal});
            this.dgvMateriales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMateriales.Name = "dgvMateriales";
            this.dgvMateriales.TabIndex = 1;
            //
            // colMatFamilia
            //
            this.colMatFamilia.DataPropertyName = "Familia";
            this.colMatFamilia.HeaderText = "Familia";
            this.colMatFamilia.MinimumWidth = 80;
            this.colMatFamilia.Name = "colMatFamilia";
            //
            // colMatGrupo
            //
            this.colMatGrupo.DataPropertyName = "Grupo";
            this.colMatGrupo.HeaderText = "Grupo";
            this.colMatGrupo.MinimumWidth = 70;
            this.colMatGrupo.Name = "colMatGrupo";
            //
            // colMatCaracteristica
            //
            this.colMatCaracteristica.DataPropertyName = "Caracteristica";
            this.colMatCaracteristica.FillWeight = 140F;
            this.colMatCaracteristica.HeaderText = "Característica";
            this.colMatCaracteristica.MinimumWidth = 100;
            this.colMatCaracteristica.Name = "colMatCaracteristica";
            //
            // colMatMedidas
            //
            this.colMatMedidas.DataPropertyName = "Medidas";
            this.colMatMedidas.HeaderText = "Medidas";
            this.colMatMedidas.MinimumWidth = 70;
            this.colMatMedidas.Name = "colMatMedidas";
            //
            // colMatFabricante
            //
            this.colMatFabricante.DataPropertyName = "Fabricante";
            this.colMatFabricante.HeaderText = "Fabricante";
            this.colMatFabricante.MinimumWidth = 80;
            this.colMatFabricante.Name = "colMatFabricante";
            //
            // colMatCantidad
            //
            this.colMatCantidad.DataPropertyName = "Cantidad";
            this.colMatCantidad.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colMatCantidad.DefaultCellStyle.Format = "0.###";
            this.colMatCantidad.FillWeight = 55F;
            this.colMatCantidad.HeaderText = "Cant.";
            this.colMatCantidad.MinimumWidth = 50;
            this.colMatCantidad.Name = "colMatCantidad";
            //
            // colMatUnidad
            //
            this.colMatUnidad.DataPropertyName = "TipoUnidad";
            this.colMatUnidad.FillWeight = 60F;
            this.colMatUnidad.HeaderText = "Unidad";
            this.colMatUnidad.MinimumWidth = 65;
            this.colMatUnidad.Name = "colMatUnidad";
            //
            // colMatSubtotal
            //
            this.colMatSubtotal.DataPropertyName = "Subtotal";
            this.colMatSubtotal.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colMatSubtotal.DefaultCellStyle.Format = "N2";
            this.colMatSubtotal.FillWeight = 65F;
            this.colMatSubtotal.HeaderText = "Subtotal";
            this.colMatSubtotal.MinimumWidth = 70;
            this.colMatSubtotal.Name = "colMatSubtotal";
            //
            // flpDetalleBotones
            //
            this.flpDetalleBotones.AutoSize = true;
            this.flpDetalleBotones.Controls.Add(this.labTotal);
            this.flpDetalleBotones.Controls.Add(this.botImprimirMateriales);
            this.flpDetalleBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDetalleBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpDetalleBotones.Name = "flpDetalleBotones";
            this.flpDetalleBotones.TabIndex = 2;
            //
            // labTotal
            //
            this.labTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTotal.AutoSize = true;
            this.labTotal.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labTotal.Name = "labTotal";
            this.labTotal.Text = "Total";
            //
            // botImprimirMateriales
            //
            this.botImprimirMateriales.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.botImprimirMateriales.Name = "botImprimirMateriales";
            this.botImprimirMateriales.Size = new System.Drawing.Size(180, 34);
            this.botImprimirMateriales.TabIndex = 0;
            this.botImprimirMateriales.Text = "Imprimir materiales";
            this.botImprimirMateriales.Click += new System.EventHandler(this.botImprimirMateriales_Click);
            //
            // labResumen
            //
            this.labResumen.AutoSize = true;
            this.labResumen.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labResumen.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.labResumen.Name = "labResumen";
            this.labResumen.Text = "Resumen";
            //
            // flpBotones
            //
            this.flpBotones.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botNuevo);
            this.flpBotones.Controls.Add(this.botModificar);
            this.flpBotones.Controls.Add(this.botEliminar);
            this.flpBotones.Controls.Add(this.botImprimir);
            this.flpBotones.Controls.Add(this.botSalir);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 3;
            //
            // botNuevo
            //
            this.botNuevo.Margin = new System.Windows.Forms.Padding(6);
            this.botNuevo.Name = "botNuevo";
            this.botNuevo.Size = new System.Drawing.Size(150, 44);
            this.botNuevo.TabIndex = 0;
            this.botNuevo.Text = "NUEVO";
            this.botNuevo.Click += new System.EventHandler(this.botNuevo_Click);
            //
            // botModificar
            //
            this.botModificar.Margin = new System.Windows.Forms.Padding(6);
            this.botModificar.Name = "botModificar";
            this.botModificar.Size = new System.Drawing.Size(150, 44);
            this.botModificar.TabIndex = 1;
            this.botModificar.Text = "MODIFICAR";
            this.botModificar.Click += new System.EventHandler(this.botModificar_Click);
            //
            // botEliminar
            //
            this.botEliminar.Margin = new System.Windows.Forms.Padding(6);
            this.botEliminar.Name = "botEliminar";
            this.botEliminar.Size = new System.Drawing.Size(150, 44);
            this.botEliminar.TabIndex = 2;
            this.botEliminar.Text = "ELIMINAR";
            this.botEliminar.Click += new System.EventHandler(this.botEliminar_Click);
            //
            // botImprimir
            //
            this.botImprimir.Margin = new System.Windows.Forms.Padding(24, 6, 6, 6);
            this.botImprimir.Name = "botImprimir";
            this.botImprimir.Size = new System.Drawing.Size(150, 44);
            this.botImprimir.TabIndex = 3;
            this.botImprimir.Text = "IMPRIMIR LISTA";
            this.botImprimir.Click += new System.EventHandler(this.botImprimir_Click);
            //
            // botSalir
            //
            this.botSalir.Margin = new System.Windows.Forms.Padding(24, 6, 6, 6);
            this.botSalir.Name = "botSalir";
            this.botSalir.Size = new System.Drawing.Size(150, 44);
            this.botSalir.TabIndex = 4;
            this.botSalir.Text = "SALIR";
            this.botSalir.Click += new System.EventHandler(this.botSalir_Click);
            //
            // frmModelos
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmModelos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modelos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmModelos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModelos_KeyDown);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.flpFiltros.ResumeLayout(false);
            this.flpFiltros.PerformLayout();
            this.splPrincipal.Panel1.ResumeLayout(false);
            this.splPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splPrincipal)).EndInit();
            this.splPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModelos)).EndInit();
            this.tlpDetalle.ResumeLayout(false);
            this.tlpDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).EndInit();
            this.flpDetalleBotones.ResumeLayout(false);
            this.flpDetalleBotones.PerformLayout();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labTitulo;
        private System.Windows.Forms.FlowLayoutPanel flpFiltros;
        private System.Windows.Forms.Label labEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Label labBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.SplitContainer splPrincipal;
        private System.Windows.Forms.DataGridView dgvModelos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesde;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidadMateriales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorMateriales;
        private System.Windows.Forms.TableLayoutPanel tlpDetalle;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Label labMateriales;
        private System.Windows.Forms.DataGridView dgvMateriales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatFamilia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatGrupo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatCaracteristica;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatMedidas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatFabricante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatSubtotal;
        private System.Windows.Forms.FlowLayoutPanel flpDetalleBotones;
        private System.Windows.Forms.Label labTotal;
        private System.Windows.Forms.Button botImprimirMateriales;
        private System.Windows.Forms.Label labResumen;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botNuevo;
        private System.Windows.Forms.Button botModificar;
        private System.Windows.Forms.Button botEliminar;
        private System.Windows.Forms.Button botImprimir;
        private System.Windows.Forms.Button botSalir;
    }
}
