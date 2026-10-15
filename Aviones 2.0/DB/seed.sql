-- Datos de prueba para Aviones (taller de aeromodelismo).
-- Borra todos los datos (menos EstadosModelo) y vuelve a cargar. Lo carga reset.ps1 después de las migraciones.

SET NOCOUNT ON;

DELETE FROM dbo.PedidosCompra;
DELETE FROM dbo.MaterialesModelo;
DELETE FROM dbo.Modelos;
DELETE FROM dbo.Lotes;
DELETE FROM dbo.Materiales;
DELETE FROM dbo.TiposUnidad;
DELETE FROM dbo.Familias;

-- Referencias -------------------------------------------------------------
SET IDENTITY_INSERT dbo.Familias ON;
INSERT INTO dbo.Familias (IdFamilia, Nombre) VALUES
 (1, N'Banco de trabajo'), (2, N'Electrónica'), (3, N'Madera'), (4, N'Revestimientos'),
 (5, N'Adhesivos'), (6, N'Motorización'), (7, N'Ferretería'), (8, N'Tren de aterrizaje'),
 (9, N'Pinturas y acabados'), (10, N'Fibra y composites');
SET IDENTITY_INSERT dbo.Familias OFF;

SET IDENTITY_INSERT dbo.TiposUnidad ON;
INSERT INTO dbo.TiposUnidad (IdTipoUnidad, Nombre) VALUES (1, N'Unidades'), (2, N'Metros');
SET IDENTITY_INSERT dbo.TiposUnidad OFF;

