-- Migración 4: cada material dice si se repone (se vuelve a comprar) o no.
-- Lo que no se repone (obsoleto, de una sola vez, herramientas) nunca aparece en el stock crítico.
-- Todos los materiales que ya existen quedan como que se reponen.

SET NOCOUNT ON;

ALTER TABLE dbo.Materiales ADD SeRepone BIT NOT NULL CONSTRAINT DF_Materiales_SeRepone DEFAULT 1;
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
    ISNULL((SELECT SUM(P.Cantidad) FROM dbo.PedidosCompra P
            WHERE P.IdMaterial = M.IdMaterial AND P.Estado = 'P'), 0) AS PedidoPendiente,
    COUNT(L.IdLote)                            AS CantidadLotes,
    ISNULL(SUM(L.Cantidad), 0)                 AS Cantidad,
    ISNULL(SUM(L.DisponibleNuevo), 0)          AS DisponibleNuevo,
    ISNULL(SUM(L.DisponibleUsado), 0)          AS DisponibleUsado,
    ISNULL(SUM(L.DisponibleNuevo + L.DisponibleUsado), 0) AS Disponible,
    ISNULL(SUM((L.DisponibleNuevo + L.DisponibleUsado) * L.ValorUnitario), 0) AS ValorDisponible
FROM dbo.Materiales M
JOIN dbo.Familias F     ON F.IdFamilia = M.IdFamilia
JOIN dbo.TiposUnidad T  ON T.IdTipoUnidad = M.IdTipoUnidad
LEFT JOIN dbo.Lotes L   ON L.IdMaterial = M.IdMaterial
GROUP BY M.IdMaterial, M.IdFamilia, F.Nombre, M.Grupo, M.Caracteristica, M.Color, M.Medidas,
         M.Codigo, M.IdTipoUnidad, T.Nombre, M.StockMinimo, M.SeRepone;
