using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Aviones_2._0.Common
{
    /// <summary>
    /// Ordenar una grilla haciendo clic en el título de una columna (otro clic invierte el orden).
    /// Funciona con grillas enlazadas a una List&lt;T&gt;. El orden elegido se recuerda y se vuelve
    /// a aplicar con <see cref="Aplicar{T}"/> cada vez que la pantalla recarga los datos.
    /// </summary>
    public static class OrdenGrilla
    {
        private class Estado
        {
            public string Propiedad;
            public bool Descendente;
            public DataGridViewColumn Columna;
        }

        private static readonly ConditionalWeakTable<DataGridView, Estado> estados =
            new ConditionalWeakTable<DataGridView, Estado>();

        public static void Habilitar(params DataGridView[] grillas)
        {
            foreach (var g in grillas)
            {
                Habilitar(g);
            }
        }

        private static void Habilitar(DataGridView dgv)
        {
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                if (!string.IsNullOrEmpty(c.DataPropertyName))
                {
                    c.SortMode = DataGridViewColumnSortMode.Programmatic;
                }
            }
            dgv.ColumnHeaderMouseClick += (s, e) => AlHacerClic(dgv, e.ColumnIndex);
            dgv.DataBindingComplete += (s, e) => MostrarFlecha(dgv);
        }

        /// <summary>Devuelve la lista con el orden elegido por el usuario (o sin cambios si no eligió).</summary>
        public static List<T> Aplicar<T>(DataGridView dgv, List<T> lista)
        {
            if (lista == null || !estados.TryGetValue(dgv, out var estado) || estado.Propiedad == null)
            {
                return lista;
            }
            return Ordenar(lista.Cast<object>(), typeof(T), estado).Cast<T>().ToList();
        }

        private static void AlHacerClic(DataGridView dgv, int indiceColumna)
        {
            var columna = dgv.Columns[indiceColumna];
            if (columna.SortMode != DataGridViewColumnSortMode.Programmatic || !(dgv.DataSource is IList actual))
            {
                return;
            }

            var estado = estados.GetOrCreateValue(dgv);
            estado.Descendente = estado.Propiedad == columna.DataPropertyName && !estado.Descendente;
            estado.Propiedad = columna.DataPropertyName;
            estado.Columna = columna;

            Type tipo = TipoElemento(actual);
            if (tipo == null)
            {
                return;
            }

            object seleccionado = dgv.CurrentRow?.DataBoundItem;
            int primeraVisible = Math.Max(0, dgv.FirstDisplayedScrollingRowIndex);
            var ordenados = Ordenar(actual.Cast<object>(), tipo, estado);

            var nueva = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(tipo));
            foreach (var item in ordenados)
            {
                nueva.Add(item);
            }
            dgv.DataSource = nueva;

            // Mantener la fila que estaba elegida
            dgv.ClearSelection();
            foreach (DataGridViewRow fila in dgv.Rows)
            {
                if (ReferenceEquals(fila.DataBoundItem, seleccionado))
                {
                    var celda = fila.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
                    if (celda != null) dgv.CurrentCell = celda;
                    fila.Selected = true;
                    break;
                }
            }
            if (dgv.Rows.Count > 0 && dgv.CurrentRow == null)
            {
                dgv.FirstDisplayedScrollingRowIndex = Math.Min(primeraVisible, dgv.Rows.Count - 1);
            }
        }

        private static IEnumerable<object> Ordenar(IEnumerable<object> items, Type tipo, Estado estado)
        {
            var propiedad = tipo.GetProperty(estado.Propiedad);
            if (propiedad == null)
            {
                return items;
            }
            var comparador = new Comparador();
            return estado.Descendente
                ? items.OrderByDescending(o => propiedad.GetValue(o), comparador)
                : items.OrderBy(o => propiedad.GetValue(o), comparador);
        }

        private static Type TipoElemento(IList lista)
        {
            var tipo = lista.GetType();
            if (tipo.IsGenericType)
            {
                return tipo.GetGenericArguments()[0];
            }
            return lista.Count > 0 ? lista[0].GetType() : null;
        }

        private static void MostrarFlecha(DataGridView dgv)
        {
            if (estados.TryGetValue(dgv, out var estado) && estado.Columna != null && dgv.Columns.Contains(estado.Columna))
            {
                estado.Columna.HeaderCell.SortGlyphDirection =
                    estado.Descendente ? SortOrder.Descending : SortOrder.Ascending;
            }
        }

        /// <summary>Vacíos al final; texto sin distinguir mayúsculas; números y fechas por valor.</summary>
        private class Comparador : IComparer<object>
        {
            public int Compare(object a, object b)
            {
                bool vacioA = a == null || (a is string sa && sa.Length == 0);
                bool vacioB = b == null || (b is string sb && sb.Length == 0);
                if (vacioA || vacioB)
                {
                    return vacioA == vacioB ? 0 : (vacioA ? 1 : -1);
                }
                if (a is string textoA && b is string textoB)
                {
                    return string.Compare(textoA, textoB, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
                }
                if (a is IComparable comparable && a.GetType() == b.GetType())
                {
                    return comparable.CompareTo(b);
                }
                return string.Compare(a.ToString(), b.ToString(), StringComparison.CurrentCultureIgnoreCase);
            }
        }
    }
}
