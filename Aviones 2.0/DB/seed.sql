-- Datos de prueba para Aviones (taller de aeromodelismo)
-- Borra todo y vuelve a cargar. Ejecutar después de schema.sql:
--   sqlcmd -S localhost -E -C -i seed.sql

USE Aviones;
GO
SET NOCOUNT ON;

DELETE FROM dbo.MaterialesModelo;
DELETE FROM dbo.Modelos;
DELETE FROM dbo.Materiales;
DELETE FROM dbo.Familiares;

-- Familias ----------------------------------------------------------------
SET IDENTITY_INSERT dbo.Familiares ON;
INSERT INTO dbo.Familiares (IdFamilia, Familia) VALUES
 (1, N'Banco de trabajo'),
 (2, N'Electrónica'),
 (3, N'Madera'),
 (4, N'Revestimientos'),
 (5, N'Adhesivos'),
 (6, N'Motorización'),
 (7, N'Ferretería'),
 (8, N'Tren de aterrizaje'),
 (9, N'Pinturas y acabados'),
 (10, N'Fibra y composites');
SET IDENTITY_INSERT dbo.Familiares OFF;

-- Materiales (Disponible se calcula al final según lo usado en modelos) ---
SET IDENTITY_INSERT dbo.Materiales ON;
INSERT INTO dbo.Materiales
 (IdMaterial, IdFamilia, Grupo, Caracteristica, Color, Medidas, Codigo, Estado, Tipo, Fabricante, Ubicacion, Cantidad, Valor, Cantacomprar) VALUES
 -- Banco de trabajo
 (1,  1, N'Cuchilla', N'Acero #11', N'', N'', N'X211', N'N', N'Unidades', N'X-Acto', N'Cajón herramientas', 20, 0.6, 10),
 (2,  1, N'Mango cuchilla', N'Aluminio', N'Plata', N'', N'X3201', N'U', N'Unidades', N'X-Acto', N'Escritorio', 2, 6.5, NULL),
 (3,  1, N'Plancha de entelar', N'Termostato regulable', N'Negro', N'', N'TOPR2100', N'U', N'Unidades', N'Top Flite', N'Estante', 1, 35.0, NULL),
 (4,  1, N'Pistola de calor', N'1500 W', N'Naranja', N'', N'HT1000', N'N', N'Unidades', N'Wagner', N'Estante', 1, 28.0, NULL),
 (5,  1, N'Lija', N'Grano 220', N'', N'230 x 280', N'', N'N', N'Unidades', N'3M', N'Cajón herramientas', 25, 0.8, 20),
 (6,  1, N'Alfileres en T', N'Acero niquelado', N'', N'32 mm', N'', N'N', N'Unidades', N'Great Planes', N'Cajón herramientas', 200, 0.0, NULL),
 (7,  1, N'Base de corte', N'Autocurable', N'Verde', N'450 x 300', N'CM-A3', N'U', N'Unidades', N'Olfa', N'Piso', 1, 22.0, NULL),
 -- Electrónica
 (8,  2, N'Servo', N'Micro 9 g', N'Azul', N'23 x 12 x 29', N'SG90', N'N', N'Unidades', N'TowerPro', N'Caja electrónica', 12, 3.5, 8),
 (9,  2, N'Servo', N'Estándar 43 g', N'Negro', N'40 x 20 x 37', N'HS-311', N'N', N'Unidades', N'Hitec', N'Caja electrónica', 8, 13.0, 4),
 (10, 2, N'Servo', N'Digital engranaje metálico', N'Negro', N'40 x 20 x 38', N'MG996R', N'N', N'Unidades', N'TowerPro', N'Caja electrónica', 4, 9.0, NULL),
 (11, 2, N'Batería', N'LiPo 3S 2200 mAh 25C', N'Amarillo', N'105 x 34 x 24', N'', N'N', N'Unidades', N'Turnigy', N'Bolsa LiPo', 4, 22.0, 2),
 (12, 2, N'Batería', N'LiPo 4S 3300 mAh 35C', N'Negro', N'135 x 43 x 30', N'', N'U', N'Unidades', N'Gens Ace', N'Bolsa LiPo', 2, 45.0, NULL),
 (13, 2, N'Batería', N'LiPo 2S 1000 mAh 20C', N'Azul', N'70 x 30 x 15', N'', N'N', N'Unidades', N'Zippy', N'Bolsa LiPo', 3, 10.0, NULL),
 (14, 2, N'Variador', N'ESC 40 A con BEC', N'Azul', N'55 x 28 x 7', N'Skywalker 40A', N'N', N'Unidades', N'Hobbywing', N'Caja electrónica', 3, 25.0, 1),
 (15, 2, N'Variador', N'ESC 30 A', N'Rojo', N'50 x 25 x 8', N'', N'N', N'Unidades', N'ZTW', N'Caja electrónica', 2, 17.0, NULL),
 (16, 2, N'Receptor', N'6 canales 2.4 GHz', N'Negro', N'46 x 25 x 14', N'FS-iA6B', N'N', N'Unidades', N'FlySky', N'Caja electrónica', 4, 9.0, 2),
 (17, 2, N'Cargador', N'Para baterías de lipo', N'Azul', N'', N'B6AC', N'U', N'Unidades', N'IMAX', N'Escritorio', 1, 50.0, NULL),
 (18, 2, N'Cable', N'Silicona 14 AWG', N'Rojo', N'', N'', N'N', N'Metros', N'Turnigy', N'Caja electrónica', 5, 1.2, NULL),
 (19, 2, N'Conector', N'Par macho/hembra', N'Amarillo', N'', N'XT60', N'N', N'Unidades', N'Amass', N'Caja electrónica', 20, 0.8, NULL),
 (20, 2, N'Extensión servo', N'300 mm', N'Negro', N'', N'', N'N', N'Unidades', N'Futaba', N'Caja electrónica', 15, 0.9, NULL),
 -- Madera
 (21, 3, N'Balsa', N'Tabla', N'3/32 x 4 x 36', N'2.4 x 100 x 914', N'', N'N', N'Unidades', N'Balsa USA', N'Caja balsa', 20, 6.1, 10),
 (22, 3, N'Balsa', N'Tabla', N'1/16 x 4 x 36', N'1.6 x 100 x 914', N'', N'N', N'Unidades', N'Balsa USA', N'Caja balsa', 15, 5.2, NULL),
 (23, 3, N'Balsa', N'Tabla', N'1/8 x 4 x 36', N'3.2 x 100 x 914', N'', N'N', N'Unidades', N'Balsa USA', N'Caja balsa', 10, 7.4, 5),
 (24, 3, N'Balsa', N'Varilla cuadrada', N'1/4 x 1/4 x 36', N'6.4 x 6.4 x 914', N'', N'N', N'Unidades', N'Balsa USA', N'Caja balsa', 30, 1.1, NULL),
 (25, 3, N'Balsa', N'Bloque', N'1 x 3 x 12', N'25 x 76 x 305', N'', N'N', N'Unidades', N'Balsa USA', N'Caja balsa', 3, 8.5, NULL),
 (26, 3, N'Abedul', N'Contrachapado', N'1/32 x 12 x 48', N'0.8 x 305 x 1219', N'', N'N', N'Unidades', N'Balsa USA', N'Caja balsa', 3, 20.6, NULL),
 (27, 3, N'Abedul', N'Contrachapado aeronáutico', N'1/8 x 12 x 24', N'3 x 305 x 610', N'', N'N', N'Unidades', N'Midwest', N'Caja balsa', 5, 12.8, 2),
 (28, 3, N'Pino', N'Larguero', N'1/4 x 1/4 x 36', N'6.4 x 6.4 x 914', N'', N'N', N'Unidades', N'Midwest', N'Caja balsa', 12, 1.9, NULL),
 (29, 3, N'Haya', N'Varilla redonda', N'1/4 x 36', N'6.4 x 914', N'', N'N', N'Unidades', N'Midwest', N'Caja balsa', 10, 1.4, NULL),
 -- Revestimientos
 (30, 4, N'Plástico', N'Monokote', N'Blanco', N'1.8 x 66', N'TOPQ0204', N'N', N'Metros', N'Top Flite', N'Bolsa Macro', 3.6, 12.0, NULL),
 (31, 4, N'Plástico', N'Monokote', N'Rojo', N'1.8 x 66', N'TOPQ0201', N'N', N'Metros', N'Top Flite', N'Bolsa Macro', 1.8, 12.0, 2),
 (32, 4, N'Plástico', N'Monokote', N'Transparente', N'1.1 x 60', N'TOPQ0200', N'N', N'Metros', N'Top Fly', N'Bolsa Macro', 0.7, 12.0, NULL),
 (33, 4, N'Plástico', N'Oracover', N'Amarillo Cub', N'2 x 60', N'21-030', N'N', N'Metros', N'Oracover', N'Bolsa Macro', 2.0, 15.0, NULL),
 (34, 4, N'Tela', N'Solartex', N'Blanco antiguo', N'2 x 60', N'', N'N', N'Metros', N'Solarfilm', N'Bolsa Macro', 2.0, 18.0, NULL),
 (35, 4, N'Cinta', N'Tela de bisagra', N'Transparente', N'25 mm', N'', N'N', N'Metros', N'3M', N'Cajón adhesivos', 15, 0.4, NULL),
 -- Adhesivos
 (36, 5, N'Cianoacrilato', N'Fino', N'', N'20 g', N'IC-2000', N'N', N'Unidades', N'Bob Smith Industries', N'Cajón adhesivos', 4, 7.5, 2),
 (37, 5, N'Cianoacrilato', N'Medio', N'', N'20 g', N'MaxiCure', N'N', N'Unidades', N'Bob Smith Industries', N'Cajón adhesivos', 3, 7.5, NULL),
 (38, 5, N'Epoxi', N'30 minutos', N'', N'270 ml', N'BSI-204', N'N', N'Unidades', N'Bob Smith Industries', N'Cajón adhesivos', 2, 18.0, NULL),
 (39, 5, N'Epoxi', N'5 minutos', N'', N'270 ml', N'BSI-201', N'N', N'Unidades', N'Bob Smith Industries', N'Cajón adhesivos', 1, 14.0, 1),
 (40, 5, N'Cola blanca', N'Alifática', N'', N'236 ml', N'', N'N', N'Unidades', N'Titebond', N'Cajón adhesivos', 2, 6.0, NULL),
 (41, 5, N'Activador', N'Aerosol para cianoacrilato', N'', N'59 ml', N'', N'N', N'Unidades', N'Zap', N'Cajón adhesivos', 1, 9.0, NULL),
 -- Motorización
 (42, 6, N'Motor', N'Brushless 1000 KV', N'Plata', N'28 x 30', N'A2212/13T', N'N', N'Unidades', N'Racerstar', N'Caja motores', 3, 12.0, 2),
 (43, 6, N'Motor', N'Brushless 920 KV', N'Rojo', N'28 x 36', N'2826', N'U', N'Unidades', N'SunnySky', N'Caja motores', 2, 18.0, NULL),
 (44, 6, N'Motor', N'Brushless 580 KV', N'Negro', N'42 x 50', N'D3548', N'N', N'Unidades', N'Turnigy', N'Caja motores', 1, 29.0, 1),
 (45, 6, N'Hélice', N'Nylon', N'Negro', N'10 x 4.7', N'', N'N', N'Unidades', N'APC', N'Caja hélices', 6, 3.2, 4),
 (46, 6, N'Hélice', N'Madera', N'Natural', N'11 x 7', N'', N'N', N'Unidades', N'Master Airscrew', N'Caja hélices', 4, 5.5, NULL),
 (47, 6, N'Hélice', N'Plegable', N'Negro', N'12 x 6', N'', N'N', N'Unidades', N'Aeronaut', N'Caja hélices', 2, 14.0, NULL),
 (48, 6, N'Spinner', N'Aluminio', N'Plata', N'38 mm', N'', N'N', N'Unidades', N'Dubro', N'Caja hélices', 4, 6.0, NULL),
 -- Ferretería
 (49, 7, N'Varilla de mando', N'Acero 2 mm', N'', N'2 x 300', N'', N'N', N'Unidades', N'Dubro', N'Cajón ferretería', 20, 0.7, NULL),
 (50, 7, N'Horn', N'Nylon', N'Blanco', N'', N'', N'N', N'Unidades', N'Dubro', N'Cajón ferretería', 30, 0.4, NULL),
 (51, 7, N'Clevis', N'Metálico 2-56', N'Plata', N'', N'', N'N', N'Unidades', N'Dubro', N'Cajón ferretería', 24, 0.5, NULL),
 (52, 7, N'Bisagra', N'Laminada para CA', N'Blanco', N'25 x 32', N'', N'N', N'Unidades', N'Great Planes', N'Cajón ferretería', 60, 0.2, NULL),
 (53, 7, N'Tornillo', N'Nylon', N'Blanco', N'M5 x 30', N'', N'N', N'Unidades', N'', N'Cajón ferretería', 40, 0.2, NULL),
 (54, 7, N'Tuerca ciega', N'Acero', N'', N'M3', N'', N'N', N'Unidades', N'', N'Cajón ferretería', 30, 0.2, NULL),
 (55, 7, N'Velcro', N'Adhesivo', N'Negro', N'20 mm', N'', N'N', N'Metros', N'Velcro', N'Cajón ferretería', 3, 2.5, NULL),
 -- Tren de aterrizaje
 (56, 8, N'Rueda', N'Espuma liviana', N'Negro', N'63 mm', N'', N'N', N'Unidades', N'Dubro', N'Cajón ferretería', 6, 4.5, 4),
 (57, 8, N'Rueda', N'Espuma liviana', N'Negro', N'76 mm', N'', N'N', N'Unidades', N'Dubro', N'Cajón ferretería', 4, 5.2, NULL),
 (58, 8, N'Tren principal', N'Aluminio', N'Plata', N'300 mm', N'', N'N', N'Unidades', N'Great Planes', N'Estante', 2, 14.0, NULL),
 (59, 8, N'Rueda de cola', N'Conjunto', N'Negro', N'25 mm', N'', N'U', N'Unidades', N'Sullivan', N'Cajón ferretería', 3, 6.0, NULL),
 -- Pinturas y acabados
 (60, 9, N'Pintura', N'Acrílico', N'Blanco', N'10 ml', N'X-2', N'N', N'Unidades', N'Tamiya', N'Estante', 3, 3.0, NULL),
 (61, 9, N'Pintura', N'Acrílico', N'Negro', N'10 ml', N'X-1', N'N', N'Unidades', N'Tamiya', N'Estante', 3, 3.0, NULL),
 (62, 9, N'Barniz', N'Poliuretano al agua', N'Transparente', N'237 ml', N'', N'N', N'Unidades', N'Minwax', N'Estante', 1, 11.0, NULL),
 (63, 9, N'Calcomanía', N'Vinilo', N'Varios', N'A4', N'', N'N', N'Unidades', N'', N'Estante', 4, 2.5, NULL),
 -- Fibra y composites
 (64, 10, N'Carbono', N'Tubo', N'Negro', N'8 x 6 x 1000', N'', N'N', N'Unidades', N'', N'Caja balsa', 4, 9.0, 2),
 (65, 10, N'Carbono', N'Varilla', N'Negro', N'3 x 1000', N'', N'N', N'Unidades', N'', N'Caja balsa', 6, 3.5, NULL),
 (66, 10, N'Fibra de vidrio', N'Tela 48 g/m²', N'Blanco', N'1000 mm ancho', N'', N'N', N'Metros', N'', N'Bolsa Macro', 3, 7.0, NULL);
