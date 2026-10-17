namespace Aviones_2._0.Forms.Compras
{
    partial class frmCompras
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
            this.splListas = new System.Windows.Forms.SplitContainer();
            this.tlpPedidos = new System.Windows.Forms.TableLayoutPanel();
            this.labPedidos = new System.Windows.Forms.Label();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.colPedFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedFamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedDisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedCosto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPedNota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpPedidos = new System.Windows.Forms.FlowLayoutPanel();
            this.botRecibirPedido = new System.Windows.Forms.Button();
            this.botModificarPedido = new System.Windows.Forms.Button();
            this.botEliminarPedido = new System.Windows.Forms.Button();
            this.botImprimirPedidos = new System.Windows.Forms.Button();
            this.labTotalPedidos = new System.Windows.Forms.Label();
            this.tlpCriticos = new System.Windows.Forms.TableLayoutPanel();
            this.flpCriticosTitulo = new System.Windows.Forms.FlowLayoutPanel();
            this.labCriticos = new System.Windows.Forms.Label();
            this.labMinimo = new System.Windows.Forms.Label();
            this.txtMinimo = new System.Windows.Forms.TextBox();
            this.labMinimoAyuda = new System.Windows.Forms.Label();
            this.botAplicarMinimo = new System.Windows.Forms.Button();
            this.dgvCriticos = new System.Windows.Forms.DataGridView();
            this.colCriFamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCriMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCriCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCriUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCriDisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCriMinimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCriPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpCriticos = new System.Windows.Forms.FlowLayoutPanel();
            this.botPedir = new System.Windows.Forms.Button();
            this.botImprimirCriticos = new System.Windows.Forms.Button();
            this.chkSoloSinPedido = new System.Windows.Forms.CheckBox();
            this.labResumenCriticos = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botSalir = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splListas)).BeginInit();
            this.splListas.Panel1.SuspendLayout();
            this.splListas.Panel2.SuspendLayout();
            this.splListas.SuspendLayout();
            this.tlpPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.flpPedidos.SuspendLayout();
            this.tlpCriticos.SuspendLayout();
            this.flpCriticosTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCriticos)).BeginInit();
            this.flpCriticos.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 1;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.Controls.Add(this.labTitulo, 0, 0);
            this.tlpPrincipal.Controls.Add(this.splListas, 0, 1);
            this.tlpPrincipal.Controls.Add(this.flpBotones, 0, 2);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.tlpPrincipal.RowCount = 3;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.labTitulo.Text = "Compras";
            //
            // splListas
            //
            this.splListas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splListas.Name = "splListas";
            this.splListas.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splListas.Panel1.Controls.Add(this.tlpPedidos);
            this.splListas.Panel2.Controls.Add(this.tlpCriticos);
            this.splListas.Panel2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.splListas.Size = new System.Drawing.Size(1224, 620);
            this.splListas.SplitterDistance = 300;
            this.splListas.TabIndex = 1;
            //
            // tlpPedidos
            //
            this.tlpPedidos.ColumnCount = 1;
            this.tlpPedidos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPedidos.Controls.Add(this.labPedidos, 0, 0);
            this.tlpPedidos.Controls.Add(this.dgvPedidos, 0, 1);
            this.tlpPedidos.Controls.Add(this.flpPedidos, 0, 2);
            this.tlpPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPedidos.Name = "tlpPedidos";
            this.tlpPedidos.RowCount = 3;
            this.tlpPedidos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPedidos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPedidos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPedidos.TabIndex = 0;
            //
            // labPedidos
            //
            this.labPedidos.AutoSize = true;
            this.labPedidos.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.labPedidos.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.labPedidos.Name = "labPedidos";
            this.labPedidos.Text = "Lista de compras (pedidos pendientes)";
            //
            // dgvPedidos
            //
            this.dgvPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPedFecha,
            this.colPedFamilia,
            this.colPedMaterial,
            this.colPedCodigo,
            this.colPedCantidad,
            this.colPedUnidad,
            this.colPedDisponible,
            this.colPedPrecio,
            this.colPedCosto,
            this.colPedNota});
            this.dgvPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.TabIndex = 0;
            this.dgvPedidos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPedidos_CellDoubleClick);
            this.dgvPedidos.SelectionChanged += new System.EventHandler(this.dgvPedidos_SelectionChanged);
            this.dgvPedidos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvPedidos_KeyDown);
            //
            // colPedFecha
            //
            this.colPedFecha.DataPropertyName = "FechaPedido";
            this.colPedFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            this.colPedFecha.FillWeight = 60F;
            this.colPedFecha.HeaderText = "Pedido";
            this.colPedFecha.MinimumWidth = 80;
            this.colPedFecha.Name = "colPedFecha";
            //
            // colPedFamilia
            //
            this.colPedFamilia.DataPropertyName = "Familia";
            this.colPedFamilia.FillWeight = 80F;
            this.colPedFamilia.HeaderText = "Familia";
            this.colPedFamilia.MinimumWidth = 80;
            this.colPedFamilia.Name = "colPedFamilia";
            //
            // colPedMaterial
            //
            this.colPedMaterial.DataPropertyName = "Material";
            this.colPedMaterial.FillWeight = 200F;
            this.colPedMaterial.HeaderText = "Material";
            this.colPedMaterial.MinimumWidth = 150;
            this.colPedMaterial.Name = "colPedMaterial";
            //
            // colPedCodigo
            //
            this.colPedCodigo.DataPropertyName = "Codigo";
            this.colPedCodigo.FillWeight = 60F;
            this.colPedCodigo.HeaderText = "Código";
            this.colPedCodigo.MinimumWidth = 60;
            this.colPedCodigo.Name = "colPedCodigo";
            //
            // colPedCantidad
            //
            this.colPedCantidad.DataPropertyName = "Cantidad";
            this.colPedCantidad.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPedCantidad.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.colPedCantidad.DefaultCellStyle.Format = "0.###";
            this.colPedCantidad.FillWeight = 55F;
            this.colPedCantidad.HeaderText = "Comprar";
            this.colPedCantidad.MinimumWidth = 65;
            this.colPedCantidad.Name = "colPedCantidad";
            //
            // colPedUnidad
            //
            this.colPedUnidad.DataPropertyName = "TipoUnidad";
            this.colPedUnidad.FillWeight = 55F;
            this.colPedUnidad.HeaderText = "Unidad";
            this.colPedUnidad.MinimumWidth = 65;
            this.colPedUnidad.Name = "colPedUnidad";
            //
            // colPedDisponible
            //
            this.colPedDisponible.DataPropertyName = "Disponible";
            this.colPedDisponible.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPedDisponible.DefaultCellStyle.Format = "0.###";
            this.colPedDisponible.FillWeight = 55F;
            this.colPedDisponible.HeaderText = "Hay";
            this.colPedDisponible.MinimumWidth = 50;
            this.colPedDisponible.Name = "colPedDisponible";
            //
            // colPedPrecio
            //
            this.colPedPrecio.DataPropertyName = "Precio";
            this.colPedPrecio.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPedPrecio.DefaultCellStyle.Format = "N2";
            this.colPedPrecio.FillWeight = 60F;
            this.colPedPrecio.HeaderText = "Precio est.";
            this.colPedPrecio.MinimumWidth = 70;
            this.colPedPrecio.Name = "colPedPrecio";
            //
            // colPedCosto
            //
            this.colPedCosto.DataPropertyName = "CostoEstimado";
            this.colPedCosto.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPedCosto.DefaultCellStyle.Format = "N2";
            this.colPedCosto.FillWeight = 60F;
            this.colPedCosto.HeaderText = "Costo est.";
            this.colPedCosto.MinimumWidth = 70;
            this.colPedCosto.Name = "colPedCosto";
            //
            // colPedNota
            //
            this.colPedNota.DataPropertyName = "Nota";
            this.colPedNota.FillWeight = 150F;
            this.colPedNota.HeaderText = "Nota";
            this.colPedNota.MinimumWidth = 80;
            this.colPedNota.Name = "colPedNota";
            //
            // flpPedidos
            //
            this.flpPedidos.AutoSize = true;
            this.flpPedidos.Controls.Add(this.botRecibirPedido);
            this.flpPedidos.Controls.Add(this.botModificarPedido);
            this.flpPedidos.Controls.Add(this.botEliminarPedido);
            this.flpPedidos.Controls.Add(this.botImprimirPedidos);
            this.flpPedidos.Controls.Add(this.labTotalPedidos);
            this.flpPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPedidos.Name = "flpPedidos";
            this.flpPedidos.TabIndex = 1;
            //
            // botRecibirPedido
            //
            this.botRecibirPedido.Name = "botRecibirPedido";
            this.botRecibirPedido.Size = new System.Drawing.Size(150, 38);
            this.botRecibirPedido.TabIndex = 0;
            this.botRecibirPedido.Text = "RECIBIR";
            this.botRecibirPedido.Click += new System.EventHandler(this.botRecibirPedido_Click);
            //
            // botModificarPedido
            //
            this.botModificarPedido.Name = "botModificarPedido";
            this.botModificarPedido.Size = new System.Drawing.Size(150, 38);
            this.botModificarPedido.TabIndex = 0;
            this.botModificarPedido.Text = "MODIFICAR";
            this.botModificarPedido.Click += new System.EventHandler(this.botModificarPedido_Click);
            //
            // botEliminarPedido
            //
            this.botEliminarPedido.Name = "botEliminarPedido";
            this.botEliminarPedido.Size = new System.Drawing.Size(150, 38);
            this.botEliminarPedido.TabIndex = 1;
            this.botEliminarPedido.Text = "ELIMINAR";
            this.botEliminarPedido.Click += new System.EventHandler(this.botEliminarPedido_Click);
            //
            // botImprimirPedidos
            //
            this.botImprimirPedidos.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.botImprimirPedidos.Name = "botImprimirPedidos";
            this.botImprimirPedidos.Size = new System.Drawing.Size(170, 38);
            this.botImprimirPedidos.TabIndex = 2;
            this.botImprimirPedidos.Text = "IMPRIMIR LISTA";
            this.botImprimirPedidos.Click += new System.EventHandler(this.botImprimirPedidos_Click);
            //
            // labTotalPedidos
            //
            this.labTotalPedidos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labTotalPedidos.AutoSize = true;
            this.labTotalPedidos.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labTotalPedidos.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.labTotalPedidos.Name = "labTotalPedidos";
            this.labTotalPedidos.Text = "";
            //
            // tlpCriticos
            //
            this.tlpCriticos.ColumnCount = 1;
            this.tlpCriticos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCriticos.Controls.Add(this.flpCriticosTitulo, 0, 0);
            this.tlpCriticos.Controls.Add(this.dgvCriticos, 0, 1);
            this.tlpCriticos.Controls.Add(this.flpCriticos, 0, 2);
            this.tlpCriticos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCriticos.Name = "tlpCriticos";
            this.tlpCriticos.RowCount = 3;
            this.tlpCriticos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCriticos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCriticos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCriticos.TabIndex = 0;
            //
            // flpCriticosTitulo
            //
            this.flpCriticosTitulo.AutoSize = true;
            this.flpCriticosTitulo.Controls.Add(this.labCriticos);
            this.flpCriticosTitulo.Controls.Add(this.labMinimo);
            this.flpCriticosTitulo.Controls.Add(this.txtMinimo);
            this.flpCriticosTitulo.Controls.Add(this.labMinimoAyuda);
            this.flpCriticosTitulo.Controls.Add(this.botAplicarMinimo);
            this.flpCriticosTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCriticosTitulo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.flpCriticosTitulo.Name = "flpCriticosTitulo";
            this.flpCriticosTitulo.TabIndex = 0;
            //
            // labCriticos
            //
            this.labCriticos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCriticos.AutoSize = true;
            this.labCriticos.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.labCriticos.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
            this.labCriticos.Name = "labCriticos";
            this.labCriticos.Text = "Stock crítico";
            //
            // labMinimo
            //
            this.labMinimo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMinimo.AutoSize = true;
            this.labMinimo.Name = "labMinimo";
            this.labMinimo.Text = "Avisar cuando quede";
            //
            // txtMinimo
            //
            this.txtMinimo.Name = "txtMinimo";
            this.txtMinimo.Size = new System.Drawing.Size(60, 23);
            this.txtMinimo.TabIndex = 0;
            this.txtMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMinimo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMinimo_KeyDown);
            //
            // labMinimoAyuda
            //
            this.labMinimoAyuda.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMinimoAyuda.AutoSize = true;
            this.labMinimoAyuda.Name = "labMinimoAyuda";
            this.labMinimoAyuda.Text = "o menos (para los materiales sin mínimo propio)";
            //
            // botAplicarMinimo
            //
            this.botAplicarMinimo.AutoSize = true;
            this.botAplicarMinimo.Name = "botAplicarMinimo";
            this.botAplicarMinimo.TabIndex = 1;
            this.botAplicarMinimo.Text = "Aplicar";
            this.botAplicarMinimo.Click += new System.EventHandler(this.botAplicarMinimo_Click);
            //
            // dgvCriticos
            //
            this.dgvCriticos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCriFamilia,
            this.colCriMaterial,
            this.colCriCodigo,
            this.colCriUnidad,
            this.colCriDisponible,
            this.colCriMinimo,
            this.colCriPedido});
            this.dgvCriticos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCriticos.Name = "dgvCriticos";
            this.dgvCriticos.TabIndex = 1;
            this.dgvCriticos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCriticos_CellDoubleClick);
            this.dgvCriticos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCriticos_CellFormatting);
            this.dgvCriticos.SelectionChanged += new System.EventHandler(this.dgvCriticos_SelectionChanged);
            //
            // colCriFamilia
            //
            this.colCriFamilia.DataPropertyName = "Familia";
            this.colCriFamilia.FillWeight = 80F;
            this.colCriFamilia.HeaderText = "Familia";
            this.colCriFamilia.MinimumWidth = 80;
            this.colCriFamilia.Name = "colCriFamilia";
            //
            // colCriMaterial
            //
            this.colCriMaterial.DataPropertyName = "Descripcion";
            this.colCriMaterial.FillWeight = 220F;
            this.colCriMaterial.HeaderText = "Material";
            this.colCriMaterial.MinimumWidth = 150;
            this.colCriMaterial.Name = "colCriMaterial";
            //
            // colCriCodigo
            //
            this.colCriCodigo.DataPropertyName = "Codigo";
            this.colCriCodigo.FillWeight = 60F;
            this.colCriCodigo.HeaderText = "Código";
            this.colCriCodigo.MinimumWidth = 60;
            this.colCriCodigo.Name = "colCriCodigo";
            //
            // colCriUnidad
            //
            this.colCriUnidad.DataPropertyName = "TipoUnidad";
            this.colCriUnidad.FillWeight = 55F;
            this.colCriUnidad.HeaderText = "Unidad";
            this.colCriUnidad.MinimumWidth = 65;
            this.colCriUnidad.Name = "colCriUnidad";
            //
            // colCriDisponible
            //
            this.colCriDisponible.DataPropertyName = "Disponible";
            this.colCriDisponible.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCriDisponible.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.colCriDisponible.DefaultCellStyle.Format = "0.###";
            this.colCriDisponible.FillWeight = 55F;
            this.colCriDisponible.HeaderText = "Hay";
            this.colCriDisponible.MinimumWidth = 50;
            this.colCriDisponible.Name = "colCriDisponible";
            //
            // colCriMinimo
            //
            this.colCriMinimo.DataPropertyName = "MinimoAplicado";
            this.colCriMinimo.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCriMinimo.DefaultCellStyle.Format = "0.###";
            this.colCriMinimo.FillWeight = 55F;
            this.colCriMinimo.HeaderText = "Mínimo";
            this.colCriMinimo.MinimumWidth = 60;
            this.colCriMinimo.Name = "colCriMinimo";
            //
            // colCriPedido
            //
            this.colCriPedido.DataPropertyName = "PedidoPendiente";
            this.colCriPedido.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCriPedido.DefaultCellStyle.Format = "0.###";
            this.colCriPedido.FillWeight = 60F;
            this.colCriPedido.HeaderText = "Ya pedido";
            this.colCriPedido.MinimumWidth = 70;
            this.colCriPedido.Name = "colCriPedido";
            //
            // flpCriticos
            //
            this.flpCriticos.AutoSize = true;
            this.flpCriticos.Controls.Add(this.botPedir);
            this.flpCriticos.Controls.Add(this.botImprimirCriticos);
            this.flpCriticos.Controls.Add(this.chkSoloSinPedido);
            this.flpCriticos.Controls.Add(this.labResumenCriticos);
            this.flpCriticos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCriticos.Name = "flpCriticos";
            this.flpCriticos.TabIndex = 2;
            //
            // botPedir
            //
            this.botPedir.Name = "botPedir";
            this.botPedir.Size = new System.Drawing.Size(220, 38);
            this.botPedir.TabIndex = 0;
            this.botPedir.Text = "AGREGAR A LA LISTA";
            this.botPedir.Click += new System.EventHandler(this.botPedir_Click);
            //
            // botImprimirCriticos
            //
            this.botImprimirCriticos.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.botImprimirCriticos.Name = "botImprimirCriticos";
            this.botImprimirCriticos.Size = new System.Drawing.Size(170, 38);
            this.botImprimirCriticos.TabIndex = 1;
            this.botImprimirCriticos.Text = "IMPRIMIR";
            this.botImprimirCriticos.Click += new System.EventHandler(this.botImprimirCriticos_Click);
            //
            // chkSoloSinPedido
            //
            this.chkSoloSinPedido.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSoloSinPedido.AutoSize = true;
            this.chkSoloSinPedido.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.chkSoloSinPedido.Name = "chkSoloSinPedido";
            this.chkSoloSinPedido.TabIndex = 2;
            this.chkSoloSinPedido.Text = "Sólo los que no están pedidos";
            this.chkSoloSinPedido.CheckedChanged += new System.EventHandler(this.chkSoloSinPedido_CheckedChanged);
            //
            // labResumenCriticos
            //
            this.labResumenCriticos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labResumenCriticos.AutoSize = true;
            this.labResumenCriticos.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labResumenCriticos.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.labResumenCriticos.Name = "labResumenCriticos";
            this.labResumenCriticos.Text = "";
            //
            // flpBotones
            //
            this.flpBotones.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botSalir);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 2;
            //
            // botSalir
            //
            this.botSalir.Margin = new System.Windows.Forms.Padding(6);
            this.botSalir.Name = "botSalir";
            this.botSalir.Size = new System.Drawing.Size(150, 44);
            this.botSalir.TabIndex = 0;
            this.botSalir.Text = "SALIR";
            this.botSalir.Click += new System.EventHandler(this.botSalir_Click);
            //
            // frmCompras
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compras";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCompras_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCompras_KeyDown);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.splListas.Panel1.ResumeLayout(false);
            this.splListas.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splListas)).EndInit();
            this.splListas.ResumeLayout(false);
            this.tlpPedidos.ResumeLayout(false);
            this.tlpPedidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.flpPedidos.ResumeLayout(false);
            this.flpPedidos.PerformLayout();
            this.tlpCriticos.ResumeLayout(false);
            this.tlpCriticos.PerformLayout();
            this.flpCriticosTitulo.ResumeLayout(false);
            this.flpCriticosTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCriticos)).EndInit();
            this.flpCriticos.ResumeLayout(false);
            this.flpCriticos.PerformLayout();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labTitulo;
        private System.Windows.Forms.SplitContainer splListas;
        private System.Windows.Forms.TableLayoutPanel tlpPedidos;
        private System.Windows.Forms.Label labPedidos;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedFamilia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedDisponible;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedCosto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedNota;
        private System.Windows.Forms.FlowLayoutPanel flpPedidos;
        private System.Windows.Forms.Button botRecibirPedido;
        private System.Windows.Forms.Button botModificarPedido;
        private System.Windows.Forms.Button botEliminarPedido;
        private System.Windows.Forms.Button botImprimirPedidos;
        private System.Windows.Forms.Label labTotalPedidos;
        private System.Windows.Forms.TableLayoutPanel tlpCriticos;
        private System.Windows.Forms.FlowLayoutPanel flpCriticosTitulo;
        private System.Windows.Forms.Label labCriticos;
        private System.Windows.Forms.Label labMinimo;
        private System.Windows.Forms.TextBox txtMinimo;
        private System.Windows.Forms.Label labMinimoAyuda;
        private System.Windows.Forms.Button botAplicarMinimo;
        private System.Windows.Forms.DataGridView dgvCriticos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCriFamilia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCriMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCriCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCriUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCriDisponible;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCriMinimo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCriPedido;
        private System.Windows.Forms.FlowLayoutPanel flpCriticos;
        private System.Windows.Forms.Button botPedir;
        private System.Windows.Forms.Button botImprimirCriticos;
        private System.Windows.Forms.CheckBox chkSoloSinPedido;
        private System.Windows.Forms.Label labResumenCriticos;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botSalir;
    }
}
