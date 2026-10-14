using System.Windows.Forms;

namespace Aviones_2._0.Common
{
    /// <summary>Medidas de la pantalla, para ubicar los botones del menú principal.</summary>
    public class Componentes
    {
        public int puntoMedioVertical { get; set; }
        public int puntoMedioHorizontal { get; set; }

        public Componentes()
        {
            puntoMedioHorizontal = Screen.PrimaryScreen.Bounds.Width / 2;
            puntoMedioVertical = Screen.PrimaryScreen.Bounds.Height / 2;
        }
    }
}