-- Materiales (catálogo) ---------------------------------------------------
SET IDENTITY_INSERT dbo.Materiales ON;
INSERT INTO dbo.Materiales
 (IdMaterial, IdFamilia, Grupo, Caracteristica, Color, Medidas, Codigo, IdTipoUnidad, StockMinimo) VALUES
 (1,  1, N'Cuchilla', N'Acero #11', NULL, NULL, N'X211', 1, 10),
 (2,  1, N'Mango cuchilla', N'Aluminio', N'Plata', NULL, N'X3201', 1, NULL),
 (3,  1, N'Plancha de entelar', N'Termostato regulable', N'Negro', NULL, N'TOPR2100', 1, NULL),
 (4,  1, N'Pistola de calor', N'1500 W', N'Naranja', NULL, N'HT1000', 1, NULL),
 (5,  1, N'Lija', N'Grano 220', NULL, N'230 x 280', NULL, 1, 20),
 (6,  1, N'Alfileres en T', N'Acero niquelado', NULL, N'32 mm', NULL, 1, NULL),
 (7,  1, N'Base de corte', N'Autocurable', N'Verde', N'450 x 300', N'CM-A3', 1, NULL),
 (8,  2, N'Servo', N'Micro 9 g', N'Azul', N'23 x 12 x 29', N'SG90', 1, 8),
 (9,  2, N'Servo', N'Estándar 43 g', N'Negro', N'40 x 20 x 37', N'HS-311', 1, 4),
 (10, 2, N'Servo', N'Digital engranaje metálico', N'Negro', N'40 x 20 x 38', N'MG996R', 1, NULL),
 (11, 2, N'Batería', N'LiPo 3S 2200 mAh 25C', N'Amarillo', N'105 x 34 x 24', NULL, 1, 2),
 (12, 2, N'Batería', N'LiPo 4S 3300 mAh 35C', N'Negro', N'135 x 43 x 30', NULL, 1, NULL),
 (13, 2, N'Batería', N'LiPo 2S 1000 mAh 20C', N'Azul', N'70 x 30 x 15', NULL, 1, NULL),
 (14, 2, N'Variador', N'ESC 40 A con BEC', N'Azul', N'55 x 28 x 7', N'Skywalker 40A', 1, 1),
 (15, 2, N'Variador', N'ESC 30 A', N'Rojo', N'50 x 25 x 8', NULL, 1, NULL),
 (16, 2, N'Receptor', N'6 canales 2.4 GHz', N'Negro', N'46 x 25 x 14', N'FS-iA6B', 1, 2),
 (17, 2, N'Cargador', N'Para baterías de lipo', N'Azul', NULL, N'B6AC', 1, NULL),
 (18, 2, N'Cable', N'Silicona 14 AWG', N'Rojo', NULL, NULL, 2, NULL),
 (19, 2, N'Conector', N'Par macho/hembra', N'Amarillo', NULL, N'XT60', 1, NULL),
 (20, 2, N'Extensión servo', N'300 mm', N'Negro', NULL, NULL, 1, NULL),
 (21, 3, N'Balsa', N'Tabla', N'3/32 x 4 x 36', N'2.4 x 100 x 914', NULL, 1, 10),
 (22, 3, N'Balsa', N'Tabla', N'1/16 x 4 x 36', N'1.6 x 100 x 914', NULL, 1, NULL),
 (23, 3, N'Balsa', N'Tabla', N'1/8 x 4 x 36', N'3.2 x 100 x 914', NULL, 1, 5),
 (24, 3, N'Balsa', N'Varilla cuadrada', N'1/4 x 1/4 x 36', N'6.4 x 6.4 x 914', NULL, 1, NULL),
 (25, 3, N'Balsa', N'Bloque', N'1 x 3 x 12', N'25 x 76 x 305', NULL, 1, NULL),
 (26, 3, N'Abedul', N'Contrachapado', N'1/32 x 12 x 48', N'0.8 x 305 x 1219', NULL, 1, NULL),
 (27, 3, N'Abedul', N'Contrachapado aeronáutico', N'1/8 x 12 x 24', N'3 x 305 x 610', NULL, 1, 2),
 (28, 3, N'Pino', N'Larguero', N'1/4 x 1/4 x 36', N'6.4 x 6.4 x 914', NULL, 1, NULL),
 (29, 3, N'Haya', N'Varilla redonda', N'1/4 x 36', N'6.4 x 914', NULL, 1, NULL),
 (30, 4, N'Plástico', N'Monokote', N'Blanco', N'1.8 x 66', N'TOPQ0204', 2, NULL),
 (31, 4, N'Plástico', N'Monokote', N'Rojo', N'1.8 x 66', N'TOPQ0201', 2, 2),
 (32, 4, N'Plástico', N'Monokote', N'Transparente', N'1.1 x 60', N'TOPQ0200', 2, NULL),
 (33, 4, N'Plástico', N'Oracover', N'Amarillo Cub', N'2 x 60', N'21-030', 2, NULL),
 (34, 4, N'Tela', N'Solartex', N'Blanco antiguo', N'2 x 60', NULL, 2, NULL),
 (35, 4, N'Cinta', N'Tela de bisagra', N'Transparente', N'25 mm', NULL, 2, NULL),
 (36, 5, N'Cianoacrilato', N'Fino', NULL, N'20 g', N'IC-2000', 1, 2),
 (37, 5, N'Cianoacrilato', N'Medio', NULL, N'20 g', N'MaxiCure', 1, NULL),
 (38, 5, N'Epoxi', N'30 minutos', NULL, N'270 ml', N'BSI-204', 1, NULL),
 (39, 5, N'Epoxi', N'5 minutos', NULL, N'270 ml', N'BSI-201', 1, 1),
 (40, 5, N'Cola blanca', N'Alifática', NULL, N'236 ml', NULL, 1, NULL),
 (41, 5, N'Activador', N'Aerosol para cianoacrilato', NULL, N'59 ml', NULL, 1, NULL),
 (42, 6, N'Motor', N'Brushless 1000 KV', N'Plata', N'28 x 30', N'A2212/13T', 1, 2),
 (43, 6, N'Motor', N'Brushless 920 KV', N'Rojo', N'28 x 36', N'2826', 1, NULL),
 (44, 6, N'Motor', N'Brushless 580 KV', N'Negro', N'42 x 50', N'D3548', 1, 1),
 (45, 6, N'Hélice', N'Nylon', N'Negro', N'10 x 4.7', NULL, 1, 4),
 (46, 6, N'Hélice', N'Madera', N'Natural', N'11 x 7', NULL, 1, NULL),
 (47, 6, N'Hélice', N'Plegable', N'Negro', N'12 x 6', NULL, 1, NULL),
 (48, 6, N'Spinner', N'Aluminio', N'Plata', N'38 mm', NULL, 1, NULL),
 (49, 7, N'Varilla de mando', N'Acero 2 mm', NULL, N'2 x 300', NULL, 1, NULL),
 (50, 7, N'Horn', N'Nylon', N'Blanco', NULL, NULL, 1, NULL),
 (51, 7, N'Clevis', N'Metálico 2-56', N'Plata', NULL, NULL, 1, NULL),
 (52, 7, N'Bisagra', N'Laminada para CA', N'Blanco', N'25 x 32', NULL, 1, NULL),
 (53, 7, N'Tornillo', N'Nylon', N'Blanco', N'M5 x 30', NULL, 1, NULL),
 (54, 7, N'Tuerca ciega', N'Acero', NULL, N'M3', NULL, 1, NULL),
 (55, 7, N'Velcro', N'Adhesivo', N'Negro', N'20 mm', NULL, 2, NULL),
 (56, 8, N'Rueda', N'Espuma liviana', N'Negro', N'63 mm', NULL, 1, 4),
 (57, 8, N'Rueda', N'Espuma liviana', N'Negro', N'76 mm', NULL, 1, NULL),
 (58, 8, N'Tren principal', N'Aluminio', N'Plata', N'300 mm', NULL, 1, NULL),
 (59, 8, N'Rueda de cola', N'Conjunto', N'Negro', N'25 mm', NULL, 1, NULL),
 (60, 9, N'Pintura', N'Acrílico', N'Blanco', N'10 ml', N'X-2', 1, NULL),
 (61, 9, N'Pintura', N'Acrílico', N'Negro', N'10 ml', N'X-1', 1, NULL),
 (62, 9, N'Barniz', N'Poliuretano al agua', N'Transparente', N'237 ml', NULL, 1, NULL),
 (63, 9, N'Calcomanía', N'Vinilo', N'Varios', N'A4', NULL, 1, NULL),
 (64, 10, N'Carbono', N'Tubo', N'Negro', N'8 x 6 x 1000', NULL, 1, 2),
 (65, 10, N'Carbono', N'Varilla', N'Negro', N'3 x 1000', NULL, 1, NULL),
 (66, 10, N'Fibra de vidrio', N'Tela 48 g/m²', N'Blanco', N'1000 mm ancho', NULL, 2, NULL);
