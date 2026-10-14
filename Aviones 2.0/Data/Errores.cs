using System;
using System.Data.SqlClient;

namespace Aviones_2._0.Data
{
    /// <summary>
    /// Una operación no se puede hacer por una regla del negocio.
    /// El mensaje está pensado para mostrarse tal cual al usuario.
    /// </summary>
    public class ReglaNegocioException : Exception
    {
        public ReglaNegocioException(string mensaje) : base(mensaje) { }
    }

    public class StockInsuficienteException : ReglaNegocioException
    {
        public int IdLote { get; }
        public decimal Pedido { get; }

        public StockInsuficienteException(int idLote, decimal pedido)
            : base($"No hay stock suficiente en el lote {idLote} para usar {pedido}.")
        {
            IdLote = idLote;
            Pedido = pedido;
        }
    }

    internal static class SqlErrores
    {
        /// <summary>Violación de UNIQUE o PRIMARY KEY.</summary>
        public static bool EsDuplicado(SqlException ex) => ex.Number == 2627 || ex.Number == 2601;

        /// <summary>Violación de FOREIGN KEY o CHECK.</summary>
        public static bool EsRestriccion(SqlException ex) => ex.Number == 547;
    }
}
