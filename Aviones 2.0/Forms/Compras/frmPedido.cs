using System;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.Forms.Compras
{
    /// <summary>
    /// Agrega un material a la lista de compras (pedido) o corrige un pedido pendiente.
    /// Muestra el stock actual, el mínimo y lo que ya está pedido para decidir cuánto comprar.
    /// </summary>
    public partial class frmPedido : Form
    {
        private readonly CompraRepositorio compras = new CompraRepositorio();
        private readonly int idMaterial;
        private readonly PedidoCompra pedido;

        /// <summary>Pedido nuevo para un material.</summary>
        public frmPedido(int idMaterial) : this(idMaterial, null) { }

        /// <summary>Corregir un pedido pendiente.</summary>
        public frmPedido(PedidoCompra pedido) : this(pedido.IdMaterial, pedido) { }

        private frmPedido(int idMaterial, PedidoCompra pedido)
        {
            InitializeComponent();
            this.idMaterial = idMaterial;
            this.pedido = pedido;
            Estilo.Botones(botGuardar);
            Estilo.BotonSecundario(botCancelar);
            Estilo.EnterAvanza(this);
        }

        private void frmPedido_Load(object sender, EventArgs e)
        {
            bool ok = Mensajes.Intentar(this, () =>
            {
                var m = new MaterialRepositorio().ObtenerStock(idMaterial)
                        ?? throw new ReglaNegocioException("El material ya no existe.");
                decimal minimo = m.StockMinimo ?? new ConfiguracionRepositorio().ObtenerStockMinimoGeneral();
                var otros = compras.ListarPendientesDe(idMaterial).Where(p => p.IdPedido != pedido?.IdPedido).ToList();

                labMaterial.Text = $"{m.Familia}: {m.Descripcion}" + (string.IsNullOrEmpty(m.Codigo) ? "" : $" ({m.Codigo})");
                labUnidad.Text = m.TipoUnidad;
                labStock.Text = $"Disponible: {Numeros.Cantidad(m.Disponible)}"
                    + (m.DisponibleUsado > 0 ? $" ({Numeros.Cantidad(m.DisponibleNuevo)} nuevo, {Numeros.Cantidad(m.DisponibleUsado)} usado)" : "")
                    + $"   ·   Mínimo: {Numeros.Cantidad(minimo)}" + (m.StockMinimo.HasValue ? "" : " (general)")
                    + (otros.Count > 0
                        ? $"\nYa pedido: {string.Join(", ", otros.Select(p => $"{Numeros.Cantidad(p.Cantidad)} el {p.FechaPedido:dd/MM}"))}"
                        : "");

                if (pedido == null)
                {
                    Text = "Agregar a la lista de compras";
                    // Sugerencia: lo que falta para llegar al mínimo, sin contar lo ya pedido
                    decimal falta = minimo - m.Disponible - otros.Sum(p => p.Cantidad);
                    if (falta > 0)
                    {
                        txtCantidad.Text = Numeros.Cantidad(Math.Ceiling(falta));
                    }
                }
                else
                {
                    Text = "Modificar pedido";
                    botGuardar.Text = "GUARDAR";
                    txtCantidad.Text = Numeros.Cantidad(pedido.Cantidad);
                    txtNota.Text = pedido.Nota;
                }
            });

            if (!ok)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }
            ActiveControl = txtCantidad;
            txtCantidad.SelectAll();
        }

        private void botGuardar_Click(object sender, EventArgs e)
        {
            if (!Numeros.TryLeer(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                Mensajes.Aviso(this, "Escriba cuánto hay que comprar (mayor que cero).");
                txtCantidad.Focus();
                return;
            }

            bool guardado = Mensajes.Intentar(this, () =>
            {
                if (pedido == null) compras.CrearPedido(idMaterial, cantidad, txtNota.Text);
                else compras.ModificarPedido(pedido.IdPedido, cantidad, txtNota.Text);
            });
            if (guardado)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
