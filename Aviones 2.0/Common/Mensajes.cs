using System;
using System.Windows.Forms;
using Aviones_2._0.Data;

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
                MessageBox.Show(dueño, ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(dueño, "Ocurrió un error inesperado:\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Aviso(IWin32Window dueño, string mensaje) =>
            MessageBox.Show(dueño, mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static bool Confirmar(IWin32Window dueño, string pregunta, string titulo = "Confirmar") =>
            MessageBox.Show(dueño, pregunta, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            == DialogResult.Yes;

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
