using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.Forms.Modelos
{
    /// <summary>
    /// Elimina un modelo preguntando, material por material, cuántas piezas vuelven al stock como nuevas,
    /// cuántas como usadas (se reciclan); el resto se descarta.
    /// </summary>
    public partial class frmEliminarModelo : Form
    {
        public class FilaDevolucion
        {
            public int IdMaterialModelo { get; set; }
            public string Estado { get; set; }
            public string Condicion => EstadosMaterial.Nombre(Estado);
            public string Familia { get; set; }
            public string Material { get; set; }
            public string Fabricante { get; set; }
            public decimal Usado { get; set; }
            public decimal VuelveNuevo { get; set; }
            public decimal VuelveUsado { get; set; }
            public decimal SeDescarta => Usado - VuelveNuevo - VuelveUsado;
            public string Unidad { get; set; }
            public bool PuedeVolverNuevo => Estado != EstadosMaterial.Usado;
        }

        private readonly ModeloRepositorio modelos = new ModeloRepositorio();
        private readonly Modelo modelo;
        private readonly BindingList<FilaDevolucion> filas = new BindingList<FilaDevolucion>();

        public frmEliminarModelo(Modelo modelo)
        {
            InitializeComponent();
            this.modelo = modelo;
            Estilo.Grilla(dgvMateriales);
            dgvMateriales.ReadOnly = false;
            dgvMateriales.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvMateriales.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvMateriales.BackgroundColor = Color.Linen;
            Estilo.Botones(botEliminar);
            botEliminar.BackColor = Color.Firebrick;
            Estilo.BotonSecundario(botCancelar);

            labTitulo.Text = $"Eliminar \"{modelo.Nombre}\"" + (string.IsNullOrEmpty(modelo.Alias) ? "" : $" ({modelo.Alias})");
            foreach (var m in modelos.ListarMateriales(modelo.IdModelo))
            {
                filas.Add(new FilaDevolucion
                {
                    IdMaterialModelo = m.IdMaterialModelo,
                    Estado = m.Estado,
                    Familia = m.Familia,
                    Material = string.Join(" ", new[] { m.Grupo, m.Caracteristica, m.Color, m.Medidas }
                        .Where(s => !string.IsNullOrWhiteSpace(s))),
                    Fabricante = m.Fabricante,
                    Usado = m.Cantidad,
                    Unidad = m.TipoUnidad
                });
            }
            dgvMateriales.DataSource = filas;
            filas.ListChanged += (s, e) => ActualizarResumen();
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            decimal nuevo = filas.Sum(f => f.VuelveNuevo);
            decimal usado = filas.Sum(f => f.VuelveUsado);
            int descartan = filas.Count(f => f.SeDescarta > 0);
            labResumen.Text = $"Vuelven {Numeros.Cantidad(nuevo)} como nuevos y {Numeros.Cantidad(usado)} como usados   ·   "
                              + $"{descartan} materiales con algo que se descarta";
        }

        private void Repartir(bool todoUsado)
        {
            dgvMateriales.EndEdit();
            foreach (var f in filas)
            {
                f.VuelveNuevo = 0m;
                f.VuelveUsado = todoUsado ? f.Usado : 0m;
            }
            filas.ResetBindings();
        }

        private void botTodos_Click(object sender, EventArgs e) => Repartir(todoUsado: true);

        private void botNinguno_Click(object sender, EventArgs e) => Repartir(todoUsado: false);

        private void dgvMateriales_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Lo que salió del stock usado no puede volver como nuevo
            if (e.ColumnIndex == colVuelveNuevo.Index && !filas[e.RowIndex].PuedeVolverNuevo)
            {
                e.Cancel = true;
            }
        }

        private void dgvMateriales_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Recalcula "Se descarta" de la fila
                BeginInvoke(new Action(() => filas.ResetItem(e.RowIndex)));
            }
        }

        private void dgvMateriales_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.ColumnIndex != colVuelveNuevo.Index && e.ColumnIndex != colVuelveUsado.Index)
            {
                return;
            }
            string texto = e.Value?.ToString();
            if (string.IsNullOrWhiteSpace(texto))
            {
                e.Value = 0m;
                e.ParsingApplied = true;
            }
            else if (Numeros.TryLeer(texto, out decimal valor))
            {
                e.Value = valor;
                e.ParsingApplied = true;
            }
        }

        private void dgvMateriales_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != colVuelveNuevo.Index && e.ColumnIndex != colVuelveUsado.Index)
            {
                return;
            }
            var fila = filas[e.RowIndex];
            string texto = e.FormattedValue?.ToString();
            decimal valor = 0m;
            if (!string.IsNullOrWhiteSpace(texto) && (!Numeros.TryLeer(texto, out valor) || valor < 0))
            {
                Mensajes.Aviso(this, "Escriba un número (0 si no vuelve nada).");
                e.Cancel = true;
                return;
            }
            decimal otro = e.ColumnIndex == colVuelveNuevo.Index ? fila.VuelveUsado : fila.VuelveNuevo;
            if (valor + otro > fila.Usado)
            {
                Mensajes.Aviso(this, $"Entre nuevo y usado no pueden volver más de {Numeros.Cantidad(fila.Usado)}.");
                e.Cancel = true;
            }
        }

        private void dgvMateriales_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Lo valida CellValidating; evita el cuadro de error por defecto
            e.ThrowException = false;
        }

        private void dgvMateriales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= filas.Count)
            {
                return;
            }
            var fila = filas[e.RowIndex];
            if (e.ColumnIndex == colVuelveNuevo.Index && !fila.PuedeVolverNuevo)
            {
                e.Value = "—";
                e.CellStyle.BackColor = Color.Gainsboro;
                e.FormattingApplied = true;
            }
            else if ((e.ColumnIndex == colVuelveNuevo.Index || e.ColumnIndex == colVuelveUsado.Index || e.ColumnIndex == colDescarta.Index)
                     && e.Value is decimal d && d == 0m)
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void botEliminar_Click(object sender, EventArgs e)
        {
            if (!dgvMateriales.EndEdit())
            {
                return;
            }

            var devolver = filas.Where(f => f.VuelveNuevo + f.VuelveUsado > 0)
                .Select(f => new MaterialADevolver(f.IdMaterialModelo, new Devolucion(f.VuelveNuevo, f.VuelveUsado)))
                .ToList();

            string pregunta = devolver.Count == 0
                ? $"Se elimina \"{modelo.Nombre}\" y todos sus materiales se descartan.\n\n¿Continuar?"
                : $"Se elimina \"{modelo.Nombre}\". {labResumen.Text}.\n\n¿Continuar?";
            if (!Mensajes.Confirmar(this, pregunta, "Eliminar modelo"))
            {
                return;
            }

            if (Mensajes.Intentar(this, () => modelos.Eliminar(modelo.IdModelo, devolver)))
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
