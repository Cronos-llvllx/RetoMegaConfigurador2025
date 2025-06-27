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
	Alcance TINYINT NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Duracion INTEGER DEFAULT NULL,
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

-- FUNCIONES Y PROCEDIMIENTOS.


-- CASOS DE USO.
