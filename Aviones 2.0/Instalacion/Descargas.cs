using System;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aviones_2._0.Instalacion
{
    internal static class Descargas
    {
        /// <summary>GitHub y Microsoft sólo aceptan TLS 1.2 o más nuevo.</summary>
        public static void PrepararConexiones() => ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

        public static WebClient Cliente()
        {
            var cliente = new WebClient();
            cliente.Headers[HttpRequestHeader.UserAgent] = "Aviones/" + Rutas.Version;
            return cliente;
        }

        /// <summary>Descarga un archivo mostrando el avance. Devuelve false si falló (ya avisó).</summary>
        public static bool Bajar(string url, string destino, frmProgreso progreso, string que)
        {
            using (var cliente = Cliente())
            {
                cliente.DownloadProgressChanged += (s, e) =>
                {
                    progreso.Mensaje($"Descargando {que}... {e.BytesReceived / 1048576.0:0.0} MB"
                        + (e.TotalBytesToReceive > 0 ? $" de {e.TotalBytesToReceive / 1048576.0:0.0} MB" : ""));
                    progreso.Avance(e.TotalBytesToReceive > 0 ? e.ProgressPercentage : (int?)null);
                };
                var tarea = cliente.DownloadFileTaskAsync(new Uri(url), destino);
                while (!tarea.IsCompleted)
                {
                    Application.DoEvents();
                    Task.Delay(50).Wait();
                }
                if (tarea.IsFaulted)
                {
                    var error = tarea.Exception?.GetBaseException();
                    MessageBox.Show(progreso, $"No se pudo descargar {que}.\n\n{error?.Message}\n\nRevise la conexión a internet y vuelva a abrir el programa.",
                        "Descarga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
        }

        /// <summary>El usuario dijo que no en el aviso de permisos de Windows.</summary>
        public static bool EsPermisoNegado(Exception ex) => ex is Win32Exception w && w.NativeErrorCode == 1223;
    }
}