SET IDENTITY_INSERT dbo.Materiales OFF;

-- Lotes: el lote principal de cada material tiene el mismo número que el material;
-- los lotes 101+ son segundas compras (otra marca, precio o ubicación).
-- Lo disponible se calcula al final según lo usado en modelos.
SET IDENTITY_INSERT dbo.Lotes ON;
INSERT INTO dbo.Lotes (IdLote, IdMaterial, Fabricante, Ubicacion, Cantidad, DisponibleNuevo, ValorUnitario) VALUES
 (1, 1, N'X-Acto', N'Cajón herramientas', 20, 20, 0.60),
 (2, 2, N'X-Acto', N'Escritorio', 2, 2, 6.50),
 (3, 3, N'Top Flite', N'Estante', 1, 1, 35.00),
 (4, 4, N'Wagner', N'Estante', 1, 1, 28.00),
 (5, 5, N'3M', N'Cajón herramientas', 25, 25, 0.80),
 (6, 6, N'Great Planes', N'Cajón herramientas', 200, 200, 0.03),
 (7, 7, N'Olfa', N'Piso', 1, 1, 22.00),
 (8, 8, N'TowerPro', N'Caja electrónica', 12, 12, 3.50),
 (9, 9, N'Hitec', N'Caja electrónica', 8, 8, 13.00),
 (10, 10, N'TowerPro', N'Caja electrónica', 4, 4, 9.00),
 (11, 11, N'Turnigy', N'Bolsa LiPo', 4, 4, 22.00),
 (12, 12, N'Gens Ace', N'Bolsa LiPo', 2, 2, 45.00),
 (13, 13, N'Zippy', N'Bolsa LiPo', 3, 3, 10.00),
 (14, 14, N'Hobbywing', N'Caja electrónica', 3, 3, 25.00),
 (15, 15, N'ZTW', N'Caja electrónica', 2, 2, 17.00),
 (16, 16, N'FlySky', N'Caja electrónica', 4, 4, 9.00),
 (17, 17, N'IMAX', N'Escritorio', 1, 1, 50.00),
 (18, 18, N'Turnigy', N'Caja electrónica', 5, 5, 1.20),
 (19, 19, N'Amass', N'Caja electrónica', 20, 20, 0.80),
 (20, 20, N'Futaba', N'Caja electrónica', 15, 15, 0.90),
 (21, 21, N'Balsa USA', N'Caja balsa', 20, 20, 6.10),
 (22, 22, N'Balsa USA', N'Caja balsa', 15, 15, 5.20),
 (23, 23, N'Balsa USA', N'Caja balsa', 10, 10, 7.40),
 (24, 24, N'Balsa USA', N'Caja balsa', 30, 30, 1.10),
 (25, 25, N'Balsa USA', N'Caja balsa', 3, 3, 8.50),
 (26, 26, N'Balsa USA', N'Caja balsa', 3, 3, 20.60),
 (27, 27, N'Midwest', N'Caja balsa', 5, 5, 12.80),
 (28, 28, N'Midwest', N'Caja balsa', 12, 12, 1.90),
 (29, 29, N'Midwest', N'Caja balsa', 10, 10, 1.40),
 (30, 30, N'Top Flite', N'Bolsa Macro', 3.6, 3.6, 12.00),
 (31, 31, N'Top Flite', N'Bolsa Macro', 1.8, 1.8, 12.00),
 (32, 32, N'Top Flite', N'Bolsa Macro', 0.66, 0.66, 12.00),
 (33, 33, N'Oracover', N'Bolsa Macro', 2, 2, 15.00),
 (34, 34, N'Solarfilm', N'Bolsa Macro', 2, 2, 18.00),
 (35, 35, N'3M', N'Cajón adhesivos', 15, 15, 0.40),
 (36, 36, N'Bob Smith Industries', N'Cajón adhesivos', 4, 4, 7.50),
 (37, 37, N'Bob Smith Industries', N'Cajón adhesivos', 3, 3, 7.50),
 (38, 38, N'Bob Smith Industries', N'Cajón adhesivos', 2, 2, 18.00),
 (39, 39, N'Bob Smith Industries', N'Cajón adhesivos', 1, 1, 14.00),
 (40, 40, N'Titebond', N'Cajón adhesivos', 2, 2, 6.00),
 (41, 41, N'Zap', N'Cajón adhesivos', 1, 1, 9.00),
 (42, 42, N'Racerstar', N'Caja motores', 3, 3, 12.00),
 (43, 43, N'SunnySky', N'Caja motores', 2, 2, 18.00),
 (44, 44, N'Turnigy', N'Caja motores', 1, 1, 29.00),
 (45, 45, N'APC', N'Caja hélices', 6, 6, 3.20),
 (46, 46, N'Master Airscrew', N'Caja hélices', 4, 4, 5.50),
 (47, 47, N'Aeronaut', N'Caja hélices', 2, 2, 14.00),
 (48, 48, N'Dubro', N'Caja hélices', 4, 4, 6.00),
 (49, 49, N'Dubro', N'Cajón ferretería', 20, 20, 0.70),
 (50, 50, N'Dubro', N'Cajón ferretería', 30, 30, 0.40),
 (51, 51, N'Dubro', N'Cajón ferretería', 24, 24, 0.50),
 (52, 52, N'Great Planes', N'Cajón ferretería', 60, 60, 0.15),
 (53, 53, NULL, N'Cajón ferretería', 40, 40, 0.20),
 (54, 54, NULL, N'Cajón ferretería', 30, 30, 0.15),
 (55, 55, N'Velcro', N'Cajón ferretería', 3, 3, 2.50),
 (56, 56, N'Dubro', N'Cajón ferretería', 6, 6, 4.50),
 (57, 57, N'Dubro', N'Cajón ferretería', 4, 4, 5.20),
 (58, 58, N'Great Planes', N'Estante', 2, 2, 14.00),
 (59, 59, N'Sullivan', N'Cajón ferretería', 3, 3, 6.00),
 (60, 60, N'Tamiya', N'Estante', 3, 3, 3.00),
 (61, 61, N'Tamiya', N'Estante', 3, 3, 3.00),
 (62, 62, N'Minwax', N'Estante', 1, 1, 11.00),
 (63, 63, NULL, N'Estante', 4, 4, 2.50),
 (64, 64, NULL, N'Caja balsa', 4, 4, 9.00),
 (65, 65, NULL, N'Caja balsa', 6, 6, 3.50),
 (66, 66, NULL, N'Bolsa Macro', 3, 3, 7.00),
 -- segundas compras
 (101, 8,  N'TowerPro', N'Cajón herramientas', 6, 6, 3.00),
 (102, 21, N'SIG', N'Caja balsa 2', 8, 8, 6.80),
 (103, 30, N'Top Flite', N'Bolsa Macro', 1.8, 1.8, 13.50),
 (104, 11, N'Tattu', N'Bolsa LiPo', 2, 2, 28.00),
 (105, 42, N'Racerstar', N'Caja motores', 1, 1, 11.00);
