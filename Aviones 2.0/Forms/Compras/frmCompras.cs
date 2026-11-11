using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;
using Aviones_2._0.Forms.Comun;
using Aviones_2._0.Forms.Materiales;

namespace Aviones_2._0.Forms.Compras
{
    /// <summary>
    /// El proceso de compras. Arriba, la lista de compras (pedidos pendientes); abajo, los materiales
    /// (todos o sólo el stock crítico), desde donde se piden. Cerrar pedido imprime y manda la lista de
    /// un vendedor; cuando llega, Recibir pedido deja corregir los datos y Pasar a stock lo ingresa todo junto.
    /// </summary>
    public class frmCompras : Form
    {
        private readonly CompraRepositorio compras = new CompraRepositorio();
        private readonly ConfiguracionRepositorio configuracion = new ConfiguracionRepositorio();
        private readonly MaterialRepositorio materiales = new MaterialRepositorio();
        private readonly LoteRepositorio lotes = new LoteRepositorio();
        private readonly ListaRepositorio vendedores = ListaRepositorio.Vendedores();

        /// <summary>Filtro de la lista de compras: todos los vendedores, o los pedidos sin vendedor.</summary>
        private const int TodosLosVendedores = -1, SinVendedor = 0;
        /// <summary>Filtro de la lista de compras por estado.</summary>
        private const int MostrarTodos = 0, MostrarAbiertos = 1, MostrarCerrados = 2;
        /// <summary>Naranja oscuro: se lee bien sobre el fondo verde claro.</summary>
        private static readonly Color YaPedido = Color.FromArgb(200, 85, 0);
        private static readonly Color Fondo = Color.Honeydew;

        private List<PedidoCompra> pendientes = new List<PedidoCompra>();
        private List<StockCritico> todosLosMateriales = new List<StockCritico>();
        private decimal minimoGeneral;
        private bool soloCriticos;
        private bool cargando;
        private Font cursiva;
        /// <summary>El material cuya foto se está mostrando en cada caja, para no volver a leerla.</summary>
        private int? fotoArriba, fotoAbajo;

        /// <summary>El pedido que se está recibiendo; null fuera de la recepción.</summary>
        private BindingList<FilaRecepcion> recepcion;
        /// <summary>"de HobbyKing" o "sin vendedor", para los textos de la recepción.</summary>
        private string vendedorRecepcion;

