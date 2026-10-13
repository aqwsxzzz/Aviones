namespace Aviones_2._0.Forms.Materiales
{
    partial class frmMateriales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMateriales));
            this.labTituloPrincipal = new System.Windows.Forms.Label();
            this.botIngresar = new System.Windows.Forms.Button();
            this.botSalir = new System.Windows.Forms.Button();
            this.botModifica = new System.Windows.Forms.Button();
            this.botImprimir = new System.Windows.Forms.Button();
            this.botEliminar = new System.Windows.Forms.Button();
            this.botBuscar = new System.Windows.Forms.Button();
            this.labFamiliaMat = new System.Windows.Forms.Label();
            this.botIngresarFam = new System.Windows.Forms.Button();
            this.listFamiliaMat = new System.Windows.Forms.ListBox();
            this.botCancelarMat = new System.Windows.Forms.Button();
            this.texNuevoFam = new System.Windows.Forms.TextBox();
            this.labIngresoMat = new System.Windows.Forms.Label();
            this.botGuardarFam = new System.Windows.Forms.Button();
            this.botCancelarFam = new System.Windows.Forms.Button();
            this.labModificaMat = new System.Windows.Forms.Label();
            this.labEliminaMat = new System.Windows.Forms.Label();
            this.labTituloDgv = new System.Windows.Forms.Label();
            this.labIngresoFamilia = new System.Windows.Forms.Label();
            this.botEliminarFam = new System.Windows.Forms.Button();
            this.labListaFamilia1 = new System.Windows.Forms.Label();
            this.labListaFamilia2 = new System.Windows.Forms.Label();
            this.botSalirFotoAgrandada = new System.Windows.Forms.Button();
            this.botRegresa = new System.Windows.Forms.Button();
            this.picboxFotoMat = new System.Windows.Forms.PictureBox();
            this.picboxRecuaTituloMat = new System.Windows.Forms.PictureBox();
            this.picboxRecuadroGrande = new System.Windows.Forms.PictureBox();
            this.picboxRecuadroTitulodgv = new System.Windows.Forms.PictureBox();
            this.picboxRecuaMat = new System.Windows.Forms.PictureBox();
            this.panRecuaIngreso = new System.Windows.Forms.Panel();
            this.texFamiliaBuscar = new System.Windows.Forms.TextBox();
            this.panFamiliasUnidades = new System.Windows.Forms.Panel();
            this.botFamilias = new System.Windows.Forms.Button();
            this.botRegistros = new System.Windows.Forms.Button();
            this.botIngresarTipo = new System.Windows.Forms.Button();
            this.botEliminarTipo = new System.Windows.Forms.Button();
            this.listUbicaciones = new System.Windows.Forms.ListBox();
            this.labEliminarFamilia = new System.Windows.Forms.Label();
            this.botInforma = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.botModificarFam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picboxFotoMat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaTituloMat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuadroGrande)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuadroTitulodgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaMat)).BeginInit();
            this.SuspendLayout();
            // 
            // labTituloPrincipal
            // 
            this.labTituloPrincipal.AutoSize = true;
            this.labTituloPrincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTituloPrincipal.ForeColor = System.Drawing.Color.Black;
            this.labTituloPrincipal.Location = new System.Drawing.Point(855, 85);
            this.labTituloPrincipal.Name = "labTituloPrincipal";
            this.labTituloPrincipal.Size = new System.Drawing.Size(197, 33);
            this.labTituloPrincipal.TabIndex = 1;
            this.labTituloPrincipal.Text = "MATERIALES";
            // 
            // botIngresar
            // 
            this.botIngresar.BackColor = System.Drawing.Color.CadetBlue;
            this.botIngresar.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.botIngresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botIngresar.ForeColor = System.Drawing.Color.Black;
            this.botIngresar.Location = new System.Drawing.Point(472, 673);
            this.botIngresar.Name = "botIngresar";
            this.botIngresar.Size = new System.Drawing.Size(160, 71);
            this.botIngresar.TabIndex = 2;
            this.botIngresar.Text = "INGRESAR MODIFICAR  BORRAR";
            this.botIngresar.UseVisualStyleBackColor = false;
            this.botIngresar.Click += new System.EventHandler(this.botIngresar_Click);
            // 
            // botSalir
            // 
            this.botSalir.BackColor = System.Drawing.Color.DarkSlateGray;
            this.botSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.botSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botSalir.ForeColor = System.Drawing.Color.White;
            this.botSalir.Location = new System.Drawing.Point(1586, 672);
            this.botSalir.Name = "botSalir";
            this.botSalir.Size = new System.Drawing.Size(160, 60);
            this.botSalir.TabIndex = 0;
            this.botSalir.Text = "SALIR";
            this.botSalir.UseVisualStyleBackColor = false;
            this.botSalir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // botModifica
            // 
            this.botModifica.Enabled = false;
            this.botModifica.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botModifica.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botModifica.ForeColor = System.Drawing.Color.Black;
            this.botModifica.Location = new System.Drawing.Point(829, 674);
            this.botModifica.Name = "botModifica";
            this.botModifica.Size = new System.Drawing.Size(160, 71);
            this.botModifica.TabIndex = 36;
            this.botModifica.Text = "MODIFICAR";
            this.botModifica.UseVisualStyleBackColor = false;
            this.botModifica.Click += new System.EventHandler(this.botModifica_Click);
            // 
            // botImprimir
            // 
            this.botImprimir.BackColor = System.Drawing.Color.CadetBlue;
            this.botImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botImprimir.ForeColor = System.Drawing.Color.Black;
            this.botImprimir.Location = new System.Drawing.Point(1361, 673);
            this.botImprimir.Name = "botImprimir";
            this.botImprimir.Size = new System.Drawing.Size(160, 71);
            this.botImprimir.TabIndex = 6;
            this.botImprimir.Text = "IMPRIMIR";
            this.botImprimir.UseVisualStyleBackColor = false;
            this.botImprimir.Click += new System.EventHandler(this.botImprimir_Click);
            // 
            // botEliminar
            // 
            this.botEliminar.Enabled = false;
            this.botEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botEliminar.ForeColor = System.Drawing.Color.Black;
            this.botEliminar.Location = new System.Drawing.Point(1007, 674);
            this.botEliminar.Name = "botEliminar";
            this.botEliminar.Size = new System.Drawing.Size(160, 71);
            this.botEliminar.TabIndex = 5;
            this.botEliminar.Text = "ELIMINAR";
            this.botEliminar.UseVisualStyleBackColor = true;
            this.botEliminar.Click += new System.EventHandler(this.botEliminar_Click);
            // 
            // botBuscar
            // 
            this.botBuscar.BackColor = System.Drawing.Color.CadetBlue;
            this.botBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botBuscar.ForeColor = System.Drawing.Color.Black;
            this.botBuscar.Location = new System.Drawing.Point(651, 673);
            this.botBuscar.Name = "botBuscar";
            this.botBuscar.Size = new System.Drawing.Size(160, 71);
            this.botBuscar.TabIndex = 3;
            this.botBuscar.Text = "BUSCAR";
            this.botBuscar.UseVisualStyleBackColor = false;
            this.botBuscar.Click += new System.EventHandler(this.botBuscar_Click);
            // 
            // labFamiliaMat
            // 
            this.labFamiliaMat.AutoSize = true;
            this.labFamiliaMat.BackColor = System.Drawing.Color.SteelBlue;
            this.labFamiliaMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFamiliaMat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labFamiliaMat.Location = new System.Drawing.Point(756, 421);
            this.labFamiliaMat.Name = "labFamiliaMat";
            this.labFamiliaMat.Size = new System.Drawing.Size(132, 31);
            this.labFamiliaMat.TabIndex = 8;
            this.labFamiliaMat.Text = "Familias:";
            this.labFamiliaMat.Visible = false;
            // 
            // botIngresarFam
            // 
            this.botIngresarFam.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botIngresarFam.ForeColor = System.Drawing.SystemColors.ControlText;
            this.botIngresarFam.Location = new System.Drawing.Point(603, 491);
            this.botIngresarFam.Name = "botIngresarFam";
            this.botIngresarFam.Size = new System.Drawing.Size(131, 50);
            this.botIngresarFam.TabIndex = 9;
            this.botIngresarFam.Text = "Ingresar ";
            this.botIngresarFam.UseVisualStyleBackColor = true;
            this.botIngresarFam.Visible = false;
            this.botIngresarFam.Click += new System.EventHandler(this.botIngresarFam_Click);
            // 
            // listFamiliaMat
            // 
            this.listFamiliaMat.BackColor = System.Drawing.Color.SteelBlue;
            this.listFamiliaMat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listFamiliaMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listFamiliaMat.ForeColor = System.Drawing.Color.Black;
            this.listFamiliaMat.FormattingEnabled = true;
            this.listFamiliaMat.ItemHeight = 20;
            this.listFamiliaMat.Location = new System.Drawing.Point(13, 288);
            this.listFamiliaMat.Name = "listFamiliaMat";
            this.listFamiliaMat.Size = new System.Drawing.Size(200, 20);
            this.listFamiliaMat.Sorted = true;
            this.listFamiliaMat.TabIndex = 17;
            this.listFamiliaMat.Visible = false;
            this.listFamiliaMat.SelectedIndexChanged += new System.EventHandler(this.listFamiliaMat_SelectedIndexChanged);
            // 
            // botCancelarMat
            // 
            this.botCancelarMat.BackColor = System.Drawing.Color.SteelBlue;
            this.botCancelarMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botCancelarMat.Location = new System.Drawing.Point(1198, 434);
            this.botCancelarMat.Name = "botCancelarMat";
            this.botCancelarMat.Size = new System.Drawing.Size(174, 50);
            this.botCancelarMat.TabIndex = 20;
            this.botCancelarMat.Text = "Cancelar";
            this.botCancelarMat.UseVisualStyleBackColor = false;
            this.botCancelarMat.Visible = false;
            this.botCancelarMat.Click += new System.EventHandler(this.botCancelarMat_Click);
            // 
            // texNuevoFam
            // 
            this.texNuevoFam.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texNuevoFam.Location = new System.Drawing.Point(752, 138);
            this.texNuevoFam.Margin = new System.Windows.Forms.Padding(4);
            this.texNuevoFam.Multiline = true;
            this.texNuevoFam.Name = "texNuevoFam";
            this.texNuevoFam.Size = new System.Drawing.Size(237, 35);
            this.texNuevoFam.TabIndex = 21;
            this.texNuevoFam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.texNuevoFam.Visible = false;
            this.texNuevoFam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.texNuevoFam_KeyDown);
            // 
            // labIngresoMat
            // 
            this.labIngresoMat.AutoSize = true;
            this.labIngresoMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labIngresoMat.ForeColor = System.Drawing.Color.Black;
            this.labIngresoMat.Location = new System.Drawing.Point(50, 81);
            this.labIngresoMat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labIngresoMat.Name = "labIngresoMat";
            this.labIngresoMat.Size = new System.Drawing.Size(186, 24);
            this.labIngresoMat.TabIndex = 22;
            this.labIngresoMat.Text = "Ingreso de registro";
            this.labIngresoMat.Visible = false;
            // 
            // botGuardarFam
            // 
            this.botGuardarFam.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botGuardarFam.Location = new System.Drawing.Point(560, 414);
            this.botGuardarFam.Margin = new System.Windows.Forms.Padding(4);
            this.botGuardarFam.Name = "botGuardarFam";
            this.botGuardarFam.Size = new System.Drawing.Size(174, 50);
            this.botGuardarFam.TabIndex = 23;
            this.botGuardarFam.Text = "GUARDAR";
            this.botGuardarFam.UseVisualStyleBackColor = true;
            this.botGuardarFam.Visible = false;
            this.botGuardarFam.Click += new System.EventHandler(this.botGuardarFam_Click);
            // 
            // botCancelarFam
            // 
            this.botCancelarFam.BackColor = System.Drawing.Color.SteelBlue;
            this.botCancelarFam.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botCancelarFam.ForeColor = System.Drawing.Color.Black;
            this.botCancelarFam.Location = new System.Drawing.Point(1198, 491);
            this.botCancelarFam.Margin = new System.Windows.Forms.Padding(4);
            this.botCancelarFam.Name = "botCancelarFam";
            this.botCancelarFam.Size = new System.Drawing.Size(147, 50);
            this.botCancelarFam.TabIndex = 24;
            this.botCancelarFam.Text = "Cancelar";
            this.botCancelarFam.UseVisualStyleBackColor = false;
            this.botCancelarFam.Visible = false;
            this.botCancelarFam.Click += new System.EventHandler(this.botCancelarFam_Click);
            // 
            // labModificaMat
            // 
            this.labModificaMat.AutoSize = true;
            this.labModificaMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labModificaMat.ForeColor = System.Drawing.Color.Black;
            this.labModificaMat.Location = new System.Drawing.Point(41, 35);
            this.labModificaMat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labModificaMat.Name = "labModificaMat";
            this.labModificaMat.Size = new System.Drawing.Size(241, 24);
            this.labModificaMat.TabIndex = 25;
            this.labModificaMat.Text = "Modificación de Registro";
            this.labModificaMat.Visible = false;
            // 
            // labEliminaMat
            // 
            this.labEliminaMat.AutoSize = true;
            this.labEliminaMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEliminaMat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labEliminaMat.Location = new System.Drawing.Point(36, 127);
            this.labEliminaMat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labEliminaMat.Name = "labEliminaMat";
            this.labEliminaMat.Size = new System.Drawing.Size(184, 25);
            this.labEliminaMat.TabIndex = 26;
            this.labEliminaMat.Text = "Eliminar registro";
            this.labEliminaMat.Visible = false;
            // 
            // labTituloDgv
            // 
            this.labTituloDgv.AutoSize = true;
            this.labTituloDgv.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTituloDgv.Location = new System.Drawing.Point(415, 64);
            this.labTituloDgv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTituloDgv.Name = "labTituloDgv";
            this.labTituloDgv.Size = new System.Drawing.Size(217, 25);
            this.labTituloDgv.TabIndex = 27;
            this.labTituloDgv.Text = "Listado de materiales";
            // 
            // labIngresoFamilia
            // 
            this.labIngresoFamilia.AutoSize = true;
            this.labIngresoFamilia.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labIngresoFamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labIngresoFamilia.Location = new System.Drawing.Point(322, 19);
            this.labIngresoFamilia.Name = "labIngresoFamilia";
            this.labIngresoFamilia.Size = new System.Drawing.Size(216, 25);
            this.labIngresoFamilia.TabIndex = 31;
            this.labIngresoFamilia.Text = "Ingrese nueva familia";
            this.labIngresoFamilia.Visible = false;
            // 
            // botEliminarFam
            // 
            this.botEliminarFam.BackColor = System.Drawing.Color.LightYellow;
            this.botEliminarFam.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botEliminarFam.ForeColor = System.Drawing.Color.Red;
            this.botEliminarFam.Location = new System.Drawing.Point(912, 491);
            this.botEliminarFam.Name = "botEliminarFam";
            this.botEliminarFam.Size = new System.Drawing.Size(131, 50);
            this.botEliminarFam.TabIndex = 32;
            this.botEliminarFam.Text = "Eliminar";
            this.botEliminarFam.UseVisualStyleBackColor = false;
            this.botEliminarFam.Visible = false;
            this.botEliminarFam.Click += new System.EventHandler(this.botEliminarFam_Click);
            // 
            // labListaFamilia1
            // 
            this.labListaFamilia1.AutoSize = true;
            this.labListaFamilia1.BackColor = System.Drawing.Color.SteelBlue;
            this.labListaFamilia1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labListaFamilia1.ForeColor = System.Drawing.Color.Indigo;
            this.labListaFamilia1.Location = new System.Drawing.Point(42, 342);
            this.labListaFamilia1.Name = "labListaFamilia1";
            this.labListaFamilia1.Size = new System.Drawing.Size(94, 20);
            this.labListaFamilia1.TabIndex = 33;
            this.labListaFamilia1.Text = " LISTA DE";
            this.labListaFamilia1.Visible = false;
            // 
            // labListaFamilia2
            // 
            this.labListaFamilia2.AutoSize = true;
            this.labListaFamilia2.BackColor = System.Drawing.Color.SteelBlue;
            this.labListaFamilia2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labListaFamilia2.ForeColor = System.Drawing.Color.Indigo;
            this.labListaFamilia2.Location = new System.Drawing.Point(45, 374);
            this.labListaFamilia2.Name = "labListaFamilia2";
            this.labListaFamilia2.Size = new System.Drawing.Size(92, 20);
            this.labListaFamilia2.TabIndex = 35;
            this.labListaFamilia2.Text = "FAMILIAS";
            this.labListaFamilia2.Visible = false;
            // 
            // botSalirFotoAgrandada
            // 
            this.botSalirFotoAgrandada.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botSalirFotoAgrandada.Location = new System.Drawing.Point(1185, 115);
            this.botSalirFotoAgrandada.Name = "botSalirFotoAgrandada";
            this.botSalirFotoAgrandada.Size = new System.Drawing.Size(41, 38);
            this.botSalirFotoAgrandada.TabIndex = 37;
            this.botSalirFotoAgrandada.Text = "X";
            this.botSalirFotoAgrandada.UseVisualStyleBackColor = true;
            this.botSalirFotoAgrandada.Visible = false;
            this.botSalirFotoAgrandada.Click += new System.EventHandler(this.botSalirFotoAgrandada_Click);
            // 
            // botRegresa
            // 
            this.botRegresa.BackColor = System.Drawing.Color.SteelBlue;
            this.botRegresa.FlatAppearance.BorderSize = 0;
            this.botRegresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botRegresa.ForeColor = System.Drawing.Color.White;
            this.botRegresa.Location = new System.Drawing.Point(1606, 546);
            this.botRegresa.Name = "botRegresa";
            this.botRegresa.Size = new System.Drawing.Size(160, 60);
            this.botRegresa.TabIndex = 38;
            this.botRegresa.Text = "REGRESA";
            this.botRegresa.UseVisualStyleBackColor = false;
            this.botRegresa.Visible = false;
            this.botRegresa.Click += new System.EventHandler(this.botRegresa_Click);
            // 
            // picboxFotoMat
            // 
            this.picboxFotoMat.BackColor = System.Drawing.Color.SkyBlue;
            this.picboxFotoMat.Location = new System.Drawing.Point(1457, 8);
            this.picboxFotoMat.Margin = new System.Windows.Forms.Padding(4);
            this.picboxFotoMat.Name = "picboxFotoMat";
            this.picboxFotoMat.Size = new System.Drawing.Size(354, 329);
            this.picboxFotoMat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picboxFotoMat.TabIndex = 30;
            this.picboxFotoMat.TabStop = false;
            this.picboxFotoMat.Visible = false;
            this.picboxFotoMat.DoubleClick += new System.EventHandler(this.picboxFotoMat_DoubleClick);
            // 
            // picboxRecuaTituloMat
            // 
            this.picboxRecuaTituloMat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picboxRecuaTituloMat.Location = new System.Drawing.Point(33, 19);
            this.picboxRecuaTituloMat.Margin = new System.Windows.Forms.Padding(4);
            this.picboxRecuaTituloMat.Name = "picboxRecuaTituloMat";
            this.picboxRecuaTituloMat.Size = new System.Drawing.Size(258, 58);
            this.picboxRecuaTituloMat.TabIndex = 29;
            this.picboxRecuaTituloMat.TabStop = false;
            this.picboxRecuaTituloMat.Visible = false;
            // 
            // picboxRecuadroGrande
            // 
            this.picboxRecuadroGrande.BackColor = System.Drawing.Color.SteelBlue;
            this.picboxRecuadroGrande.Location = new System.Drawing.Point(182, 338);
            this.picboxRecuadroGrande.Name = "picboxRecuadroGrande";
            this.picboxRecuadroGrande.Size = new System.Drawing.Size(100, 50);
            this.picboxRecuadroGrande.TabIndex = 19;
            this.picboxRecuadroGrande.TabStop = false;
            this.picboxRecuadroGrande.Visible = false;
            // 
            // picboxRecuadroTitulodgv
            // 
            this.picboxRecuadroTitulodgv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picboxRecuadroTitulodgv.Location = new System.Drawing.Point(1433, 127);
            this.picboxRecuadroTitulodgv.Name = "picboxRecuadroTitulodgv";
            this.picboxRecuadroTitulodgv.Size = new System.Drawing.Size(281, 63);
            this.picboxRecuadroTitulodgv.TabIndex = 11;
            this.picboxRecuadroTitulodgv.TabStop = false;
            // 
            // picboxRecuaMat
            // 
            this.picboxRecuaMat.BackColor = System.Drawing.Color.SteelBlue;
            this.picboxRecuaMat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picboxRecuaMat.Location = new System.Drawing.Point(721, 320);
            this.picboxRecuaMat.Name = "picboxRecuaMat";
            this.picboxRecuaMat.Size = new System.Drawing.Size(450, 68);
            this.picboxRecuaMat.TabIndex = 7;
            this.picboxRecuaMat.TabStop = false;
            this.picboxRecuaMat.Visible = false;
            // 
            // panRecuaIngreso
            // 
            this.panRecuaIngreso.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panRecuaIngreso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panRecuaIngreso.Location = new System.Drawing.Point(472, 146);
            this.panRecuaIngreso.Name = "panRecuaIngreso";
            this.panRecuaIngreso.Size = new System.Drawing.Size(200, 100);
            this.panRecuaIngreso.TabIndex = 39;
            this.panRecuaIngreso.Visible = false;
            // 
            // texFamiliaBuscar
            // 
            this.texFamiliaBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texFamiliaBuscar.Location = new System.Drawing.Point(752, 205);
            this.texFamiliaBuscar.Name = "texFamiliaBuscar";
            this.texFamiliaBuscar.Size = new System.Drawing.Size(400, 26);
            this.texFamiliaBuscar.TabIndex = 40;
            this.texFamiliaBuscar.Visible = false;
            // 
            // panFamiliasUnidades
            // 
            this.panFamiliasUnidades.BackColor = System.Drawing.Color.SteelBlue;
            this.panFamiliasUnidades.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panFamiliasUnidades.Location = new System.Drawing.Point(12, 524);
            this.panFamiliasUnidades.Name = "panFamiliasUnidades";
            this.panFamiliasUnidades.Size = new System.Drawing.Size(413, 136);
            this.panFamiliasUnidades.TabIndex = 42;
            this.panFamiliasUnidades.Visible = false;
            // 
            // botFamilias
            // 
            this.botFamilias.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botFamilias.Location = new System.Drawing.Point(13, 679);
            this.botFamilias.Name = "botFamilias";
            this.botFamilias.Size = new System.Drawing.Size(140, 55);
            this.botFamilias.TabIndex = 43;
            this.botFamilias.Text = "FAMILIAS";
            this.botFamilias.UseVisualStyleBackColor = true;
            this.botFamilias.Visible = false;
            this.botFamilias.Click += new System.EventHandler(this.botFamilias_Click);
            // 
            // botRegistros
            // 
            this.botRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botRegistros.Location = new System.Drawing.Point(159, 679);
            this.botRegistros.Name = "botRegistros";
            this.botRegistros.Size = new System.Drawing.Size(140, 55);
            this.botRegistros.TabIndex = 45;
            this.botRegistros.Text = "MATERIAL";
            this.botRegistros.UseVisualStyleBackColor = true;
            this.botRegistros.Visible = false;
            this.botRegistros.Click += new System.EventHandler(this.botRegistros_Click);
            // 
            // botIngresarTipo
            // 
            this.botIngresarTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botIngresarTipo.Location = new System.Drawing.Point(620, 564);
            this.botIngresarTipo.Name = "botIngresarTipo";
            this.botIngresarTipo.Size = new System.Drawing.Size(156, 44);
            this.botIngresarTipo.TabIndex = 47;
            this.botIngresarTipo.Text = "Ingresar";
            this.botIngresarTipo.UseVisualStyleBackColor = true;
            this.botIngresarTipo.Visible = false;
            this.botIngresarTipo.Click += new System.EventHandler(this.botIngresarTipo_Click);
            // 
            // botEliminarTipo
            // 
            this.botEliminarTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botEliminarTipo.Location = new System.Drawing.Point(801, 561);
            this.botEliminarTipo.Name = "botEliminarTipo";
            this.botEliminarTipo.Size = new System.Drawing.Size(156, 44);
            this.botEliminarTipo.TabIndex = 48;
            this.botEliminarTipo.Text = "Eliminar";
            this.botEliminarTipo.UseVisualStyleBackColor = true;
            this.botEliminarTipo.Visible = false;
            // 
            // listUbicaciones
            // 
            this.listUbicaciones.FormattingEnabled = true;
            this.listUbicaciones.Location = new System.Drawing.Point(12, 434);
            this.listUbicaciones.Name = "listUbicaciones";
            this.listUbicaciones.Size = new System.Drawing.Size(180, 30);
            this.listUbicaciones.TabIndex = 49;
            this.listUbicaciones.Visible = false;
            // 
            // labEliminarFamilia
            // 
            this.labEliminarFamilia.AutoSize = true;
            this.labEliminarFamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEliminarFamilia.ForeColor = System.Drawing.Color.Black;
            this.labEliminarFamilia.Location = new System.Drawing.Point(54, 176);
            this.labEliminarFamilia.Name = "labEliminarFamilia";
            this.labEliminarFamilia.Size = new System.Drawing.Size(196, 31);
            this.labEliminarFamilia.TabIndex = 50;
            this.labEliminarFamilia.Text = "Eliminar familia";
            this.labEliminarFamilia.Visible = false;
            // 
            // botInforma
            // 
            this.botInforma.BackColor = System.Drawing.Color.CadetBlue;
            this.botInforma.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botInforma.ForeColor = System.Drawing.Color.Black;
            this.botInforma.Location = new System.Drawing.Point(1185, 673);
            this.botInforma.Name = "botInforma";
            this.botInforma.Size = new System.Drawing.Size(160, 71);
            this.botInforma.TabIndex = 51;
            this.botInforma.Text = "INFORMACION";
            this.botInforma.UseVisualStyleBackColor = false;
            this.botInforma.Click += new System.EventHandler(this.botInforma_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage_1);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // botModificarFam
            // 
            this.botModificarFam.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botModificarFam.ForeColor = System.Drawing.SystemColors.ControlText;
            this.botModificarFam.Location = new System.Drawing.Point(757, 491);
            this.botModificarFam.Name = "botModificarFam";
            this.botModificarFam.Size = new System.Drawing.Size(131, 50);
            this.botModificarFam.TabIndex = 52;
            this.botModificarFam.Text = "Modificar";
            this.botModificarFam.UseVisualStyleBackColor = true;
            this.botModificarFam.Visible = false;
            this.botModificarFam.Click += new System.EventHandler(this.botModificarFam_Click);
            // 
            // frmMateriales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1840, 766);
            this.Controls.Add(this.botModificarFam);
            this.Controls.Add(this.botInforma);
            this.Controls.Add(this.labEliminarFamilia);
            this.Controls.Add(this.listUbicaciones);
            this.Controls.Add(this.botEliminarTipo);
            this.Controls.Add(this.botIngresarTipo);
            this.Controls.Add(this.botSalirFotoAgrandada);
            this.Controls.Add(this.botFamilias);
            this.Controls.Add(this.botRegistros);
            this.Controls.Add(this.picboxFotoMat);
            this.Controls.Add(this.labFamiliaMat);
            this.Controls.Add(this.texFamiliaBuscar);
            this.Controls.Add(this.botRegresa);
            this.Controls.Add(this.botModifica);
            this.Controls.Add(this.labListaFamilia2);
            this.Controls.Add(this.labListaFamilia1);
            this.Controls.Add(this.botEliminarFam);
            this.Controls.Add(this.labIngresoFamilia);
            this.Controls.Add(this.labModificaMat);
            this.Controls.Add(this.picboxRecuaTituloMat);
            this.Controls.Add(this.labTituloDgv);
            this.Controls.Add(this.labEliminaMat);
            this.Controls.Add(this.botCancelarFam);
            this.Controls.Add(this.botGuardarFam);
            this.Controls.Add(this.labIngresoMat);
            this.Controls.Add(this.texNuevoFam);
            this.Controls.Add(this.botCancelarMat);
            this.Controls.Add(this.picboxRecuadroGrande);
            this.Controls.Add(this.listFamiliaMat);
            this.Controls.Add(this.picboxRecuadroTitulodgv);
            this.Controls.Add(this.botIngresarFam);
            this.Controls.Add(this.picboxRecuaMat);
            this.Controls.Add(this.botImprimir);
            this.Controls.Add(this.botEliminar);
            this.Controls.Add(this.botBuscar);
            this.Controls.Add(this.botIngresar);
            this.Controls.Add(this.labTituloPrincipal);
            this.Controls.Add(this.botSalir);
            this.Controls.Add(this.panFamiliasUnidades);
            this.Controls.Add(this.panRecuaIngreso);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "frmMateriales";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMateriales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picboxFotoMat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaTituloMat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuadroGrande)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuadroTitulodgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxRecuaMat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label labIngresoMat;
        public System.Windows.Forms.Button botCancelarFam;
        public System.Windows.Forms.Label labTituloDgv;
        public System.Windows.Forms.PictureBox picboxFotoMat;
        public System.Windows.Forms.Button botSalir;
        public System.Windows.Forms.Button botIngresar;
        public System.Windows.Forms.Button botBuscar;
        public System.Windows.Forms.Button botEliminar;
        public System.Windows.Forms.Button botImprimir;
        public System.Windows.Forms.Button botModifica;
        public System.Windows.Forms.PictureBox picboxRecuadroTitulodgv;
        public System.Windows.Forms.ListBox listFamiliaMat;
        public System.Windows.Forms.Label labTituloPrincipal;
        public System.Windows.Forms.Button botIngresarFam;
        public System.Windows.Forms.Button botCancelarMat;
        public System.Windows.Forms.Label labFamiliaMat;
        public System.Windows.Forms.PictureBox picboxRecuaMat;
        public System.Windows.Forms.PictureBox picboxRecuadroGrande;
        public System.Windows.Forms.TextBox texNuevoFam;
        public System.Windows.Forms.Button botGuardarFam;
        public System.Windows.Forms.Label labModificaMat;
        public System.Windows.Forms.Label labEliminaMat;
        public System.Windows.Forms.PictureBox picboxRecuaTituloMat;
        public System.Windows.Forms.Label labIngresoFamilia;
        public System.Windows.Forms.Button botEliminarFam;
        public System.Windows.Forms.Label labListaFamilia1;
        public System.Windows.Forms.Label labListaFamilia2;
        public System.Windows.Forms.Button botSalirFotoAgrandada;
        public System.Windows.Forms.Panel panRecuaIngreso;
        public System.Windows.Forms.Button botFamilias;
        public System.Windows.Forms.Button botRegistros;
        public System.Windows.Forms.Panel panFamiliasUnidades;
        public System.Windows.Forms.Button botRegresa;
        private System.Windows.Forms.Button botIngresarTipo;
        private System.Windows.Forms.Button botEliminarTipo;
        private System.Windows.Forms.ListBox listUbicaciones;
        public System.Windows.Forms.Label labEliminarFamilia;
        public System.Windows.Forms.TextBox texFamiliaBuscar;
        public System.Windows.Forms.Button botInforma;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        public System.Windows.Forms.Button botModificarFam;
    }
}