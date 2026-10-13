namespace Aviones_2._0.Forms.Materiales
{
    partial class frmIngreso
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
            this.botCancelar = new System.Windows.Forms.Button();
            this.botAceptar = new System.Windows.Forms.Button();
            this.botCargarFoto = new System.Windows.Forms.Button();
            this.texEstado = new System.Windows.Forms.TextBox();
            this.texValor = new System.Windows.Forms.TextBox();
            this.texTipo = new System.Windows.Forms.TextBox();
            this.texCantidad = new System.Windows.Forms.TextBox();
            this.texUbicacion = new System.Windows.Forms.TextBox();
            this.texFabricante = new System.Windows.Forms.TextBox();
            this.texCodigo = new System.Windows.Forms.TextBox();
            this.texMedidas = new System.Windows.Forms.TextBox();
            this.texCaracteristica = new System.Windows.Forms.TextBox();
            this.texGrupo = new System.Windows.Forms.TextBox();
            this.texFamilia = new System.Windows.Forms.TextBox();
            this.labFabricante = new System.Windows.Forms.Label();
            this.labEstado = new System.Windows.Forms.Label();
            this.labValor = new System.Windows.Forms.Label();
            this.labTipo = new System.Windows.Forms.Label();
            this.labCantidad = new System.Windows.Forms.Label();
            this.labUbicacion = new System.Windows.Forms.Label();
            this.labCodigo = new System.Windows.Forms.Label();
            this.labMedidas = new System.Windows.Forms.Label();
            this.labCaracteristica = new System.Windows.Forms.Label();
            this.labGrupo = new System.Windows.Forms.Label();
            this.picboxRecuaIngreso1 = new System.Windows.Forms.PictureBox();
            this.picboxRecuaIngreso2 = new System.Windows.Forms.PictureBox();
            this.gruboxEstado = new System.Windows.Forms.GroupBox();
            this.radbotUsado = new System.Windows.Forms.RadioButton();
            this.radbotNuevo = new System.Windows.Forms.RadioButton();
            this.labColor = new System.Windows.Forms.Label();
            this.texColor = new System.Windows.Forms.TextBox();
            this.comboxTipoDeUnidades = new System.Windows.Forms.ComboBox();
            this.picboxRecuaIngreso3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaIngreso1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaIngreso2)).BeginInit();
            this.gruboxEstado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaIngreso3)).BeginInit();
            this.SuspendLayout();
            // 
            // botCancelar
            // 
            this.botCancelar.Location = new System.Drawing.Point(1305, 821);
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(110, 43);
            this.botCancelar.TabIndex = 52;
            this.botCancelar.Text = "CANCELAR";
            this.botCancelar.UseVisualStyleBackColor = true;
            this.botCancelar.Click += new System.EventHandler(this.botCancelar_Click);
            // 
            // botAceptar
            // 
            this.botAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botAceptar.Location = new System.Drawing.Point(1162, 821);
            this.botAceptar.Name = "botAceptar";
            this.botAceptar.Size = new System.Drawing.Size(110, 43);
            this.botAceptar.TabIndex = 51;
            this.botAceptar.Text = "GUARDAR";
            this.botAceptar.UseVisualStyleBackColor = true;
            this.botAceptar.Visible = false;
            this.botAceptar.Click += new System.EventHandler(this.botAceptar_Click);
            // 
            // botCargarFoto
            // 
            this.botCargarFoto.Location = new System.Drawing.Point(282, 821);
            this.botCargarFoto.Name = "botCargarFoto";
            this.botCargarFoto.Size = new System.Drawing.Size(110, 43);
            this.botCargarFoto.TabIndex = 50;
            this.botCargarFoto.Text = "CARGAR FOTO";
            this.botCargarFoto.UseVisualStyleBackColor = true;
            this.botCargarFoto.Click += new System.EventHandler(this.botCargarFoto_Click);
            // 
            // texEstado
            // 
            this.texEstado.BackColor = System.Drawing.Color.LightSteelBlue;
            this.texEstado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texEstado.Location = new System.Drawing.Point(967, 739);
            this.texEstado.Name = "texEstado";
            this.texEstado.Size = new System.Drawing.Size(75, 22);
            this.texEstado.TabIndex = 49;
            this.texEstado.Visible = false;
            this.texEstado.Click += new System.EventHandler(this.texEstado_Click);
            // 
            // texValor
            // 
            this.texValor.BackColor = System.Drawing.Color.White;
            this.texValor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texValor.Location = new System.Drawing.Point(1172, 756);
            this.texValor.Name = "texValor";
            this.texValor.Size = new System.Drawing.Size(100, 22);
            this.texValor.TabIndex = 48;
            // 
            // texTipo
            // 
            this.texTipo.BackColor = System.Drawing.Color.White;
            this.texTipo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texTipo.Location = new System.Drawing.Point(1172, 715);
            this.texTipo.Name = "texTipo";
            this.texTipo.Size = new System.Drawing.Size(200, 22);
            this.texTipo.TabIndex = 47;
            this.texTipo.Click += new System.EventHandler(this.texTipo_Click);
            // 
            // texCantidad
            // 
            this.texCantidad.BackColor = System.Drawing.Color.White;
            this.texCantidad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texCantidad.Location = new System.Drawing.Point(1170, 674);
            this.texCantidad.Name = "texCantidad";
            this.texCantidad.Size = new System.Drawing.Size(50, 22);
            this.texCantidad.TabIndex = 46;
            // 
            // texUbicacion
            // 
            this.texUbicacion.BackColor = System.Drawing.Color.White;
            this.texUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texUbicacion.Location = new System.Drawing.Point(1172, 634);
            this.texUbicacion.Name = "texUbicacion";
            this.texUbicacion.Size = new System.Drawing.Size(200, 22);
            this.texUbicacion.TabIndex = 45;
            // 
            // texFabricante
            // 
            this.texFabricante.BackColor = System.Drawing.Color.White;
            this.texFabricante.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texFabricante.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texFabricante.Location = new System.Drawing.Point(787, 691);
            this.texFabricante.Name = "texFabricante";
            this.texFabricante.Size = new System.Drawing.Size(200, 22);
            this.texFabricante.TabIndex = 44;
            // 
            // texCodigo
            // 
            this.texCodigo.BackColor = System.Drawing.Color.White;
            this.texCodigo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texCodigo.Location = new System.Drawing.Point(787, 633);
            this.texCodigo.Name = "texCodigo";
            this.texCodigo.Size = new System.Drawing.Size(200, 22);
            this.texCodigo.TabIndex = 43;
            // 
            // texMedidas
            // 
            this.texMedidas.BackColor = System.Drawing.Color.White;
            this.texMedidas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texMedidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texMedidas.Location = new System.Drawing.Point(413, 756);
            this.texMedidas.Name = "texMedidas";
            this.texMedidas.Size = new System.Drawing.Size(200, 22);
            this.texMedidas.TabIndex = 42;
            // 
            // texCaracteristica
            // 
            this.texCaracteristica.BackColor = System.Drawing.Color.White;
            this.texCaracteristica.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texCaracteristica.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texCaracteristica.Location = new System.Drawing.Point(413, 674);
            this.texCaracteristica.Name = "texCaracteristica";
            this.texCaracteristica.Size = new System.Drawing.Size(200, 22);
            this.texCaracteristica.TabIndex = 41;
            // 
            // texGrupo
            // 
            this.texGrupo.BackColor = System.Drawing.Color.White;
            this.texGrupo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texGrupo.Location = new System.Drawing.Point(413, 633);
            this.texGrupo.Name = "texGrupo";
            this.texGrupo.Size = new System.Drawing.Size(200, 22);
            this.texGrupo.TabIndex = 40;
            // 
            // texFamilia
            // 
            this.texFamilia.BackColor = System.Drawing.Color.LightSteelBlue;
            this.texFamilia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.texFamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texFamilia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.texFamilia.Location = new System.Drawing.Point(116, 386);
            this.texFamilia.Name = "texFamilia";
            this.texFamilia.ReadOnly = true;
            this.texFamilia.Size = new System.Drawing.Size(420, 40);
            this.texFamilia.TabIndex = 39;
            // 
            // labFabricante
            // 
            this.labFabricante.AutoSize = true;
            this.labFabricante.BackColor = System.Drawing.Color.CadetBlue;
            this.labFabricante.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFabricante.ForeColor = System.Drawing.Color.White;
            this.labFabricante.Location = new System.Drawing.Point(695, 693);
            this.labFabricante.Name = "labFabricante";
            this.labFabricante.Size = new System.Drawing.Size(87, 18);
            this.labFabricante.TabIndex = 38;
            this.labFabricante.Text = "Fabricante";
            // 
            // labEstado
            // 
            this.labEstado.AutoSize = true;
            this.labEstado.BackColor = System.Drawing.Color.CadetBlue;
            this.labEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEstado.ForeColor = System.Drawing.Color.White;
            this.labEstado.Location = new System.Drawing.Point(715, 753);
            this.labEstado.Name = "labEstado";
            this.labEstado.Size = new System.Drawing.Size(61, 18);
            this.labEstado.TabIndex = 37;
            this.labEstado.Text = "Estado";
            // 
            // labValor
            // 
            this.labValor.AutoSize = true;
            this.labValor.BackColor = System.Drawing.Color.CadetBlue;
            this.labValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labValor.ForeColor = System.Drawing.Color.White;
            this.labValor.Location = new System.Drawing.Point(1111, 756);
            this.labValor.Name = "labValor";
            this.labValor.Size = new System.Drawing.Size(47, 18);
            this.labValor.TabIndex = 36;
            this.labValor.Text = "Valor";
            // 
            // labTipo
            // 
            this.labTipo.AutoSize = true;
            this.labTipo.BackColor = System.Drawing.Color.CadetBlue;
            this.labTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipo.ForeColor = System.Drawing.Color.White;
            this.labTipo.Location = new System.Drawing.Point(1116, 715);
            this.labTipo.Name = "labTipo";
            this.labTipo.Size = new System.Drawing.Size(41, 18);
            this.labTipo.TabIndex = 35;
            this.labTipo.Text = "Tipo";
            // 
            // labCantidad
            // 
            this.labCantidad.AutoSize = true;
            this.labCantidad.BackColor = System.Drawing.Color.CadetBlue;
            this.labCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCantidad.ForeColor = System.Drawing.Color.White;
            this.labCantidad.Location = new System.Drawing.Point(1087, 678);
            this.labCantidad.Name = "labCantidad";
            this.labCantidad.Size = new System.Drawing.Size(74, 18);
            this.labCantidad.TabIndex = 34;
            this.labCantidad.Text = "Cantidad";
            // 
            // labUbicacion
            // 
            this.labUbicacion.AutoSize = true;
            this.labUbicacion.BackColor = System.Drawing.Color.CadetBlue;
            this.labUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labUbicacion.ForeColor = System.Drawing.Color.White;
            this.labUbicacion.Location = new System.Drawing.Point(1081, 638);
            this.labUbicacion.Name = "labUbicacion";
            this.labUbicacion.Size = new System.Drawing.Size(83, 18);
            this.labUbicacion.TabIndex = 33;
            this.labUbicacion.Text = "Ubicacion";
            // 
            // labCodigo
            // 
            this.labCodigo.AutoSize = true;
            this.labCodigo.BackColor = System.Drawing.Color.CadetBlue;
            this.labCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.ForeColor = System.Drawing.Color.White;
            this.labCodigo.Location = new System.Drawing.Point(716, 637);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(62, 18);
            this.labCodigo.TabIndex = 32;
            this.labCodigo.Text = "Codigo";
            // 
            // labMedidas
            // 
            this.labMedidas.AutoSize = true;
            this.labMedidas.BackColor = System.Drawing.Color.CadetBlue;
            this.labMedidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labMedidas.ForeColor = System.Drawing.Color.White;
            this.labMedidas.Location = new System.Drawing.Point(334, 758);
            this.labMedidas.Name = "labMedidas";
            this.labMedidas.Size = new System.Drawing.Size(71, 18);
            this.labMedidas.TabIndex = 31;
            this.labMedidas.Text = "Medidas";
            // 
            // labCaracteristica
            // 
            this.labCaracteristica.AutoSize = true;
            this.labCaracteristica.BackColor = System.Drawing.Color.CadetBlue;
            this.labCaracteristica.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCaracteristica.ForeColor = System.Drawing.Color.White;
            this.labCaracteristica.Location = new System.Drawing.Point(299, 676);
            this.labCaracteristica.Name = "labCaracteristica";
            this.labCaracteristica.Size = new System.Drawing.Size(113, 18);
            this.labCaracteristica.TabIndex = 30;
            this.labCaracteristica.Text = "Caracteristica";
            // 
            // labGrupo
            // 
            this.labGrupo.AutoSize = true;
            this.labGrupo.BackColor = System.Drawing.Color.CadetBlue;
            this.labGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labGrupo.ForeColor = System.Drawing.Color.White;
            this.labGrupo.Location = new System.Drawing.Point(345, 635);
            this.labGrupo.Name = "labGrupo";
            this.labGrupo.Size = new System.Drawing.Size(55, 18);
            this.labGrupo.TabIndex = 29;
            this.labGrupo.Text = "Grupo";
            // 
            // picboxRecuaIngreso1
            // 
            this.picboxRecuaIngreso1.BackColor = System.Drawing.Color.CadetBlue;
            this.picboxRecuaIngreso1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picboxRecuaIngreso1.Location = new System.Drawing.Point(282, 608);
            this.picboxRecuaIngreso1.Name = "picboxRecuaIngreso1";
            this.picboxRecuaIngreso1.Size = new System.Drawing.Size(378, 193);
            this.picboxRecuaIngreso1.TabIndex = 53;
            this.picboxRecuaIngreso1.TabStop = false;
            // 
            // picboxRecuaIngreso2
            // 
            this.picboxRecuaIngreso2.BackColor = System.Drawing.Color.CadetBlue;
            this.picboxRecuaIngreso2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picboxRecuaIngreso2.Location = new System.Drawing.Point(660, 608);
            this.picboxRecuaIngreso2.Name = "picboxRecuaIngreso2";
            this.picboxRecuaIngreso2.Size = new System.Drawing.Size(382, 193);
            this.picboxRecuaIngreso2.TabIndex = 55;
            this.picboxRecuaIngreso2.TabStop = false;
            // 
            // gruboxEstado
            // 
            this.gruboxEstado.BackColor = System.Drawing.Color.CadetBlue;
            this.gruboxEstado.Controls.Add(this.radbotUsado);
            this.gruboxEstado.Controls.Add(this.radbotNuevo);
            this.gruboxEstado.Location = new System.Drawing.Point(787, 739);
            this.gruboxEstado.Name = "gruboxEstado";
            this.gruboxEstado.Size = new System.Drawing.Size(169, 39);
            this.gruboxEstado.TabIndex = 56;
            this.gruboxEstado.TabStop = false;
            this.gruboxEstado.Visible = false;
            // 
            // radbotUsado
            // 
            this.radbotUsado.AutoSize = true;
            this.radbotUsado.BackColor = System.Drawing.Color.CadetBlue;
            this.radbotUsado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbotUsado.ForeColor = System.Drawing.Color.White;
            this.radbotUsado.Location = new System.Drawing.Point(88, 10);
            this.radbotUsado.Name = "radbotUsado";
            this.radbotUsado.Size = new System.Drawing.Size(75, 22);
            this.radbotUsado.TabIndex = 32;
            this.radbotUsado.Text = "Usado";
            this.radbotUsado.UseVisualStyleBackColor = false;
            this.radbotUsado.Click += new System.EventHandler(this.radbotUsado_Click);
            // 
            // radbotNuevo
            // 
            this.radbotNuevo.AutoSize = true;
            this.radbotNuevo.BackColor = System.Drawing.Color.CadetBlue;
            this.radbotNuevo.Checked = true;
            this.radbotNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbotNuevo.ForeColor = System.Drawing.Color.White;
            this.radbotNuevo.Location = new System.Drawing.Point(6, 10);
            this.radbotNuevo.Name = "radbotNuevo";
            this.radbotNuevo.Size = new System.Drawing.Size(74, 22);
            this.radbotNuevo.TabIndex = 31;
            this.radbotNuevo.TabStop = true;
            this.radbotNuevo.Text = "Nuevo";
            this.radbotNuevo.UseVisualStyleBackColor = false;
            this.radbotNuevo.Click += new System.EventHandler(this.radbotNuevo_Click);
            // 
            // labColor
            // 
            this.labColor.AutoSize = true;
            this.labColor.BackColor = System.Drawing.Color.CadetBlue;
            this.labColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labColor.ForeColor = System.Drawing.Color.White;
            this.labColor.Location = new System.Drawing.Point(353, 717);
            this.labColor.Name = "labColor";
            this.labColor.Size = new System.Drawing.Size(50, 18);
            this.labColor.TabIndex = 57;
            this.labColor.Text = "Color";
            // 
            // texColor
            // 
            this.texColor.BackColor = System.Drawing.Color.White;
            this.texColor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.texColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texColor.Location = new System.Drawing.Point(413, 715);
            this.texColor.Name = "texColor";
            this.texColor.Size = new System.Drawing.Size(200, 22);
            this.texColor.TabIndex = 58;
            // 
            // comboxTipoDeUnidades
            // 
            this.comboxTipoDeUnidades.FormattingEnabled = true;
            this.comboxTipoDeUnidades.Location = new System.Drawing.Point(146, 480);
            this.comboxTipoDeUnidades.Name = "comboxTipoDeUnidades";
            this.comboxTipoDeUnidades.Size = new System.Drawing.Size(200, 21);
            this.comboxTipoDeUnidades.TabIndex = 59;
            this.comboxTipoDeUnidades.Visible = false;
            this.comboxTipoDeUnidades.SelectedIndexChanged += new System.EventHandler(this.comboxTipoDeUnidades_SelectedIndexChanged);
            // 
            // picboxRecuaIngreso3
            // 
            this.picboxRecuaIngreso3.BackColor = System.Drawing.Color.CadetBlue;
            this.picboxRecuaIngreso3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picboxRecuaIngreso3.Location = new System.Drawing.Point(1043, 608);
            this.picboxRecuaIngreso3.Name = "picboxRecuaIngreso3";
            this.picboxRecuaIngreso3.Size = new System.Drawing.Size(382, 193);
            this.picboxRecuaIngreso3.TabIndex = 60;
            this.picboxRecuaIngreso3.TabStop = false;
            // 
            // frmIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1457, 882);
            this.Controls.Add(this.comboxTipoDeUnidades);
            this.Controls.Add(this.texColor);
            this.Controls.Add(this.labColor);
            this.Controls.Add(this.gruboxEstado);
            this.Controls.Add(this.botCancelar);
            this.Controls.Add(this.botAceptar);
            this.Controls.Add(this.botCargarFoto);
            this.Controls.Add(this.texEstado);
            this.Controls.Add(this.texValor);
            this.Controls.Add(this.texTipo);
            this.Controls.Add(this.texCantidad);
            this.Controls.Add(this.texUbicacion);
            this.Controls.Add(this.texFabricante);
            this.Controls.Add(this.texCodigo);
            this.Controls.Add(this.texMedidas);
            this.Controls.Add(this.texCaracteristica);
            this.Controls.Add(this.texGrupo);
            this.Controls.Add(this.texFamilia);
            this.Controls.Add(this.labFabricante);
            this.Controls.Add(this.labEstado);
            this.Controls.Add(this.labValor);
            this.Controls.Add(this.labTipo);
            this.Controls.Add(this.labCantidad);
            this.Controls.Add(this.labUbicacion);
            this.Controls.Add(this.labCodigo);
            this.Controls.Add(this.labMedidas);
            this.Controls.Add(this.labCaracteristica);
            this.Controls.Add(this.labGrupo);
            this.Controls.Add(this.picboxRecuaIngreso1);
            this.Controls.Add(this.picboxRecuaIngreso2);
            this.Controls.Add(this.picboxRecuaIngreso3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(600, 150);
            this.Name = "frmIngreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmIngreso";
            this.Load += new System.EventHandler(this.frmIngreso_Load);
            this.LocationChanged += new System.EventHandler(this.frmIngreso_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaIngreso1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaIngreso2)).EndInit();
            this.gruboxEstado.ResumeLayout(false);
            this.gruboxEstado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaIngreso3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botAceptar;
        private System.Windows.Forms.Button botCargarFoto;
        public System.Windows.Forms.TextBox texEstado;
        private System.Windows.Forms.TextBox texValor;
        private System.Windows.Forms.TextBox texTipo;
        private System.Windows.Forms.TextBox texCantidad;
        private System.Windows.Forms.TextBox texUbicacion;
        private System.Windows.Forms.TextBox texFabricante;
        private System.Windows.Forms.TextBox texCodigo;
        private System.Windows.Forms.TextBox texMedidas;
        private System.Windows.Forms.TextBox texCaracteristica;
        public System.Windows.Forms.TextBox texGrupo;
        public System.Windows.Forms.TextBox texFamilia;
        private System.Windows.Forms.Label labFabricante;
        private System.Windows.Forms.Label labEstado;
        private System.Windows.Forms.Label labValor;
        private System.Windows.Forms.Label labTipo;
        private System.Windows.Forms.Label labCantidad;
        private System.Windows.Forms.Label labUbicacion;
        private System.Windows.Forms.Label labCodigo;
        private System.Windows.Forms.Label labMedidas;
        private System.Windows.Forms.Label labCaracteristica;
        private System.Windows.Forms.Label labGrupo;
        private System.Windows.Forms.PictureBox picboxRecuaIngreso1;
        private System.Windows.Forms.PictureBox picboxRecuaIngreso2;
        private System.Windows.Forms.GroupBox gruboxEstado;
        private System.Windows.Forms.RadioButton radbotUsado;
        private System.Windows.Forms.RadioButton radbotNuevo;
        private System.Windows.Forms.Label labColor;
        private System.Windows.Forms.TextBox texColor;
        private System.Windows.Forms.ComboBox comboxTipoDeUnidades;
        private System.Windows.Forms.PictureBox picboxRecuaIngreso3;
    }
}