using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;

namespace Aviones_2._0.Forms.Materiales
{
    /// <summary>Valor del stock disponible (lo que queda en los lotes × su precio), por familia.</summary>
    public partial class frmInformeValor : Form
    {
        public class FilaFamilia
        {
            public string Familia { get; set; }
            public int Materiales { get; set; }
            public int SinStock { get; set; }
            public decimal Valor { get; set; }
        }

        public frmInformeValor(IEnumerable<MaterialStock> materiales)
        {
            InitializeComponent();
            Estilo.Grilla(dgvFamilias);
            OrdenGrilla.Habilitar(dgvFamilias);
            Estilo.Botones(botImprimir);
            Estilo.BotonSecundario(botCerrar);

            var filas = materiales
                .GroupBy(m => m.Familia)
                .Select(g => new FilaFamilia
                {
                    Familia = g.Key,
                    Materiales = g.Count(),
                    SinStock = g.Count(m => m.Disponible == 0),
                    Valor = g.Sum(m => m.ValorDisponible)
                })
                .OrderBy(f => f.Familia)
                .ToList();

            dgvFamilias.DataSource = filas;
            labTotal.Text = $"Total: $ {Numeros.Dinero(filas.Sum(f => f.Valor))}";
        }

        private void botImprimir_Click(object sender, EventArgs e)
        {
            ImpresionGrilla.VistaPrevia(this, dgvFamilias, labTitulo.Text, labTotal.Text);
        }
    }
}
