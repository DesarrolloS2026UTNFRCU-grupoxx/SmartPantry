# SmartPantry

Repositorio del Trabajo Práctico Integrador de Desarrollo de Software 2026.

## Integrantes
- Alvarez Nicolas - @nicolasutnalvarez
- Cheveste Ulises - @Sesuu2003
- Delfino Jeremias - @Jere-Delfino03
- Ocampo Emmanuel - @unentero

## Requisitos Previos

Para compilar y ejecutar la solución es necesario contar con el siguiente software instalado:

* **IDE / Entorno:** Visual Studio 2022 o 2026 con Desarrollo de ASP.NET y web / Visual Studio Code.
* **.NET SDK:** 10.0 o superior (compatible con C# 14).
* **Node.js:** v24.15.0 o superior.
* **Gestor de paquetes:** Yarn 1.22.x (`npm install -g yarn@1.22.22`).
* **Base de Datos:** SQL Server LocalDB (`MSSQLLocalDB`), SQL Server Developer o Express, y SQL Server Management Studio (SSMS).
* **Herramientas de ABP y Control de Versiones:** ABP Studio / ABP CLI (`dotnet tool install -g Volo.Abp.Studio.Cli`) y Git.

## Cómo ejecutar y Configuración local

### 1. Configuración de Base de Datos y Dependencias
1. Iniciar la instancia local de SQL Server LocalDB:
   sqllocaldb start MSSQLLocalDB

2. Configurar la cadena de conexión en `src/SmartPantry.DbMigrator/appsettings.json` y `src/SmartPantry.HttpApi.Host/appsettings.json`:
   "ConnectionStrings": { "Default": "Server=(localdb)\\MSSQLLocalDB;Database=SmartPantryDB;Trusted_Connection=True;TrustServerCertificate=True" }

   > **Nota sobre credenciales:** La cadena configurada utiliza autenticación integrada de Windows para desarrollo local. En caso de utilizar credenciales SQL remotas, no deben versionarse en `appsettings.json`, sino gestionarse mediante **User Secrets** (`dotnet user-secrets`) o la variable de entorno `ConnectionStrings__Default`.

3. Restaurar paquetes de .NET e instalar librerías ABP:
   dotnet restore ./SmartPantry.slnx
   abp install-libs

4. Ejecutar `SmartPantry.DbMigrator` como proyecto de inicio en Visual Studio (F5) para crear la base y aplicar migraciones iniciales.

### 2. Ejecución de Servicios y URLs
- **Backend (API):**
  - Inicio: Establecer `SmartPantry.HttpApi.Host` como proyecto de inicio en Visual Studio y presionar `Ctrl + F5`.
  - URL local: https://localhost:44316/swagger
  - Detención: Cerrar la ventana de consola o presionar `Shift + F5`.

- **Frontend (Angular):**
  - Inicio: Desde la carpeta `angular`, ejecutar `yarn install` (primera vez) y luego `yarn start`.
  - URL local: http://localhost:4200
  - Detención: Presionar `Ctrl + C` en la terminal.

### 3. Arquitectura
La interfaz Angular se ejecuta en el navegador y consume `HttpApi.Host` mediante HTTP; no accede a la base de datos directamente.

## Verificación

Comandos de build y test de .NET y Angular ejecutados correctamente por el grupo:

### Backend (.NET)
dotnet build ./SmartPantry.slnx --configuration Release
dotnet test ./SmartPantry.slnx --configuration Release

### Frontend (Angular)
- cd angular
- yarn build
- yarn test --watch=false
