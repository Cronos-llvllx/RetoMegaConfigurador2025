-- BASE DE DATOS.
CREATE DATABASE megadb
GO

USE megadb
GO

-- TABLAS.
CREATE TABLE Servicio (
	Idservicio INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Cantidad INTEGER DEFAULT NULL,
	PrecioBase NUMERIC(6, 2) NOT NULL,
	Tipo TINYINT NOT NULL
);

CREATE TABLE Paquete (
	Idpaquete INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	PrecioBase Numeric(6, 2) NOT NULL,
	Tipo TINYINT NOT NULL
);

CREATE TABLE Promocion (
	Idpromocion INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Alcance TINYINT DEFAULT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Duracion INTEGER DEFAULT NULL,
	FechaRegistro DATE DEFAULT CAST(GETDATE() AS DATE),
	PrecioPorcen NUMERIC(6, 2) NOT NULL,
	Tipo TINYINT NOT NULL,
	Vigencia DATE NOT NULL
);

CREATE TABLE Ciudad (
	Idciudad INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Nombre NVARCHAR(100) NOT NULL
);

GO

CREATE TABLE Colonia (
	Idcolonia INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Idciudad INTEGER NOT NULL,
	Nombre NVARCHAR(100) NOT NULL,

	CONSTRAINT fk_colonia_idciudad FOREIGN KEY(Idciudad) REFERENCES dbo.Ciudad(Idciudad)
);

GO

CREATE TABLE Suscriptor (
	Idsuscriptor INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Idcolonia INTEGER NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Nombre NVARCHAR(100) NOT NULL,
	Telefono NVARCHAR(10) NOT NULL,
	Tipo TINYINT NOT NULL,

	CONSTRAINT fk_suscriptor_idcolonia FOREIGN KEY(Idcolonia) REFERENCES dbo.Colonia(Idcolonia)
);

GO

CREATE TABLE Contrato (
	Idcontrato INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Idsuscriptor INTEGER NOT NULL,
	FechaContr DATE NOT NULL,
	FechaFin DATE DEFAULT NULL,
	PrecioBase NUMERIC(6, 2) NOT NULL,

	CONSTRAINT fk_contrato_idcontrato FOREIGN KEY(Idsuscriptor) REFERENCES dbo.Suscriptor(Idsuscriptor)
);

GO

CREATE TABLE PromoPersonalizada (
  Idpromopersonalizada INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,
  Idcontrato INTEGER NOT NULL,
  FechaAplicacion DATE NOT NULL,
  Descripcion NVARCHAR(100) NOT NULL,
  PrecioPorcen NUMERIC(6,2) NOT NULL,

  CONSTRAINT fk_promopersonalizada_idcontrato FOREIGN KEY(Idcontrato) REFERENCES dbo.Contrato(Idcontrato)
);

GO

-- RELACIONES.
CREATE TABLE ContratoPaquete (
	Idcontrato INTEGER NOT NULL,
	Idpaquete INTEGER NOT NULL,
	FechaAdicion DATE NOT NULL,
	FechaRetiro DATE DEFAULT NULL,

	CONSTRAINT pk_contratopaquete PRIMARY KEY(Idcontrato, Idpaquete),
	CONSTRAINT fk_contratopaquete_idcontrato FOREIGN KEY(Idcontrato) REFERENCES dbo.Contrato(Idcontrato),
	CONSTRAINT fk_contratopaquete_idpaquete FOREIGN KEY(Idpaquete) REFERENCES dbo.Paquete(Idpaquete)
);

CREATE TABLE PaqueteServicio (
	Idpaquete INTEGER NOT NULL,
	Idservicio INTEGER NOT NULL,

	CONSTRAINT pk_paqueteservicio PRIMARY KEY(Idpaquete, Idservicio),
	CONSTRAINT fk_paqueteservicio_idpaquete FOREIGN KEY(Idpaquete) REFERENCES dbo.Paquete(Idpaquete),
	CONSTRAINT fk_paqueteservicio_idservicio FOREIGN KEY(Idservicio) REFERENCES dbo.Servicio(Idservicio)
);

