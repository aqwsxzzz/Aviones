-- Migración 5:
--   * Valor de cada material = todo lo comprado x su valor unitario (esté guardado, usado o en un modelo).
--   * Nota del material (se ve al elegirlo en Materiales). Las notas de los pedidos pendientes pasan al material.
--   * Vendedores: lista de a quién se le compra, y cada pedido dice su vendedor.

SET NOCOUNT ON;

ALTER TABLE dbo.Materiales ADD Nota NVARCHAR(500) NULL;

CREATE TABLE dbo.Vendedores (
    IdVendedor INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Vendedores PRIMARY KEY,
    Nombre     NVARCHAR(100) NOT NULL CONSTRAINT UQ_Vendedores_Nombre UNIQUE
);

ALTER TABLE dbo.PedidosCompra ADD
    IdVendedor INT NULL CONSTRAINT FK_PedidosCompra_Vendedores REFERENCES dbo.Vendedores (IdVendedor);
GO

-- Las notas que había en los pedidos pendientes quedan en su material (si hay varias, separadas por " / ")
UPDATE M SET Nota = LEFT(N.Notas, 500)
FROM dbo.Materiales M
JOIN (
    SELECT P.IdMaterial,
           STUFF((SELECT N' / ' + P2.Nota
                  FROM dbo.PedidosCompra P2
                  WHERE P2.IdMaterial = P.IdMaterial AND P2.Estado = 'P' AND LTRIM(RTRIM(ISNULL(P2.Nota, N''))) <> N''
                  ORDER BY P2.FechaPedido, P2.IdPedido
                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 3, N'') AS Notas
    FROM dbo.PedidosCompra P
    WHERE P.Estado = 'P' AND LTRIM(RTRIM(ISNULL(P.Nota, N''))) <> N''
    GROUP BY P.IdMaterial
) N ON N.IdMaterial = M.IdMaterial;

CREATE INDEX IX_PedidosCompra_IdVendedor ON dbo.PedidosCompra (IdVendedor);
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
            WHERE P.IdMaterial = M.IdMaterial AND P.Estado = 'P'), 0) AS PedidoPendiente,
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
