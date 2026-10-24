using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Aviones_2._0.Forms.Comun;

namespace Aviones_2._0.Common
{
    /// <summary>Un cambio de una versión: lo que se pidió, lo que se hizo y en qué quedó distinto.</summary>
    public class Novedad
    {
        public string Pedido { get; set; }
        public string Hecho { get; set; }

        /// <summary>Si quedó distinto a lo pedido, por qué. null si se hizo tal cual.</summary>
        public string Diferencia { get; set; }
    }

    public class VersionNovedades
    {
        public Version Version { get; set; }
        public List<Novedad> Cambios { get; } = new List<Novedad>();
    }

    /// <summary>
    /// Las novedades de cada versión (Novedades.txt, dentro del programa). Se muestran solas
    /// al abrir el programa después de una actualización, hasta que se marca "No volver a mostrar".
    /// </summary>
    public static class Novedades
    {
        /// <summary>La última versión cuyas novedades se marcaron como vistas en esta computadora.</summary>
        private static string ArchivoVistas => Path.Combine(Path.GetDirectoryName(Instalacion.Rutas.Datos), "novedades_vistas.txt");

        public static List<VersionNovedades> Todas()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Novedades.txt"))
            using (var lector = new StreamReader(stream))
            {
                return Leer(lector.ReadToEnd());
            }
        }

        /// <summary>
        /// "= versión" empieza una versión; "* " lo pedido; "> " lo hecho; "! " en qué quedó distinto.
        /// Las demás líneas se ignoran. De la más nueva a la más vieja.
        /// </summary>
        public static List<VersionNovedades> Leer(string texto)
        {
            var versiones = new List<VersionNovedades>();
            VersionNovedades actual = null;
            Novedad cambio = null;
            foreach (string renglon in texto.Split('\n').Select(r => r.TrimEnd('\r')))
            {
                if (renglon.StartsWith("= ") && Version.TryParse(renglon.Substring(2).Trim(), out var numero))
                {
                    versiones.Add(actual = new VersionNovedades { Version = numero });
                    cambio = null;
                }
                else if (actual != null && renglon.StartsWith("* "))
                {
                    actual.Cambios.Add(cambio = new Novedad { Pedido = renglon.Substring(2).Trim() });
                }
                else if (cambio != null && renglon.StartsWith("> "))
                {
                    cambio.Hecho = renglon.Substring(2).Trim();
                }
                else if (cambio != null && renglon.StartsWith("! "))
                {
                    cambio.Diferencia = renglon.Substring(2).Trim();
                }
            }
            return versiones.OrderByDescending(v => v.Version).ToList();
        }

        /// <summary>
        /// Las versiones a mostrar al abrir: las posteriores a la última vista, hasta la instalada.
        /// Si nunca se vio ninguna, sólo la instalada.
        /// </summary>
        public static List<VersionNovedades> Pendientes(IEnumerable<VersionNovedades> todas, Version instalada, Version vista) =>
            todas.Where(v => v.Version <= instalada && (vista == null ? v.Version == instalada : v.Version > vista))
                 .Where(v => v.Cambios.Count > 0)
                 .ToList();

        /// <summary>Al abrir el programa: muestra lo nuevo de esta versión si todavía no se marcó como visto.</summary>
        public static void MostrarSiHay(IWin32Window dueño)
        {
            List<VersionNovedades> pendientes;
            try
            {
                pendientes = Pendientes(Todas(), Instalacion.Rutas.Version, LeerVista());
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                return;   // no se pudo leer: no molesta
            }
            if (pendientes.Count == 0)
            {
                return;
            }
            using (var ventana = new frmNovedades(pendientes, conCasilla: true))
            {
                ventana.ShowDialog(dueño);
                if (ventana.NoVolverAMostrar)
                {
                    GuardarVista(Instalacion.Rutas.Version);
                }
            }
        }

        /// <summary>Desde el menú: todas las versiones.</summary>
        public static void MostrarTodas(IWin32Window dueño)
        {
            using (var ventana = new frmNovedades(Todas(), conCasilla: false))
            {
                ventana.ShowDialog(dueño);
            }
        }

        private static Version LeerVista()
        {
            if (!File.Exists(ArchivoVistas))
            {
                return null;
            }
            return Version.TryParse(File.ReadAllText(ArchivoVistas).Trim(), out var v) ? v : null;
        }

        private static void GuardarVista(Version version)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ArchivoVistas));
                File.WriteAllText(ArchivoVistas, version.ToString());
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException) { }
        }
    }
}
