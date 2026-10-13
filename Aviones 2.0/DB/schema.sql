-- Esquema de la base de datos Aviones
-- Recrea las tablas tal como están en Aviones.mdf, más:
--   * Modelos.IdModelo como IDENTITY (el programa inserta modelos sin IdModelo)
--   * MaterialesModelo (usada por el programa, faltaba en la base)
-- Ejecutar con: sqlcmd -S localhost -E -C -i schema.sql

IF DB_ID('Aviones') IS NULL
    CREATE DATABASE Aviones;
GO

USE Aviones;
GO

IF OBJECT_ID('dbo.Familiares') IS NULL
CREATE TABLE dbo.Familiares (
    IdFamilia INT IDENTITY(1,1) NOT NULL,
    Familia   NVARCHAR(40) NULL
);
GO

IF OBJECT_ID('dbo.Materiales') IS NULL
CREATE TABLE dbo.Materiales (
    IdMaterial     INT IDENTITY(1,1) NOT NULL,
    IdFamilia      INT NOT NULL,
    Grupo          NVARCHAR(40) NULL,
    Caracteristica NVARCHAR(40) NULL,
    Color          NVARCHAR(40) NULL,
    Medidas        NVARCHAR(40) NULL,
    Codigo         NVARCHAR(40) NULL,
    Estado         NVARCHAR(40) NULL,
    Tipo           NVARCHAR(40) NULL,
    ImageData      VARBINARY(MAX) NULL,
    Fabricante     NVARCHAR(40) NULL,
    Ubicacion      NVARCHAR(40) NULL,
    Cantidad       DECIMAL(18,1) NULL,
    Disponible     NVARCHAR(40) NULL,
    Valor          DECIMAL(18,1) NULL,
    Cantacomprar   DECIMAL(18,1) NULL,
    CONSTRAINT PK_Materiales PRIMARY KEY CLUSTERED (IdMaterial)
);
GO

IF OBJECT_ID('dbo.Modelos') IS NULL
CREATE TABLE dbo.Modelos (
    IdModelo INT IDENTITY(1,1) NOT NULL,
    Modelo   NVARCHAR(40) NULL,
    Alias    NVARCHAR(40) NULL,
    Estado   NVARCHAR(40) NULL,
    Desde    DATE NULL,
    Foto     VARBINARY(MAX) NULL,
    CONSTRAINT PK_Modelos PRIMARY KEY CLUSTERED (IdModelo)
);
GO

IF OBJECT_ID('dbo.MaterialesModelo') IS NULL
CREATE TABLE dbo.MaterialesModelo (
    IdMaterialesModelo INT IDENTITY(1,1) NOT NULL,
    IdModelo           INT NOT NULL,
    IdMaterial         INT NOT NULL,
    CantidadxModelo    DECIMAL(18,1) NULL,
    CONSTRAINT PK_MaterialesModelo PRIMARY KEY CLUSTERED (IdMaterialesModelo)
);
GO
