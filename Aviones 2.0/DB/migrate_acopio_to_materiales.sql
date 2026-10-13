-- Migration script: copy Acopio fields into Materiales and preserve Acopio as Acopio_Historial
-- Run this script in SSMS connected to the server and with the database context set to the target database.
-- Example: select the database "Aviones" in the dropdown, or run:  USE Aviones;  GO

-- NOTE: You said the tables have no important data; nevertheless review the script before running.

SET NOCOUNT ON;
GO

-- Change to your database if needed
-- USE Aviones;
-- GO

-- 1) Add columns to Materiales if they do not already exist
IF COL_LENGTH('dbo.Materiales','Fabricante') IS NULL
BEGIN
    ALTER TABLE dbo.Materiales
    ADD Fabricante NVARCHAR(200) NULL;
END

IF COL_LENGTH('dbo.Materiales','Ubicacion') IS NULL
BEGIN
    ALTER TABLE dbo.Materiales
    ADD Ubicacion NVARCHAR(200) NULL;
END

IF COL_LENGTH('dbo.Materiales','Cantidad') IS NULL
BEGIN
    ALTER TABLE dbo.Materiales
    ADD Cantidad INT NULL;
END

IF COL_LENGTH('dbo.Materiales','Disponible') IS NULL
BEGIN
    ALTER TABLE dbo.Materiales
    ADD Disponible INT NULL;
END

IF COL_LENGTH('dbo.Materiales','Valor') IS NULL
BEGIN
    ALTER TABLE dbo.Materiales
    ADD Valor DECIMAL(18,2) NULL;
END

IF COL_LENGTH('dbo.Materiales','IdAcopioUltimo') IS NULL
BEGIN
    ALTER TABLE dbo.Materiales
    ADD IdAcopioUltimo INT NULL;
END

GO

-- 2) Populate the new columns from Acopio (last record per IdMaterial if any)
IF OBJECT_ID('dbo.Acopio','U') IS NOT NULL
BEGIN
    ;WITH LastAcopio AS (
        SELECT A.*, ROW_NUMBER() OVER (PARTITION BY A.IdMaterial ORDER BY A.IdAcopio DESC) rn
        FROM dbo.Acopio A
    )
    UPDATE M
    SET M.Fabricante = L.Fabricante,
        M.Ubicacion = L.Ubicacion,
        M.Cantidad = L.Cantidad,
        M.Disponible = L.Disponible,
        M.Valor = L.Valor,
        M.IdAcopioUltimo = L.IdAcopio
    FROM dbo.Materiales M
    LEFT JOIN LastAcopio L ON M.IdMaterial = L.IdMaterial AND L.rn = 1;
END
ELSE
BEGIN
    PRINT 'Table dbo.Acopio not found - skipping data copy step.';
END

GO

-- 3) Optionally rename Acopio to Acopio_Historial so application can continue to use Materiales only
-- This keeps historical data available. Remove or comment out this block if you don't want to rename the table now.
IF OBJECT_ID('dbo.Acopio','U') IS NOT NULL
BEGIN
    EXEC sp_rename 'dbo.Acopio', 'Acopio_Historial';
    PRINT 'Renamed dbo.Acopio to dbo.Acopio_Historial';
END
ELSE
BEGIN
    PRINT 'dbo.Acopio not present; no rename performed.';
END

GO

PRINT 'Migration script finished.';
GO
