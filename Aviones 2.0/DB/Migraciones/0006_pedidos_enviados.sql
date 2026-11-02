-- Migración 6: los pedidos tienen un paso más. P por enviar (se está armando la lista),
-- E enviado (se cerró el pedido y se mandó al vendedor), C recibido, X cancelado.
-- Sólo se recibe lo que ya se envió. Los pedidos pendientes que ya había quedan como enviados,
-- para que se puedan seguir recibiendo como hasta ahora.

SET NOCOUNT ON;

ALTER TABLE dbo.PedidosCompra DROP CONSTRAINT CK_PedidosCompra_Estado;
ALTER TABLE dbo.PedidosCompra ADD CONSTRAINT CK_PedidosCompra_Estado CHECK (Estado IN ('P', 'E', 'C', 'X'));
ALTER TABLE dbo.PedidosCompra ADD FechaEnvio DATE NULL;
GO

UPDATE dbo.PedidosCompra SET Estado = 'E', FechaEnvio = FechaPedido WHERE Estado = 'P';
GO

ALTER VIEW dbo.vMaterialesStock AS
SELECT
    M.IdMaterial,
    M.IdFamilia,
    F.Nombre                                   AS Familia,
    M.Grupo,
    M.Caracteristica,
    M.Color,
    M.Medidas,
    M.Codigo,
    M.IdTipoUnidad,
    T.Nombre                                   AS TipoUnidad,
    M.StockMinimo,
    M.SeRepone,
    M.Nota,
    ISNULL((SELECT SUM(P.Cantidad) FROM dbo.PedidosCompra P
            WHERE P.IdMaterial = M.IdMaterial AND P.Estado IN ('P', 'E')), 0) AS PedidoPendiente,
    COUNT(L.IdLote)                            AS CantidadLotes,
    ISNULL(SUM(L.Cantidad), 0)                 AS Cantidad,
    ISNULL(SUM(L.DisponibleNuevo), 0)          AS DisponibleNuevo,
    ISNULL(SUM(L.DisponibleUsado), 0)          AS DisponibleUsado,
    ISNULL(SUM(L.DisponibleNuevo + L.DisponibleUsado), 0) AS Disponible,
    ISNULL(SUM((L.DisponibleNuevo + L.DisponibleUsado) * L.ValorUnitario), 0) AS ValorDisponible,
    ISNULL(SUM(L.Cantidad * L.ValorUnitario), 0) AS Valor
FROM dbo.Materiales M
JOIN dbo.Familias F     ON F.IdFamilia = M.IdFamilia
JOIN dbo.TiposUnidad T  ON T.IdTipoUnidad = M.IdTipoUnidad
LEFT JOIN dbo.Lotes L   ON L.IdMaterial = M.IdMaterial
GROUP BY M.IdMaterial, M.IdFamilia, F.Nombre, M.Grupo, M.Caracteristica, M.Color, M.Medidas,
         M.Codigo, M.IdTipoUnidad, T.Nombre, M.StockMinimo, M.SeRepone, M.Nota;
