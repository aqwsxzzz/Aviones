-- Esquema de la base de datos Aviones.
-- Borra y vuelve a crear todas las tablas (también las del esquema anterior).
-- Ejecutar contra la base deseada; lo más simple es usar reset.ps1.
--
--   Familias, TiposUnidad, EstadosModelo  tablas de referencia
--   Materiales                            qué es cada material (catálogo)
--   Lotes                                 cada ingreso de un material: fabricante, ubicación,
--                                         cantidad, precio y lo que queda, separado en nuevo
--                                         y usado (antes "Acopio")
--   Modelos                               los aviones
--   MaterialesModelo                      cuánto de cada lote se usó en cada modelo

SET NOCOUNT ON;

DROP VIEW IF EXISTS dbo.vMaterialesStock;
DROP TABLE IF EXISTS
    dbo.MaterialesModelo,
    dbo.Lotes,
    dbo.Modelos,
    dbo.EstadosModelo,
    dbo.Materiales,
    dbo.TiposUnidad,
    dbo.Familias,
    -- tablas del esquema anterior
    dbo.Familiares,
    dbo.Acopio,
    dbo.Acopio_Historial;
GO

CREATE TABLE dbo.Familias (
    IdFamilia INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Familias PRIMARY KEY,
    Nombre    NVARCHAR(60) NOT NULL CONSTRAINT UQ_Familias_Nombre UNIQUE
);

CREATE TABLE dbo.TiposUnidad (
    IdTipoUnidad INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TiposUnidad PRIMARY KEY,
    Nombre       NVARCHAR(30) NOT NULL CONSTRAINT UQ_TiposUnidad_Nombre UNIQUE
);

CREATE TABLE dbo.EstadosModelo (
    IdEstadoModelo INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_EstadosModelo PRIMARY KEY,
    Nombre         NVARCHAR(30) NOT NULL CONSTRAINT UQ_EstadosModelo_Nombre UNIQUE
);

CREATE TABLE dbo.Materiales (
    IdMaterial       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Materiales PRIMARY KEY,
    IdFamilia        INT NOT NULL CONSTRAINT FK_Materiales_Familias REFERENCES dbo.Familias (IdFamilia),
    Grupo            NVARCHAR(60) NOT NULL,
    Caracteristica   NVARCHAR(100) NULL,
    Color            NVARCHAR(60) NULL,
    Medidas          NVARCHAR(60) NULL,
    Codigo           NVARCHAR(60) NULL,
    IdTipoUnidad     INT NOT NULL CONSTRAINT FK_Materiales_TiposUnidad REFERENCES dbo.TiposUnidad (IdTipoUnidad),
    Imagen           VARBINARY(MAX) NULL,
    CantidadAComprar DECIMAL(18,3) NULL CONSTRAINT CK_Materiales_CantidadAComprar CHECK (CantidadAComprar >= 0)
);
CREATE INDEX IX_Materiales_IdFamilia ON dbo.Materiales (IdFamilia);

CREATE TABLE dbo.Lotes (
    IdLote        INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Lotes PRIMARY KEY,
    IdMaterial    INT NOT NULL CONSTRAINT FK_Lotes_Materiales REFERENCES dbo.Materiales (IdMaterial) ON DELETE CASCADE,
    Fabricante    NVARCHAR(100) NULL,
    Ubicacion     NVARCHAR(100) NULL,
    Cantidad      DECIMAL(18,3) NOT NULL CONSTRAINT CK_Lotes_Cantidad CHECK (Cantidad >= 0),
    -- Se compró usado: todo entra como usado y nunca hay disponible nuevo
    CompradoUsado BIT NOT NULL CONSTRAINT DF_Lotes_CompradoUsado DEFAULT 0,
    -- Lo que queda. Lo que vuelve de un modelo como reciclado suma a DisponibleUsado.
    DisponibleNuevo DECIMAL(18,3) NOT NULL CONSTRAINT DF_Lotes_DisponibleNuevo DEFAULT 0 CONSTRAINT CK_Lotes_DisponibleNuevo CHECK (DisponibleNuevo >= 0),
    DisponibleUsado DECIMAL(18,3) NOT NULL CONSTRAINT DF_Lotes_DisponibleUsado DEFAULT 0 CONSTRAINT CK_Lotes_DisponibleUsado CHECK (DisponibleUsado >= 0),
    ValorUnitario DECIMAL(18,2) NOT NULL CONSTRAINT DF_Lotes_ValorUnitario DEFAULT 0
                  CONSTRAINT CK_Lotes_ValorUnitario CHECK (ValorUnitario >= 0),
    FechaIngreso  DATE NOT NULL CONSTRAINT DF_Lotes_FechaIngreso DEFAULT CAST(GETDATE() AS DATE),
    CONSTRAINT CK_Lotes_Disponible CHECK (DisponibleNuevo + DisponibleUsado <= Cantidad),
    CONSTRAINT CK_Lotes_CompradoUsado CHECK (CompradoUsado = 0 OR DisponibleNuevo = 0)
);
CREATE INDEX IX_Lotes_IdMaterial ON dbo.Lotes (IdMaterial);

CREATE TABLE dbo.Modelos (
    IdModelo       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Modelos PRIMARY KEY,
    Nombre         NVARCHAR(100) NOT NULL,
    Alias          NVARCHAR(100) NULL,
    IdEstadoModelo INT NOT NULL CONSTRAINT FK_Modelos_EstadosModelo REFERENCES dbo.EstadosModelo (IdEstadoModelo),
    Desde          DATE NULL,
    Foto           VARBINARY(MAX) NULL
);

-- Borrar un modelo borra sus renglones; borrar un lote usado en un modelo no está permitido.
-- Estado dice de qué stock del lote salió (N nuevo, U usado), para saber adónde puede volver.
CREATE TABLE dbo.MaterialesModelo (
    IdMaterialModelo INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_MaterialesModelo PRIMARY KEY,
    IdModelo         INT NOT NULL CONSTRAINT FK_MaterialesModelo_Modelos REFERENCES dbo.Modelos (IdModelo) ON DELETE CASCADE,
    IdLote           INT NOT NULL CONSTRAINT FK_MaterialesModelo_Lotes REFERENCES dbo.Lotes (IdLote),
    Estado           CHAR(1) NOT NULL CONSTRAINT CK_MaterialesModelo_Estado CHECK (Estado IN ('N', 'U')),
    Cantidad         DECIMAL(18,3) NOT NULL CONSTRAINT CK_MaterialesModelo_Cantidad CHECK (Cantidad > 0)
);
CREATE INDEX IX_MaterialesModelo_IdModelo ON dbo.MaterialesModelo (IdModelo);
CREATE INDEX IX_MaterialesModelo_IdLote ON dbo.MaterialesModelo (IdLote);
GO

-- Un renglón por material con el stock sumado de todos sus lotes.
CREATE VIEW dbo.vMaterialesStock AS
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
    M.CantidadAComprar,
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
         M.Codigo, M.IdTipoUnidad, T.Nombre, M.CantidadAComprar;
GO

INSERT INTO dbo.EstadosModelo (Nombre) VALUES
    (N'Activo'), (N'En construcción'), (N'En reparación'), (N'Inactivo');
GO
