using System;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.Forms.Materiales
{
    /// <summary>Alta o corrección de un lote (un ingreso de stock de un material).</summary>
    public partial class frmLoteEditor : Form
    {
        private readonly LoteRepositorio lotes = new LoteRepositorio();
        private readonly int idMaterial;
        private readonly Lote lote;

        /// <param name="lote">null para un lote nuevo.</param>
        public frmLoteEditor(int idMaterial, string descripcionMaterial, Lote lote)
        {
            InitializeComponent();
            this.idMaterial = idMaterial;
            this.lote = lote;
            labMaterial.Text = descripcionMaterial;
            Estilo.Botones(botGuardar);
            Estilo.BotonSecundario(botCancelar);
            Estilo.EnterAvanza(this);
        }

        private void frmLoteEditor_Load(object sender, EventArgs e)
        {
            Mensajes.Intentar(this, () =>
            {
                txtFabricante.AutoCompleteCustomSource.AddRange(lotes.ListarFabricantes().ToArray());
                txtUbicacion.AutoCompleteCustomSource.AddRange(lotes.ListarUbicaciones().ToArray());
            });

            if (lote == null)
            {
                Text = "Nuevo ingreso";
                dtpFecha.Value = DateTime.Today;
                labUso.Text = "Todo lo ingresado queda disponible, como nuevo o como usado según se compró.";
            }
            else
            {
                Text = "Modificar lote";
                txtFabricante.Text = lote.Fabricante;
                txtUbicacion.Text = lote.Ubicacion;
                txtCantidad.Text = Numeros.Cantidad(lote.Cantidad);
                txtValor.Text = Numeros.Dinero(lote.ValorUnitario);
                dtpFecha.Value = lote.FechaIngreso;
                radCompraUsado.Checked = lote.CompradoUsado;
                radCompraNuevo.Checked = !lote.CompradoUsado;
                flpCompra.Enabled = false;   // cómo se compró no se cambia después
                labUso.Text = $"Quedan {Numeros.Cantidad(lote.DisponibleNuevo)} nuevos y {Numeros.Cantidad(lote.DisponibleUsado)} usados"
                    + (lote.Consumido > 0 ? $"; salieron {Numeros.Cantidad(lote.Consumido)} (en modelos o descartados)." : ".")
                    + "\nSi cambia la cantidad, lo disponible " + (lote.CompradoUsado ? "usado" : "nuevo")
                    + " cambia en la misma diferencia.";
            }
            ActiveControl = txtFabricante;
        }

        private void botGuardar_Click(object sender, EventArgs e)
        {
            if (!Numeros.TryLeer(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                Mensajes.Aviso(this, "La cantidad tiene que ser un número mayor que cero.");
                txtCantidad.Focus();
                return;
            }
            if (!Numeros.TryLeerOpcional(txtValor.Text, out decimal? valor) || valor < 0)
            {
                Mensajes.Aviso(this, "El valor unitario tiene que ser un número.");
                txtValor.Focus();
                return;
            }

            var datos = new Lote
            {
                IdLote = lote?.IdLote ?? 0,
                IdMaterial = idMaterial,
                Fabricante = string.IsNullOrWhiteSpace(txtFabricante.Text) ? null : txtFabricante.Text.Trim(),
                Ubicacion = string.IsNullOrWhiteSpace(txtUbicacion.Text) ? null : txtUbicacion.Text.Trim(),
                Cantidad = cantidad,
                CompradoUsado = radCompraUsado.Checked,
                ValorUnitario = valor ?? 0m,
                FechaIngreso = dtpFecha.Value.Date
            };

            bool guardado = Mensajes.Intentar(this, () =>
            {
                if (lote == null) lotes.Crear(datos);
                else lotes.Actualizar(datos);
            });
            if (guardado)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
