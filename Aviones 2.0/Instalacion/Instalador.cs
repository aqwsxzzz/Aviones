using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Aviones_2._0.Instalacion
{
    /// <summary>
    /// La primera vez que se abre el programa desde la carpeta descargada, se copia a la carpeta
    /// del usuario (sin pedir permisos, así las actualizaciones también los evitan) y crea los accesos directos.
    /// </summary>
    public static class Instalador
    {
        /// <summary>Si corresponde, ofrece instalar. true: se abrió la copia instalada y este proceso tiene que terminar.</summary>
        public static bool OfrecerInstalacion()
        {
            if (Rutas.MismaCarpeta(Rutas.CarpetaActual, Rutas.Programa))
            {
                return false;
            }

            if (Rutas.CarpetaActual.StartsWith(Path.GetTempPath(), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Parece que abrió el programa directamente desde el archivo .zip.\n\n" +
                    "Primero descomprímalo: clic derecho sobre el .zip → \"Extraer todo...\", y abra \"Aviones 2.0.exe\" desde la carpeta extraída.",
                    "Aviones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            string instalado = Path.Combine(Rutas.Programa, Path.GetFileName(Rutas.Ejecutable));
            string pregunta = File.Exists(instalado)
                ? "Aviones ya está instalado en esta computadora. ¿Reemplazarlo con esta copia?\n\nSus datos no se tocan."
                : "¿Instalar Aviones en esta computadora?\n\nSe crea un acceso directo en el Escritorio y en el menú Inicio, " +
                  "y desde entonces el programa se actualiza solo.";
            var respuesta = MessageBox.Show(pregunta, "Aviones - Instalar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                return false;
            }

            try
            {
                CopiarCarpeta(Rutas.CarpetaActual, Rutas.Programa);
                CrearAccesos(instalado);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("No se pudo instalar:\n" + ex.Message + "\n\n¿Está abierto Aviones en otra ventana? Ciérrelo y pruebe otra vez.",
                    "Aviones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            MessageBox.Show("Listo. Aviones quedó instalado; puede borrar la carpeta y el .zip descargados.\n\n" +
                            "De ahora en más ábralo con el acceso directo \"Aviones\" del Escritorio.",
                "Aviones", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(new ProcessStartInfo(instalado) { WorkingDirectory = Rutas.Programa, UseShellExecute = true });
            return true;
        }

        private static void CopiarCarpeta(string origen, string destino)
        {
            Directory.CreateDirectory(destino);
            foreach (string archivo in Directory.GetFiles(origen).Where(a => !a.EndsWith(".old", StringComparison.OrdinalIgnoreCase)))
            {
                File.Copy(archivo, Path.Combine(destino, Path.GetFileName(archivo)), true);
            }
            foreach (string carpeta in Directory.GetDirectories(origen))
            {
                CopiarCarpeta(carpeta, Path.Combine(destino, Path.GetFileName(carpeta)));
            }
        }

        private static void CrearAccesos(string ejecutable)
        {
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string menu = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            foreach (string carpeta in new[] { escritorio, menu })
            {
                dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
                dynamic acceso = shell.CreateShortcut(Path.Combine(carpeta, "Aviones.lnk"));
                acceso.TargetPath = ejecutable;
                acceso.WorkingDirectory = Path.GetDirectoryName(ejecutable);
                acceso.Description = "Control de stock de materiales y aviones";
                acceso.Save();
            }
        }
    }
}
