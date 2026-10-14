using System.Data;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    /// <summary>
    /// Movimientos de stock sobre un lote, separados en nuevo y usado.
    /// Siempre dentro de la transacción de quien llama.
    /// </summary>
    internal static class Stock
    {
        /// <summary>Saca material del stock nuevo o usado del lote.</summary>
        public static void Descontar(IDbConnection cn, IDbTransaction tx, int idLote, string estado, decimal cantidad)
        {
            ValidarCantidad(cantidad);
            string columna = Columna(estado);
            int filas = cn.Execute(
                $"UPDATE dbo.Lotes SET {columna} = {columna} - @cantidad WHERE IdLote = @idLote AND {columna} >= @cantidad",
                new { idLote, cantidad }, tx);
            if (filas == 0)
            {
                throw new StockInsuficienteException(idLote, cantidad);
            }
        }

        /// <summary>
        /// Material que sale de un modelo (hasta <paramref name="saliente"/>): una parte puede volver al stock
        /// nuevo, otra al usado, y el resto se descarta. Lo que salió del stock usado no puede volver como nuevo.
        /// </summary>
        public static void Devolver(IDbConnection cn, IDbTransaction tx, int idLote, string estadoOrigen,
            Devolucion devolucion, decimal saliente)
        {
            if (devolucion.Nuevo < 0 || devolucion.Usado < 0)
            {
                throw new ReglaNegocioException("Las cantidades que vuelven no pueden ser negativas.");
            }
            if (devolucion.Total > saliente)
            {
                throw new ReglaNegocioException($"No puede volver más de lo que sale ({saliente}).");
            }
            if (devolucion.Nuevo > 0 && estadoOrigen == EstadosMaterial.Usado)
            {
                throw new ReglaNegocioException("Material que salió del stock usado no puede volver como nuevo.");
            }
            if (devolucion.Total == 0)
            {
                return;
            }

            int filas = cn.Execute(
                @"UPDATE dbo.Lotes SET DisponibleNuevo = DisponibleNuevo + @nuevo, DisponibleUsado = DisponibleUsado + @usado
                  WHERE IdLote = @idLote AND DisponibleNuevo + DisponibleUsado + @nuevo + @usado <= Cantidad",
                new { idLote, nuevo = devolucion.Nuevo, usado = devolucion.Usado }, tx);
            if (filas == 0)
            {
                throw new ReglaNegocioException($"No se puede devolver {devolucion.Total} al lote {idLote}: superaría la cantidad ingresada.");
            }
        }

        public static void ValidarCantidad(decimal cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ReglaNegocioException("La cantidad tiene que ser mayor que cero.");
            }
        }

        public static void ValidarEstado(string estado)
        {
            if (estado != EstadosMaterial.Nuevo && estado != EstadosMaterial.Usado)
            {
                throw new ReglaNegocioException("El material tiene que salir del stock nuevo o del usado.");
            }
        }

        private static string Columna(string estado)
        {
            ValidarEstado(estado);
            return estado == EstadosMaterial.Usado ? "DisponibleUsado" : "DisponibleNuevo";
        }
    }
}
