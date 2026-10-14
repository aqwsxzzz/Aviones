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
        /// Material que sale de un modelo: según el destino vuelve al stock nuevo, al usado, o se descarta.
        /// Lo que salió del stock usado no puede volver como nuevo.
        /// </summary>
        public static void Devolver(IDbConnection cn, IDbTransaction tx, int idLote, string estadoOrigen,
            DestinoMaterial destino, decimal cantidad)
        {
            ValidarCantidad(cantidad);
            if (destino == DestinoMaterial.Descartar)
            {
                return;
            }
            if (destino == DestinoMaterial.Nuevo && estadoOrigen == EstadosMaterial.Usado)
            {
                throw new ReglaNegocioException("Material que salió del stock usado no puede volver como nuevo.");
            }

            string columna = Columna(destino == DestinoMaterial.Nuevo ? EstadosMaterial.Nuevo : EstadosMaterial.Usado);
            int filas = cn.Execute(
                $@"UPDATE dbo.Lotes SET {columna} = {columna} + @cantidad
                   WHERE IdLote = @idLote AND DisponibleNuevo + DisponibleUsado + @cantidad <= Cantidad",
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