CREATE TABLE PromocionPaquete (
	Idpromocion INTEGER NOT NULL,
	Idpaquete INTEGER NOT NULL,

	CONSTRAINT pk_promocionpaquete PRIMARY KEY(Idpromocion, Idpaquete),
	CONSTRAINT fk_promocionpaquete_idpromocion FOREIGN KEY(Idpromocion) REFERENCES dbo.Promocion(Idpromocion),
	CONSTRAINT fk_promocionpaquete_idpaquete FOREIGN KEY(Idpaquete) REFERENCES dbo.Paquete(Idpaquete)
);

CREATE TABLE PromocionCiudad (
	Idpromocion INTEGER NOT NULL,
	Idciudad INTEGER NOT NULL,

	CONSTRAINT pk_promocionciudad PRIMARY KEY(Idpromocion, Idciudad),
	CONSTRAINT fk_promocionciudad_idpromocion FOREIGN KEY(Idpromocion) REFERENCES dbo.Promocion(Idpromocion),
	CONSTRAINT fk_promocionciudad_idciudad FOREIGN KEY(Idciudad) REFERENCES dbo.Ciudad(Idciudad)
);

CREATE TABLE PromocionColonia (
	Idpromocion INTEGER NOT NULL,
	Idcolonia INTEGER NOT NULL,

	CONSTRAINT pk_promocioncolonia PRIMARY KEY(Idpromocion, Idcolonia),
	CONSTRAINT fk_promocioncolonia_idpromocion FOREIGN KEY (Idpromocion) REFERENCES dbo.Promocion(Idpromocion),
	CONSTRAINT fk_promocioncolonia_idcolonia FOREIGN KEY (Idcolonia) REFERENCES dbo.Colonia(Idcolonia)

);

CREATE TABLE PromocionContrato (
	Idpromocion INTEGER NOT NULL,
	Idcontrato INTEGER NOT NULL,

	CONSTRAINT pk_promocioncontrato PRIMARY KEY(Idpromocion, Idcontrato),
	CONSTRAINT fk_promocioncontrato_idpromocion FOREIGN KEY(Idpromocion) REFERENCES dbo.Promocion(Idpromocion),
	CONSTRAINT fk_promocioncontrato_idcontrato FOREIGN KEY(Idcontrato) REFERENCES dbo.Contrato(Idcontrato)
);
GO

-- FUNCIONES Y PROCEDIMIENTOS.


-- CASOS DE USO.

-- Solo para promociones para servicios (mensualidades).
-- Existe restricción en cómo aplicar promociones específicamente para colonias o ciudades:
--  1. ** RECONSIDERAR LA POSIBILIDAD DE SOPORTAR VARIAS COLONIAS DE VARIAS CIUDADES: Si se aplican promociones a una o más colonias, solo se puede aplicar a una ciudad (de esas colonias).
--  2. Si se aplican promociones a una o más ciudades, no se pueden aplicar a colonias específicas (por defecto, ya aplican a todas las colonias).
--  2. Cada restricción puede agregar paquetes específicos que aplicarán para esas promociones. Si no se especifican paquetes, entonces todos los paquetes aplicarán para dichas promociones.

-- **CONSIDERACIONES EXTRSA**
-- Promociones por desperfectos técnicos...

-- El tipo de suscriptor, promociones que aplican al precio de contratación, promociones que aplican a paquetes contratados

-- 1. Promoción de contratación.
-- Paquetes previamente registrados.

-- 2. Promociones de paquetes que aplican al público general a todos o varios paquetes.
-- Alta de promoción.

-- 3. Promociones de paquetes que aplican a una o varias colonias (posiblemente varias colonias por varias ciudades).

-- 4. Promociones de paquetes que aplican a una o varias ciudades (todas las colonias).

-- 5. Promociones que aplican para la contratación se los servicios.


-- Definición de datos de prueba.
-- Ciudades.
INSERT INTO dbo.Ciudad (Nombre) VALUES ('Guadalajara'); -- 1;
INSERT INTO dbo.Ciudad (Nombre) VALUES ('Tlaquepaque'); -- 2;
INSERT INTO dbo.Ciudad (Nombre) VALUES ('Zapopan'); -- 3;
GO

