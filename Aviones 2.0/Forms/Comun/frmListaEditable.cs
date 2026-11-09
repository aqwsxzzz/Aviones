using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;

namespace Aviones_2._0.Forms.Comun
{
    public class ItemLista
    {
        public int Id { get; }
        public string Nombre { get; }

        public ItemLista(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public override string ToString() => Nombre;
    }

    /// <summary>
    /// Editor de una lista simple de nombres (familias, unidades de medida…).
    /// Las reglas (nombres repetidos, en uso) las aplica el repositorio que se pasa.
    /// </summary>
    public partial class frmListaEditable : Form
    {
        private readonly Func<IEnumerable<ItemLista>> listar;
        private readonly Func<string, int> crear;
        private readonly Action<int, string> renombrar;
        private readonly Action<int> eliminar;

        /// <summary>Id del último elemento agregado, para seleccionarlo al volver.</summary>
        public int? UltimoCreado { get; private set; }

        public frmListaEditable(string titulo, Func<IEnumerable<ItemLista>> listar, Func<string, int> crear,
            Action<int, string> renombrar, Action<int> eliminar)
        {
            InitializeComponent();
            Estilo.ColorDeLaSeccion(this);
            Text = titulo;
            this.listar = listar;
            this.crear = crear;
            this.renombrar = renombrar;
            this.eliminar = eliminar;
            Estilo.Botones(botAgregar, botRenombrar, botEliminar);
            Estilo.BotonSecundario(botCerrar);
            AcceptButton = botAgregar;
            txtNombre.TextChanged += (s, e) => ActualizarBotones();
        }

        private void frmListaEditable_Load(object sender, EventArgs e)
        {
            // Se abre sin nada elegido y sin nombre: AGREGAR se enciende recién al escribir uno nuevo
            Cargar(null);
            txtNombre.Clear();
            ActiveControl = txtNombre;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Al mostrarse, la lista elige sola el primero: se deja sin ninguno
            lstItems.ClearSelected();
            txtNombre.Clear();
            ActualizarBotones();
        }

        private ItemLista Seleccionado => lstItems.SelectedItem as ItemLista;

        private void Cargar(int? seleccionar)
        {
            Mensajes.Intentar(this, () =>
            {
                var items = listar().ToList();
                lstItems.DataSource = items;
                lstItems.SelectedItem = items.FirstOrDefault(i => i.Id == seleccionar);
                if (seleccionar == null) lstItems.ClearSelected();
            });
            ActualizarBotones();
        }

        private void ActualizarBotones()
        {
            botRenombrar.Enabled = Seleccionado != null;
            botEliminar.Enabled = Seleccionado != null;
            // Sólo se puede agregar un nombre escrito que todavía no esté en la lista
            string nombre = txtNombre.Text.Trim();
            botAgregar.Enabled = nombre.Length > 0
                && !lstItems.Items.Cast<ItemLista>().Any(i => string.Equals(i.Nombre, nombre, StringComparison.CurrentCultureIgnoreCase));
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Seleccionado != null)
            {
                txtNombre.Text = Seleccionado.Nombre;
            }
            ActualizarBotones();
        }

        private void botAgregar_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (Mensajes.Intentar(this, () => id = crear(txtNombre.Text)))
            {
                // Agregado: se cierra y queda elegido en la pantalla que la abrió
                UltimoCreado = id;
                DialogResult = DialogResult.OK;
            }
        }

        private void botRenombrar_Click(object sender, EventArgs e)
        {
            var item = Seleccionado;
            if (item != null && Mensajes.Intentar(this, () => renombrar(item.Id, txtNombre.Text)))
            {
                Cargar(item.Id);
            }
        }

        private void botEliminar_Click(object sender, EventArgs e)
        {
            var item = Seleccionado;
            if (item != null
                && Mensajes.Confirmar(this, $"¿Eliminar \"{item.Nombre}\"?")
                && Mensajes.Intentar(this, () => eliminar(item.Id)))
            {
                txtNombre.Clear();
                Cargar(null);
            }
        }
    }
}
