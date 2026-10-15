using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Aviones_2._0.Data;
using Aviones_2._0.Instalacion;

namespace Aviones_2._0
{
    internal static class Program
    {
        /// <summary>
        /// Al abrir: instala (la primera vez), busca actualizaciones, verifica que esté LocalDB
        /// y pone la base al día con las migraciones. Recién entonces abre el menú.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Descargas.PrepararConexiones();
            Actualizador.LimpiarAnterior();

#if !DEBUG
            // Al compilar para probar (Debug) se usa la carpeta del proyecto y no se busca en GitHub
            if (Instalador.OfrecerInstalacion() || Actualizador.OfrecerActualizacion())
            {
                return; // se abrió el programa instalado o la versión nueva
            }
#endif

            if (!Requisitos.Verificar(Db.ConnectionString) || !PrepararBase())
            {
                return;
            }
            Application.Run(new PantallaPrincipal());
        }

        /// <summary>Crea la base si es la primera vez y le aplica los cambios de esta versión.</summary>
        private static bool PrepararBase()
        {
            bool localDb = Requisitos.UsaLocalDb(Db.ConnectionString);
            using (var progreso = new frmProgreso("Preparando la base de datos..."))
            {
                progreso.Show();
                Application.DoEvents();
                try
                {
                    Migrador.Actualizar(Db.ConnectionString,
                        carpetaDatos: localDb ? Rutas.Datos : null,
                        carpetaCopias: localDb ? Rutas.Copias : null);
                    return true;
                }
                catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException)
                {
                    progreso.Hide();
                    MessageBox.Show(
                        "No se pudo abrir la base de datos:\n\n" + ex.Message +
                        (localDb ? "\n\nSi el problema sigue, reinicie la computadora y vuelva a abrir el programa." : ""),
                        "Aviones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
