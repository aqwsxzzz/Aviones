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
            Estilo.Botones(botAgregarPedido, botCerrarPedido, botRecibirPedido, botModificarPedido, botEliminarPedido, botImprimirPedidos, botPedir, botImprimirCriticos);
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
            if (!pedido.Enviado)
            {
                Mensajes.Aviso(this, "Este pedido todavía no se envió. Primero CERRAR PEDIDO; cuando llegue, se recibe.", "Recibir");
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

        /// <summary>
        /// Cerrar el pedido de un vendedor: imprime la lista de lo que todavía no se le mandó
        /// y lo deja como enviado. Recién entonces se puede recibir.
        /// </summary>
        private void botCerrarPedido_Click(object sender, EventArgs e)
        {
            List<PedidoCompra> porEnviar = null;
            if (!Mensajes.Intentar(this, () => porEnviar = compras.ListarPendientes().Where(p => !p.Enviado).ToList()))
            {
                return;
            }
            if (porEnviar.Count == 0)
            {
                Mensajes.Aviso(this, "No hay pedidos por enviar: todo lo de la lista ya se mandó.", "Cerrar pedido");
                return;
            }

            // De qué vendedor: el del filtro, o el único que tiene pedidos por enviar
            int filtro = cboVendedor.SelectedValue is int v ? v : TodosLosVendedores;
            var vendedoresPorEnviar = porEnviar.Select(p => p.IdVendedor ?? SinVendedor).Distinct().ToList();
            if (filtro == TodosLosVendedores)
            {
                if (vendedoresPorEnviar.Count > 1)
                {
                    Mensajes.Aviso(this, "Hay pedidos por enviar de más de un vendedor.\n\nElija arriba, en \"Vendedor\", el del pedido que va a cerrar.", "Cerrar pedido");
                    cboVendedor.Focus();
                    cboVendedor.DroppedDown = true;
                    return;
                }
                filtro = vendedoresPorEnviar[0];
                cboVendedor.SelectedValue = filtro;   // la lista queda mostrando ese vendedor
            }
            var delVendedor = porEnviar.Where(p => (p.IdVendedor ?? SinVendedor) == filtro).ToList();
            if (delVendedor.Count == 0)
            {
                Mensajes.Aviso(this, "Este vendedor no tiene pedidos por enviar.", "Cerrar pedido");
                return;
            }
            string vendedor = filtro == SinVendedor ? "sin vendedor" : cboVendedor.Text;

            // Se imprime sólo lo que se le va a mandar
            decimal total = delVendedor.Sum(p => p.CostoEstimado ?? 0m);
            dgvPedidos.DataSource = delVendedor;
            bool impreso;
            try
            {
                impreso = ImpresionGrilla.VistaPrevia(this, dgvPedidos, $"Pedido a {vendedor} ({DateTime.Today:dd/MM/yyyy})",
                    $"{delVendedor.Count} materiales   ·   Costo estimado: $ {Numeros.Dinero(total)}");
            }
            finally
            {
                Mensajes.Intentar(this, () => CargarPedidos());
            }

            string pregunta = impreso
                ? $"¿Cerrar el pedido a {vendedor}?\n\nLos {delVendedor.Count} materiales quedan como enviados y ya se pueden recibir cuando lleguen."
                : $"No se imprimió.\n\n¿Cerrar igual el pedido a {vendedor}? (por ejemplo, si lo mandó por mail)";
            if (Mensajes.Confirmar(this, pregunta, "Cerrar pedido")
                && Mensajes.Intentar(this, () => compras.EnviarPedidos(delVendedor.Select(p => p.IdPedido))))
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
            dgv.CurrentCell = null;
            var fila = dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault(condicion);
            if (fila != null)
            {
                dgv.CurrentCell = fila.Cells.Cast<DataGridViewCell>().First(c => c.Visible);
                fila.Selected = true;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Al mostrarse, las grillas eligen solas la primera fila: se dejan sin ninguna elegida
            foreach (var dgv in new[] { dgvPedidos, dgvCriticos })
            {
                dgv.ClearSelection();
                dgv.CurrentCell = null;
            }
            ActualizarBotones();
            MostrarFotoCritico();
        }

        private void ActualizarBotones()
        {
            var pedido = PedidoSeleccionado();
            // Primero se cierra el pedido (se manda al vendedor); recién ahí se puede recibir, y ya no se modifica
            botRecibirPedido.Enabled = pedido?.Enviado == true;
            botModificarPedido.Enabled = pedido != null && !pedido.Enviado;
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
