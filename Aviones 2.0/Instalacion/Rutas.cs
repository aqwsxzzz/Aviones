using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Aviones_2._0.Instalacion
{
    /// <summary>Dónde vive el programa y sus datos en la computadora del usuario.</summary>
    public static class Rutas
    {
        private static readonly string Local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>Carpeta del programa instalado: del usuario, así se actualiza sin pedir permisos.</summary>
        public static string Programa => Path.Combine(Local, "Programs", "Aviones");

        /// <summary>Archivos de la base de datos.</summary>
        public static string Datos => Path.Combine(Local, "Aviones", "Datos");

        /// <summary>Copias de seguridad de la base.</summary>
        public static string Copias => Path.Combine(Local, "Aviones", "Copias");

        public static string Ejecutable => Application.ExecutablePath;

        public static string CarpetaActual => Path.GetDirectoryName(Application.ExecutablePath);

        /// <summary>Versión de este programa (mayor.menor.revisión).</summary>
        public static Version Version
        {
            get
            {
                var v = Assembly.GetExecutingAssembly().GetName().Version;
                return new Version(v.Major, v.Minor, Math.Max(0, v.Build));
            }
        }

        public static bool MismaCarpeta(string a, string b) =>
            string.Equals(Path.GetFullPath(a).TrimEnd('\\'), Path.GetFullPath(b).TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);
    }
}
