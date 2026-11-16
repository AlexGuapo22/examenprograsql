# Examen - Sistema de Reparaciones (CRUD Usuarios y Equipos)

Estudiante: (Alexander Solis Rojas)

Aplicacion web en 3 capas (ASP.NET Core Razor Pages, .NET 10) con SQL Server 2022 en Docker.

## Estructura
- `BD/bd.sql` – creacion de la base `ReparacionesUH` y todas las tablas del diagrama.
- `CapaDatos/` – `Cls_Usuarios`, `Cls_Equipos` (entidades).
- `CapaLogica/` – `DBconn` (conexion), `Usuarios_CL`, `Equipos_CL` (agregar, actualizar, eliminar, listar).
- `Presentacion/` – `CatalogoUsuarios` y `CatalogoEquipos` (tabla + formulario + botones).

## Como correrlo (Mac / Windows / Linux)
1. Levantar SQL Server en Docker:
   ```
   docker run -d --name sqlserver -e ACCEPT_EULA=Y -e "MSSQL_SA_PASSWORD=Examen2026!" -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest
   ```
2. Crear la base de datos:
   ```
   docker cp BD/bd.sql sqlserver:/tmp/bd.sql
   docker exec sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "Examen2026!" -C -i /tmp/bd.sql
   ```
3. Ejecutar la app: `dotnet run --urls http://localhost:5080` y abrir http://localhost:5080