SET IDENTITY_INSERT dbo.Lotes OFF;

-- Compras de material usado
UPDATE dbo.Lotes SET CompradoUsado = 1, DisponibleNuevo = 0, DisponibleUsado = Cantidad
WHERE IdMaterial IN (2, 3, 7, 12, 17, 43, 59);

-- Fechas de ingreso repartidas en el último año
UPDATE dbo.Lotes SET FechaIngreso = DATEADD(DAY, -((IdLote * 37) % 365), '2026-09-30');

-- Modelos (estados: 1 Activo, 2 En construcción, 3 En reparación, 4 Inactivo) --
SET IDENTITY_INSERT dbo.Modelos ON;
INSERT INTO dbo.Modelos (IdModelo, Nombre, Alias, IdEstadoModelo, Desde) VALUES
 (1, N'Piper J-3 Cub', N'Cubby', 1, '2023-04-15'),
 (2, N'Extra 300', N'Extra', 1, '2024-02-10'),
 (3, N'Spitfire Mk IX', N'Spitty', 3, '2022-11-20'),
 (4, N'Cessna 182', N'Skylane', 1, '2021-08-05'),
 (5, N'Sig Kadet LT-40', N'Entrenador', 4, '2019-06-01'),
 (6, N'P-51 Mustang', N'Mustang', 2, '2026-05-12'),
 (7, N'Bixler 2', N'Planeador FPV', 1, '2025-03-22'),
 (8, N'Vought F4U Corsair', N'Corsario', 2, '2026-08-30');
