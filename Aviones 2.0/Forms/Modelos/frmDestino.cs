using System;
using System.Drawing;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;

namespace Aviones_2._0.Forms.Modelos
{
    /// <summary>
    /// Pregunta qué pasa con material que sale de un modelo: vuelve como nuevo, como usado o se descarta.
    /// No importa el motivo (error de carga, pieza rota, cambio de diseño): sólo el estado de la pieza.
    /// </summary>
    public partial class frmDestino : Form
    {
        public DestinoMaterial Destino { get; private set; }

        public frmDestino(string material, decimal cantidad, string unidad, string estadoOrigen)
        {
            InitializeComponent();
            labDetalle.Text = $"{Numeros.Cantidad(cantidad)} {unidad} de {material}";
            Estilo.Botones(botNuevo, botUsado, botDescartar);
            botDescartar.BackColor = Color.Firebrick;
            Estilo.BotonSecundario(botCancelar);

            if (estadoOrigen == EstadosMaterial.Usado)
            {
                botNuevo.Enabled = false;
                botNuevo.BackColor = Color.Gray;
                botNuevo.Text = "VUELVE COMO NUEVO\r\n(no: salió del stock usado)";
            }
        }

        /// <summary>Muestra el diálogo; devuelve null si se cancela.</summary>
        public static DestinoMaterial? Pedir(IWin32Window dueño, string material, decimal cantidad, string unidad, string estadoOrigen)
        {
            using (var f = new frmDestino(material, cantidad, unidad, estadoOrigen))
            {
                return f.ShowDialog(dueño) == DialogResult.OK ? f.Destino : (DestinoMaterial?)null;
            }
        }

        private void Elegir(DestinoMaterial destino)
        {
            Destino = destino;
            DialogResult = DialogResult.OK;
        }

        private void botNuevo_Click(object sender, EventArgs e) => Elegir(DestinoMaterial.Nuevo);

        private void botUsado_Click(object sender, EventArgs e) => Elegir(DestinoMaterial.Usado);

        private void botDescartar_Click(object sender, EventArgs e) => Elegir(DestinoMaterial.Descartar);
    }
}
