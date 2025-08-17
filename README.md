# Hahn Software

## Project Overview

Hahn Software is a modular web application consisting of:

- **API**: ASP.NET Core Web API for backend logic and data access
- **UI**: React application (located in `src/HahnSoftware.UI`)
- **Database**: Microsoft SQL Server for persistent storage

## Getting Started with Docker Compose

To run the entire stack (API, UI, and SQL Server) using Docker Compose:

1. Make sure you have [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running.
2. Open a terminal in the root of this repository.
3. Run:

```powershell
docker-compose up --build
```

This will build and start the API, UI, and SQL Server containers.

### Stopping the Services

To stop all services, press `Ctrl+C` in the terminal, then run:

```powershell
docker-compose down
```

## Project Structure

- `src/HahnSoftware.API/` - ASP.NET Core Web API project
- `src/HahnSoftware.UI/` - Frontend application
- `src/HahnSoftware.Domain/` - Domain models and logic
- `src/HahnSoftware.Application/` - Application layer (CQRS, services, etc.)
- `src/HahnSoftware.Infrastructure/` - Infrastructure and persistence
- `tests/` - Unit test projects

## Database Connection

The API connects to the SQL Server container using the following connection string (see `docker-compose.yml`):

```
Server=db;Database=HahnSoftwareDb;User=sa;Password=HahnSoftware@123;
```

## Notes

- Ensure ports 7276 (API), 3000 (UI), and 1433 (SQL Server) are available on your machine.
