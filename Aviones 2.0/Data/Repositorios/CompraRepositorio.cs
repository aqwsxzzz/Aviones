using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    /// <summary>Stock crítico y pedidos de compra.</summary>
    public class CompraRepositorio
    {
        private const string SelectPedido = @"
            SELECT P.IdPedido, P.IdMaterial, F.Nombre AS Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo,
                   T.Nombre AS TipoUnidad, P.Cantidad, P.FechaPedido, P.Nota, P.Estado,
                   S.Disponible,
                   (SELECT TOP 1 L.ValorUnitario FROM dbo.Lotes L
                     WHERE L.IdMaterial = P.IdMaterial ORDER BY L.FechaIngreso DESC, L.IdLote DESC) AS UltimoPrecio
            FROM dbo.PedidosCompra P
            JOIN dbo.Materiales M        ON M.IdMaterial = P.IdMaterial
            JOIN dbo.Familias F          ON F.IdFamilia = M.IdFamilia
            JOIN dbo.TiposUnidad T       ON T.IdTipoUnidad = M.IdTipoUnidad
            JOIN dbo.vMaterialesStock S  ON S.IdMaterial = P.IdMaterial";

        /// <summary>
        /// Materiales con disponible (nuevo + usado) menor o igual que su mínimo propio,
        /// o que el mínimo general si no tienen uno.
        /// </summary>
        public List<StockCritico> ListarStockCritico(decimal minimoGeneral)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<StockCritico>(@"
                    SELECT S.*, ISNULL(S.StockMinimo, @minimoGeneral) AS MinimoAplicado
                    FROM dbo.vMaterialesStock S
                    WHERE S.Disponible <= ISNULL(S.StockMinimo, @minimoGeneral)
                    ORDER BY S.Familia, S.Grupo, S.Caracteristica, S.Medidas",
                    new { minimoGeneral }).ToList();
            }
        }

        /// <summary>Lo que falta comprar, del pedido más viejo al más nuevo.</summary>
        public List<PedidoCompra> ListarPendientes()
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<PedidoCompra>(SelectPedido + " WHERE P.Estado = 'P' ORDER BY P.FechaPedido, P.IdPedido").ToList();
            }
        }

        public List<PedidoCompra> ListarPendientesDe(int idMaterial)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<PedidoCompra>(SelectPedido + " WHERE P.Estado = 'P' AND P.IdMaterial = @idMaterial ORDER BY P.FechaPedido",
                    new { idMaterial }).ToList();
            }
        }

        public PedidoCompra Obtener(int idPedido)
        {
            using (var cn = Db.Abrir())
            {
                return cn.QuerySingleOrDefault<PedidoCompra>(SelectPedido + " WHERE P.IdPedido = @idPedido", new { idPedido });
            }
        }

        public int CrearPedido(int idMaterial, decimal cantidad, string nota)
        {
            Validar(cantidad);
            using (var cn = Db.Abrir())
            {
                return cn.ExecuteScalar<int>(@"
                    INSERT INTO dbo.PedidosCompra (IdMaterial, Cantidad, Nota) VALUES (@idMaterial, @cantidad, @nota);
                    SELECT CAST(SCOPE_IDENTITY() AS int);",
                    new { idMaterial, cantidad, nota = Limpiar(nota) });
            }
        }

        /// <summary>Corrige un pedido pendiente.</summary>
        public void ModificarPedido(int idPedido, decimal cantidad, string nota)
        {
            Validar(cantidad);
            using (var cn = Db.Abrir())
            {
                int filas = cn.Execute(
                    "UPDATE dbo.PedidosCompra SET Cantidad = @cantidad, Nota = @nota WHERE IdPedido = @idPedido AND Estado = 'P'",
                    new { idPedido, cantidad, nota = Limpiar(nota) });
                if (filas == 0)
                {
                    throw new ReglaNegocioException("El pedido ya no está pendiente.");
                }
            }
        }

        /// <summary>Saca un pedido pendiente de la lista (ya no hace falta comprarlo).</summary>
        public void EliminarPedido(int idPedido)
        {
            using (var cn = Db.Abrir())
            {
                cn.Execute("DELETE FROM dbo.PedidosCompra WHERE IdPedido = @idPedido AND Estado = 'P'", new { idPedido });
            }
        }

        private static void Validar(decimal cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ReglaNegocioException("La cantidad a comprar tiene que ser mayor que cero.");
            }
        }

        private static string Limpiar(string texto) => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
    }
}
