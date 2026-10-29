using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;
using Aviones_2._0.Forms.Materiales;

namespace Aviones_2._0.Forms.Compras
{
    /// <summary>
    /// Dos listas: los pedidos de compra pendientes (lo que hay que comprar) y el stock crítico
    /// (materiales con su mínimo o menos), desde donde se agregan pedidos.
    /// </summary>
    public partial class frmCompras : Form
    {
        private readonly CompraRepositorio compras = new CompraRepositorio();
        private readonly ConfiguracionRepositorio configuracion = new ConfiguracionRepositorio();
        private readonly MaterialRepositorio materiales = new MaterialRepositorio();
        private List<StockCritico> criticos = new List<StockCritico>();
        private readonly ListaRepositorio vendedores = ListaRepositorio.Vendedores();
        private bool cargandoVendedores;
        /// <summary>Filtro de la lista de compras: todos los vendedores, o los pedidos sin vendedor.</summary>
        private const int TodosLosVendedores = -1, SinVendedor = 0;
        private decimal minimoGeneral;
        /// <summary>Naranja oscuro: se lee bien sobre el fondo verde claro.</summary>
        private static readonly Color YaPedido = Color.FromArgb(200, 85, 0);
        /// <summary>El material cuya foto se está mostrando, para no volver a leerla.</summary>
        private int? idFotoCritico;

        public frmCompras()
        {
            InitializeComponent();
            Imagenes.MarcarSinImagen(picCritico);
            Estilo.Grilla(dgvPedidos);
            Estilo.Grilla(dgvCriticos);
            dgvPedidos.BackgroundColor = Color.Honeydew;
            dgvCriticos.BackgroundColor = Color.Honeydew;
            dgvCriticos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Estilo.Botones(botAgregarPedido, botRecibirPedido, botModificarPedido, botEliminarPedido, botImprimirPedidos, botPedir, botImprimirCriticos);
            Estilo.BotonSecundario(botSalir);
            OrdenGrilla.Habilitar(dgvPedidos, dgvCriticos);
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            splListas.SplitterDistance = splListas.Height / 2;
            Mensajes.Intentar(this, () =>
            {
                minimoGeneral = configuracion.ObtenerStockMinimoGeneral();
                txtMinimo.Text = Numeros.Cantidad(minimoGeneral);
                CargarVendedores();
                CargarPedidos();
                CargarCriticos();
            });
        }

        #region Pedidos

        private PedidoCompra PedidoSeleccionado() =>
            dgvPedidos.SelectedRows.Count > 0 ? dgvPedidos.SelectedRows[0].DataBoundItem as PedidoCompra : null;

