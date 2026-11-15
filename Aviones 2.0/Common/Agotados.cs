using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Aviones_2._0.Data.Repositorios;

namespace Aviones_2._0.Common
{
    /// <summary>
    /// Aviso de los materiales que no se reponen y se quedaron sin stock, para decidir si se borran.
    /// Se toma lo disponible antes de un cambio (usar en un modelo, corregir o borrar un lote)
    /// y después se avisa sólo de los que tenían y quedaron en cero.
    /// </summary>
    public static class Agotados
    {
        /// <summary>Lo disponible ahora de cada material.</summary>
        public static Dictionary<int, decimal> Antes(IEnumerable<int> idsMaterial)
        {
            var materiales = new MaterialRepositorio();
            return idsMaterial.Distinct()
                .Select(id => materiales.ObtenerStock(id))
                .Where(m => m != null)
                .ToDictionary(m => m.IdMaterial, m => m.Disponible);
        }

        /// <summary>
        /// Avisa de los que tenían stock, no se reponen y quedaron en cero; si no se usan en ningún modelo,
        /// ofrece borrarlos. Devuelve si se borró alguno.
        /// </summary>
        public static bool Revisar(IWin32Window dueño, Dictionary<int, decimal> antes)
        {
            if (antes == null)
            {
                return false;
            }
            var materiales = new MaterialRepositorio();
            bool borrado = false;
            foreach (var id in antes.Where(a => a.Value > 0).Select(a => a.Key))
            {
                Mensajes.Intentar(dueño, () =>
                {
                    var m = materiales.ObtenerStock(id);
                    if (m == null || m.SeRepone || m.Disponible != 0)
                    {
                        return;
                    }
                    int modelos = materiales.ListarUsos(id).Select(u => u.IdModelo).Distinct().Count();
                    string aviso = $"Se terminó \"{m.Familia} {m.Descripcion}\", que está marcado como que no se repone.";
                    if (modelos > 0)
                    {
                        Mensajes.Informar(dueño, aviso + $"\n\nComo se usa en {modelos} {(modelos == 1 ? "modelo" : "modelos")}, queda en Materiales con stock 0 "
                            + "(si se elimina, sale también de esos modelos).", "Material agotado");
                    }
                    else if (Mensajes.Confirmar(dueño, aviso + "\n\n¿Eliminarlo de Materiales?", "Material agotado"))
                    {
                        materiales.Eliminar(id);
                        borrado = true;
                    }
                });
            }
            return borrado;
        }
    }
}
