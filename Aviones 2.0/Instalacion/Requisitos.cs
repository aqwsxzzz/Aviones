using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Aviones_2._0.Instalacion
{
    /// <summary>
    /// Lo que tiene que estar instalado para que el programa funcione. La base de datos usa
    /// SQL Server Express LocalDB (gratis, de Microsoft); si falta, se ofrece bajarlo e instalarlo.
    /// </summary>
    public static class Requisitos
    {
        private static readonly string[] InstaladoresLocalDb =
        {
            // SQL Server 2025 y, si no responde, 2022 (descargas oficiales de Microsoft)
            "https://download.microsoft.com/download/dea8c210-c44a-4a9d-9d80-0c81578860c5/ENU/SqlLocalDB.msi",
            "https://download.microsoft.com/download/3/8/d/38de7036-2433-4207-8eae-06e247e17b25/SqlLocalDB.msi"
        };

        private const string PaginaLocalDb = "https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb";

        /// <summary>Verifica (e instala si hace falta) lo necesario para esta cadena de conexión. false: no se puede seguir.</summary>
        public static bool Verificar(string connectionString)
        {
            if (!UsaLocalDb(connectionString) || LocalDbInstalado())
            {
                return true;
            }

            var respuesta = MessageBox.Show(
                "Para guardar sus datos, Aviones necesita \"SQL Server Express LocalDB\", un programa gratuito de Microsoft (unos 65 MB).\n\n" +
                "No está instalado en esta computadora. ¿Descargarlo e instalarlo ahora?\n\n" +
                "Windows le va a pedir permiso para instalarlo: responda que Sí.",
                "Aviones - Falta un programa", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (respuesta != DialogResult.Yes)
            {
                Instrucciones();
                return false;
            }

            if (!InstalarLocalDb())
            {
                return false;
            }
            if (!LocalDbInstalado())
            {
                MessageBox.Show("La instalación de LocalDB no terminó bien.", "Aviones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Instrucciones();
                return false;
            }
            return true;
        }

        public static bool UsaLocalDb(string connectionString) =>
            new SqlConnectionStringBuilder(connectionString).DataSource.TrimStart().StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase);

        public static bool LocalDbInstalado()
        {
            using (var raiz = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var clave = raiz.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions"))
            {
                return clave != null && clave.GetSubKeyNames().Length > 0;
            }
        }

        private static bool InstalarLocalDb()
        {
            string carpeta = Path.Combine(Path.GetTempPath(), "Aviones_LocalDB");
            Directory.CreateDirectory(carpeta);
            string msi = Path.Combine(carpeta, "SqlLocalDB.msi");

            using (var progreso = new frmProgreso("Instalando SQL Server Express LocalDB"))
            {
                progreso.Show();
                bool bajado = false;
                foreach (string url in InstaladoresLocalDb)
                {
                    if (File.Exists(msi))
                    {
                        File.Delete(msi);
                    }
                    if (Descargas.Bajar(url, msi, progreso, "SQL Server Express LocalDB"))
                    {
                        bajado = true;
                        break;
                    }
                }
                if (!bajado)
                {
                    Instrucciones();
                    return false;
                }

                if (!FirmadoPorMicrosoft(msi))
                {
                    MessageBox.Show(progreso, "El archivo descargado no tiene la firma de Microsoft; no se instala por seguridad.",
                        "Aviones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                progreso.Mensaje("Instalando... (Windows va a pedir permiso)");
                progreso.Avance(null);
                try
                {
                    using (var proceso = Process.Start(new ProcessStartInfo("msiexec.exe", $"/i \"{msi}\" /qb IACCEPTSQLLOCALDBLICENSETERMS=YES")
                    {
                        UseShellExecute = true,
                        Verb = "runas"
                    }))
                    {
                        while (!proceso.WaitForExit(100))
                        {
                            Application.DoEvents();
                        }
                        // 0 = bien, 3010 = bien pero pide reiniciar
                        if (proceso.ExitCode != 0 && proceso.ExitCode != 3010)
                        {
                            MessageBox.Show(progreso, $"El instalador de LocalDB terminó con el código {proceso.ExitCode}.",
                                "Aviones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Instrucciones();
                            return false;
                        }
                    }
                }
                catch (Exception ex) when (Descargas.EsPermisoNegado(ex))
                {
                    MessageBox.Show(progreso, "Sin el permiso de Windows no se puede instalar LocalDB.", "Aviones",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                finally
                {
                    try { File.Delete(msi); } catch (IOException) { }
                }
            }
            return true;
        }

        /// <summary>
        /// Control básico de que el instalador es de Microsoft (la descarga ya viene por HTTPS de su sitio;
        /// Windows además muestra el editor verificado al pedir permiso).
        /// </summary>
        private static bool FirmadoPorMicrosoft(string archivo)
        {
            try
            {
                return X509Certificate.CreateFromSignedFile(archivo).Subject.Contains("O=Microsoft Corporation");
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return false;
            }
        }

        private static void Instrucciones()
        {
            var respuesta = MessageBox.Show(
                "Para instalarlo a mano:\n\n" +
                "1. Busque en internet \"SQL Server Express LocalDB\" o abra la página de Microsoft.\n" +
                "2. Descargue e instale \"SqlLocalDB.msi\".\n" +
                "3. Vuelva a abrir Aviones.\n\n" +
                "¿Abrir la página de Microsoft ahora?",
                "Aviones - Instalar LocalDB", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (respuesta == DialogResult.Yes)
            {
                Process.Start(new ProcessStartInfo(PaginaLocalDb) { UseShellExecute = true });
            }
        }
    }
}
