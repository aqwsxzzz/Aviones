namespace Aviones_2._0.Forms.Materiales
{
    partial class frmMateriales
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
            this.components = new System.ComponentModel.Container();
            this.tlpPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.labTitulo = new System.Windows.Forms.Label();
            this.flpFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.labFamilia = new System.Windows.Forms.Label();
            this.cboFamilia = new System.Windows.Forms.ComboBox();
            this.labBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.chkSinStock = new System.Windows.Forms.CheckBox();
            this.chkAComprar = new System.Windows.Forms.CheckBox();
            this.botLimpiarFiltros = new System.Windows.Forms.Button();
            this.splPrincipal = new System.Windows.Forms.SplitContainer();
            this.dgvMateriales = new System.Windows.Forms.DataGridView();
            this.colFamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGrupo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCaracteristica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMedidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDispNuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDispUsado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorDisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAComprar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpDetalle = new System.Windows.Forms.TableLayoutPanel();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.labLotes = new System.Windows.Forms.Label();
            this.dgvLotes = new System.Windows.Forms.DataGridView();
            this.colLoteFabricante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteUbicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteNuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteUsado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoteFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpBotonesLote = new System.Windows.Forms.FlowLayoutPanel();
            this.botAgregarLote = new System.Windows.Forms.Button();
            this.botModificarLote = new System.Windows.Forms.Button();
            this.botEliminarLote = new System.Windows.Forms.Button();
            this.labUsos = new System.Windows.Forms.Label();
            this.dgvUsos = new System.Windows.Forms.DataGridView();
            this.colUsoModelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsoAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsoCondicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsoCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labResumen = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botNuevo = new System.Windows.Forms.Button();
            this.botModificar = new System.Windows.Forms.Button();
            this.botEliminar = new System.Windows.Forms.Button();
            this.botComprar = new System.Windows.Forms.Button();
            this.botFamilias = new System.Windows.Forms.Button();
            this.botTipos = new System.Windows.Forms.Button();
            this.botInforme = new System.Windows.Forms.Button();
            this.botImprimir = new System.Windows.Forms.Button();
            this.botSalir = new System.Windows.Forms.Button();
            this.timBuscar = new System.Windows.Forms.Timer(this.components);
            this.tlpPrincipal.SuspendLayout();
            this.flpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splPrincipal)).BeginInit();
            this.splPrincipal.Panel1.SuspendLayout();
            this.splPrincipal.Panel2.SuspendLayout();
            this.splPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).BeginInit();
            this.tlpDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLotes)).BeginInit();
            this.flpBotonesLote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsos)).BeginInit();
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
            this.tlpPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.tlpPrincipal.RowCount = 5;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.Size = new System.Drawing.Size(1264, 761);
            this.tlpPrincipal.TabIndex = 0;
            //
            // labTitulo
            //
            this.labTitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labTitulo.AutoSize = true;
            this.labTitulo.Font = new System.Drawing.Font("Arial", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.labTitulo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.labTitulo.Name = "labTitulo";
            this.labTitulo.TabIndex = 0;
            this.labTitulo.Text = "Materiales";
            //
            // flpFiltros
            //
            this.flpFiltros.AutoSize = true;
            this.flpFiltros.Controls.Add(this.labFamilia);
            this.flpFiltros.Controls.Add(this.cboFamilia);
            this.flpFiltros.Controls.Add(this.labBuscar);
            this.flpFiltros.Controls.Add(this.txtBuscar);
            this.flpFiltros.Controls.Add(this.chkSinStock);
            this.flpFiltros.Controls.Add(this.chkAComprar);
            this.flpFiltros.Controls.Add(this.botLimpiarFiltros);
            this.flpFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFiltros.Name = "flpFiltros";
            this.flpFiltros.TabIndex = 1;
            //
            // labFamilia
            //
            this.labFamilia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFamilia.AutoSize = true;
            this.labFamilia.Name = "labFamilia";
            this.labFamilia.TabIndex = 0;
            this.labFamilia.Text = "Familia:";
            //
            // cboFamilia
            //
            this.cboFamilia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFamilia.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.cboFamilia.Name = "cboFamilia";
            this.cboFamilia.Size = new System.Drawing.Size(220, 24);
            this.cboFamilia.TabIndex = 1;
            this.cboFamilia.SelectedIndexChanged += new System.EventHandler(this.Filtro_Changed);
            //
            // labBuscar
            //
            this.labBuscar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labBuscar.AutoSize = true;
            this.labBuscar.Name = "labBuscar";
            this.labBuscar.TabIndex = 2;
            this.labBuscar.Text = "Buscar:";
            //
            // txtBuscar
            //
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(300, 23);
            this.txtBuscar.TabIndex = 3;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            //
            // chkSinStock
            //
            this.chkSinStock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSinStock.AutoSize = true;
            this.chkSinStock.Name = "chkSinStock";
            this.chkSinStock.TabIndex = 4;
            this.chkSinStock.Text = "Sin stock";
            this.chkSinStock.CheckedChanged += new System.EventHandler(this.Filtro_Changed);
            //
            // chkAComprar
            //
            this.chkAComprar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAComprar.AutoSize = true;
            this.chkAComprar.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.chkAComprar.Name = "chkAComprar";
            this.chkAComprar.TabIndex = 5;
            this.chkAComprar.Text = "Con pedido de compra";
            this.chkAComprar.CheckedChanged += new System.EventHandler(this.Filtro_Changed);
            //
            // botLimpiarFiltros
            //
            this.botLimpiarFiltros.AutoSize = true;
            this.botLimpiarFiltros.Name = "botLimpiarFiltros";
            this.botLimpiarFiltros.TabIndex = 6;
            this.botLimpiarFiltros.Text = "Limpiar filtros";
            this.botLimpiarFiltros.Click += new System.EventHandler(this.botLimpiarFiltros_Click);
            //
            // splPrincipal
            //
            this.splPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splPrincipal.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splPrincipal.Name = "splPrincipal";
            this.splPrincipal.Panel1.Controls.Add(this.dgvMateriales);
            this.splPrincipal.Panel2.Controls.Add(this.tlpDetalle);
            this.splPrincipal.Panel2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.splPrincipal.Size = new System.Drawing.Size(1224, 560);
            this.splPrincipal.SplitterDistance = 760;
            this.splPrincipal.TabIndex = 2;
            //
            // dgvMateriales
            //
            this.dgvMateriales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFamilia,
            this.colGrupo,
            this.colCaracteristica,
            this.colColor,
            this.colMedidas,
            this.colCodigo,
            this.colUnidad,
            this.colCantidad,
            this.colDispNuevo,
            this.colDispUsado,
            this.colDisponible,
            this.colValorDisponible,
            this.colAComprar,
            this.colLotes});
            this.dgvMateriales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMateriales.Name = "dgvMateriales";
            this.dgvMateriales.TabIndex = 0;
            this.dgvMateriales.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMateriales_CellDoubleClick);
            this.dgvMateriales.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMateriales_CellFormatting);
            this.dgvMateriales.SelectionChanged += new System.EventHandler(this.dgvMateriales_SelectionChanged);
            this.dgvMateriales.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvMateriales_KeyDown);
            //
            // colFamilia
            //
            this.colFamilia.DataPropertyName = "Familia";
            this.colFamilia.FillWeight = 110F;
            this.colFamilia.HeaderText = "Familia";
            this.colFamilia.MinimumWidth = 95;
            this.colFamilia.Name = "colFamilia";
            //
            // colGrupo
            //
            this.colGrupo.DataPropertyName = "Grupo";
            this.colGrupo.FillWeight = 110F;
            this.colGrupo.HeaderText = "Grupo";
            this.colGrupo.MinimumWidth = 90;
            this.colGrupo.Name = "colGrupo";
            //
            // colCaracteristica
            //
            this.colCaracteristica.DataPropertyName = "Caracteristica";
            this.colCaracteristica.FillWeight = 150F;
            this.colCaracteristica.HeaderText = "Característica";
            this.colCaracteristica.MinimumWidth = 120;
            this.colCaracteristica.Name = "colCaracteristica";
            //
            // colColor
            //
            this.colColor.DataPropertyName = "Color";
            this.colColor.HeaderText = "Color";
            this.colColor.MinimumWidth = 70;
            this.colColor.Name = "colColor";
            //
            // colMedidas
            //
            this.colMedidas.DataPropertyName = "Medidas";
            this.colMedidas.HeaderText = "Medidas";
            this.colMedidas.MinimumWidth = 80;
            this.colMedidas.Name = "colMedidas";
            //
            // colCodigo
            //
            this.colCodigo.DataPropertyName = "Codigo";
            this.colCodigo.FillWeight = 80F;
            this.colCodigo.HeaderText = "Código";
            this.colCodigo.MinimumWidth = 70;
            this.colCodigo.Name = "colCodigo";
            //
            // colDispNuevo
            //
            this.colDispNuevo.DataPropertyName = "DisponibleNuevo";
            this.colDispNuevo.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDispNuevo.DefaultCellStyle.Format = "0.###";
            this.colDispNuevo.FillWeight = 55F;
            this.colDispNuevo.HeaderText = "Nuevo";
            this.colDispNuevo.MinimumWidth = 55;
            this.colDispNuevo.Name = "colDispNuevo";
            //
            // colDispUsado
            //
            this.colDispUsado.DataPropertyName = "DisponibleUsado";
            this.colDispUsado.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDispUsado.DefaultCellStyle.Format = "0.###";
            this.colDispUsado.FillWeight = 55F;
            this.colDispUsado.HeaderText = "Usado";
            this.colDispUsado.MinimumWidth = 55;
            this.colDispUsado.Name = "colDispUsado";
            //
            // colUnidad
            //
            this.colUnidad.DataPropertyName = "TipoUnidad";
            this.colUnidad.FillWeight = 65F;
            this.colUnidad.HeaderText = "Unidad";
            this.colUnidad.MinimumWidth = 70;
            this.colUnidad.Name = "colUnidad";
            //
            // colCantidad
            //
            this.colCantidad.DataPropertyName = "Cantidad";
            this.colCantidad.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCantidad.DefaultCellStyle.Format = "0.###";
            this.colCantidad.FillWeight = 55F;
            this.colCantidad.HeaderText = "Cant.";
            this.colCantidad.MinimumWidth = 55;
            this.colCantidad.Name = "colCantidad";
            //
            // colDisponible
            //
            this.colDisponible.DataPropertyName = "Disponible";
            this.colDisponible.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDisponible.DefaultCellStyle.Format = "0.###";
            this.colDisponible.FillWeight = 65F;
            this.colDisponible.HeaderText = "Disponible total";
            this.colDisponible.MinimumWidth = 80;
            this.colDisponible.Name = "colDisponible";
            //
            // colValorDisponible
            //
            this.colValorDisponible.DataPropertyName = "ValorDisponible";
            this.colValorDisponible.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colValorDisponible.DefaultCellStyle.Format = "N2";
            this.colValorDisponible.FillWeight = 70F;
            this.colValorDisponible.HeaderText = "Valor disp.";
            this.colValorDisponible.MinimumWidth = 85;
            this.colValorDisponible.Name = "colValorDisponible";
            //
            // colAComprar
            //
            this.colAComprar.DataPropertyName = "PedidoPendiente";
            this.colAComprar.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colAComprar.DefaultCellStyle.Format = "0.###";
            this.colAComprar.FillWeight = 60F;
            this.colAComprar.HeaderText = "Pedido";
            this.colAComprar.MinimumWidth = 80;
            this.colAComprar.Name = "colAComprar";
            //
            // colLotes
            //
            this.colLotes.DataPropertyName = "CantidadLotes";
            this.colLotes.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLotes.FillWeight = 40F;
            this.colLotes.HeaderText = "Lotes";
            this.colLotes.MinimumWidth = 50;
            this.colLotes.Name = "colLotes";
            //
            // tlpDetalle
            //
            this.tlpDetalle.ColumnCount = 1;
            this.tlpDetalle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDetalle.Controls.Add(this.picFoto, 0, 0);
            this.tlpDetalle.Controls.Add(this.labLotes, 0, 1);
            this.tlpDetalle.Controls.Add(this.dgvLotes, 0, 2);
            this.tlpDetalle.Controls.Add(this.flpBotonesLote, 0, 3);
            this.tlpDetalle.Controls.Add(this.labUsos, 0, 4);
            this.tlpDetalle.Controls.Add(this.dgvUsos, 0, 5);
            this.tlpDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDetalle.Name = "tlpDetalle";
            this.tlpDetalle.RowCount = 6;
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
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
            // labLotes
            //
            this.labLotes.AutoSize = true;
            this.labLotes.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labLotes.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.labLotes.Name = "labLotes";
            this.labLotes.Text = "Lotes (ingresos)";
            //
            // dgvLotes
            //
            this.dgvLotes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLoteFabricante,
            this.colLoteUbicacion,
            this.colLoteCompra,
            this.colLoteCantidad,
            this.colLoteNuevo,
            this.colLoteUsado,
            this.colLoteValor,
            this.colLoteFecha});
            this.dgvLotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLotes.Name = "dgvLotes";
            this.dgvLotes.TabIndex = 1;
            this.dgvLotes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLotes_CellDoubleClick);
            //
            // colLoteFabricante
            //
            this.colLoteFabricante.DataPropertyName = "Fabricante";
            this.colLoteFabricante.FillWeight = 120F;
            this.colLoteFabricante.HeaderText = "Fabricante";
            this.colLoteFabricante.MinimumWidth = 75;
            this.colLoteFabricante.Name = "colLoteFabricante";
            //
            // colLoteUbicacion
            //
            this.colLoteUbicacion.DataPropertyName = "Ubicacion";
            this.colLoteUbicacion.FillWeight = 120F;
            this.colLoteUbicacion.HeaderText = "Ubicación";
            this.colLoteUbicacion.MinimumWidth = 75;
            this.colLoteUbicacion.Name = "colLoteUbicacion";
            //
            // colLoteCantidad
            //
            this.colLoteCantidad.DataPropertyName = "Cantidad";
            this.colLoteCantidad.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLoteCantidad.DefaultCellStyle.Format = "0.###";
            this.colLoteCantidad.FillWeight = 60F;
            this.colLoteCantidad.HeaderText = "Cant.";
            this.colLoteCantidad.MinimumWidth = 50;
            this.colLoteCantidad.Name = "colLoteCantidad";
            //
            // colLoteCompra
            //
            this.colLoteCompra.DataPropertyName = "Compra";
            this.colLoteCompra.FillWeight = 55F;
            this.colLoteCompra.HeaderText = "Compra";
            this.colLoteCompra.MinimumWidth = 55;
            this.colLoteCompra.Name = "colLoteCompra";
            //
            // colLoteNuevo
            //
            this.colLoteNuevo.DataPropertyName = "DisponibleNuevo";
            this.colLoteNuevo.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLoteNuevo.DefaultCellStyle.Format = "0.###";
            this.colLoteNuevo.FillWeight = 50F;
            this.colLoteNuevo.HeaderText = "Nuevo";
            this.colLoteNuevo.MinimumWidth = 50;
            this.colLoteNuevo.Name = "colLoteNuevo";
            //
            // colLoteUsado
            //
            this.colLoteUsado.DataPropertyName = "DisponibleUsado";
            this.colLoteUsado.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLoteUsado.DefaultCellStyle.Format = "0.###";
            this.colLoteUsado.FillWeight = 50F;
            this.colLoteUsado.HeaderText = "Usado";
            this.colLoteUsado.MinimumWidth = 50;
            this.colLoteUsado.Name = "colLoteUsado";
            //
            // colLoteValor
            //
            this.colLoteValor.DataPropertyName = "ValorUnitario";
            this.colLoteValor.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLoteValor.DefaultCellStyle.Format = "N2";
            this.colLoteValor.FillWeight = 65F;
            this.colLoteValor.HeaderText = "Valor unit.";
            this.colLoteValor.MinimumWidth = 70;
            this.colLoteValor.Name = "colLoteValor";
            //
            // colLoteFecha
            //
            this.colLoteFecha.DataPropertyName = "FechaIngreso";
            this.colLoteFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            this.colLoteFecha.FillWeight = 75F;
            this.colLoteFecha.HeaderText = "Ingreso";
            this.colLoteFecha.MinimumWidth = 75;
            this.colLoteFecha.Name = "colLoteFecha";
            //
            // flpBotonesLote
            //
            this.flpBotonesLote.AutoSize = true;
            this.flpBotonesLote.Controls.Add(this.botAgregarLote);
            this.flpBotonesLote.Controls.Add(this.botModificarLote);
            this.flpBotonesLote.Controls.Add(this.botEliminarLote);
            this.flpBotonesLote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotonesLote.Name = "flpBotonesLote";
            this.flpBotonesLote.TabIndex = 2;
            //
            // botAgregarLote
            //
            this.botAgregarLote.Name = "botAgregarLote";
            this.botAgregarLote.Size = new System.Drawing.Size(130, 34);
            this.botAgregarLote.TabIndex = 0;
            this.botAgregarLote.Text = "Agregar lote";
            this.botAgregarLote.Click += new System.EventHandler(this.botAgregarLote_Click);
            //
            // botModificarLote
            //
            this.botModificarLote.Name = "botModificarLote";
            this.botModificarLote.Size = new System.Drawing.Size(130, 34);
            this.botModificarLote.TabIndex = 1;
            this.botModificarLote.Text = "Modificar lote";
            this.botModificarLote.Click += new System.EventHandler(this.botModificarLote_Click);
            //
            // botEliminarLote
            //
            this.botEliminarLote.Name = "botEliminarLote";
            this.botEliminarLote.Size = new System.Drawing.Size(130, 34);
            this.botEliminarLote.TabIndex = 2;
            this.botEliminarLote.Text = "Eliminar lote";
            this.botEliminarLote.Click += new System.EventHandler(this.botEliminarLote_Click);
            //
            // labUsos
            //
            this.labUsos.AutoSize = true;
            this.labUsos.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labUsos.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.labUsos.Name = "labUsos";
            this.labUsos.Text = "Usado en modelos";
            //
            // dgvUsos
            //
            this.dgvUsos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUsoModelo,
            this.colUsoAlias,
            this.colUsoCondicion,
            this.colUsoCantidad});
            this.dgvUsos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsos.Name = "dgvUsos";
            this.dgvUsos.TabIndex = 3;
            //
            // colUsoModelo
            //
            this.colUsoModelo.DataPropertyName = "Modelo";
            this.colUsoModelo.FillWeight = 140F;
            this.colUsoModelo.HeaderText = "Modelo";
            this.colUsoModelo.Name = "colUsoModelo";
            //
            // colUsoAlias
            //
            this.colUsoAlias.DataPropertyName = "Alias";
            this.colUsoAlias.HeaderText = "Alias";
            this.colUsoAlias.Name = "colUsoAlias";
            //
            // colUsoCondicion
            //
            this.colUsoCondicion.DataPropertyName = "Condicion";
            this.colUsoCondicion.FillWeight = 55F;
            this.colUsoCondicion.HeaderText = "Salió";
            this.colUsoCondicion.MinimumWidth = 55;
            this.colUsoCondicion.Name = "colUsoCondicion";
            //
            // colUsoCantidad
            //
            this.colUsoCantidad.DataPropertyName = "Cantidad";
            this.colUsoCantidad.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colUsoCantidad.DefaultCellStyle.Format = "0.###";
            this.colUsoCantidad.FillWeight = 60F;
            this.colUsoCantidad.HeaderText = "Cant.";
            this.colUsoCantidad.Name = "colUsoCantidad";
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
            this.flpBotones.Controls.Add(this.botComprar);
            this.flpBotones.Controls.Add(this.botFamilias);
            this.flpBotones.Controls.Add(this.botTipos);
            this.flpBotones.Controls.Add(this.botInforme);
            this.flpBotones.Controls.Add(this.botImprimir);
            this.flpBotones.Controls.Add(this.botSalir);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 3;
            //
            // botNuevo
            //
            this.botNuevo.Margin = new System.Windows.Forms.Padding(6);
            this.botNuevo.Name = "botNuevo";
            this.botNuevo.Size = new System.Drawing.Size(132, 44);
            this.botNuevo.TabIndex = 0;
            this.botNuevo.Text = "NUEVO";
            this.botNuevo.Click += new System.EventHandler(this.botNuevo_Click);
            //
            // botModificar
            //
            this.botModificar.Margin = new System.Windows.Forms.Padding(6);
            this.botModificar.Name = "botModificar";
            this.botModificar.Size = new System.Drawing.Size(132, 44);
            this.botModificar.TabIndex = 1;
            this.botModificar.Text = "MODIFICAR";
            this.botModificar.Click += new System.EventHandler(this.botModificar_Click);
            //
            // botEliminar
            //
            this.botEliminar.Margin = new System.Windows.Forms.Padding(6);
            this.botEliminar.Name = "botEliminar";
            this.botEliminar.Size = new System.Drawing.Size(132, 44);
            this.botEliminar.TabIndex = 2;
            this.botEliminar.Text = "ELIMINAR";
            this.botEliminar.Click += new System.EventHandler(this.botEliminar_Click);
            //
            // botComprar
            //
            this.botComprar.Margin = new System.Windows.Forms.Padding(6);
            this.botComprar.Name = "botComprar";
            this.botComprar.Size = new System.Drawing.Size(132, 44);
            this.botComprar.TabIndex = 3;
            this.botComprar.Text = "COMPRAR";
            this.botComprar.Click += new System.EventHandler(this.botComprar_Click);
            //
            // botFamilias
            //
            this.botFamilias.Margin = new System.Windows.Forms.Padding(24, 6, 6, 6);
            this.botFamilias.Name = "botFamilias";
            this.botFamilias.Size = new System.Drawing.Size(132, 44);
            this.botFamilias.TabIndex = 3;
            this.botFamilias.Text = "FAMILIAS";
            this.botFamilias.Click += new System.EventHandler(this.botFamilias_Click);
            //
            // botTipos
            //
            this.botTipos.Margin = new System.Windows.Forms.Padding(6);
            this.botTipos.Name = "botTipos";
            this.botTipos.Size = new System.Drawing.Size(132, 44);
            this.botTipos.TabIndex = 4;
            this.botTipos.Text = "UNIDADES";
            this.botTipos.Click += new System.EventHandler(this.botTipos_Click);
            //
            // botInforme
            //
            this.botInforme.Margin = new System.Windows.Forms.Padding(24, 6, 6, 6);
            this.botInforme.Name = "botInforme";
            this.botInforme.Size = new System.Drawing.Size(132, 44);
            this.botInforme.TabIndex = 5;
            this.botInforme.Text = "INFORME";
            this.botInforme.Click += new System.EventHandler(this.botInforme_Click);
            //
            // botImprimir
            //
            this.botImprimir.Margin = new System.Windows.Forms.Padding(6);
            this.botImprimir.Name = "botImprimir";
            this.botImprimir.Size = new System.Drawing.Size(132, 44);
            this.botImprimir.TabIndex = 6;
            this.botImprimir.Text = "IMPRIMIR";
            this.botImprimir.Click += new System.EventHandler(this.botImprimir_Click);
            //
            // botSalir
            //
            this.botSalir.Margin = new System.Windows.Forms.Padding(24, 6, 6, 6);
            this.botSalir.Name = "botSalir";
            this.botSalir.Size = new System.Drawing.Size(132, 44);
            this.botSalir.TabIndex = 7;
            this.botSalir.Text = "SALIR";
            this.botSalir.Click += new System.EventHandler(this.botSalir_Click);
            //
            // timBuscar
            //
            this.timBuscar.Interval = 350;
            this.timBuscar.Tick += new System.EventHandler(this.timBuscar_Tick);
            //
            // frmMateriales
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmMateriales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Materiales";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMateriales_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMateriales_KeyDown);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.flpFiltros.ResumeLayout(false);
            this.flpFiltros.PerformLayout();
            this.splPrincipal.Panel1.ResumeLayout(false);
            this.splPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splPrincipal)).EndInit();
            this.splPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).EndInit();
            this.tlpDetalle.ResumeLayout(false);
            this.tlpDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLotes)).EndInit();
            this.flpBotonesLote.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsos)).EndInit();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labTitulo;
        private System.Windows.Forms.FlowLayoutPanel flpFiltros;
        private System.Windows.Forms.Label labFamilia;
        private System.Windows.Forms.ComboBox cboFamilia;
        private System.Windows.Forms.Label labBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.CheckBox chkSinStock;
        private System.Windows.Forms.CheckBox chkAComprar;
        private System.Windows.Forms.Button botLimpiarFiltros;
        private System.Windows.Forms.SplitContainer splPrincipal;
        private System.Windows.Forms.DataGridView dgvMateriales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFamilia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGrupo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCaracteristica;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMedidas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDispNuevo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDispUsado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisponible;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorDisponible;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAComprar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLotes;
        private System.Windows.Forms.TableLayoutPanel tlpDetalle;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Label labLotes;
        private System.Windows.Forms.DataGridView dgvLotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteFabricante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteUbicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteNuevo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteUsado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoteFecha;
        private System.Windows.Forms.FlowLayoutPanel flpBotonesLote;
        private System.Windows.Forms.Button botAgregarLote;
        private System.Windows.Forms.Button botModificarLote;
        private System.Windows.Forms.Button botEliminarLote;
        private System.Windows.Forms.Label labUsos;
        private System.Windows.Forms.DataGridView dgvUsos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsoModelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsoAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsoCondicion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsoCantidad;
        private System.Windows.Forms.Label labResumen;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botNuevo;
        private System.Windows.Forms.Button botModificar;
        private System.Windows.Forms.Button botEliminar;
        private System.Windows.Forms.Button botComprar;
        private System.Windows.Forms.Button botFamilias;
        private System.Windows.Forms.Button botTipos;
        private System.Windows.Forms.Button botInforme;
        private System.Windows.Forms.Button botImprimir;
        private System.Windows.Forms.Button botSalir;
        private System.Windows.Forms.Timer timBuscar;
    }
}
