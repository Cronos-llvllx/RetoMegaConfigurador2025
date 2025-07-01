IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Ciudad] (
    [Idciudad] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Ciudad] PRIMARY KEY ([Idciudad])
);

CREATE TABLE [Paquete] (
    [Idpaquete] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [PrecioBase] decimal(6,2) NOT NULL,
    [Tipo] tinyint NOT NULL,
    CONSTRAINT [PK_Paquete] PRIMARY KEY ([Idpaquete])
);

CREATE TABLE [Promocion] (
    [Idpromocion] int NOT NULL IDENTITY,
    [Alcance] tinyint NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [Duracion] int NULL,
    [FechaRegistro] datetime2 NOT NULL,
    [PrecioPorcen] decimal(6,2) NOT NULL,
    [Tipo] tinyint NOT NULL,
    [Vigencia] datetime2 NOT NULL,
    CONSTRAINT [PK_Promocion] PRIMARY KEY ([Idpromocion])
);

CREATE TABLE [Servicio] (
    [Idservicio] int NOT NULL IDENTITY,
    [Cantidad] int NULL,
    [PrecioBase] decimal(6,2) NOT NULL,
    [Tipo] tinyint NOT NULL,
    CONSTRAINT [PK_Servicio] PRIMARY KEY ([Idservicio])
);

CREATE TABLE [Colonia] (
    [Idcolonia] int NOT NULL IDENTITY,
    [Idciudad] int NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Colonia] PRIMARY KEY ([Idcolonia]),
    CONSTRAINT [FK_Colonia_Ciudad_Idciudad] FOREIGN KEY ([Idciudad]) REFERENCES [Ciudad] ([Idciudad]) ON DELETE CASCADE
);

CREATE TABLE [PromocionCiudad] (
    [Idpromocion] int NOT NULL,
    [Idciudad] int NOT NULL,
    CONSTRAINT [PK_PromocionCiudad] PRIMARY KEY ([Idpromocion], [Idciudad]),
    CONSTRAINT [FK_PromocionCiudad_Ciudad_Idciudad] FOREIGN KEY ([Idciudad]) REFERENCES [Ciudad] ([Idciudad]) ON DELETE CASCADE,
    CONSTRAINT [FK_PromocionCiudad_Promocion_Idpromocion] FOREIGN KEY ([Idpromocion]) REFERENCES [Promocion] ([Idpromocion]) ON DELETE CASCADE
);

CREATE TABLE [PromocionPaquete] (
    [Idpromocion] int NOT NULL,
    [Idpaquete] int NOT NULL,
    CONSTRAINT [PK_PromocionPaquete] PRIMARY KEY ([Idpromocion], [Idpaquete]),
    CONSTRAINT [FK_PromocionPaquete_Paquete_Idpaquete] FOREIGN KEY ([Idpaquete]) REFERENCES [Paquete] ([Idpaquete]) ON DELETE CASCADE,
    CONSTRAINT [FK_PromocionPaquete_Promocion_Idpromocion] FOREIGN KEY ([Idpromocion]) REFERENCES [Promocion] ([Idpromocion]) ON DELETE CASCADE
);

CREATE TABLE [PaqueteServicio] (
    [Idpaquete] int NOT NULL,
    [Idservicio] int NOT NULL,
    CONSTRAINT [PK_PaqueteServicio] PRIMARY KEY ([Idpaquete], [Idservicio]),
    CONSTRAINT [FK_PaqueteServicio_Paquete_Idpaquete] FOREIGN KEY ([Idpaquete]) REFERENCES [Paquete] ([Idpaquete]) ON DELETE CASCADE,
    CONSTRAINT [FK_PaqueteServicio_Servicio_Idservicio] FOREIGN KEY ([Idservicio]) REFERENCES [Servicio] ([Idservicio]) ON DELETE CASCADE
);

CREATE TABLE [PromocionColonia] (
    [Idpromocion] int NOT NULL,
    [Idcolonia] int NOT NULL,
    CONSTRAINT [PK_PromocionColonia] PRIMARY KEY ([Idpromocion], [Idcolonia]),
    CONSTRAINT [FK_PromocionColonia_Colonia_Idcolonia] FOREIGN KEY ([Idcolonia]) REFERENCES [Colonia] ([Idcolonia]) ON DELETE CASCADE,
    CONSTRAINT [FK_PromocionColonia_Promocion_Idpromocion] FOREIGN KEY ([Idpromocion]) REFERENCES [Promocion] ([Idpromocion]) ON DELETE CASCADE
);