--
-- Colonias.
INSERT INTO dbo.Colonia (Idciudad, Nombre) VALUES (1, 'Providencia'); -- 1
INSERT INTO dbo.Colonia (Idciudad, Nombre) VALUES (1, 'Americana'); -- 2
INSERT INTO dbo.Colonia (Idciudad, Nombre) VALUES (1, 'Chapalita'); -- 3
--
INSERT INTO dbo.Colonia (Idciudad, Nombre) VALUES (2, 'Lomas del Tapatío'); -- 4
INSERT INTO dbo.Colonia (Idciudad, Nombre) VALUES (2, 'Las Huertas'); -- 5
INSERT INTO dbo.Colonia (Idciudad, Nombre) VALUES (2, 'Fraccionamiento Revolución'); -- 6
--
INSERT INTO dbo.Colonia (Idciudad, Nombre) VALUES (3, 'Valle Imperial'); -- 7
INSERT INTO dbo.Colonia (Idciudad, Nombre) VALUES (3, 'Solares'); -- 8
INSERT INTO dbo.Colonia (Idciudad, Nombre) VALUES (3, 'Bugambilias'); -- 9

--
-- Definición de servicios.
-- Telefonía (para paquetes resienciales).
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (1, 200, 1); -- 1
-- Televisión (para paquetes residenciales).
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (1, 300, 1); -- 2
-- Internet (para paquetes residenciales).
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (100, 700, 1); -- 3
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (200, 1300, 1); -- 4
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (300, 1800, 1); -- 5
--
-- Telefonía (para paquetes empresariales).
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (2, 500, 2); -- 6
-- Televisión (para paquetes empresariales).
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (1, 400, 2); -- 7
-- Internet (para paquetes empresariales).
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (100, 800, 2); -- 8
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (200, 1400, 2); -- 9
INSERT INTO dbo.Servicio (cantidad, precioBase, tipo) VALUES (300, 1900, 2); -- 10

--
-- Paquetes.
-- Residenciales (2 pack).
INSERT INTO dbo.Paquete(Nombre, PrecioBase, Tipo) VALUES ('Simétrico 100', 900, 1); -- 1
GO
INSERT INTO dbo.PaqueteServicio VALUES (1, 1);
INSERT INTO dbo.PaqueteServicio VALUES (1, 3);
--
INSERT INTO dbo.Paquete(Nombre, PrecioBase, Tipo) VALUES ('Simétrico 200', 1500, 1); -- 2
GO
INSERT INTO dbo.PaqueteServicio VALUES (2, 1);
INSERT INTO dbo.PaqueteServicio VALUES (2, 4);
--
INSERT INTO dbo.Paquete(Nombre, PrecioBase, Tipo) VALUES ('Simétrico 300', 2000, 1); -- 3
GO
INSERT INTO dbo.PaqueteServicio VALUES (3, 1);
INSERT INTO dbo.PaqueteServicio VALUES (3, 5);
-- Residenciales (3 pack).
INSERT INTO dbo.Paquete(Nombre, PrecioBase, Tipo) VALUES ('Simétrico 100 3 Pack', 1200, 1); -- 4
GO
INSERT INTO dbo.PaqueteServicio VALUES (4, 1);
INSERT INTO dbo.PaqueteServicio VALUES (4, 2);
INSERT INTO dbo.PaqueteServicio VALUES (4, 3);
--
INSERT INTO dbo.Paquete(Nombre, PrecioBase, Tipo) VALUES ('Simétrico 200 3 Pack', 1800, 1); -- 5
GO
INSERT INTO dbo.PaqueteServicio VALUES (5, 1);
INSERT INTO dbo.PaqueteServicio VALUES (5, 2);
INSERT INTO dbo.PaqueteServicio VALUES (5, 4);
--
INSERT INTO dbo.Paquete(Nombre, PrecioBase, Tipo) VALUES ('Simétrico 300 3 Pack', 2300, 1); -- 6
GO
INSERT INTO dbo.PaqueteServicio VALUES (6, 1);
INSERT INTO dbo.PaqueteServicio VALUES (6, 2);
INSERT INTO dbo.PaqueteServicio VALUES (6, 5);
--
-- Empresariales (2 pack).
INSERT INTO dbo.Paquete(Nombre, PrecioBase, Tipo) VALUES ('Simétrico 100 Negocios', 1300, 2); -- 7
GO
INSERT INTO dbo.PaqueteServicio VALUES (7, 6);
INSERT INTO dbo.PaqueteServicio VALUES (7, 8);
--
INSERT INTO dbo.Paquete(Nombre, PrecioBase, Tipo) VALUES ('Simétrico 200 Negocios', 1900, 2); -- 8
GO
INSERT INTO dbo.PaqueteServicio VALUES (8, 6);
INSERT INTO dbo.PaqueteServicio VALUES (8, 9);
--
INSERT INTO dbo.Paquete(Nombre, PrecioBase, Tipo) VALUES ('Simétrico 300 Negocios', 2400, 2); -- 9
GO
INSERT INTO dbo.PaqueteServicio VALUES (9, 6);
INSERT INTO dbo.PaqueteServicio VALUES (9, 10);

