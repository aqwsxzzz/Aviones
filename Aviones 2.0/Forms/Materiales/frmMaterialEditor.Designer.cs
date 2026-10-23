namespace Aviones_2._0.Forms.Materiales
{
    partial class frmMaterialEditor
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
            this.labFamilia = new System.Windows.Forms.Label();
            this.flpFamilia = new System.Windows.Forms.FlowLayoutPanel();
            this.cboFamilia = new System.Windows.Forms.ComboBox();
            this.botFamilias = new System.Windows.Forms.Button();
            this.labGrupo = new System.Windows.Forms.Label();
            this.txtGrupo = new System.Windows.Forms.TextBox();
            this.labCaracteristica = new System.Windows.Forms.Label();
            this.txtCaracteristica = new System.Windows.Forms.TextBox();
            this.labColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.labMedidas = new System.Windows.Forms.Label();
            this.txtMedidas = new System.Windows.Forms.TextBox();
            this.labCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.labEstado = new System.Windows.Forms.Label();
            this.flpEstado = new System.Windows.Forms.FlowLayoutPanel();
            this.radNuevo = new System.Windows.Forms.RadioButton();
            this.radUsado = new System.Windows.Forms.RadioButton();
            this.labUnidad = new System.Windows.Forms.Label();
            this.flpUnidad = new System.Windows.Forms.FlowLayoutPanel();
            this.cboUnidad = new System.Windows.Forms.ComboBox();
            this.botUnidades = new System.Windows.Forms.Button();
            this.labAComprar = new System.Windows.Forms.Label();
            this.txtAComprar = new System.Windows.Forms.TextBox();
            this.flpImagen = new System.Windows.Forms.FlowLayoutPanel();
            this.picImagen = new System.Windows.Forms.PictureBox();
            this.botCargarImagen = new System.Windows.Forms.Button();
            this.botQuitarImagen = new System.Windows.Forms.Button();
            this.grpLote = new System.Windows.Forms.GroupBox();
            this.tlpLote = new System.Windows.Forms.TableLayoutPanel();
            this.labFabricante = new System.Windows.Forms.Label();
            this.selFabricante = new Aviones_2._0.Forms.Comun.SelectorLista();
            this.labUbicacion = new System.Windows.Forms.Label();
            this.selUbicacion = new Aviones_2._0.Forms.Comun.SelectorLista();
            this.labCantidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.labValor = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.labFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botCancelar = new System.Windows.Forms.Button();
            this.botGuardar = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            this.flpEstado.SuspendLayout();
            this.flpFamilia.SuspendLayout();
            this.flpUnidad.SuspendLayout();
            this.flpImagen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).BeginInit();
            this.grpLote.SuspendLayout();
            this.tlpLote.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 3;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 440F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tlpPrincipal.Controls.Add(this.labFamilia, 0, 0);
            this.tlpPrincipal.Controls.Add(this.flpFamilia, 1, 0);
            this.tlpPrincipal.Controls.Add(this.flpImagen, 2, 0);
            this.tlpPrincipal.Controls.Add(this.labGrupo, 0, 1);
            this.tlpPrincipal.Controls.Add(this.txtGrupo, 1, 1);
            this.tlpPrincipal.Controls.Add(this.labCaracteristica, 0, 2);
            this.tlpPrincipal.Controls.Add(this.txtCaracteristica, 1, 2);
            this.tlpPrincipal.Controls.Add(this.labColor, 0, 3);
            this.tlpPrincipal.Controls.Add(this.txtColor, 1, 3);
            this.tlpPrincipal.Controls.Add(this.labMedidas, 0, 4);
            this.tlpPrincipal.Controls.Add(this.txtMedidas, 1, 4);
            this.tlpPrincipal.Controls.Add(this.labCodigo, 0, 5);
            this.tlpPrincipal.Controls.Add(this.txtCodigo, 1, 5);
            this.tlpPrincipal.Controls.Add(this.labUnidad, 0, 7);
            this.tlpPrincipal.Controls.Add(this.flpUnidad, 1, 7);
            this.tlpPrincipal.Controls.Add(this.labAComprar, 0, 8);
            this.tlpPrincipal.Controls.Add(this.txtAComprar, 1, 8);
            this.tlpPrincipal.Controls.Add(this.grpLote, 0, 9);
            this.tlpPrincipal.Controls.Add(this.flpBotones, 0, 11);
            this.tlpPrincipal.AutoSize = true;
            this.tlpPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(16);
            this.tlpPrincipal.RowCount = 12;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.SetRowSpan(this.flpImagen, 9);
            this.tlpPrincipal.SetColumnSpan(this.grpLote, 3);
            this.tlpPrincipal.SetColumnSpan(this.flpBotones, 3);
            this.tlpPrincipal.TabIndex = 0;
            //
            // labFamilia
            //
            this.labFamilia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFamilia.AutoSize = true;
            this.labFamilia.Name = "labFamilia";
            this.labFamilia.Text = "Familia *";
            //
            // flpFamilia
            //
            this.flpFamilia.AutoSize = true;
            this.flpFamilia.Controls.Add(this.cboFamilia);
            this.flpFamilia.Controls.Add(this.botFamilias);
            this.flpFamilia.Margin = new System.Windows.Forms.Padding(0);
            this.flpFamilia.Name = "flpFamilia";
            this.flpFamilia.TabIndex = 0;
            //
            // cboFamilia
            //
            this.cboFamilia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFamilia.Name = "cboFamilia";
            this.cboFamilia.Size = new System.Drawing.Size(280, 24);
            this.cboFamilia.TabIndex = 0;
            //
            // botFamilias
            //
            this.botFamilias.AutoSize = true;
            this.botFamilias.Name = "botFamilias";
            this.botFamilias.TabIndex = 1;
            this.botFamilias.TabStop = false;
            this.botFamilias.Text = "Editar familias…";
            this.botFamilias.Click += new System.EventHandler(this.botFamilias_Click);
            //
            // labGrupo
            //
            this.labGrupo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labGrupo.AutoSize = true;
            this.labGrupo.Name = "labGrupo";
            this.labGrupo.Text = "Grupo *";
            //
            // txtGrupo
            //
            this.txtGrupo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtGrupo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtGrupo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGrupo.MaxLength = 60;
            this.txtGrupo.Name = "txtGrupo";
            this.txtGrupo.TabIndex = 1;
            //
            // labCaracteristica
            //
            this.labCaracteristica.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCaracteristica.AutoSize = true;
            this.labCaracteristica.Name = "labCaracteristica";
            this.labCaracteristica.Text = "Característica";
            //
            // txtCaracteristica
            //
            this.txtCaracteristica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCaracteristica.MaxLength = 100;
            this.txtCaracteristica.Name = "txtCaracteristica";
            this.txtCaracteristica.TabIndex = 2;
            //
            // labColor
            //
            this.labColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labColor.AutoSize = true;
            this.labColor.Name = "labColor";
            this.labColor.Text = "Color";
            //
            // txtColor
            //
            this.txtColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtColor.MaxLength = 60;
            this.txtColor.Name = "txtColor";
            this.txtColor.TabIndex = 3;
            //
            // labMedidas
            //
            this.labMedidas.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMedidas.AutoSize = true;
            this.labMedidas.Name = "labMedidas";
            this.labMedidas.Text = "Medidas";
            //
            // txtMedidas
            //
            this.txtMedidas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMedidas.MaxLength = 60;
            this.txtMedidas.Name = "txtMedidas";
            this.txtMedidas.TabIndex = 4;
            //
            // labCodigo
            //
            this.labCodigo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCodigo.AutoSize = true;
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Text = "Código";
            //
            // txtCodigo
            //
            this.txtCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCodigo.MaxLength = 60;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.TabIndex = 5;
            //
            // labEstado
            //
            this.labEstado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labEstado.AutoSize = true;
            this.labEstado.Margin = new System.Windows.Forms.Padding(16, 0, 3, 0);
            this.labEstado.Name = "labEstado";
            this.labEstado.Text = "Se compró";
            //
            // flpEstado
            //
            this.flpEstado.AutoSize = true;
            this.flpEstado.Controls.Add(this.radNuevo);
            this.flpEstado.Controls.Add(this.radUsado);
            this.flpEstado.Name = "flpEstado";
            this.flpEstado.TabIndex = 6;
            //
            // radNuevo
            //
            this.radNuevo.AutoSize = true;
            this.radNuevo.Checked = true;
            this.radNuevo.Name = "radNuevo";
            this.radNuevo.TabIndex = 0;
            this.radNuevo.TabStop = true;
            this.radNuevo.Text = "Nuevo";
            //
            // radUsado
            //
            this.radUsado.AutoSize = true;
            this.radUsado.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.radUsado.Name = "radUsado";
            this.radUsado.TabIndex = 1;
            this.radUsado.Text = "Usado";
            //
            // labUnidad
            //
            this.labUnidad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labUnidad.AutoSize = true;
            this.labUnidad.Name = "labUnidad";
            this.labUnidad.Text = "Unidad *";
            //
            // flpUnidad
            //
            this.flpUnidad.AutoSize = true;
            this.flpUnidad.Controls.Add(this.cboUnidad);
            this.flpUnidad.Controls.Add(this.botUnidades);
            this.flpUnidad.Margin = new System.Windows.Forms.Padding(0);
            this.flpUnidad.Name = "flpUnidad";
            this.flpUnidad.TabIndex = 7;
            //
            // cboUnidad
            //
            this.cboUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnidad.Name = "cboUnidad";
            this.cboUnidad.Size = new System.Drawing.Size(200, 24);
            this.cboUnidad.TabIndex = 0;
            //
            // botUnidades
            //
            this.botUnidades.AutoSize = true;
            this.botUnidades.Name = "botUnidades";
            this.botUnidades.TabIndex = 1;
            this.botUnidades.TabStop = false;
            this.botUnidades.Text = "Editar unidades…";
            this.botUnidades.Click += new System.EventHandler(this.botUnidades_Click);
            //
            // labAComprar
            //
            this.labAComprar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labAComprar.AutoSize = true;
            this.labAComprar.Name = "labAComprar";
            this.labAComprar.Text = "Stock mínimo";
            //
            // txtAComprar
            //
            this.txtAComprar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAComprar.Name = "txtAComprar";
            this.txtAComprar.Size = new System.Drawing.Size(100, 23);
            this.txtAComprar.TabIndex = 8;
            this.txtAComprar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // flpImagen
            //
            this.flpImagen.Controls.Add(this.picImagen);
            this.flpImagen.Controls.Add(this.botCargarImagen);
            this.flpImagen.Controls.Add(this.botQuitarImagen);
            this.flpImagen.AutoSize = true;
            this.flpImagen.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpImagen.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.flpImagen.Name = "flpImagen";
            this.flpImagen.WrapContents = false;
            this.flpImagen.TabIndex = 20;
            //
            // picImagen
            //
            this.picImagen.BackColor = System.Drawing.Color.Transparent;
            this.picImagen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.picImagen.Name = "picImagen";
            this.picImagen.Size = new System.Drawing.Size(220, 220);
            this.picImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImagen.TabStop = false;
            //
            // botCargarImagen
            //
            this.botCargarImagen.Name = "botCargarImagen";
            this.botCargarImagen.Size = new System.Drawing.Size(220, 32);
            this.botCargarImagen.TabIndex = 0;
            this.botCargarImagen.TabStop = false;
            this.botCargarImagen.Text = "Cargar foto…";
            this.botCargarImagen.Click += new System.EventHandler(this.botCargarImagen_Click);
            //
            // botQuitarImagen
            //
            this.botQuitarImagen.Name = "botQuitarImagen";
            this.botQuitarImagen.Size = new System.Drawing.Size(220, 32);
            this.botQuitarImagen.TabIndex = 1;
            this.botQuitarImagen.TabStop = false;
            this.botQuitarImagen.Text = "Quitar foto";
            this.botQuitarImagen.Click += new System.EventHandler(this.botQuitarImagen_Click);
            //
            // grpLote
            //
            this.grpLote.AutoSize = true;
            this.grpLote.Controls.Add(this.tlpLote);
            this.grpLote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLote.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.grpLote.Name = "grpLote";
            this.grpLote.TabIndex = 9;
            this.grpLote.TabStop = false;
            this.grpLote.Text = "Primer ingreso (opcional; se puede agregar después)";
            //
            // tlpLote
            //
            this.tlpLote.AutoSize = true;
            this.tlpLote.ColumnCount = 4;
            this.tlpLote.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpLote.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLote.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpLote.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLote.Controls.Add(this.labFabricante, 0, 0);
            this.tlpLote.Controls.Add(this.selFabricante, 1, 0);
            this.tlpLote.Controls.Add(this.labUbicacion, 2, 0);
            this.tlpLote.Controls.Add(this.selUbicacion, 3, 0);
            this.tlpLote.Controls.Add(this.labCantidad, 0, 1);
            this.tlpLote.Controls.Add(this.txtCantidad, 1, 1);
            this.tlpLote.Controls.Add(this.labValor, 2, 1);
            this.tlpLote.Controls.Add(this.txtValor, 3, 1);
            this.tlpLote.Controls.Add(this.labFecha, 0, 2);
            this.tlpLote.Controls.Add(this.dtpFecha, 1, 2);
            this.tlpLote.Controls.Add(this.labEstado, 2, 2);
            this.tlpLote.Controls.Add(this.flpEstado, 3, 2);
            this.tlpLote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLote.Name = "tlpLote";
            this.tlpLote.RowCount = 3;
            this.tlpLote.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpLote.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpLote.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpLote.TabIndex = 0;
            //
            // labFabricante
            //
            this.labFabricante.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFabricante.AutoSize = true;
            this.labFabricante.Name = "labFabricante";
            this.labFabricante.Text = "Fabricante";
            //
            // selFabricante
            //
            this.selFabricante.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selFabricante.Name = "selFabricante";
            this.selFabricante.TabIndex = 0;
            //
            // labUbicacion
            //
            this.labUbicacion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labUbicacion.AutoSize = true;
            this.labUbicacion.Margin = new System.Windows.Forms.Padding(16, 0, 3, 0);
            this.labUbicacion.Name = "labUbicacion";
            this.labUbicacion.Text = "Ubicación";
            //
            // selUbicacion
            //
            this.selUbicacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selUbicacion.Name = "selUbicacion";
            this.selUbicacion.TabIndex = 1;
            //
            // labCantidad
            //
            this.labCantidad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCantidad.AutoSize = true;
            this.labCantidad.Name = "labCantidad";
            this.labCantidad.Text = "Cantidad";
            //
            // txtCantidad
            //
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(120, 23);
            this.txtCantidad.TabIndex = 2;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // labValor
            //
            this.labValor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labValor.AutoSize = true;
            this.labValor.Margin = new System.Windows.Forms.Padding(16, 0, 3, 0);
            this.labValor.Name = "labValor";
            this.labValor.Text = "Valor unitario";
            //
            // txtValor
            //
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(120, 23);
            this.txtValor.TabIndex = 3;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // labFecha
            //
            this.labFecha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labFecha.AutoSize = true;
            this.labFecha.Name = "labFecha";
            this.labFecha.Text = "Fecha ingreso";
            //
            // dtpFecha
            //
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(120, 23);
            this.dtpFecha.TabIndex = 4;
            //
            // flpBotones
            //
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botCancelar);
            this.flpBotones.Controls.Add(this.botGuardar);
            this.flpBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 10;
            //
            // botCancelar
            //
            this.botCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCancelar.Margin = new System.Windows.Forms.Padding(6);
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(140, 40);
            this.botCancelar.TabIndex = 1;
            this.botCancelar.Text = "CANCELAR";
            //
            // botGuardar
            //
            this.botGuardar.Margin = new System.Windows.Forms.Padding(6);
            this.botGuardar.Name = "botGuardar";
            this.botGuardar.Size = new System.Drawing.Size(140, 40);
            this.botGuardar.TabIndex = 0;
            this.botGuardar.Text = "GUARDAR";
            this.botGuardar.Click += new System.EventHandler(this.botGuardar_Click);
            //
            // frmMaterialEditor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.CancelButton = this.botCancelar;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(860, 640);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMaterialEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Material";
            this.Load += new System.EventHandler(this.frmMaterialEditor_Load);
            this.Shown += new System.EventHandler(this.frmMaterialEditor_Shown);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.flpEstado.ResumeLayout(false);
            this.flpEstado.PerformLayout();
            this.flpFamilia.ResumeLayout(false);
            this.flpFamilia.PerformLayout();
            this.flpUnidad.ResumeLayout(false);
            this.flpUnidad.PerformLayout();
            this.flpImagen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).EndInit();
            this.grpLote.ResumeLayout(false);
            this.grpLote.PerformLayout();
            this.tlpLote.ResumeLayout(false);
            this.tlpLote.PerformLayout();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label labFamilia;
        private System.Windows.Forms.FlowLayoutPanel flpFamilia;
        private System.Windows.Forms.ComboBox cboFamilia;
        private System.Windows.Forms.Button botFamilias;
        private System.Windows.Forms.Label labGrupo;
        private System.Windows.Forms.TextBox txtGrupo;
        private System.Windows.Forms.Label labCaracteristica;
        private System.Windows.Forms.TextBox txtCaracteristica;
        private System.Windows.Forms.Label labColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label labMedidas;
        private System.Windows.Forms.TextBox txtMedidas;
        private System.Windows.Forms.Label labCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label labEstado;
        private System.Windows.Forms.FlowLayoutPanel flpEstado;
        private System.Windows.Forms.RadioButton radNuevo;
        private System.Windows.Forms.RadioButton radUsado;
        private System.Windows.Forms.Label labUnidad;
        private System.Windows.Forms.FlowLayoutPanel flpUnidad;
        private System.Windows.Forms.ComboBox cboUnidad;
        private System.Windows.Forms.Button botUnidades;
        private System.Windows.Forms.Label labAComprar;
        private System.Windows.Forms.TextBox txtAComprar;
        private System.Windows.Forms.FlowLayoutPanel flpImagen;
        private System.Windows.Forms.PictureBox picImagen;
        private System.Windows.Forms.Button botCargarImagen;
        private System.Windows.Forms.Button botQuitarImagen;
        private System.Windows.Forms.GroupBox grpLote;
        private System.Windows.Forms.TableLayoutPanel tlpLote;
        private System.Windows.Forms.Label labFabricante;
        private Aviones_2._0.Forms.Comun.SelectorLista selFabricante;
        private System.Windows.Forms.Label labUbicacion;
        private Aviones_2._0.Forms.Comun.SelectorLista selUbicacion;
        private System.Windows.Forms.Label labCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label labValor;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label labFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botGuardar;
    }
}
