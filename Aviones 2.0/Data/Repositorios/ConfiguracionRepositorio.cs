using System.Globalization;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    /// <summary>Valores generales del programa.</summary>
    public class ConfiguracionRepositorio
    {
        private const string ClaveStockMinimo = "StockMinimoGeneral";

        /// <summary>Mínimo para los materiales que no tienen uno propio. Si no está configurado, 1.</summary>
        public decimal ObtenerStockMinimoGeneral()
        {
            using (var cn = Db.Abrir())
            {
                string valor = cn.ExecuteScalar<string>("SELECT Valor FROM dbo.Configuracion WHERE Clave = @clave",
                    new { clave = ClaveStockMinimo });
                return decimal.TryParse(valor, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal minimo) ? minimo : 1m;
            }
        }

        public void GuardarStockMinimoGeneral(decimal minimo)
        {
            if (minimo < 0)
            {
                throw new ReglaNegocioException("El stock mínimo no puede ser negativo.");
            }
            using (var cn = Db.Abrir())
            {
                cn.Execute(@"
                    UPDATE dbo.Configuracion SET Valor = @valor WHERE Clave = @clave;
                    IF @@ROWCOUNT = 0 INSERT INTO dbo.Configuracion (Clave, Valor) VALUES (@clave, @valor);",
                    new { clave = ClaveStockMinimo, valor = minimo.ToString(CultureInfo.InvariantCulture) });
            }
        }
    }
}