--
-- Suscriptores (1 por colonia), contratos (los gastos de contratación tienen un precio base de 1000) y relación con paquetes.
-- 1
INSERT INTO dbo.Suscriptor (Idcolonia, Email, Nombre, Telefono, Tipo) VALUES (1, 'ejemplo1@gmail.com', 'Juan Escutia', '3359784511', 1);
GO
INSERT INTO dbo.Contrato (Idsuscriptor, FechaContr, PrecioBase) VALUES (1, '2024-11-05', 1000);
GO
INSERT INTO dbo.ContratoPaquete (Idcontrato, Idpaquete, FechaAdicion) VALUES (1, 1, '2024-11-05');
-- 2
INSERT INTO dbo.Suscriptor (Idcolonia, Email, Nombre, Telefono, Tipo) VALUES (2, 'ejemplo2@hotmail.com', 'Thel Vadam', '3348665856', 1);
GO
INSERT INTO dbo.Contrato (Idsuscriptor, FechaContr, PrecioBase) VALUES(2, '2025-03-27', 1000);
GO
INSERT INTO dbo.ContratoPaquete (Idcontrato, Idpaquete, FechaAdicion) VALUES (2, 2, '2025-03-27');
-- 3
INSERT INTO dbo.Suscriptor (Idcolonia, Email, Nombre, Telefono, Tipo) VALUES (3, 'ejemplo3@outlook.com', 'Raymundo Valderrábano', '3389554800', 2);
GO
INSERT INTO dbo.Contrato (Idsuscriptor, FechaContr, PrecioBase) VALUES (3, '2023-08-31', 1000);
GO
INSERT INTO dbo.ContratoPaquete (Idcontrato, Idpaquete, FechaAdicion) VALUES (3, 7, '2023-08-31');
-- 4
INSERT INTO dbo.Suscriptor (Idcolonia, Email, Nombre, Telefono, Tipo) VALUES (4, 'ejemplo4@fazbear.com', 'Freddy Fazbear', '3389303010', 1);
GO
INSERT INTO dbo.Contrato (Idsuscriptor, FechaContr, PrecioBase) VALUES (4, '2025-01-10', 1000);
GO
INSERT INTO dbo.ContratoPaquete (Idcontrato, Idpaquete, FechaAdicion) VALUES (4, 3, '2025-01-10');
-- 5
INSERT INTO dbo.Suscriptor (Idcolonia, Email, Nombre, Telefono, Tipo) VALUES (5, 'ejemplo5@live.com', 'Maria Antonieta de las Nieves', '3378441000', 1);
GO
INSERT INTO dbo.Contrato (Idsuscriptor, FechaContr, PrecioBase) VALUES (5, '2024-12-23', 1000);
GO
INSERT INTO dbo.ContratoPaquete (Idcontrato, Idpaquete, FechaAdicion) VALUES (5, 4, '2024-12-23');
-- 6
INSERT INTO dbo.Suscriptor (Idcolonia, Email, Nombre, Telefono, Tipo) VALUES (6, 'ejemplo6@gmail.com', 'Olivia Benson', '3398225050', 1);
GO
INSERT INTO dbo.Contrato (Idsuscriptor, FechaContr, PrecioBase) VALUES (6, '2025-04-11', 1000);
GO
INSERT INTO dbo.ContratoPaquete (Idcontrato, Idpaquete, FechaAdicion) VALUES (6, 5, '2025-04-11');
-- 7
INSERT INTO dbo.Suscriptor (Idcolonia, Email, Nombre, Telefono, Tipo) VALUES (7, 'ejemplo7@outlook.com', 'Aldo Camarena', '3310926826', 1);
GO
INSERT INTO dbo.Contrato (Idsuscriptor, FechaContr, PrecioBase) VALUES (7, '2025-01-01', 1000);
GO
INSERT INTO dbo.ContratoPaquete (Idcontrato, Idpaquete, FechaAdicion) VALUES (7, 6, '2025-01-01');
-- 8
INSERT INTO dbo.Suscriptor (Idcolonia, Email, Nombre, Telefono, Tipo) VALUES (8, 'ejemplo8@outlook.com', 'Fernando Medrano', '3398985014', 1);
GO
INSERT INTO dbo.Contrato (Idsuscriptor, FechaContr, PrecioBase) VALUES (8, '2025-02-15', 1000);
GO
INSERT INTO dbo.ContratoPaquete (Idcontrato, Idpaquete, FechaAdicion) VALUES (8, 1, '2025-02-15');
-- 9
INSERT INTO dbo.Suscriptor (Idcolonia, Email, Nombre, Telefono, Tipo) VALUES (9, 'ejemplo9@hotmail.com', 'Arturo Martínez', '3340222126', 2);
GO
INSERT INTO dbo.Contrato (Idsuscriptor, FechaContr, PrecioBase) VALUES (9, '2024-09-14', 1000);
GO
INSERT INTO dbo.ContratoPaquete (Idcontrato, Idpaquete, FechaAdicion) VALUES (9, 7, '2024-09-14');

