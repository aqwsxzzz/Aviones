using System;
using System.Drawing;
using System.Windows.Forms;
using Aviones_2._0.Data;
using Aviones_2._0.Forms.Comun;

namespace Aviones_2._0.Common
{
    public static class Mensajes
    {
        /// <summary>
        /// Las reglas del negocio se muestran como aviso con su mensaje;
        /// cualquier otro error se muestra completo para poder informarlo.
        /// </summary>
        public static void Error(IWin32Window dueño, Exception ex)
        {
            if (ex is ReglaNegocioException)
            {
                Mostrar(dueño, "Aviso", ex.Message, frmMensaje.Tipo.Aviso, new[] { "ACEPTAR" }, 0, 0);
            }
            else
            {
                Mostrar(dueño, "Error", "Ocurrió un error inesperado:\n\n" + ex, frmMensaje.Tipo.Error, new[] { "ACEPTAR" }, 0, 0);
            }
        }

        public static void Aviso(IWin32Window dueño, string mensaje, string titulo = "Aviso") =>
            Mostrar(dueño, titulo, mensaje, frmMensaje.Tipo.Aviso, new[] { "ACEPTAR" }, 0, 0);

        public static void Informar(IWin32Window dueño, string mensaje, string titulo = "Aviones") =>
            Mostrar(dueño, titulo, mensaje, frmMensaje.Tipo.Informacion, new[] { "ACEPTAR" }, 0, 0);

        /// <summary>Sí o No; por defecto No, para que un Enter de más no confirme nada.</summary>
        public static bool Confirmar(IWin32Window dueño, string pregunta, string titulo = "Confirmar") =>
            Mostrar(dueño, titulo, pregunta, frmMensaje.Tipo.Pregunta, new[] { "SÍ", "NO" }, 1, 1) == 0;

        /// <summary>Pregunta con varias respuestas. Devuelve la elegida (0 la primera), o la última si se cancela.</summary>
        public static int Elegir(IWin32Window dueño, string pregunta, string titulo, params string[] respuestas) =>
            Mostrar(dueño, titulo, pregunta, frmMensaje.Tipo.Pregunta, respuestas, 0, respuestas.Length - 1);

        private static int Mostrar(IWin32Window dueño, string titulo, string texto, frmMensaje.Tipo tipo,
            string[] botones, int porDefecto, int cancelar)
        {
            using (var ventana = new frmMensaje(ColorDe(dueño), titulo, texto, tipo, botones, porDefecto, cancelar))
            {
                ventana.ShowDialog(dueño);
                return ventana.Elegido < 0 ? cancelar : ventana.Elegido;
            }
        }

        /// <summary>El color de fondo de la pantalla que muestra el mensaje.</summary>
        private static Color ColorDe(IWin32Window dueño)
        {
            var control = dueño == null ? null : Control.FromHandle(dueño.Handle);
            var form = control?.FindForm() ?? control as Form;
            return form?.BackColor ?? Color.Empty;
        }

        /// <summary>Ejecuta la acción mostrando el error si falla. Devuelve si salió bien.</summary>
        public static bool Intentar(IWin32Window dueño, Action accion)
        {
            try
            {
                accion();
                return true;
            }
            catch (Exception ex)
            {
                Error(dueño, ex);
                return false;
            }
        }
    }
}
