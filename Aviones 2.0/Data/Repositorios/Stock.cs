using System.Data;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    /// <summary>Movimientos de stock sobre un lote. Siempre dentro de la transacción de quien llama.</summary>
    internal static class Stock
    {
        public static void Descontar(IDbConnection cn, IDbTransaction tx, int idLote, decimal cantidad)
        {
            ValidarCantidad(cantidad);
            int filas = cn.Execute(
                "UPDATE dbo.Lotes SET Disponible = Disponible - @cantidad WHERE IdLote = @idLote AND Disponible >= @cantidad",
                new { idLote, cantidad }, tx);
            if (filas == 0)
            {
                throw new StockInsuficienteException(idLote, cantidad);
            }
        }

        public static void Devolver(IDbConnection cn, IDbTransaction tx, int idLote, decimal cantidad)
        {
            ValidarCantidad(cantidad);
            int filas = cn.Execute(
                "UPDATE dbo.Lotes SET Disponible = Disponible + @cantidad WHERE IdLote = @idLote AND Disponible + @cantidad <= Cantidad",
                new { idLote, cantidad }, tx);
            if (filas == 0)
            {
                throw new ReglaNegocioException($"No se puede devolver {cantidad} al lote {idLote}: superaría la cantidad ingresada.");
            }
        }

        public static void ValidarCantidad(decimal cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ReglaNegocioException("La cantidad tiene que ser mayor que cero.");
            }
        }
    }
}