SET IDENTITY_INSERT dbo.Materiales OFF;

-- Modelos (Estado: Activo / En reparación / En construcción / Inactivo) ---
SET IDENTITY_INSERT dbo.Modelos ON;
INSERT INTO dbo.Modelos (IdModelo, Modelo, Alias, Estado, Desde) VALUES
 (1, N'Piper J-3 Cub', N'Cubby', N'Activo', '2023-04-15'),
 (2, N'Extra 300', N'Extra', N'Activo', '2024-02-10'),
 (3, N'Spitfire Mk IX', N'Spitty', N'En reparación', '2022-11-20'),
 (4, N'Cessna 182', N'Skylane', N'Activo', '2021-08-05'),
 (5, N'Sig Kadet LT-40', N'Entrenador', N'Inactivo', '2019-06-01'),
 (6, N'P-51 Mustang', N'Mustang', N'En construcción', '2026-05-12'),
 (7, N'Bixler 2', N'Planeador FPV', N'Activo', '2025-03-22'),
 (8, N'Vought F4U Corsair', N'Corsario', N'En construcción', '2026-08-30');
SET IDENTITY_INSERT dbo.Modelos OFF;

-- Materiales usados por cada modelo -------------------------------------
INSERT INTO dbo.MaterialesModelo (IdModelo, IdMaterial, CantidadxModelo) VALUES
 -- Piper J-3 Cub
 (1, 21, 6), (1, 24, 6), (1, 27, 1), (1, 33, 1.2), (1, 8, 4), (1, 11, 1), (1, 14, 1),
 (1, 16, 1), (1, 42, 1), (1, 45, 1), (1, 56, 2), (1, 49, 4), (1, 52, 12), (1, 36, 1),
 -- Extra 300
 (2, 9, 4), (2, 12, 1), (2, 44, 1), (2, 46, 1), (2, 31, 0.9), (2, 30, 1.5), (2, 58, 1),
 (2, 57, 2), (2, 64, 1), (2, 50, 6),
 -- Spitfire Mk IX
 (3, 9, 2), (3, 10, 2), (3, 43, 1), (3, 15, 1), (3, 22, 6), (3, 23, 4), (3, 52, 12),
 (3, 60, 1), (3, 61, 1),
 -- Cessna 182
 (4, 8, 4), (4, 11, 1), (4, 14, 1), (4, 16, 1), (4, 42, 1), (4, 45, 1), (4, 26, 1),
 (4, 56, 3), (4, 30, 1.2),
 -- Sig Kadet LT-40
 (5, 28, 6), (5, 21, 5), (5, 34, 1.5), (5, 9, 2), (5, 40, 1),
 -- P-51 Mustang
 (6, 23, 2), (6, 27, 2), (6, 29, 2), (6, 38, 1), (6, 65, 2),
 -- Bixler 2
 (7, 11, 1), (7, 8, 2), (7, 16, 1), (7, 35, 2), (7, 47, 1),
 -- Vought F4U Corsair
 (8, 22, 4), (8, 25, 1), (8, 24, 8), (8, 66, 0.8), (8, 19, 2);

-- Disponible = Cantidad - lo asignado a modelos
UPDATE M
SET Disponible = FORMAT(M.Cantidad - ISNULL(U.Usado, 0), '0.##', 'en-US')
FROM dbo.Materiales M
LEFT JOIN (SELECT IdMaterial, SUM(CantidadxModelo) AS Usado
           FROM dbo.MaterialesModelo GROUP BY IdMaterial) U
       ON U.IdMaterial = M.IdMaterial;

SELECT 'Familiares' AS Tabla, COUNT(*) AS Filas FROM dbo.Familiares
UNION ALL SELECT 'Materiales', COUNT(*) FROM dbo.Materiales
UNION ALL SELECT 'Modelos', COUNT(*) FROM dbo.Modelos
UNION ALL SELECT 'MaterialesModelo', COUNT(*) FROM dbo.MaterialesModelo;
SELECT IdMaterial, Grupo, Caracteristica, Cantidad, Disponible
FROM dbo.Materiales WHERE CAST(Disponible AS DECIMAL(18,2)) < 0;
GO
