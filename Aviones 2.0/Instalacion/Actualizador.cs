using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Aviones_2._0.Instalacion
{
    /// <summary>
    /// Al abrir, compara esta versión con la última publicada en GitHub (Releases) y, si hay una nueva,
    /// ofrece bajarla y reemplazar el programa. Sin internet sigue de largo.
    /// </summary>
    public static class Actualizador
    {
        private const string Repositorio = "aqwsxzzz/Aviones";
        private const string ExtensionVieja = ".old";

        public class Publicacion
        {
            public Version Version { get; set; }
            public string Notas { get; set; }
            public string UrlZip { get; set; }
        }

        /// <summary>Borra lo que quedó de la actualización anterior.</summary>
        public static void LimpiarAnterior()
        {
            try
            {
                foreach (string viejo in Directory.GetFiles(Rutas.CarpetaActual, "*" + ExtensionVieja, SearchOption.AllDirectories))
                {
                    try { File.Delete(viejo); } catch (IOException) { } catch (UnauthorizedAccessException) { }
                }
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }

        /// <summary>
        /// Si hay versión nueva, pregunta y actualiza. true: se abrió la versión nueva y este proceso tiene que terminar.
        /// </summary>
        public static bool OfrecerActualizacion()
        {
            var nueva = BuscarNueva();
            if (nueva == null)
            {
                return false;
            }

            string notas = string.IsNullOrWhiteSpace(nueva.Notas) ? "" : "\n\nNovedades:\n" + Recortar(nueva.Notas.Trim(), 1200);
            var respuesta = MessageBox.Show(
                $"Hay una versión nueva de Aviones: {nueva.Version} (usted tiene la {Rutas.Version})." + notas +
                "\n\n¿Actualizar ahora? Tarda unos segundos y el programa se vuelve a abrir solo.",
                "Aviones - Actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            return respuesta == DialogResult.Yes && Actualizar(nueva);
        }

        /// <summary>La última versión publicada si es más nueva que esta; null si no hay o no hay internet.</summary>
        public static Publicacion BuscarNueva()
        {
            try
            {
                var pedido = (HttpWebRequest)WebRequest.Create($"https://api.github.com/repos/{Repositorio}/releases/latest");
                pedido.UserAgent = "Aviones/" + Rutas.Version;
                pedido.Accept = "application/vnd.github+json";
                pedido.Timeout = 5000;
                pedido.ReadWriteTimeout = 5000;
                string json;
                using (var respuesta = pedido.GetResponse())
                using (var lector = new StreamReader(respuesta.GetResponseStream()))
                {
                    json = lector.ReadToEnd();
                }

                var datos = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(json);
                if (!Version.TryParse(((string)datos["tag_name"]).TrimStart('v', 'V'), out var version) || version <= Rutas.Version)
                {
                    return null;
                }
                string zip = ((ArrayList)datos["assets"]).Cast<Dictionary<string, object>>()
                    .Where(a => ((string)a["name"]).EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                    .Select(a => (string)a["browser_download_url"])
                    .FirstOrDefault();
                if (zip == null)
                {
                    return null;
                }
                return new Publicacion { Version = version, Notas = datos.TryGetValue("body", out var body) ? body as string : null, UrlZip = zip };
            }
            catch (Exception ex) when (ex is WebException || ex is IOException || ex is InvalidOperationException
                                       || ex is ArgumentException || ex is InvalidCastException || ex is KeyNotFoundException)
            {
                return null; // sin internet o GitHub no responde: se sigue con esta versión
            }
        }

        private static bool Actualizar(Publicacion nueva)
        {
            string temporal = Path.Combine(Path.GetTempPath(), "Aviones_actualizacion");
            string zip = Path.Combine(temporal, "Aviones.zip");
            string extraido = Path.Combine(temporal, "nuevo");

            using (var progreso = new frmProgreso("Actualizando Aviones"))
            {
                progreso.Show();
                try
                {
                    if (Directory.Exists(temporal))
                    {
                        Directory.Delete(temporal, true);
                    }
                    Directory.CreateDirectory(temporal);
                    if (!Descargas.Bajar(nueva.UrlZip, zip, progreso, $"la versión {nueva.Version}"))
                    {
                        return false;
                    }

                    progreso.Mensaje("Instalando la versión nueva...");
                    progreso.Avance(null);
                    ZipFile.ExtractToDirectory(zip, extraido);
                    if (!File.Exists(Path.Combine(extraido, Path.GetFileName(Rutas.Ejecutable))))
                    {
                        throw new InvalidDataException("El archivo descargado no contiene el programa.");
                    }
                    Reemplazar(extraido, Rutas.CarpetaActual);
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is InvalidDataException)
                {
                    MessageBox.Show(progreso, "No se pudo actualizar; se sigue con la versión actual.\n\n" + ex.Message,
                        "Aviones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                finally
                {
                    try { Directory.Delete(temporal, true); } catch (IOException) { } catch (UnauthorizedAccessException) { }
                }
            }

            Process.Start(new ProcessStartInfo(Rutas.Ejecutable) { WorkingDirectory = Rutas.CarpetaActual, UseShellExecute = true });
            return true;
        }

        /// <summary>
        /// Pone los archivos nuevos en lugar de los actuales. Windows deja renombrar un programa abierto,
        /// así que cada archivo actual pasa a ".old" (se borra en la próxima apertura). Si algo falla, se deshace.
        /// </summary>
        private static void Reemplazar(string origen, string destino)
        {
            var hechos = new List<(string Destino, string Viejo)>();
            try
            {
                foreach (string nuevo in Directory.GetFiles(origen, "*", SearchOption.AllDirectories))
                {
                    string relativo = nuevo.Substring(origen.Length).TrimStart('\\');
                    string final = Path.Combine(destino, relativo);
                    Directory.CreateDirectory(Path.GetDirectoryName(final));
                    string viejo = null;
                    if (File.Exists(final))
                    {
                        viejo = final + ExtensionVieja;
                        if (File.Exists(viejo))
                        {
                            File.Delete(viejo);
                        }
                        File.Move(final, viejo);
                    }
                    hechos.Add((final, viejo));
                    File.Copy(nuevo, final);
                }
            }
            catch
            {
                foreach (var (final, viejo) in Enumerable.Reverse(hechos))
                {
                    try
                    {
                        if (File.Exists(final)) File.Delete(final);
                        if (viejo != null) File.Move(viejo, final);
                    }
                    catch (IOException) { }
                    catch (UnauthorizedAccessException) { }
                }
                throw;
            }
        }

        private static string Recortar(string texto, int largo) => texto.Length <= largo ? texto : texto.Substring(0, largo) + "...";
    }
}
