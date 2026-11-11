using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    /// <summary>Stock crítico y pedidos de compra.</summary>
    public class CompraRepositorio
    {
        private const string SelectPedido = @"
            SELECT P.IdPedido, P.IdMaterial, F.Nombre AS Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo,
                   T.Nombre AS TipoUnidad, P.Cantidad, P.FechaPedido, P.Nota, P.IdVendedor, V.Nombre AS Vendedor, P.Estado, P.FechaEnvio,
                   P.FechaCierre, P.CantidadRecibida, P.IdLote, P.PrecioEstimado,
                   S.Disponible,
                   (SELECT TOP 1 L.ValorUnitario FROM dbo.Lotes L
                     WHERE L.IdMaterial = P.IdMaterial ORDER BY L.FechaIngreso DESC, L.IdLote DESC) AS UltimoPrecio
            FROM dbo.PedidosCompra P
            JOIN dbo.Materiales M        ON M.IdMaterial = P.IdMaterial
            JOIN dbo.Familias F          ON F.IdFamilia = M.IdFamilia
            JOIN dbo.TiposUnidad T       ON T.IdTipoUnidad = M.IdTipoUnidad
            JOIN dbo.vMaterialesStock S  ON S.IdMaterial = P.IdMaterial
            LEFT JOIN dbo.Vendedores V   ON V.IdVendedor = P.IdVendedor";

        /// <summary>
        /// Materiales con disponible (nuevo + usado) menor o igual que su mínimo propio,
        /// o que el mínimo general si no tienen uno.
        /// </summary>
        public List<StockCritico> ListarStockCritico(decimal minimoGeneral) =>
            ListarParaComprar(minimoGeneral).Where(m => m.EsCritico).ToList();

        /// <summary>Todos los materiales, con el mínimo que les toca y el último precio pagado.</summary>
        public List<StockCritico> ListarParaComprar(decimal minimoGeneral)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<StockCritico>(@"
                    SELECT S.*, ISNULL(S.StockMinimo, @minimoGeneral) AS MinimoAplicado,
                           (SELECT TOP 1 L.ValorUnitario FROM dbo.Lotes L
                             WHERE L.IdMaterial = S.IdMaterial ORDER BY L.FechaIngreso DESC, L.IdLote DESC) AS UltimoPrecio
                    FROM dbo.vMaterialesStock S
                    ORDER BY S.Familia, S.Grupo, S.Caracteristica, S.Medidas",
                    new { minimoGeneral }).ToList();
            }
        }

        /// <summary>Lo que falta comprar o que todavía no llegó (por enviar y enviado), del pedido más viejo al más nuevo.</summary>
        public List<PedidoCompra> ListarPendientes()
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<PedidoCompra>(SelectPedido + " WHERE P.Estado IN ('P', 'E') ORDER BY P.FechaPedido, P.IdPedido").ToList();
            }
        }

        public List<PedidoCompra> ListarPendientesDe(int idMaterial)
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<PedidoCompra>(SelectPedido + " WHERE P.Estado IN ('P', 'E') AND P.IdMaterial = @idMaterial ORDER BY P.FechaPedido",
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

        /// <summary>Vendedor del pedido más reciente del material que tuvo uno; null si nunca.</summary>
        public int? UltimoVendedor(int idMaterial)
        {
            using (var cn = Db.Abrir())
            {
                return cn.ExecuteScalar<int?>(
                    "SELECT TOP 1 IdVendedor FROM dbo.PedidosCompra WHERE IdMaterial = @idMaterial AND IdVendedor IS NOT NULL ORDER BY FechaPedido DESC, IdPedido DESC",
                    new { idMaterial });
            }
        }

        /// <param name="precioEstimado">Precio por unidad que se espera pagar; null para usar el último precio.</param>
        /// <param name="idVendedor">A quién se le compra; null si todavía no se sabe.</param>
        public int CrearPedido(int idMaterial, decimal cantidad, string nota, decimal? precioEstimado = null, int? idVendedor = null)
        {
            Validar(cantidad, precioEstimado);
            using (var cn = Db.Abrir())
            {
                return cn.ExecuteScalar<int>(@"
                    INSERT INTO dbo.PedidosCompra (IdMaterial, Cantidad, Nota, PrecioEstimado, IdVendedor)
                    VALUES (@idMaterial, @cantidad, @nota, @precioEstimado, @idVendedor);
                    SELECT CAST(SCOPE_IDENTITY() AS int);",
                    new { idMaterial, cantidad, nota = Limpiar(nota), precioEstimado, idVendedor });
            }
        }

        /// <summary>Corrige un pedido que todavía no se envió.</summary>
        public void ModificarPedido(int idPedido, decimal cantidad, string nota, decimal? precioEstimado = null, int? idVendedor = null)
        {
            Validar(cantidad, precioEstimado);
            using (var cn = Db.Abrir())
            {
                int filas = cn.Execute(@"
                    UPDATE dbo.PedidosCompra SET Cantidad = @cantidad, Nota = @nota, PrecioEstimado = @precioEstimado, IdVendedor = @idVendedor
                    WHERE IdPedido = @idPedido AND Estado = 'P'",
                    new { idPedido, cantidad, nota = Limpiar(nota), precioEstimado, idVendedor });
                if (filas == 0)
                {
                    throw new ReglaNegocioException("Sólo se puede modificar un pedido que todavía no se envió.");
                }
            }
        }

        /// <summary>Saca un pedido de la lista (ya no hace falta comprarlo, o se canceló la compra).</summary>
        public void EliminarPedido(int idPedido)
        {
            using (var cn = Db.Abrir())
            {
                cn.Execute("DELETE FROM dbo.PedidosCompra WHERE IdPedido = @idPedido AND Estado IN ('P', 'E')", new { idPedido });
            }
        }

        /// <summary>Cerrar el pedido: los pedidos indicados que todavía no se enviaron quedan enviados con la fecha de hoy.</summary>
        public int EnviarPedidos(IEnumerable<int> idsPedido)
        {
            var ids = idsPedido.ToList();
            if (ids.Count == 0)
            {
                return 0;
            }
            using (var cn = Db.Abrir())
            {
                return cn.Execute(
                    "UPDATE dbo.PedidosCompra SET Estado = 'E', FechaEnvio = CAST(GETDATE() AS DATE) WHERE IdPedido IN @ids AND Estado = 'P'",
                    new { ids });
            }
        }

        /// <summary>
        /// Registra que llegó lo pedido (o parte): ingresa un lote con lo recibido y cierra el pedido.
        /// Si llegó menos y <paramref name="esperarResto"/>, el pedido sigue pendiente por lo que falta
        /// y lo recibido queda como un pedido cerrado aparte. Devuelve el lote ingresado.
        /// </summary>
        public int Recibir(int idPedido, Lote recibido, bool esperarResto)
        {
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                int idLote = Recibir(cn, tx, idPedido, recibido, esperarResto);
                tx.Commit();
                return idLote;
            }
        }

        /// <summary>
        /// Pasar a stock lo que llegó de un pedido cerrado, todo junto: corrige los materiales que se
        /// cambiaron e ingresa un lote por cada uno que llegó. Lo que llegó de menos sigue pendiente;
        /// lo que no llegó (cantidad 0) queda como estaba. Si algo falla, no se guarda nada.
        /// Devuelve cuántos materiales entraron.
        /// </summary>
        public int PasarAStock(IEnumerable<Recepcion> recepciones)
        {
            var llegaron = recepciones.Where(r => r.Lote.Cantidad != 0).ToList();
            if (llegaron.Count == 0)
            {
                throw new ReglaNegocioException("No llegó nada: escriba en \"Llegó\" cuánto se recibió de cada material.");
            }
            using (var cn = Db.Abrir())
            using (var tx = cn.BeginTransaction())
            {
                foreach (var r in llegaron)
                {
                    if (r.Material != null)
                    {
                        MaterialRepositorio.Actualizar(cn, tx, r.Material);
                    }
                    Recibir(cn, tx, r.IdPedido, r.Lote, esperarResto: true);
                }
                tx.Commit();
            }
            return llegaron.Count;
        }

        private static int Recibir(IDbConnection cn, IDbTransaction tx, int idPedido, Lote recibido, bool esperarResto)
        {
            if (recibido.Cantidad <= 0)
            {
                throw new ReglaNegocioException("La cantidad recibida tiene que ser mayor que cero.");
            }
            var pedido = cn.QuerySingleOrDefault<PedidoCompra>(
                "SELECT IdPedido, IdMaterial, Cantidad, FechaPedido, Nota, Estado FROM dbo.PedidosCompra WITH (UPDLOCK) WHERE IdPedido = @idPedido AND Estado IN ('P', 'E')",
                new { idPedido }, tx);
            if (pedido == null)
            {
                throw new ReglaNegocioException("El pedido ya no está pendiente.");
            }
            if (!pedido.Enviado)
            {
                throw new ReglaNegocioException("Primero hay que cerrar el pedido (enviarlo al vendedor); después se recibe.");
            }

            recibido.IdMaterial = pedido.IdMaterial;
            int idLote = LoteRepositorio.Insertar(cn, tx, recibido);
            var datos = new { idPedido, recibida = recibido.Cantidad, idLote, fecha = recibido.FechaIngreso.Date };

            if (esperarResto && recibido.Cantidad < pedido.Cantidad)
            {
                cn.Execute("UPDATE dbo.PedidosCompra SET Cantidad = Cantidad - @recibida WHERE IdPedido = @idPedido", datos, tx);
                cn.Execute(@"
                    INSERT INTO dbo.PedidosCompra (IdMaterial, Cantidad, FechaPedido, Nota, PrecioEstimado, IdVendedor, FechaEnvio, Estado, FechaCierre, CantidadRecibida, IdLote)
                    SELECT IdMaterial, @recibida, FechaPedido, Nota, PrecioEstimado, IdVendedor, FechaEnvio, 'C', @fecha, @recibida, @idLote
                    FROM dbo.PedidosCompra WHERE IdPedido = @idPedido", datos, tx);
            }
            else
            {
                cn.Execute(@"
                    UPDATE dbo.PedidosCompra
                    SET Estado = 'C', FechaCierre = @fecha, CantidadRecibida = @recibida, IdLote = @idLote
                    WHERE IdPedido = @idPedido", datos, tx);
            }
            return idLote;
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
