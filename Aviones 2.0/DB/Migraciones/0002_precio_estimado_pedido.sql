-- Migración 2: precio estimado en cada pedido de compra.
-- Sin dato se usa el precio del último lote del material.
ALTER TABLE dbo.PedidosCompra ADD PrecioEstimado DECIMAL(18,2) NULL
    CONSTRAINT CK_PedidosCompra_PrecioEstimado CHECK (PrecioEstimado >= 0);
