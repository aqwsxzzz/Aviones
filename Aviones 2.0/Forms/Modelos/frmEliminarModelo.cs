using System;
using System.Collections.Generic;
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
    /// Elimina un modelo preguntando, material por material, qué pasa con cada pieza:
    /// vuelve como usado (se recicla), como nuevo, o se descarta. Puede volver sólo una parte.
    /// </summary>
    public partial class frmEliminarModelo : Form
    {
        public class FilaDevolucion
        {
            public int IdMaterialModelo { get; set; }
            public string Estado { get; set; }
            public string Condicion => EstadosMaterial.Nombre(Estado);
            public DestinoMaterial Destino { get; set; } = DestinoMaterial.Descartar;
            public string Familia { get; set; }
            public string Material { get; set; }
            public string Fabricante { get; set; }
            public decimal Usado { get; set; }
            public decimal CantidadVuelve { get; set; }
            public string Unidad { get; set; }
            public bool Vuelve => Destino != DestinoMaterial.Descartar;
        }

        private class OpcionDestino
        {
            public DestinoMaterial Valor { get; set; }
            public string Texto { get; set; }
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

            colDestino.ValueType = typeof(DestinoMaterial);
            colDestino.DisplayMember = nameof(OpcionDestino.Texto);
            colDestino.ValueMember = nameof(OpcionDestino.Valor);
            colDestino.DataSource = new List<OpcionDestino>
            {
                new OpcionDestino { Valor = DestinoMaterial.Descartar, Texto = "Se descarta" },
                new OpcionDestino { Valor = DestinoMaterial.Usado, Texto = "Vuelve como usado" },
                new OpcionDestino { Valor = DestinoMaterial.Nuevo, Texto = "Vuelve como nuevo" }
            };

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
                    CantidadVuelve = m.Cantidad,
                    Unidad = m.TipoUnidad
                });
            }
            dgvMateriales.DataSource = filas;
            filas.ListChanged += (s, e) => ActualizarResumen();
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            int usados = filas.Count(f => f.Destino == DestinoMaterial.Usado);
            int nuevos = filas.Count(f => f.Destino == DestinoMaterial.Nuevo);
            int descartados = filas.Count - usados - nuevos;
            labResumen.Text = $"Vuelven como usados: {usados}   ·   como nuevos: {nuevos}   ·   se descartan: {descartados}";
        }

        private void MarcarTodos(DestinoMaterial destino)
        {
            dgvMateriales.EndEdit();
            foreach (var f in filas)
            {
                f.Destino = destino;
            }
            filas.ResetBindings();
        }

        private void botTodos_Click(object sender, EventArgs e) => MarcarTodos(DestinoMaterial.Usado);

        private void botNinguno_Click(object sender, EventArgs e) => MarcarTodos(DestinoMaterial.Descartar);

        private void dgvMateriales_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Que la elección se aplique al momento, sin tener que salir de la celda
            if (dgvMateriales.IsCurrentCellDirty && dgvMateriales.CurrentCell is DataGridViewComboBoxCell)
            {
                dgvMateriales.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvMateriales_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var fila = filas[e.RowIndex];
            if (e.ColumnIndex == colDestino.Index && fila.Destino == DestinoMaterial.Nuevo && fila.Estado == EstadosMaterial.Usado)
            {
                Mensajes.Aviso(this, "Ese material salió del stock usado: no puede volver como nuevo.");
                fila.Destino = DestinoMaterial.Usado;
                BeginInvoke(new Action(() => filas.ResetItem(e.RowIndex)));
            }
            else if (e.ColumnIndex == colCantidadVuelve.Index && fila.CantidadVuelve > 0 && !fila.Vuelve)
            {
                // Escribir una cantidad implica que vuelve; por defecto como usado (se recicla)
                fila.Destino = DestinoMaterial.Usado;
                BeginInvoke(new Action(() => filas.ResetItem(e.RowIndex)));
            }
            ActualizarResumen();
        }

        private void dgvMateriales_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.ColumnIndex == colCantidadVuelve.Index && Numeros.TryLeer(e.Value?.ToString(), out decimal valor))
            {
                e.Value = valor;
                e.ParsingApplied = true;
            }
        }

        private void dgvMateriales_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != colCantidadVuelve.Index)
            {
                return;
            }
            var fila = filas[e.RowIndex];
            if (!Numeros.TryLeer(e.FormattedValue?.ToString(), out decimal valor) || valor < 0 || valor > fila.Usado)
            {
                Mensajes.Aviso(this, $"Escriba una cantidad entre 0 y {Numeros.Cantidad(fila.Usado)}.");
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
            if (e.RowIndex >= 0 && e.RowIndex < filas.Count && !filas[e.RowIndex].Vuelve)
            {
                e.CellStyle.ForeColor = Estilo.SinStock;
                if (e.ColumnIndex == colCantidadVuelve.Index)
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
            }
        }

        private void botEliminar_Click(object sender, EventArgs e)
        {
            if (!dgvMateriales.EndEdit())
            {
                return;
            }

            var devolver = filas.Where(f => f.Vuelve && f.CantidadVuelve > 0)
                .Select(f => new MaterialADevolver(f.IdMaterialModelo, f.CantidadVuelve, f.Destino))
                .ToList();

            string pregunta = devolver.Count == 0
                ? $"Se elimina \"{modelo.Nombre}\" y todos sus materiales se descartan.\n\n¿Continuar?"
                : $"Se elimina \"{modelo.Nombre}\" y {devolver.Count} materiales vuelven al stock; el resto se descarta.\n\n¿Continuar?";
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