SET IDENTITY_INSERT dbo.Modelos OFF;

-- Lotes usados por cada modelo (sale del stock nuevo, o del usado si se compró usado) --
INSERT INTO dbo.MaterialesModelo (IdModelo, IdLote, Estado, Cantidad)
SELECT V.IdModelo, V.IdLote, CASE WHEN L.CompradoUsado = 1 THEN 'U' ELSE 'N' END, V.Cantidad
FROM (VALUES
 -- Piper J-3 Cub
 (1, 102, 6), (1, 24, 6), (1, 27, 1), (1, 33, 1.2), (1, 101, 4), (1, 11, 1), (1, 14, 1),
 (1, 16, 1), (1, 42, 1), (1, 45, 1), (1, 56, 2), (1, 49, 4), (1, 52, 12), (1, 36, 1),
 -- Extra 300
 (2, 9, 4), (2, 12, 1), (2, 44, 1), (2, 46, 1), (2, 31, 0.9), (2, 30, 1.5), (2, 58, 1),
 (2, 57, 2), (2, 64, 1), (2, 50, 6),
 -- Spitfire Mk IX
 (3, 9, 2), (3, 10, 2), (3, 43, 1), (3, 15, 1), (3, 22, 6), (3, 23, 4), (3, 52, 12),
 (3, 60, 1), (3, 61, 1),
 -- Cessna 182
 (4, 8, 4), (4, 11, 1), (4, 14, 1), (4, 16, 1), (4, 105, 1), (4, 45, 1), (4, 26, 1),
 (4, 56, 3), (4, 103, 1.2),
 -- Sig Kadet LT-40
 (5, 28, 6), (5, 21, 5), (5, 34, 1.5), (5, 9, 2), (5, 40, 1),
 -- P-51 Mustang
 (6, 23, 2), (6, 27, 2), (6, 29, 2), (6, 38, 1), (6, 65, 2),
 -- Bixler 2
 (7, 104, 1), (7, 8, 2), (7, 16, 1), (7, 35, 2), (7, 47, 1),
 -- Vought F4U Corsair
 (8, 22, 4), (8, 25, 1), (8, 24, 8), (8, 66, 0.8), (8, 19, 2)
) V (IdModelo, IdLote, Cantidad)
JOIN dbo.Lotes L ON L.IdLote = V.IdLote;

