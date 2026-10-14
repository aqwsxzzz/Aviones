using System.Globalization;

namespace Aviones_2._0.Common
{
    /// <summary>
    /// Lectura y formato de cantidades y precios. Se acepta coma o punto como separador decimal,
    /// así "1,5" y "1.5" valen lo mismo sin importar la configuración regional de Windows.
    /// </summary>
    public static class Numeros
    {
        public static bool TryLeer(string texto, out decimal valor)
        {
            valor = 0m;
            if (string.IsNullOrWhiteSpace(texto))
            {
                return false;
            }

            string t = texto.Trim().Replace(" ", "");
            int coma = t.LastIndexOf(',');
            int punto = t.LastIndexOf('.');
            if (coma >= 0 && punto >= 0)
            {
                // Con los dos separadores, el último es el decimal: "1.234,5" o "1,234.5"
                t = coma > punto ? t.Replace(".", "").Replace(',', '.') : t.Replace(",", "");
            }
            else
            {
                t = t.Replace(',', '.');
            }

            return decimal.TryParse(t, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out valor);
        }

        /// <summary>Lee un número opcional: vacío es null.</summary>
        public static bool TryLeerOpcional(string texto, out decimal? valor)
        {
            valor = null;
            if (string.IsNullOrWhiteSpace(texto))
            {
                return true;
            }
            bool ok = TryLeer(texto, out decimal leido);
            if (ok) valor = leido;
            return ok;
        }

        /// <summary>Cantidad sin ceros de más: 3.600 se muestra "3,6".</summary>
        public static string Cantidad(decimal valor) => valor.ToString("0.###", CultureInfo.CurrentCulture);

        public static string Cantidad(decimal? valor) => valor.HasValue ? Cantidad(valor.Value) : "";

        public static string Dinero(decimal valor) => valor.ToString("N2", CultureInfo.CurrentCulture);
    }
}
