-- Migración 3: fabricantes y ubicaciones pasan a ser listas, como las familias y las unidades.
-- Las listas se arman con lo que ya estaba escrito en los lotes (sin espacios de más; lo que sólo
-- cambia en mayúsculas o minúsculas queda como uno solo) y cada lote apunta a su elemento.

SET NOCOUNT ON;

CREATE TABLE dbo.Fabricantes (
    IdFabricante INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Fabricantes PRIMARY KEY,
    Nombre       NVARCHAR(100) NOT NULL CONSTRAINT UQ_Fabricantes_Nombre UNIQUE
);

CREATE TABLE dbo.Ubicaciones (
    IdUbicacion INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Ubicaciones PRIMARY KEY,
    Nombre      NVARCHAR(100) NOT NULL CONSTRAINT UQ_Ubicaciones_Nombre UNIQUE
);

ALTER TABLE dbo.Lotes ADD
    IdFabricante INT NULL CONSTRAINT FK_Lotes_Fabricantes REFERENCES dbo.Fabricantes (IdFabricante),
    IdUbicacion  INT NULL CONSTRAINT FK_Lotes_Ubicaciones REFERENCES dbo.Ubicaciones (IdUbicacion);
GO

-- De cada nombre repetido queda la forma más usada
INSERT INTO dbo.Fabricantes (Nombre)
SELECT Nombre FROM (
    SELECT LTRIM(RTRIM(Fabricante)) AS Nombre,
           ROW_NUMBER() OVER (PARTITION BY LTRIM(RTRIM(Fabricante)) ORDER BY COUNT(*) DESC, LTRIM(RTRIM(Fabricante)) COLLATE Latin1_General_BIN) AS Orden
    FROM dbo.Lotes
    WHERE LTRIM(RTRIM(ISNULL(Fabricante, N''))) <> N''
    GROUP BY LTRIM(RTRIM(Fabricante)) COLLATE Latin1_General_BIN, LTRIM(RTRIM(Fabricante))
) AS F
WHERE Orden = 1
ORDER BY Nombre;

INSERT INTO dbo.Ubicaciones (Nombre)
SELECT Nombre FROM (
    SELECT LTRIM(RTRIM(Ubicacion)) AS Nombre,
           ROW_NUMBER() OVER (PARTITION BY LTRIM(RTRIM(Ubicacion)) ORDER BY COUNT(*) DESC, LTRIM(RTRIM(Ubicacion)) COLLATE Latin1_General_BIN) AS Orden
    FROM dbo.Lotes
    WHERE LTRIM(RTRIM(ISNULL(Ubicacion, N''))) <> N''
    GROUP BY LTRIM(RTRIM(Ubicacion)) COLLATE Latin1_General_BIN, LTRIM(RTRIM(Ubicacion))
) AS U
WHERE Orden = 1
ORDER BY Nombre;

UPDATE L SET IdFabricante = F.IdFabricante
FROM dbo.Lotes L JOIN dbo.Fabricantes F ON F.Nombre = LTRIM(RTRIM(L.Fabricante));

UPDATE L SET IdUbicacion = U.IdUbicacion
FROM dbo.Lotes L JOIN dbo.Ubicaciones U ON U.Nombre = LTRIM(RTRIM(L.Ubicacion));

CREATE INDEX IX_Lotes_IdFabricante ON dbo.Lotes (IdFabricante);
CREATE INDEX IX_Lotes_IdUbicacion ON dbo.Lotes (IdUbicacion);

ALTER TABLE dbo.Lotes DROP COLUMN Fabricante, Ubicacion;