CREATE TABLE [Suscriptor] (
    [Idsuscriptor] int NOT NULL IDENTITY,
    [Idcolonia] int NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [Telefono] nvarchar(max) NOT NULL,
    [Tipo] tinyint NOT NULL,
    CONSTRAINT [PK_Suscriptor] PRIMARY KEY ([Idsuscriptor]),
    CONSTRAINT [FK_Suscriptor_Colonia_Idcolonia] FOREIGN KEY ([Idcolonia]) REFERENCES [Colonia] ([Idcolonia]) ON DELETE CASCADE
);

CREATE TABLE [Contrato] (
    [Idcontrato] int NOT NULL IDENTITY,
    [Idsuscriptor] int NOT NULL,
    [FechaContr] datetime2 NOT NULL,
    [FechaFin] datetime2 NULL,
    [PrecioBase] decimal(6,2) NOT NULL,
    CONSTRAINT [PK_Contrato] PRIMARY KEY ([Idcontrato]),
    CONSTRAINT [FK_Contrato_Suscriptor_Idsuscriptor] FOREIGN KEY ([Idsuscriptor]) REFERENCES [Suscriptor] ([Idsuscriptor]) ON DELETE CASCADE
);

CREATE TABLE [ContratoPaquete] (
    [Idcontrato] int NOT NULL,
    [Idpaquete] int NOT NULL,
    [FechaAdicion] datetime2 NOT NULL,
    [FechaRetiro] datetime2 NULL,
    CONSTRAINT [PK_ContratoPaquete] PRIMARY KEY ([Idcontrato], [Idpaquete]),
    CONSTRAINT [FK_ContratoPaquete_Contrato_Idcontrato] FOREIGN KEY ([Idcontrato]) REFERENCES [Contrato] ([Idcontrato]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContratoPaquete_Paquete_Idpaquete] FOREIGN KEY ([Idpaquete]) REFERENCES [Paquete] ([Idpaquete]) ON DELETE CASCADE
);

CREATE TABLE [PromocionContrato] (
    [Idpromocion] int NOT NULL,
    [Idcontrato] int NOT NULL,
    CONSTRAINT [PK_PromocionContrato] PRIMARY KEY ([Idpromocion], [Idcontrato]),
    CONSTRAINT [FK_PromocionContrato_Contrato_Idcontrato] FOREIGN KEY ([Idcontrato]) REFERENCES [Contrato] ([Idcontrato]) ON DELETE CASCADE,
    CONSTRAINT [FK_PromocionContrato_Promocion_Idpromocion] FOREIGN KEY ([Idpromocion]) REFERENCES [Promocion] ([Idpromocion]) ON DELETE CASCADE
);

CREATE TABLE [PromoPersonalizada] (
    [Idpromopersonalizada] int NOT NULL IDENTITY,
    [Idcontrato] int NOT NULL,
    [FechaAplicacion] datetime2 NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [PrecioPorcen] decimal(6,2) NOT NULL,
    CONSTRAINT [PK_PromoPersonalizada] PRIMARY KEY ([Idpromopersonalizada]),
    CONSTRAINT [FK_PromoPersonalizada_Contrato_Idcontrato] FOREIGN KEY ([Idcontrato]) REFERENCES [Contrato] ([Idcontrato]) ON DELETE CASCADE
);

CREATE INDEX [IX_Colonia_Idciudad] ON [Colonia] ([Idciudad]);

CREATE UNIQUE INDEX [IX_Contrato_Idsuscriptor] ON [Contrato] ([Idsuscriptor]);

CREATE INDEX [IX_ContratoPaquete_Idpaquete] ON [ContratoPaquete] ([Idpaquete]);

CREATE INDEX [IX_PaqueteServicio_Idservicio] ON [PaqueteServicio] ([Idservicio]);

CREATE INDEX [IX_PromocionCiudad_Idciudad] ON [PromocionCiudad] ([Idciudad]);

CREATE INDEX [IX_PromocionColonia_Idcolonia] ON [PromocionColonia] ([Idcolonia]);

CREATE INDEX [IX_PromocionContrato_Idcontrato] ON [PromocionContrato] ([Idcontrato]);

CREATE INDEX [IX_PromocionPaquete_Idpaquete] ON [PromocionPaquete] ([Idpaquete]);

CREATE INDEX [IX_PromoPersonalizada_Idcontrato] ON [PromoPersonalizada] ([Idcontrato]);

CREATE INDEX [IX_Suscriptor_Idcolonia] ON [Suscriptor] ([Idcolonia]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250630010419_InitialCreate', N'9.0.5');

COMMIT;
GO

