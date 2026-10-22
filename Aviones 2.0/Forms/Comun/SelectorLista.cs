using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Common;
using Aviones_2._0.Data;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.Forms.Comun
{
    /// <summary>
    /// Elegir un elemento de una lista de nombres (fabricante, ubicación) o dejarlo vacío,
    /// con un botón para editar la lista.
    /// </summary>
    public class SelectorLista : UserControl
    {
        private readonly ComboBox combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Margin = Padding.Empty };
        private readonly Button boton = new Button { Text = "Editar…", AutoSize = true, TabStop = false, Margin = Padding.Empty };
        private ListaLoteRepositorio lista;
        private string titulo;

        public SelectorLista()
        {
            Controls.Add(combo);
            Controls.Add(boton);
            boton.Click += boton_Click;
        }

        /// <summary>Qué lista muestra y el título de su editor.</summary>
        public void Configurar(ListaLoteRepositorio lista, string titulo)
        {
            this.lista = lista;
            this.titulo = titulo;
        }

        /// <summary>Lee la lista y deja elegido el elemento indicado (null: vacío).</summary>
        public void Cargar(int? seleccionar)
        {
            var elementos = new List<ElementoLista> { new ElementoLista { Id = 0, Nombre = "" } };
            elementos.AddRange(lista.Listar());
            combo.DisplayMember = nameof(ElementoLista.Nombre);
            combo.ValueMember = nameof(ElementoLista.Id);
            combo.DataSource = elementos;
            Valor = seleccionar;
        }

        /// <summary>El elemento elegido; null si quedó vacío.</summary>
        public int? Valor
        {
            get => combo.SelectedValue is int id && id > 0 ? id : (int?)null;
            set
            {
                combo.SelectedValue = value ?? 0;
                if (combo.SelectedIndex < 0 && combo.Items.Count > 0)
                {
                    combo.SelectedIndex = 0;   // ya no existe: queda vacío
                }
            }
        }

        private void boton_Click(object sender, EventArgs e)
        {
            int? actual = Valor;
            using (var editor = new frmListaEditable(titulo,
                () => lista.Listar().Select(x => new ItemLista(x.Id, x.Nombre)),
                nombre => lista.Crear(nombre),
                (id, nombre) => lista.Renombrar(id, nombre),
                id => lista.Eliminar(id)))
            {
                editor.ShowDialog(FindForm());
                Mensajes.Intentar(FindForm(), () => Cargar(editor.UltimoCreado ?? actual));
            }
            combo.Focus();
        }

        public override Size GetPreferredSize(Size proposedSize) =>
            new Size(200 + boton.PreferredSize.Width + 6, Math.Max(combo.PreferredHeight, boton.PreferredSize.Height));

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            var botonTamaño = boton.PreferredSize;
            int alto = Math.Max(combo.Height, botonTamaño.Height);
            if (Height != alto)
            {
                Height = alto;
            }
            boton.SetBounds(Width - botonTamaño.Width, (alto - botonTamaño.Height) / 2, botonTamaño.Width, botonTamaño.Height);
            combo.SetBounds(0, (alto - combo.Height) / 2, Math.Max(40, boton.Left - 6), combo.Height);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            PerformLayout();
        }
    }
}
