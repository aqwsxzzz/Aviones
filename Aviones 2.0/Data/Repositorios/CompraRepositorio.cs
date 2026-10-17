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
                   P.FechaCierre, P.CantidadRecibida, P.IdLote, P.PrecioEstimado,
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

        /// <summary>Último precio pagado por el material (del lote más reciente); null si nunca se compró.</summary>
        public decimal? UltimoPrecio(int idMaterial)
        {
            using (var cn = Db.Abrir())
            {
                return cn.ExecuteScalar<decimal?>(
                    "SELECT TOP 1 ValorUnitario FROM dbo.Lotes WHERE IdMaterial = @idMaterial ORDER BY FechaIngreso DESC, IdLote DESC",
                    new { idMaterial });
            }
        }

        /// <param name="precioEstimado">Precio por unidad que se espera pagar; null para usar el último precio.</param>
        public int CrearPedido(int idMaterial, decimal cantidad, string nota, decimal? precioEstimado = null)
        {
            Validar(cantidad, precioEstimado);
            using (var cn = Db.Abrir())
            {
                return cn.ExecuteScalar<int>(@"
                    INSERT INTO dbo.PedidosCompra (IdMaterial, Cantidad, Nota, PrecioEstimado) VALUES (@idMaterial, @cantidad, @nota, @precioEstimado);
                    SELECT CAST(SCOPE_IDENTITY() AS int);",
                    new { idMaterial, cantidad, nota = Limpiar(nota), precioEstimado });
            }
        }

        /// <summary>Corrige un pedido pendiente.</summary>
        public void ModificarPedido(int idPedido, decimal cantidad, string nota, decimal? precioEstimado = null)
        {
            Validar(cantidad, precioEstimado);
            using (var cn = Db.Abrir())
            {
                int filas = cn.Execute(
                    "UPDATE dbo.PedidosCompra SET Cantidad = @cantidad, Nota = @nota, PrecioEstimado = @precioEstimado WHERE IdPedido = @idPedido AND Estado = 'P'",
                    new { idPedido, cantidad, nota = Limpiar(nota), precioEstimado });
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

        /// <summary>
        /// Registra que llegó lo pedido (o parte): ingresa un lote con lo recibido y cierra el pedido.
        /// Si llegó menos y <paramref name="esperarResto"/>, el pedido sigue pendiente por lo que falta
        /// y lo recibido queda como un pedido cerrado aparte. Devuelve el lote ingresado.
        /// </summary>
        public int Recibir(int idPedido, Lote recibido, bool esperarResto)
        {
            if (recibido.Cantidad <= 0)
            {
                throw new ReglaNegocioException("La cantidad recibida tiene que ser mayor que cero.");
            }
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                var pedido = cn.QuerySingleOrDefault<PedidoCompra>(
                    "SELECT IdPedido, IdMaterial, Cantidad, FechaPedido, Nota FROM dbo.PedidosCompra WITH (UPDLOCK) WHERE IdPedido = @idPedido AND Estado = 'P'",
                    new { idPedido }, tx);
                if (pedido == null)
                {
                    throw new ReglaNegocioException("El pedido ya no está pendiente.");
                }

                recibido.IdMaterial = pedido.IdMaterial;
                int idLote = LoteRepositorio.Insertar(cn, tx, recibido);
                var datos = new { idPedido, recibida = recibido.Cantidad, idLote, fecha = recibido.FechaIngreso.Date };

                if (esperarResto && recibido.Cantidad < pedido.Cantidad)
                {
                    cn.Execute("UPDATE dbo.PedidosCompra SET Cantidad = Cantidad - @recibida WHERE IdPedido = @idPedido", datos, tx);
                    cn.Execute(@"
                        INSERT INTO dbo.PedidosCompra (IdMaterial, Cantidad, FechaPedido, Nota, Estado, FechaCierre, CantidadRecibida, IdLote)
                        SELECT IdMaterial, @recibida, FechaPedido, Nota, 'C', @fecha, @recibida, @idLote
                        FROM dbo.PedidosCompra WHERE IdPedido = @idPedido", datos, tx);
                }
                else
                {
                    cn.Execute(@"
                        UPDATE dbo.PedidosCompra
                        SET Estado = 'C', FechaCierre = @fecha, CantidadRecibida = @recibida, IdLote = @idLote
                        WHERE IdPedido = @idPedido", datos, tx);
                }
                tx.Commit();
                return idLote;
            }
        }

        private static void Validar(decimal cantidad, decimal? precioEstimado = null)
        {
            if (cantidad <= 0)
            {
                throw new ReglaNegocioException("La cantidad a comprar tiene que ser mayor que cero.");
            }
            if (precioEstimado < 0)
            {
                throw new ReglaNegocioException("El precio estimado no puede ser negativo.");
            }
        }

        private static string Limpiar(string texto) => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
    }
}
