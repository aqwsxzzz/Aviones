namespace Aviones_2._0.Forms.Modelos
{
    partial class frmModeloEditor
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
            this.tlpDatos = new System.Windows.Forms.TableLayoutPanel();
            this.labNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.labAlias = new System.Windows.Forms.Label();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.labEstado = new System.Windows.Forms.Label();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.labDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.flpFoto = new System.Windows.Forms.FlowLayoutPanel();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.botCargarFoto = new System.Windows.Forms.Button();
            this.botQuitarFoto = new System.Windows.Forms.Button();
            this.labMateriales = new System.Windows.Forms.Label();
            this.dgvMateriales = new System.Windows.Forms.DataGridView();
            this.colFamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFabricante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCondicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpBotonesMaterial = new System.Windows.Forms.FlowLayoutPanel();
            this.botAgregarMaterial = new System.Windows.Forms.Button();
            this.botCambiarCantidad = new System.Windows.Forms.Button();
            this.botQuitarMaterial = new System.Windows.Forms.Button();
            this.labTotal = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.botCancelar = new System.Windows.Forms.Button();
            this.botGuardar = new System.Windows.Forms.Button();
            this.labAyuda = new System.Windows.Forms.Label();
            this.tlpPrincipal.SuspendLayout();
            this.tlpDatos.SuspendLayout();
            this.flpFoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).BeginInit();
            this.flpBotonesMaterial.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 2;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.Controls.Add(this.tlpDatos, 0, 0);
            this.tlpPrincipal.Controls.Add(this.flpFoto, 1, 0);
            this.tlpPrincipal.Controls.Add(this.labMateriales, 0, 1);
            this.tlpPrincipal.Controls.Add(this.dgvMateriales, 0, 2);
            this.tlpPrincipal.Controls.Add(this.flpBotonesMaterial, 0, 3);
            this.tlpPrincipal.Controls.Add(this.labTotal, 1, 3);
            this.tlpPrincipal.Controls.Add(this.labAyuda, 0, 4);
            this.tlpPrincipal.Controls.Add(this.flpBotones, 1, 4);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(16);
            this.tlpPrincipal.RowCount = 5;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.SetColumnSpan(this.labMateriales, 2);
            this.tlpPrincipal.SetColumnSpan(this.dgvMateriales, 2);
            this.tlpPrincipal.TabIndex = 0;
            //
            // tlpDatos
            //
            this.tlpDatos.AutoSize = true;
            this.tlpDatos.ColumnCount = 2;
            this.tlpDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 380F));
            this.tlpDatos.Controls.Add(this.labNombre, 0, 0);
            this.tlpDatos.Controls.Add(this.txtNombre, 1, 0);
            this.tlpDatos.Controls.Add(this.labAlias, 0, 1);
            this.tlpDatos.Controls.Add(this.txtAlias, 1, 1);
            this.tlpDatos.Controls.Add(this.labEstado, 0, 2);
            this.tlpDatos.Controls.Add(this.cboEstado, 1, 2);
            this.tlpDatos.Controls.Add(this.labDesde, 0, 3);
            this.tlpDatos.Controls.Add(this.dtpDesde, 1, 3);
            this.tlpDatos.Name = "tlpDatos";
            this.tlpDatos.RowCount = 4;
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDatos.TabIndex = 0;
            //
            // labNombre
            //
            this.labNombre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labNombre.AutoSize = true;
            this.labNombre.Name = "labNombre";
            this.labNombre.Text = "Modelo *";
            //
            // txtNombre
            //
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombre.Font = new System.Drawing.Font("Arial", 12F);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.TabIndex = 0;
            this.txtNombre.TextChanged += new System.EventHandler(this.Datos_Changed);
            //
            // labAlias
            //
            this.labAlias.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labAlias.AutoSize = true;
            this.labAlias.Name = "labAlias";
            this.labAlias.Text = "Alias";
            //
            // txtAlias
            //
            this.txtAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAlias.Font = new System.Drawing.Font("Arial", 12F);
            this.txtAlias.MaxLength = 100;
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.TabIndex = 1;
            this.txtAlias.TextChanged += new System.EventHandler(this.Datos_Changed);
            //
            // labEstado
            //
            this.labEstado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labEstado.AutoSize = true;
            this.labEstado.Name = "labEstado";
            this.labEstado.Text = "Condición *";
            //
            // cboEstado
            //
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(220, 24);
            this.cboEstado.TabIndex = 2;
            this.cboEstado.SelectedIndexChanged += new System.EventHandler(this.Datos_Changed);
            //
            // labDesde
            //
            this.labDesde.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDesde.AutoSize = true;
            this.labDesde.Name = "labDesde";
            this.labDesde.Text = "Desde";
            //
            // dtpDesde
            //
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.ShowCheckBox = true;
            this.dtpDesde.Size = new System.Drawing.Size(150, 23);
            this.dtpDesde.TabIndex = 3;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.Datos_Changed);
            //
            // flpFoto
            //
            this.flpFoto.AutoSize = true;
            this.flpFoto.Controls.Add(this.picFoto);
            this.flpFoto.Controls.Add(this.botCargarFoto);
            this.flpFoto.Controls.Add(this.botQuitarFoto);
            this.flpFoto.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFoto.Name = "flpFoto";
            this.flpFoto.TabIndex = 1;
            this.flpFoto.WrapContents = false;
            //
            // picFoto
            //
            this.picFoto.BackColor = System.Drawing.Color.White;
            this.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFoto.Name = "picFoto";
            this.picFoto.Size = new System.Drawing.Size(260, 200);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFoto.TabStop = false;
            //
            // botCargarFoto
            //
            this.botCargarFoto.Name = "botCargarFoto";
            this.botCargarFoto.Size = new System.Drawing.Size(260, 30);
            this.botCargarFoto.TabIndex = 0;
            this.botCargarFoto.TabStop = false;
            this.botCargarFoto.Text = "Cargar foto…";
            this.botCargarFoto.Click += new System.EventHandler(this.botCargarFoto_Click);
            //
            // botQuitarFoto
            //
            this.botQuitarFoto.Name = "botQuitarFoto";
            this.botQuitarFoto.Size = new System.Drawing.Size(260, 30);
            this.botQuitarFoto.TabIndex = 1;
            this.botQuitarFoto.TabStop = false;
            this.botQuitarFoto.Text = "Quitar foto";
            this.botQuitarFoto.Click += new System.EventHandler(this.botQuitarFoto_Click);
            //
            // labMateriales
            //
            this.labMateriales.AutoSize = true;
            this.labMateriales.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labMateriales.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.labMateriales.Name = "labMateriales";
            this.labMateriales.Text = "Materiales del modelo";
            //
            // dgvMateriales
            //
            this.dgvMateriales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFamilia,
            this.colMaterial,
            this.colFabricante,
            this.colCondicion,
            this.colCantidad,
            this.colUnidad,
            this.colValorUnitario,
            this.colSubtotal});
            this.dgvMateriales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMateriales.Name = "dgvMateriales";
            this.dgvMateriales.TabIndex = 4;
            this.dgvMateriales.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMateriales_CellDoubleClick);
            this.dgvMateriales.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMateriales_CellFormatting);
            this.dgvMateriales.SelectionChanged += new System.EventHandler(this.dgvMateriales_SelectionChanged);
            //
            // colFamilia
            //
            this.colFamilia.DataPropertyName = "Familia";
            this.colFamilia.FillWeight = 90F;
            this.colFamilia.HeaderText = "Familia";
            this.colFamilia.Name = "colFamilia";
            //
            // colMaterial
            //
            this.colMaterial.DataPropertyName = "Material";
            this.colMaterial.FillWeight = 220F;
            this.colMaterial.HeaderText = "Material";
            this.colMaterial.Name = "colMaterial";
            //
            // colFabricante
            //
            this.colFabricante.DataPropertyName = "Fabricante";
            this.colFabricante.HeaderText = "Fabricante (lote)";
            this.colFabricante.Name = "colFabricante";
            //
            // colCondicion
            //
            this.colCondicion.DataPropertyName = "Condicion";
            this.colCondicion.FillWeight = 55F;
            this.colCondicion.HeaderText = "Salió";
            this.colCondicion.Name = "colCondicion";
            //
            // colCantidad
            //
            this.colCantidad.DataPropertyName = "Cantidad";
            this.colCantidad.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCantidad.DefaultCellStyle.Format = "0.###";
            this.colCantidad.FillWeight = 60F;
            this.colCantidad.HeaderText = "Cantidad";
            this.colCantidad.Name = "colCantidad";
            //
            // colUnidad
            //
            this.colUnidad.DataPropertyName = "Unidad";
            this.colUnidad.FillWeight = 60F;
            this.colUnidad.HeaderText = "Unidad";
            this.colUnidad.Name = "colUnidad";
            //
            // colValorUnitario
            //
            this.colValorUnitario.DataPropertyName = "ValorUnitario";
            this.colValorUnitario.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colValorUnitario.DefaultCellStyle.Format = "N2";
            this.colValorUnitario.FillWeight = 60F;
            this.colValorUnitario.HeaderText = "Valor unit.";
            this.colValorUnitario.Name = "colValorUnitario";
            //
            // colSubtotal
            //
            this.colSubtotal.DataPropertyName = "Subtotal";
            this.colSubtotal.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSubtotal.DefaultCellStyle.Format = "N2";
            this.colSubtotal.FillWeight = 70F;
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.Name = "colSubtotal";
            //
            // flpBotonesMaterial
            //
            this.flpBotonesMaterial.AutoSize = true;
            this.flpBotonesMaterial.Controls.Add(this.botAgregarMaterial);
            this.flpBotonesMaterial.Controls.Add(this.botCambiarCantidad);
            this.flpBotonesMaterial.Controls.Add(this.botQuitarMaterial);
            this.flpBotonesMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotonesMaterial.Name = "flpBotonesMaterial";
            this.flpBotonesMaterial.TabIndex = 5;
            //
            // botAgregarMaterial
            //
            this.botAgregarMaterial.Name = "botAgregarMaterial";
            this.botAgregarMaterial.Size = new System.Drawing.Size(210, 38);
            this.botAgregarMaterial.TabIndex = 0;
            this.botAgregarMaterial.Text = "AGREGAR MATERIALES…";
            this.botAgregarMaterial.Click += new System.EventHandler(this.botAgregarMaterial_Click);
            //
            // botCambiarCantidad
            //
            this.botCambiarCantidad.Name = "botCambiarCantidad";
            this.botCambiarCantidad.Size = new System.Drawing.Size(190, 38);
            this.botCambiarCantidad.TabIndex = 1;
            this.botCambiarCantidad.Text = "CAMBIAR CANTIDAD";
            this.botCambiarCantidad.Click += new System.EventHandler(this.botCambiarCantidad_Click);
            //
            // botQuitarMaterial
            //
            this.botQuitarMaterial.Name = "botQuitarMaterial";
            this.botQuitarMaterial.Size = new System.Drawing.Size(190, 38);
            this.botQuitarMaterial.TabIndex = 2;
            this.botQuitarMaterial.Text = "QUITAR MATERIAL";
            this.botQuitarMaterial.Click += new System.EventHandler(this.botQuitarMaterial_Click);
            //
            // labTotal
            //
            this.labTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTotal.AutoSize = true;
            this.labTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labTotal.Name = "labTotal";
            this.labTotal.Text = "Total";
            //
            // labAyuda
            //
            this.labAyuda.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labAyuda.AutoSize = true;
            this.labAyuda.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labAyuda.Name = "labAyuda";
            this.labAyuda.Text = "Los cambios de materiales recién se aplican al stock al GUARDAR.";
            //
            // flpBotones
            //
            this.flpBotones.AutoSize = true;
            this.flpBotones.Controls.Add(this.botCancelar);
            this.flpBotones.Controls.Add(this.botGuardar);
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.TabIndex = 6;
            //
            // botCancelar
            //
            this.botCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCancelar.Margin = new System.Windows.Forms.Padding(6);
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(120, 42);
            this.botCancelar.TabIndex = 1;
            this.botCancelar.Text = "CANCELAR";
            //
            // botGuardar
            //
            this.botGuardar.Margin = new System.Windows.Forms.Padding(6);
            this.botGuardar.Name = "botGuardar";
            this.botGuardar.Size = new System.Drawing.Size(120, 42);
            this.botGuardar.TabIndex = 0;
            this.botGuardar.Text = "GUARDAR";
            this.botGuardar.Click += new System.EventHandler(this.botGuardar_Click);
            //
            // frmModeloEditor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.CancelButton = this.botCancelar;
            this.ClientSize = new System.Drawing.Size(1150, 760);
            this.Controls.Add(this.tlpPrincipal);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmModeloEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modelo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmModeloEditor_FormClosing);
            this.Load += new System.EventHandler(this.frmModeloEditor_Load);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.tlpDatos.ResumeLayout(false);
            this.tlpDatos.PerformLayout();
            this.flpFoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).EndInit();
            this.flpBotonesMaterial.ResumeLayout(false);
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.TableLayoutPanel tlpDatos;
        private System.Windows.Forms.Label labNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label labAlias;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.Label labEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Label labDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.FlowLayoutPanel flpFoto;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Button botCargarFoto;
        private System.Windows.Forms.Button botQuitarFoto;
        private System.Windows.Forms.Label labMateriales;
        private System.Windows.Forms.DataGridView dgvMateriales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFamilia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFabricante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCondicion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.FlowLayoutPanel flpBotonesMaterial;
        private System.Windows.Forms.Button botAgregarMaterial;
        private System.Windows.Forms.Button botCambiarCantidad;
        private System.Windows.Forms.Button botQuitarMaterial;
        private System.Windows.Forms.Label labTotal;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botGuardar;
        private System.Windows.Forms.Label labAyuda;
    }
}