--
-- Promociones.
-- Promociones por contratación.
INSERT INTO dbo.Promocion (Nombre, PrecioPorcen, Tipo, Vigencia, FechaRegistro) VALUES ('Promo de Contratación', 1, 1, '2026-01-01', '2025-01-01');
GO
--
-- Relaciones.
INSERT INTO PromocionContrato (Idpromocion, Idcontrato) VALUES (1, 2);
INSERT INTO PromocionContrato (Idpromocion, Idcontrato) VALUES (1, 4);
INSERT INTO PromocionContrato (Idpromocion, Idcontrato) VALUES (1, 6);
INSERT INTO PromocionContrato (Idpromocion, Idcontrato) VALUES (1, 7);
INSERT INTO PromocionContrato (Idpromocion, Idcontrato) VALUES (1, 8);
--
-- Promociones por paquete.
INSERT INTO Promocion (Alcance, Nombre, Duracion, FechaRegistro, PrecioPorcen, Tipo, Vigencia) VALUES (1, 'Promo Inicial 6 Meses', 6, '2025-01-01', 0.4, 1, '2025-08-1')
GO
-- Relaciones.
INSERT INTO PromocionPaquete (Idpromocion, Idpaquete) VALUES (2, 1);
INSERT INTO PromocionPaquete (Idpromocion, Idpaquete) VALUES (2, 2);
INSERT INTO PromocionPaquete (Idpromocion, Idpaquete) VALUES (2, 3);
INSERT INTO PromocionPaquete (Idpromocion, Idpaquete) VALUES (2, 4);
INSERT INTO PromocionPaquete (Idpromocion, Idpaquete) VALUES (2, 5);
INSERT INTO PromocionPaquete (Idpromocion, Idpaquete) VALUES (2, 6);