        private readonly TableLayoutPanel tlp = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 6, Padding = new Padding(16, 6, 16, 10) };
        private readonly Label labListado = new Label { Text = "Listado de compras (pedidos pendientes)", AutoSize = true, Font = new Font("Arial", 11F, FontStyle.Bold), Margin = new Padding(3, 8, 24, 3) };
        private readonly ComboBox cboVendedor = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 200, Margin = new Padding(3, 4, 20, 3) };
        private readonly ComboBox cboMostrar = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 190, Margin = new Padding(3, 4, 20, 3) };
        private readonly Label labTotalPedidos = new Label { AutoSize = true, Font = new Font("Arial", 10F, FontStyle.Bold), Margin = new Padding(10, 8, 3, 3) };
        private readonly DataGridView dgvPedidos = new DataGridView { Dock = DockStyle.Fill };
        private readonly DataGridView dgvRecepcion = new DataGridView { Dock = DockStyle.Fill, Visible = false };
        private readonly PictureBox picArriba = Foto();
        private readonly Label labMateriales = new Label { AutoSize = true, Font = new Font("Arial", 11F, FontStyle.Bold), Margin = new Padding(3, 8, 24, 3) };
        private readonly TextBox txtBuscar = new TextBox { Width = 220, Margin = new Padding(3, 5, 20, 3) };
        private readonly FlowLayoutPanel flpMinimo = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = new Padding(0) };
        private readonly TextBox txtMinimo = new TextBox { Width = 50, TextAlign = HorizontalAlignment.Right, Margin = new Padding(3, 5, 3, 3) };
        private readonly Button botAplicarMinimo = new Button { Text = "Aplicar", AutoSize = true, Margin = new Padding(6, 3, 20, 3) };
        private readonly Label labResumenMateriales = new Label { AutoSize = true, Font = new Font("Arial", 10F, FontStyle.Bold), Margin = new Padding(3, 8, 3, 3) };
        private readonly DataGridView dgvMateriales = new DataGridView { Dock = DockStyle.Fill };
        private readonly PictureBox picAbajo = Foto();

        private readonly FlowLayoutPanel flpStock = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Anchor = AnchorStyles.Left | AnchorStyles.Bottom, Margin = new Padding(0, 0, 30, 0) };
        private readonly Button botPedir = Boton("AGREGAR AL PEDIDO", 175);
        private readonly Button botStockGeneral = Boton("STOCK GENERAL", 145);
        private readonly Button botStockCritico = Boton("STOCK CRÍTICO", 145);
        private readonly Button botNuevoMaterial = Boton("AGREGAR MATERIAL SIN STOCK", 245);
        private readonly GroupBox grpPedidos = new GroupBox { Text = "Pedidos", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Font = new Font("Arial", 11F, FontStyle.Bold), Padding = new Padding(8, 4, 8, 6) };
        private readonly Button botModificar = Boton("MODIFICAR", 115);
        private readonly Button botEliminar = Boton("ELIMINAR", 110);
        private readonly Button botImprimir = Boton("IMPRIMIR", 110);
        private readonly Button botCerrar = Boton("CERRAR PEDIDO", 145);
        private readonly Button botRecibir = Boton("RECIBIR PEDIDO", 145);
        private readonly FlowLayoutPanel flpRecepcion = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Visible = false, Anchor = AnchorStyles.Left | AnchorStyles.Bottom };
        private readonly Button botPasarAStock = Boton("PASAR A STOCK", 190);
        private readonly Button botCancelarRecepcion = Boton("CANCELAR", 140);
        private readonly Button botSalir = Boton("SALIR", 130);

        public frmCompras()
        {
            Text = "Compras";
            Font = new Font("Arial", 10F);
            BackColor = Fondo;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;

            ArmarGrillas();
            ArmarPantalla();
            Estilo.Botones(botPedir, botStockGeneral, botStockCritico, botNuevoMaterial, botModificar, botEliminar, botImprimir, botCerrar, botRecibir, botPasarAStock);
            Estilo.BotonSecundario(botCancelarRecepcion);
            Estilo.BotonSecundario(botSalir);

            cboMostrar.DataSource = new List<ElementoLista>
            {
                new ElementoLista { Id = MostrarTodos, Nombre = "Todos" },
                new ElementoLista { Id = MostrarAbiertos, Nombre = "Abiertos (por enviar)" },
                new ElementoLista { Id = MostrarCerrados, Nombre = "Cerrados (enviados)" }
            };
            cboMostrar.DisplayMember = nameof(ElementoLista.Nombre);
            cboMostrar.ValueMember = nameof(ElementoLista.Id);

            cboVendedor.SelectedIndexChanged += (s, e) => { if (!cargando) MostrarPedidos(null); };
            cboMostrar.SelectedIndexChanged += (s, e) => { if (!cargando) MostrarPedidos(null); };
            txtBuscar.TextChanged += (s, e) => MostrarMateriales(null);
            txtMinimo.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; AplicarMinimo(); } };
            botAplicarMinimo.Click += (s, e) => AplicarMinimo();
            botPedir.Click += (s, e) => Pedir();
            botStockGeneral.Click += (s, e) => CambiarVista(false);
            botStockCritico.Click += (s, e) => CambiarVista(true);
            botNuevoMaterial.Click += (s, e) => NuevoMaterial();
            botModificar.Click += (s, e) => Modificar();
            botEliminar.Click += (s, e) => Eliminar();
            botImprimir.Click += (s, e) => Imprimir();
            botCerrar.Click += (s, e) => CerrarPedido();
            botRecibir.Click += (s, e) => RecibirPedido();
            botPasarAStock.Click += (s, e) => PasarAStock();
            botCancelarRecepcion.Click += (s, e) => SalirDeRecepcion();
            botSalir.Click += (s, e) => Close();
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (recepcion != null) SalirDeRecepcion();
                    else Close();
                }
            };
            Load += (s, e) => Cargar();
        }

        #region Armado de la pantalla

        private static PictureBox Foto() => new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.Zoom,
            Cursor = Cursors.Hand,
            Margin = new Padding(16, 0, 0, 0)
        };

        private static Button Boton(string texto, int ancho) => new Button
        {
            Text = texto,
            Size = new Size(ancho, 40),
            Margin = new Padding(0, 0, 8, 0)
        };

        private static DataGridViewTextBoxColumn Columna(string propiedad, string titulo, float peso, string formato = null, bool soloLectura = true)
        {
            var c = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propiedad, HeaderText = titulo, FillWeight = peso, Name = "col" + propiedad, ReadOnly = soloLectura
            };
            if (formato != null)
            {
                c.DefaultCellStyle.Format = formato;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            return c;
        }

        private static DataGridViewComboBoxColumn Lista(string propiedad, string titulo, float peso, object opciones)
        {
            return new DataGridViewComboBoxColumn
            {
                DataPropertyName = propiedad, HeaderText = titulo, FillWeight = peso, Name = "col" + propiedad,
                DataSource = opciones,
                ValueMember = opciones is List<ElementoLista> ? nameof(ElementoLista.Id) : null,
                DisplayMember = opciones is List<ElementoLista> ? nameof(ElementoLista.Nombre) : null,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                FlatStyle = FlatStyle.Flat
            };
        }

        private void ArmarGrillas()
        {
            Estilo.Grilla(dgvPedidos);
            dgvPedidos.BackgroundColor = Fondo;
            dgvPedidos.Columns.AddRange(
                Columna(nameof(PedidoCompra.Familia), "Familia", 90),
                Columna(nameof(PedidoCompra.Grupo), "Grupo", 100),
                Columna(nameof(PedidoCompra.Caracteristica), "Característica", 130),
                Columna(nameof(PedidoCompra.Color), "Color", 70),
                Columna(nameof(PedidoCompra.Medidas), "Medidas", 90),
                Columna(nameof(PedidoCompra.Codigo), "Código", 70),
                Columna(nameof(PedidoCompra.Cantidad), "Cant. a comprar", 60, "0.###"),
                Columna(nameof(PedidoCompra.TipoUnidad), "Unidad", 60),
                Columna(nameof(PedidoCompra.Precio), "Valor unitario", 65, "N2"),
                Columna(nameof(PedidoCompra.Vendedor), "Vendedor", 100),
                Columna(nameof(PedidoCompra.EstadoEnvio), "Estado", 80));
            dgvPedidos.Columns["col" + nameof(PedidoCompra.Cantidad)].DefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
            dgvPedidos.SelectionChanged += (s, e) => { ActualizarBotones(); MostrarFotoArriba(); };
            dgvPedidos.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Modificar(); };
            dgvPedidos.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.Handled = true; Modificar(); }
                else if (e.KeyCode == Keys.Delete) { e.Handled = true; Eliminar(); }
            };

            Estilo.Grilla(dgvMateriales);
            dgvMateriales.BackgroundColor = Fondo;
            dgvMateriales.Columns.AddRange(
                Columna(nameof(StockCritico.Familia), "Familia", 90),
                Columna(nameof(StockCritico.Grupo), "Grupo", 100),
                Columna(nameof(StockCritico.Caracteristica), "Característica", 130),
                Columna(nameof(StockCritico.Color), "Color", 70),
                Columna(nameof(StockCritico.Medidas), "Medidas", 90),
                Columna(nameof(StockCritico.Codigo), "Código", 70),
                Columna(nameof(StockCritico.Disponible), "Cantidad", 55, "0.###"),
                Columna(nameof(StockCritico.TipoUnidad), "Unidad", 60),
                Columna(nameof(StockCritico.UltimoPrecio), "Valor unitario", 65, "N2"),
                Columna(nameof(StockCritico.MinimoAplicado), "Mínimo", 50, "0.###"),
                Columna(nameof(StockCritico.PedidoPendiente), "Ya pedido", 55, "0.###"));
            dgvMateriales.Columns["col" + nameof(StockCritico.Disponible)].DefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
            dgvMateriales.SelectionChanged += (s, e) => { ActualizarBotones(); MostrarFotoAbajo(); };
            dgvMateriales.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Pedir(); };
            dgvMateriales.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.Handled = true; Pedir(); } };
            dgvMateriales.CellFormatting += dgvMateriales_CellFormatting;

            Estilo.Grilla(dgvRecepcion);
            dgvRecepcion.BackgroundColor = Fondo;
            dgvRecepcion.ReadOnly = false;
            dgvRecepcion.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvRecepcion.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvRecepcion.DataError += (s, e) =>
            {
                e.ThrowException = false;
                e.Cancel = true;
                Mensajes.Aviso(this, "Escriba un número (por ejemplo 12 o 3,5).", "Recibir pedido");
            };
            dgvRecepcion.SelectionChanged += (s, e) => MostrarFotoArriba();

            OrdenGrilla.Habilitar(dgvPedidos, dgvMateriales);
        }

        /// <summary>Las columnas de la recepción: los datos del material y del lote se pueden cambiar.</summary>
        private void ArmarColumnasRecepcion()
        {
            dgvRecepcion.Columns.Clear();
            dgvRecepcion.AutoGenerateColumns = false;
            var familias = new FamiliaRepositorio().Listar().Select(f => new ElementoLista { Id = f.IdFamilia, Nombre = f.Nombre }).ToList();
            var fabricantes = ConNinguno(ListaRepositorio.Fabricantes().Listar());
            var ubicaciones = ConNinguno(ListaRepositorio.Ubicaciones().Listar());
            dgvRecepcion.Columns.AddRange(
                Lista(nameof(FilaRecepcion.IdFamilia), "Familia", 100, familias),
                Columna(nameof(FilaRecepcion.Grupo), "Grupo", 100, soloLectura: false),
                Columna(nameof(FilaRecepcion.Caracteristica), "Característica", 120, soloLectura: false),
                Columna(nameof(FilaRecepcion.Color), "Color", 70, soloLectura: false),
                Columna(nameof(FilaRecepcion.Medidas), "Medidas", 85, soloLectura: false),
                Columna(nameof(FilaRecepcion.Codigo), "Código", 70, soloLectura: false),
                Columna(nameof(FilaRecepcion.Pedido), "Pedido", 50, "0.###"),
                Columna(nameof(FilaRecepcion.Llego), "Llegó", 50, "0.###", soloLectura: false),
                Columna(nameof(FilaRecepcion.TipoUnidad), "Unidad", 60),
                Columna(nameof(FilaRecepcion.ValorUnitario), "Valor unitario", 65, "N2", soloLectura: false),
                Lista(nameof(FilaRecepcion.IdFabricante), "Fabricante", 100, fabricantes),
                Lista(nameof(FilaRecepcion.IdUbicacion), "Ubicación", 100, ubicaciones),
                Lista(nameof(FilaRecepcion.Compra), "Se compró", 65,
                    new List<string> { EstadosMaterial.Nombre(EstadosMaterial.Nuevo), EstadosMaterial.Nombre(EstadosMaterial.Usado) }));
            // Lo que se puede cambiar va en blanco; lo pedido y la unidad, en el gris de siempre
            foreach (DataGridViewColumn c in dgvRecepcion.Columns)
            {
                if (!c.ReadOnly)
                {
                    c.DefaultCellStyle.BackColor = Color.White;
                }
            }
            dgvRecepcion.Columns["col" + nameof(FilaRecepcion.Llego)].DefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
        }

        private static List<ElementoLista> ConNinguno(List<ElementoLista> lista)
        {
            lista.Insert(0, new ElementoLista { Id = 0, Nombre = "" });
            return lista;
        }

        private void ArmarPantalla()
        {
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // título
            tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // filtros de la lista de compras
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));   // lista de compras
            tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // encabezado de materiales
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));   // materiales
            tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // botones

            var titulo = new Label
            {
                Text = "Compras", AutoSize = true, Anchor = AnchorStyles.Top,
                Font = new Font("Arial", 20F, FontStyle.Bold | FontStyle.Underline), Margin = new Padding(3, 6, 3, 6)
            };
            tlp.Controls.Add(titulo, 0, 0);
            tlp.SetColumnSpan(titulo, 2);

            var filtros = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 4) };
            filtros.Controls.Add(labListado);
            filtros.Controls.Add(Etiqueta("Vendedor:"));
            filtros.Controls.Add(cboVendedor);
            filtros.Controls.Add(Etiqueta("Mostrar:"));
            filtros.Controls.Add(cboMostrar);
            filtros.Controls.Add(labTotalPedidos);
            tlp.Controls.Add(filtros, 0, 1);
            tlp.SetColumnSpan(filtros, 2);

            var celdaPedidos = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
            celdaPedidos.Controls.Add(dgvPedidos);
            celdaPedidos.Controls.Add(dgvRecepcion);
            tlp.Controls.Add(celdaPedidos, 0, 2);
            tlp.Controls.Add(picArriba, 1, 2);

            flpMinimo.Controls.Add(Etiqueta("Avisar cuando quede"));
            flpMinimo.Controls.Add(txtMinimo);
            flpMinimo.Controls.Add(Etiqueta("o menos (para los materiales sin mínimo propio)"));
            flpMinimo.Controls.Add(botAplicarMinimo);
            var encabezado = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Dock = DockStyle.Fill, Margin = new Padding(0, 14, 0, 4) };
            encabezado.Controls.Add(labMateriales);
            encabezado.Controls.Add(Etiqueta("Buscar:"));
            encabezado.Controls.Add(txtBuscar);
            encabezado.Controls.Add(flpMinimo);
            encabezado.Controls.Add(labResumenMateriales);
            tlp.Controls.Add(encabezado, 0, 3);
            tlp.SetColumnSpan(encabezado, 2);

            tlp.Controls.Add(dgvMateriales, 0, 4);
            tlp.Controls.Add(picAbajo, 1, 4);

            flpStock.Controls.Add(botPedir);
            flpStock.Controls.Add(botStockGeneral);
            flpStock.Controls.Add(botStockCritico);
            flpStock.Controls.Add(botNuevoMaterial);

            var flpPedidos = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 0) };
            foreach (var b in new[] { botModificar, botEliminar, botImprimir, botCerrar, botRecibir })
            {
                flpPedidos.Controls.Add(b);
            }
            grpPedidos.Controls.Add(flpPedidos);

            flpRecepcion.Controls.Add(botPasarAStock);
            flpRecepcion.Controls.Add(botCancelarRecepcion);

            var abajo = new TableLayoutPanel { AutoSize = true, Dock = DockStyle.Fill, ColumnCount = 2, Margin = new Padding(0, 12, 0, 0) };
            abajo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            abajo.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            var izquierda = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Dock = DockStyle.Fill, Margin = new Padding(0) };
            izquierda.Controls.Add(flpStock);
            izquierda.Controls.Add(grpPedidos);
            izquierda.Controls.Add(flpRecepcion);
            abajo.Controls.Add(izquierda, 0, 0);
            botSalir.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            botSalir.Margin = new Padding(0);
            abajo.Controls.Add(botSalir, 1, 0);
            tlp.Controls.Add(abajo, 0, 5);
            tlp.SetColumnSpan(abajo, 2);

            Controls.Add(tlp);

            picArriba.DoubleClick += (s, e) => Imagenes.Ampliar(this, picArriba.Image, "Foto del material");
            picAbajo.DoubleClick += (s, e) => Imagenes.Ampliar(this, picAbajo.Image, MaterialSeleccionado()?.Descripcion);
        }

        private static Label Etiqueta(string texto) => new Label { Text = texto, AutoSize = true, Margin = new Padding(3, 8, 3, 3) };

        #endregion

        #region Carga

        private void Cargar()
        {
            Mensajes.Intentar(this, () =>
            {
                minimoGeneral = configuracion.ObtenerStockMinimoGeneral();
                txtMinimo.Text = Numeros.Cantidad(minimoGeneral);
                CargarVendedores();
                CargarPedidos(null);
                CambiarVista(false);
            });
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Al mostrarse, las grillas eligen solas la primera fila: se dejan sin ninguna elegida
            foreach (var dgv in new[] { dgvPedidos, dgvMateriales })
            {
                dgv.ClearSelection();
                dgv.CurrentCell = null;
            }
            ActualizarBotones();
        }

        /// <summary>El filtro por vendedor: (Todos), (Sin vendedor) y cada vendedor. Conserva lo elegido.</summary>
        private void CargarVendedores()
        {
            int actual = cboVendedor.SelectedValue is int v ? v : TodosLosVendedores;
            var opciones = new List<ElementoLista>
            {
                new ElementoLista { Id = TodosLosVendedores, Nombre = "(Todos)" },
                new ElementoLista { Id = SinVendedor, Nombre = "(Sin vendedor)" }
            };
            opciones.AddRange(vendedores.Listar());
            cargando = true;
            try
            {
                cboVendedor.DisplayMember = nameof(ElementoLista.Nombre);
                cboVendedor.ValueMember = nameof(ElementoLista.Id);
                cboVendedor.DataSource = opciones;
                cboVendedor.SelectedValue = opciones.Any(o => o.Id == actual) ? actual : TodosLosVendedores;
            }
            finally
            {
                cargando = false;
            }
        }

        private void CargarPedidos(int? seleccionar)
        {
            pendientes = compras.ListarPendientes();
            MostrarPedidos(seleccionar);
        }

        private void MostrarPedidos(int? seleccionar)
        {
            int? id = seleccionar ?? PedidoSeleccionado()?.IdPedido;
            int vendedor = cboVendedor.SelectedValue is int v ? v : TodosLosVendedores;
            int mostrar = cboMostrar.SelectedValue is int m ? m : MostrarTodos;
            var visibles = pendientes
                .Where(p => vendedor == TodosLosVendedores || (p.IdVendedor ?? SinVendedor) == vendedor)
                .Where(p => mostrar == MostrarTodos || p.Enviado == (mostrar == MostrarCerrados))
                .ToList();
            dgvPedidos.DataSource = OrdenGrilla.Aplicar(dgvPedidos, visibles);
            Seleccionar(dgvPedidos, r => ((PedidoCompra)r.DataBoundItem).IdPedido == id);

            decimal total = visibles.Sum(p => p.CostoEstimado ?? 0m);
            labTotalPedidos.Text = pendientes.Count == 0
                ? "No hay nada pendiente de comprar."
                : visibles.Count == 0
                    ? "No hay pedidos con este filtro."
                    : $"{visibles.Count} pedidos   ·   Costo estimado: $ {Numeros.Dinero(total)}"
                      + (visibles.Any(p => !p.Precio.HasValue) ? " (algunos sin precio)" : "");
            ActualizarBotones();
            MostrarFotoArriba();
        }

        private void CargarMateriales(int? seleccionar)
        {
            todosLosMateriales = compras.ListarParaComprar(minimoGeneral);
            MostrarMateriales(seleccionar);
        }

        private void MostrarMateriales(int? seleccionar)
        {
            int? id = seleccionar ?? MaterialSeleccionado()?.IdMaterial;
            string texto = txtBuscar.Text.Trim();
            var visibles = todosLosMateriales
                .Where(m => !soloCriticos || m.EsCritico)
                .Where(m => texto.Length == 0 || Contiene(m.Familia, texto) || Contiene(m.Descripcion, texto) || Contiene(m.Codigo, texto))
                .ToList();
            dgvMateriales.DataSource = OrdenGrilla.Aplicar(dgvMateriales, visibles);
            Seleccionar(dgvMateriales, r => ((StockCritico)r.DataBoundItem).IdMaterial == id);

            if (soloCriticos)
            {
                int criticos = todosLosMateriales.Count(m => m.EsCritico);
                labResumenMateriales.Text = criticos == 0
                    ? "Ningún material está en su mínimo."
                    : $"{criticos} en su mínimo o menos   ·   {todosLosMateriales.Count(m => m.EsCritico && !m.TienePedido)} sin pedir";
            }
            else
            {
                labResumenMateriales.Text = $"{visibles.Count} materiales";
            }
            ActualizarBotones();
            MostrarFotoAbajo();
        }

        private static bool Contiene(string texto, string buscado) =>
            (texto ?? "").IndexOf(buscado, StringComparison.CurrentCultureIgnoreCase) >= 0;

        private void Recargar(int? idPedido, int? idMaterial = null)
        {
            Mensajes.Intentar(this, () =>
            {
                CargarVendedores();   // desde un pedido se pueden agregar vendedores
                CargarPedidos(idPedido);
                CargarMateriales(idMaterial);
            });
        }

        private static void Seleccionar(DataGridView dgv, Func<DataGridViewRow, bool> condicion)
        {
            dgv.ClearSelection();
            dgv.CurrentCell = null;
            var fila = dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault(condicion);
            if (fila != null)
            {
                dgv.CurrentCell = fila.Cells.Cast<DataGridViewCell>().First(c => c.Visible);
                fila.Selected = true;
            }
        }

        private PedidoCompra PedidoSeleccionado() =>
            dgvPedidos.SelectedRows.Count > 0 ? dgvPedidos.SelectedRows[0].DataBoundItem as PedidoCompra : null;

        private StockCritico MaterialSeleccionado() =>
            dgvMateriales.SelectedRows.Count > 0 ? dgvMateriales.SelectedRows[0].DataBoundItem as StockCritico : null;

        private void ActualizarBotones()
        {
            var pedido = PedidoSeleccionado();
            // Sin nada en la lista de compras, el recuadro Pedidos queda apagado
            grpPedidos.Enabled = pendientes.Count > 0;
            botModificar.Enabled = pedido != null && !pedido.Enviado;
            botEliminar.Enabled = pedido != null;
            botImprimir.Enabled = dgvPedidos.Rows.Count > 0;
            botCerrar.Enabled = pendientes.Any(p => !p.Enviado);
            botRecibir.Enabled = pendientes.Any(p => p.Enviado);
            botPedir.Enabled = MaterialSeleccionado() != null;
        }

        private void MostrarFotoArriba()
        {
            int? id = recepcion != null
                ? (dgvRecepcion.CurrentRow?.DataBoundItem as FilaRecepcion)?.IdMaterial
                : PedidoSeleccionado()?.IdMaterial;
            MostrarFoto(picArriba, id, ref fotoArriba);
        }

        private void MostrarFotoAbajo() => MostrarFoto(picAbajo, MaterialSeleccionado()?.IdMaterial, ref fotoAbajo);

        private void MostrarFoto(PictureBox caja, int? idMaterial, ref int? mostrado)
        {
            if (idMaterial == mostrado)
            {
                return;
            }
            mostrado = idMaterial;
            Image foto = null;
            if (idMaterial.HasValue)
            {
                Mensajes.Intentar(this, () => foto = Imagenes.DesdeBytes(materiales.ObtenerImagen(idMaterial.Value)));
            }
            Imagenes.Mostrar(caja, foto);
        }

        #endregion

        #region Materiales (abajo)

        /// <summary>Stock general (todos los materiales) o stock crítico (los que llegaron a su mínimo).</summary>
        private void CambiarVista(bool criticos)
        {
            soloCriticos = criticos;
            labMateriales.Text = criticos ? "Stock crítico" : "Stock general";
            flpMinimo.Visible = criticos;
            dgvMateriales.Columns["col" + nameof(StockCritico.MinimoAplicado)].Visible = criticos;
            // El botón de la vista que se está viendo queda marcado (oscuro)
            botStockGeneral.BackColor = criticos ? Estilo.Boton : Estilo.BotonSalir;
            botStockCritico.BackColor = criticos ? Estilo.BotonSalir : Estilo.Boton;
            Mensajes.Intentar(this, () => CargarMateriales(null));
        }

        private void dgvMateriales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !(dgvMateriales.Rows[e.RowIndex].DataBoundItem is StockCritico m))
            {
                return;
            }
            // Lo ya pedido se distingue en naranja; lo que no se repone, en cursiva
            if (m.TienePedido)
            {
                e.CellStyle.ForeColor = YaPedido;
            }
            if (!m.SeRepone)
            {
                cursiva = cursiva ?? new Font(dgvMateriales.DefaultCellStyle.Font, FontStyle.Italic);
                e.CellStyle.Font = cursiva;
            }
            if (e.ColumnIndex == dgvMateriales.Columns["col" + nameof(StockCritico.PedidoPendiente)].Index && !m.TienePedido)
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void Pedir()
        {
            var m = MaterialSeleccionado();
            if (m == null)
            {
                return;
            }
            if (!m.SeRepone && !Mensajes.Confirmar(this,
                    "Este material está marcado como que no se repone (no se vuelve a comprar).\n\n¿Agregarlo a la lista de compras igual?", "Comprar"))
            {
                return;
            }
            PedirMaterial(m.IdMaterial);
        }

        private void PedirMaterial(int idMaterial)
        {
            using (var dialogo = new frmPedido(idMaterial))
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    Recargar(null, idMaterial);
                }
            }
        }

        /// <summary>Un material que todavía no está cargado: se crea sin stock y se pide.</summary>
        private void NuevoMaterial()
        {
            int idMaterial;
            using (var editor = new frmMaterialEditor(null, null) { SinPrimerIngreso = true })
            {
                if (editor.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                idMaterial = editor.IdMaterial;
            }
            Recargar(null, idMaterial);
            PedirMaterial(idMaterial);
        }

        private void AplicarMinimo()
        {
            if (!Numeros.TryLeer(txtMinimo.Text, out decimal minimo) || minimo < 0)
            {
                Mensajes.Aviso(this, "Escriba un número (0 o más).");
                txtMinimo.Focus();
                return;
            }
            Mensajes.Intentar(this, () =>
            {
                configuracion.GuardarStockMinimoGeneral(minimo);
                minimoGeneral = minimo;
                CargarMateriales(null);
            });
        }

        #endregion

        #region Pedidos (arriba)

        private void Modificar()
        {
            var pedido = PedidoSeleccionado();
            if (pedido == null)
            {
                return;
            }
            if (pedido.Enviado)
            {
                Mensajes.Aviso(this, "Este pedido ya se envió al vendedor y no se puede cambiar.\nSi se canceló la compra, se puede ELIMINAR.", "Modificar");
                return;
            }
            using (var dialogo = new frmPedido(pedido))
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    Recargar(pedido.IdPedido);
                }
            }
        }

        private void Eliminar()
        {
            var pedido = PedidoSeleccionado();
            if (pedido != null
                && Mensajes.Confirmar(this, $"¿Sacar de la lista de compras {Numeros.Cantidad(pedido.Cantidad)} {pedido.TipoUnidad} de {pedido.Material}?", "Eliminar pedido")
                && Mensajes.Intentar(this, () => compras.EliminarPedido(pedido.IdPedido)))
            {
                Recargar(null);
            }
        }

        private void Imprimir()
        {
            // Filtrada por vendedor, se imprime la lista de ese vendedor
            string titulo = cboVendedor.SelectedValue is int v && v != TodosLosVendedores
                ? $"Lista de compras: {cboVendedor.Text}"
                : "Lista de compras";
            ImpresionGrilla.VistaPrevia(this, dgvPedidos, titulo, labTotalPedidos.Text);
        }

        /// <summary>
        /// Elige el vendedor de los pedidos dados: si son todos del mismo, ése; si no, muestra la lista
        /// de vendedores. Devuelve el Id (0 sin vendedor) y el nombre, o null si se canceló.
        /// </summary>
        private (int Id, string Nombre)? ElegirVendedor(List<PedidoCompra> pedidos, string titulo, string pregunta)
        {
            var grupos = pedidos
                .GroupBy(p => p.IdVendedor ?? SinVendedor)
                .Select(g => (Id: g.Key, Nombre: g.First().Vendedor ?? "(Sin vendedor)", Cantidad: g.Count()))
                .OrderBy(g => g.Id == SinVendedor).ThenBy(g => g.Nombre)
                .ToList();
            if (grupos.Count == 1)
            {
                return (grupos[0].Id, grupos[0].Nombre);
            }
            var opciones = grupos.Select(g => $"{g.Nombre}   ({g.Cantidad} {(g.Cantidad == 1 ? "material" : "materiales")})").ToList();
            using (var lista = new frmElegirDeLista(titulo, pregunta, opciones))
            {
                if (lista.ShowDialog(this) != DialogResult.OK)
                {
                    return null;
                }
                var g = grupos[lista.Elegido];
                return (g.Id, g.Nombre);
            }
        }

        /// <summary>Deja la lista de compras mostrando un vendedor y un estado.</summary>
        private void Filtrar(int vendedor, int mostrar)
        {
            cargando = true;
            cboVendedor.SelectedValue = vendedor;
            cboMostrar.SelectedValue = mostrar;
            cargando = false;
            MostrarPedidos(null);
        }

        /// <summary>
        /// Cerrar el pedido de un vendedor: se imprime lo que todavía no se le mandó y queda enviado.
        /// Recién entonces se puede recibir.
        /// </summary>
        private void CerrarPedido()
        {
            var porEnviar = pendientes.Where(p => !p.Enviado).ToList();
            var vendedor = ElegirVendedor(porEnviar, "Lista de vendedores", "¿A qué vendedor le va a hacer el pedido?");
            if (vendedor == null)
            {
                return;
            }
            var (id, nombre) = vendedor.Value;
            var delVendedor = porEnviar.Where(p => (p.IdVendedor ?? SinVendedor) == id).ToList();
            string aQuien = id == SinVendedor ? "sin vendedor" : nombre;

            // En la lista queda sólo lo que se le va a mandar, y eso se imprime
            Filtrar(id, MostrarAbiertos);
            decimal total = delVendedor.Sum(p => p.CostoEstimado ?? 0m);
            bool impreso = ImpresionGrilla.VistaPrevia(this, dgvPedidos, $"Pedido a {aQuien} ({DateTime.Today:dd/MM/yyyy})",
                $"{delVendedor.Count} materiales   ·   Costo estimado: $ {Numeros.Dinero(total)}");

            string pregunta = impreso
                ? $"¿Cerrar el pedido a {aQuien}?\n\nLos {delVendedor.Count} materiales quedan como enviados y se reciben cuando lleguen."
                : $"No se imprimió.\n\n¿Cerrar igual el pedido a {aQuien}? (por ejemplo, si lo mandó por mail)";
            if (Mensajes.Confirmar(this, pregunta, "Cerrar pedido")
                && Mensajes.Intentar(this, () => compras.EnviarPedidos(delVendedor.Select(p => p.IdPedido))))
            {
                Filtrar(TodosLosVendedores, MostrarTodos);
                Recargar(null);
            }
        }

        #endregion

        #region Recepción

        /// <summary>
        /// Llegó un pedido: se elige el vendedor, la lista muestra sólo lo que se le compró y se puede
        /// corregir todo (los datos del material, lo que llegó, el precio, de dónde y adónde va).
        /// </summary>
        private void RecibirPedido()
        {
            var enviados = pendientes.Where(p => p.Enviado).ToList();
            var vendedor = ElegirVendedor(enviados, "Lista de vendedores", "¿De qué vendedor llegó el pedido?");
            if (vendedor == null)
            {
                return;
            }
            var (id, nombre) = vendedor.Value;
            var delVendedor = enviados.Where(p => (p.IdVendedor ?? SinVendedor) == id).ToList();

            List<FilaRecepcion> filas = null;
            if (!Mensajes.Intentar(this, () =>
                {
                    ArmarColumnasRecepcion();
                    filas = delVendedor
                        .Select(p => new FilaRecepcion(p, materiales.Obtener(p.IdMaterial), lotes.ListarPorMaterial(p.IdMaterial).LastOrDefault()))
                        .ToList();
                }))
            {
                return;
            }
            recepcion = new BindingList<FilaRecepcion>(filas);
            vendedorRecepcion = id == SinVendedor ? "sin vendedor" : "de " + nombre;
            dgvRecepcion.DataSource = recepcion;
            ModoRecepcion(true);
        }

        private void ModoRecepcion(bool recibiendo)
        {
            SuspendLayout();
            dgvRecepcion.Visible = recibiendo;
            dgvPedidos.Visible = !recibiendo;
            cboVendedor.Enabled = !recibiendo;
            cboMostrar.Enabled = !recibiendo;
            labListado.Text = recibiendo
                ? $"Recibir el pedido {vendedorRecepcion}: corrija lo que haga falta y toque PASAR A STOCK"
                : "Listado de compras (pedidos pendientes)";
            labTotalPedidos.Visible = !recibiendo;

            // Mientras se recibe sólo se ve el pedido: los materiales de abajo y sus botones se esconden
            foreach (Control c in new Control[] { dgvMateriales, picAbajo, labMateriales.Parent })
            {
                c.Visible = !recibiendo;
            }
            tlp.RowStyles[2] = new RowStyle(SizeType.Percent, recibiendo ? 100F : 40F);
            tlp.RowStyles[4] = recibiendo ? new RowStyle(SizeType.Absolute, 0F) : new RowStyle(SizeType.Percent, 60F);
            flpStock.Visible = !recibiendo;
            grpPedidos.Visible = !recibiendo;
            flpRecepcion.Visible = recibiendo;
            ResumeLayout();

            if (!recibiendo)
            {
                recepcion = null;
                dgvRecepcion.DataSource = null;
            }
            fotoArriba = -1;   // la caja de arriba muestra el de la otra lista
            MostrarFotoArriba();
        }

        private void SalirDeRecepcion()
        {
            dgvRecepcion.CancelEdit();
            ModoRecepcion(false);
            Recargar(null);
        }

        private void PasarAStock()
        {
            if (!dgvRecepcion.EndEdit())
            {
                return;
            }
            var filas = recepcion.ToList();
            if (filas.Any(f => f.Llego < 0 || f.ValorUnitario < 0))
            {
                Mensajes.Aviso(this, "\"Llegó\" y \"Valor unitario\" no pueden ser negativos.", "Pasar a stock");
                return;
            }
            int llegaron = filas.Count(f => f.Llego > 0);
            if (llegaron == 0)
            {
                Mensajes.Aviso(this, "No llegó nada: escriba en \"Llegó\" cuánto se recibió de cada material.", "Pasar a stock");
                return;
            }

            var pregunta = new List<string> { $"¿Pasar a stock {llegaron} {(llegaron == 1 ? "material" : "materiales")} del pedido {vendedorRecepcion}?" };
            int faltan = filas.Count(f => f.Llego < f.Pedido);
            if (faltan > 0)
            {
                pregunta.Add($"De {faltan} llegó menos o nada: lo que falta sigue pendiente.");
            }
            int cambiados = filas.Count(f => f.Llego > 0 && f.MaterialCambiado);
            if (cambiados > 0)
            {
                pregunta.Add($"Se corrigen los datos de {cambiados} {(cambiados == 1 ? "material" : "materiales")} en todo el programa.");
            }
            if (!Mensajes.Confirmar(this, string.Join("\n\n", pregunta), "Pasar a stock"))
            {
                return;
            }

            var hoy = DateTime.Today;
            if (Mensajes.Intentar(this, () => compras.PasarAStock(filas.Select(f => f.ARecepcion(hoy)))))
            {
                ModoRecepcion(false);
                Recargar(null);
            }
        }

        #endregion
    }
}