-- Disponible de cada lote = Cantidad - lo usado en modelos (en el stock que corresponde)
UPDATE L
SET DisponibleNuevo = CASE WHEN L.CompradoUsado = 0 THEN L.Cantidad - ISNULL(U.Usado, 0) ELSE 0 END,
    DisponibleUsado = CASE WHEN L.CompradoUsado = 1 THEN L.Cantidad - ISNULL(U.Usado, 0) ELSE 0 END
FROM dbo.Lotes L
LEFT JOIN (SELECT IdLote, SUM(Cantidad) AS Usado FROM dbo.MaterialesModelo GROUP BY IdLote) U
       ON U.IdLote = L.IdLote;

-- Un motor reciclado de un modelo que ya no está: volvió como usado a su lote
UPDATE dbo.Lotes SET DisponibleUsado = 1 WHERE IdLote = 42;

-- Stock mínimo propio de algunos materiales (el resto usa el mínimo general)
UPDATE dbo.Materiales SET StockMinimo = NULL;
UPDATE dbo.Materiales SET StockMinimo = 4  WHERE IdMaterial IN (8, 9);        -- servos
UPDATE dbo.Materiales SET StockMinimo = 2  WHERE IdMaterial IN (11, 42, 45);  -- LiPo 3S, motor, hélice
UPDATE dbo.Materiales SET StockMinimo = 5  WHERE IdMaterial IN (21, 22, 23);  -- tablas de balsa
UPDATE dbo.Materiales SET StockMinimo = 10 WHERE IdMaterial IN (1, 52);       -- cuchillas, bisagras
UPDATE dbo.Materiales SET StockMinimo = 0  WHERE IdMaterial IN (3, 4, 17);    -- herramientas: no se recompran

-- Pedidos pendientes
INSERT INTO dbo.PedidosCompra (IdMaterial, Cantidad, FechaPedido, Nota) VALUES
 (8,  10, '2026-10-02', N'Para el Mustang y el Corsair'),
 (44, 1,  '2026-10-05', NULL),
 (31, 2,  '2026-10-05', N'Rojo para el Extra'),
 (21, 10, '2026-10-09', N'Preguntar precio en SIG');
GO
