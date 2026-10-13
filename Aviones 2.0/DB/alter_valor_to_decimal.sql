-- Alter Valor column to decimal(18,2) so decimal values are preserved
-- Run this in SSMS against the Aviones database

-- Check current type
SELECT COLUMN_NAME, DATA_TYPE, NUMERIC_PRECISION, NUMERIC_SCALE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Materiales' AND COLUMN_NAME = 'Valor';

-- Alter column to decimal
ALTER TABLE dbo.Materiales
ALTER COLUMN Valor DECIMAL(18,2) NULL;

-- Example: Update a row to test decimal value (change IdMaterial accordingly)
-- UPDATE dbo.Materiales SET Valor = 2.1 WHERE IdMaterial = 3006;

PRINT 'Column Valor altered to DECIMAL(18,2).';
