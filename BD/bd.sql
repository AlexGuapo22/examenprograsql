-- =============================================
-- Base de datos del examen: Sistema de Reparaciones
-- Motor: SQL Server 2022 (corriendo en Docker)
-- =============================================

IF DB_ID('ReparacionesUH') IS NULL
    CREATE DATABASE ReparacionesUH;
GO

USE ReparacionesUH;
GO

-- creacion de las tablas (segun el diagrama)

CREATE TABLE Usuarios
(
    UsuarioID          INT IDENTITY(1,1) PRIMARY KEY,
    Nombre             VARCHAR(100) NOT NULL,
    CorreoElectronico  VARCHAR(100) NOT NULL,
    Telefono           VARCHAR(20)
);

CREATE TABLE Equipos
(
    EquipoID    INT IDENTITY(1,1) PRIMARY KEY,
    TipoEquipo  VARCHAR(50)  NOT NULL,
    Modelo      VARCHAR(100) NOT NULL,
    UsuarioID   INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioID)
);

CREATE TABLE Tecnicos
(
    TecnicoID     INT IDENTITY(1,1) PRIMARY KEY,
    Nombre        VARCHAR(100) NOT NULL,
    Especialidad  VARCHAR(100)
);

CREATE TABLE Reparaciones
(
    ReparacionID    INT IDENTITY(1,1) PRIMARY KEY,
    EquipoID        INT NOT NULL FOREIGN KEY REFERENCES Equipos(EquipoID),
    FechaSolicitud  DATE NOT NULL,
    Estado          VARCHAR(50) NOT NULL
);

CREATE TABLE Asignaciones
(
    AsignacionID     INT IDENTITY(1,1) PRIMARY KEY,
    ReparacionID     INT NOT NULL FOREIGN KEY REFERENCES Reparaciones(ReparacionID),
    TecnicoID        INT NOT NULL FOREIGN KEY REFERENCES Tecnicos(TecnicoID),
    FechaAsignacion  DATE NOT NULL
);

CREATE TABLE DetallesReparacion
(
    DetalleID     INT IDENTITY(1,1) PRIMARY KEY,
    ReparacionID  INT NOT NULL FOREIGN KEY REFERENCES Reparaciones(ReparacionID),
    Descripcion   VARCHAR(255)
);
GO

-- datos de prueba
INSERT INTO Usuarios (Nombre, CorreoElectronico, Telefono) VALUES
    ('Maria Lopez',  'maria@uh.com', '8888-1111'),
    ('Jose Ramirez', 'jose@uh.com',  '8888-2222');

INSERT INTO Equipos (TipoEquipo, Modelo, UsuarioID) VALUES
    ('Laptop',    'Dell Latitude 5420', 1),
    ('Impresora', 'HP LaserJet M404',   2);
GO

-- consultas de ejemplo
SELECT * FROM Usuarios;
SELECT * FROM Equipos;