        private void CargarPedidos(int? seleccionar = null)
        {
            int? id = seleccionar ?? PedidoSeleccionado()?.IdPedido;
            int filtro = cboVendedor.SelectedValue is int v ? v : TodosLosVendedores;
            var pedidos = compras.ListarPendientes()
                .Where(p => filtro == TodosLosVendedores || (p.IdVendedor ?? SinVendedor) == filtro)
                .ToList();
            dgvPedidos.DataSource = OrdenGrilla.Aplicar(dgvPedidos, pedidos);
            Seleccionar(dgvPedidos, r => ((PedidoCompra)r.DataBoundItem).IdPedido == id);

            decimal total = pedidos.Sum(p => p.CostoEstimado ?? 0m);
            labTotalPedidos.Text = pedidos.Count == 0
                ? (filtro == TodosLosVendedores ? "No hay nada pendiente de comprar." : "No hay pedidos pendientes de este vendedor.")
                : $"{pedidos.Count} pedidos   ·   Costo estimado: $ {Numeros.Dinero(total)}"
                  + (pedidos.Any(p => !p.Precio.HasValue) ? " (algunos sin precio)" : "");
            ActualizarBotones();
        }

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e) => ActualizarBotones();

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
            cargandoVendedores = true;
            try
            {
                cboVendedor.DisplayMember = nameof(ElementoLista.Nombre);
                cboVendedor.ValueMember = nameof(ElementoLista.Id);
                cboVendedor.DataSource = opciones;
                cboVendedor.SelectedValue = opciones.Any(o => o.Id == actual) ? actual : TodosLosVendedores;
            }
            finally
            {
                cargandoVendedores = false;
            }
        }

        private void cboVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargandoVendedores)
            {
                Mensajes.Intentar(this, () => CargarPedidos());
            }
        }

        /// <summary>Pedir cualquier material, tenga el stock que tenga, o uno nuevo que todavía no está cargado.</summary>
        private void botAgregarPedido_Click(object sender, EventArgs e)
        {
            int idMaterial;
            using (var elegir = new frmElegirMaterial())
            {
                if (elegir.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                idMaterial = elegir.IdMaterial;
            }
            using (var dialogo = new frmPedido(idMaterial))
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    Recargar(null, idMaterial);
                }
            }
        }

        private void dgvPedidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                botModificarPedido_Click(sender, e);
            }
        }

        private void dgvPedidos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                botModificarPedido_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                botEliminarPedido_Click(sender, e);
            }
        }

        private void botRecibirPedido_Click(object sender, EventArgs e)
        {
            var pedido = PedidoSeleccionado();
            if (pedido == null)
            {
                return;
            }
            using (var dialogo = new frmLoteEditor(pedido))
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    Recargar(null);
                }
            }
        }

        private void botModificarPedido_Click(object sender, EventArgs e)
        {
            var pedido = PedidoSeleccionado();
            if (pedido == null)
            {
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

        private void botEliminarPedido_Click(object sender, EventArgs e)
        {
            var pedido = PedidoSeleccionado();
            if (pedido != null
                && Mensajes.Confirmar(this, $"¿Sacar de la lista de compras {Numeros.Cantidad(pedido.Cantidad)} {pedido.TipoUnidad} de {pedido.Material}?", "Eliminar pedido")
                && Mensajes.Intentar(this, () => compras.EliminarPedido(pedido.IdPedido)))
            {
                Recargar(null);
            }
        }

        private void botImprimirPedidos_Click(object sender, EventArgs e)
        {
            // Filtrada por vendedor, se imprime la lista de ese vendedor para mandarle la compra
            string titulo = cboVendedor.SelectedValue is int v && v != TodosLosVendedores
                ? $"Lista de compras: {cboVendedor.Text}"
                : "Lista de compras";
            ImpresionGrilla.VistaPrevia(this, dgvPedidos, titulo, labTotalPedidos.Text);
        }

        #endregion

        #region Stock crítico

        private StockCritico CriticoSeleccionado() =>
            dgvCriticos.SelectedRows.Count > 0 ? dgvCriticos.SelectedRows[0].DataBoundItem as StockCritico : null;

        private void CargarCriticos(int? seleccionar = null)
        {
            int? id = seleccionar ?? CriticoSeleccionado()?.IdMaterial;
            criticos = compras.ListarStockCritico(minimoGeneral);
            var visibles = chkSoloSinPedido.Checked ? criticos.Where(c => !c.TienePedido).ToList() : criticos;
            dgvCriticos.DataSource = OrdenGrilla.Aplicar(dgvCriticos, visibles);
            Seleccionar(dgvCriticos, r => ((StockCritico)r.DataBoundItem).IdMaterial == id);

            int sinPedido = criticos.Count(c => !c.TienePedido);
            labResumenCriticos.Text = criticos.Count == 0
                ? "Ningún material está en su mínimo."
                : $"{criticos.Count} materiales en su mínimo o menos   ·   {sinPedido} sin pedir";
            ActualizarBotones();
        }

        private void dgvCriticos_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
            MostrarFotoCritico();
        }

        private void MostrarFotoCritico()
        {
            int? id = CriticoSeleccionado()?.IdMaterial;
            if (id == idFotoCritico)
            {
                return;
            }
            idFotoCritico = id;
            Image foto = null;
            if (id.HasValue)
            {
                Mensajes.Intentar(this, () => foto = Imagenes.DesdeBytes(materiales.ObtenerImagen(id.Value)));
            }
            Imagenes.Mostrar(picCritico, foto);
        }

        private void picCritico_DoubleClick(object sender, EventArgs e)
        {
            var critico = CriticoSeleccionado();
            if (critico != null)
            {
                Imagenes.Ampliar(this, picCritico.Image, critico.Descripcion);
            }
        }

        private void dgvCriticos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                botPedir_Click(sender, e);
            }
        }

        private void dgvCriticos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !(dgvCriticos.Rows[e.RowIndex].DataBoundItem is StockCritico c))
            {
                return;
            }
            // Lo ya pedido se distingue en naranja; el resto, en el color normal
            if (c.TienePedido)
            {
                e.CellStyle.ForeColor = YaPedido;
            }
            if (e.ColumnIndex == colCriPedido.Index && !c.TienePedido)
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void botPedir_Click(object sender, EventArgs e)
        {
            var critico = CriticoSeleccionado();
            if (critico == null)
            {
                return;
            }
            using (var dialogo = new frmPedido(critico.IdMaterial))
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    Recargar(null, critico.IdMaterial);
                }
            }
        }

        private void chkSoloSinPedido_CheckedChanged(object sender, EventArgs e)
        {
            Mensajes.Intentar(this, () => CargarCriticos());
        }

        private void txtMinimo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                botAplicarMinimo_Click(sender, e);
            }
        }

        private void botAplicarMinimo_Click(object sender, EventArgs e)
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
                CargarCriticos();
            });
        }

        private void botImprimirCriticos_Click(object sender, EventArgs e)
        {
            ImpresionGrilla.VistaPrevia(this, dgvCriticos, "Stock crítico", labResumenCriticos.Text);
        }

        #endregion

        private void Recargar(int? idPedido, int? idMaterialCritico = null)
        {
            Mensajes.Intentar(this, () =>
            {
                CargarVendedores();   // desde un pedido se pueden agregar vendedores
                CargarPedidos(idPedido);
                CargarCriticos(idMaterialCritico);
            });
        }

        private static void Seleccionar(DataGridView dgv, Func<DataGridViewRow, bool> condicion)
        {
            dgv.ClearSelection();
            var fila = dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault(condicion) ?? dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault();
            if (fila != null)
            {
                dgv.CurrentCell = fila.Cells.Cast<DataGridViewCell>().First(c => c.Visible);
                fila.Selected = true;
            }
        }

        private void ActualizarBotones()
        {
            botRecibirPedido.Enabled = PedidoSeleccionado() != null;
            botModificarPedido.Enabled = PedidoSeleccionado() != null;
            botEliminarPedido.Enabled = PedidoSeleccionado() != null;
            botPedir.Enabled = CriticoSeleccionado() != null;
        }

        private void botSalir_Click(object sender, EventArgs e) => Close();

        private void frmCompras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
