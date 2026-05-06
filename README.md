# ManagerAssistant

ManagerAssistant is a layered ASP.NET Core Web API for football player-manager operations.
It provides authentication/authorization, player/team/manager management, offer workflows, contract generation, and external player import.

## Tech Stack

- .NET 8 (`net8.0`)
- ASP.NET Core Web API
- Entity Framework Core 8
- PostgreSQL (`Npgsql.EntityFrameworkCore.PostgreSQL`)
- JWT Bearer Authentication
- FluentValidation
- Swagger / OpenAPI

## Solution Architecture

The solution follows a layered architecture:

- `Domain`: Entities and DTOs
- `Application`: Service interfaces, business services, repository abstractions, validation contracts
- `Infrastructure`: EF Core DAL implementations, auth/token/hash services, DI wiring, import integration, migrations
- `WebAPI`: Controllers and HTTP host

Main solution file: `ManagerAssistant.sln`

## Core Features

- JWT-based login and role-based authorization
- User registration for `Player` and `Manager` types
- Player management (query, create, update, delete)
- Manager management
- Team and league query operations
- Offer lifecycle:
    - manager creates offer
    - player accepts/rejects offer
    - accepted offers can result in contract creation
- Contract management
- Import teams/players from external football API (`api.football-data.org`)

## Project Structure

- `Application/`
    - `IServices/`, `Services/`
    - `DataAccess/` abstraction interfaces
    - `Validators/FluentValidation/`
    - `Abstraction/ISecurityServices/`
- `Domain/`
    - `Entities/Concrete/`
    - `Entities/Auth/`
    - `Entities/DTOs/`
- `Infrastructure/`
    - `DataAccess/Concrete/EntityFramework/`
    - `Concretes/SecurityServices/`
    - `DataAccess/ImportDataViaApi/`
    - `DependencyResolvers/DependencyResolver/`
    - `Migrations/`
- `WebAPI/`
    - `Controllers/`
    - `Program.cs`
    - `appsettings.json`

## Prerequisites

- .NET SDK 8.x
- PostgreSQL 14+ (or compatible)
- A database created for the project (example: `ManagerDb`)

## Configuration

Update `WebAPI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=ManagerDb;Username=postgres;Password=<your-password>"
  },
  "TokenOptions": {
    "Audience": "http://localhost:5239/",
    "Issuer": "http://localhost:5239/",
    "AccessTokenExpiration": 100,
    "SecurityKey": "<at-least-64-characters-secret-key>"
  }
}
```

### Important Security Notes

- `PlayerImportHandler` currently contains a hardcoded external API key in source code; move this to configuration/secret storage.

## Run Locally

From repository root:

```bash
dotnet restore
dotnet build
dotnet run --project WebAPI
```


## Authentication and Roles

JWT Bearer authentication is enabled.

Authorization roles used by controllers:

- `Admin`
- `Manager`
- `Player`

Typical auth flow:

1. Register user via `POST /api/Auth/register`
2. Login via `POST /api/Auth/login`
3. Copy token from response
4. Use `Authorization: Bearer <token>` header for protected endpoints

## API Endpoints Overview

Base route prefix: `api/<Controller>`

### Auth

- `POST /api/Auth/login`
- `POST /api/Auth/register`

### User (Admin)

- `GET /api/User/GetAllUsers`
- `GET /api/User/UserById?userId=...`
- `GET /api/User/UserByEmail?email=...`
- `GET /api/User/UserClaims?userId=...`
- `PUT /api/User/UpdateUser`
- `DELETE /api/User/DeleteUser?userId=...`

### Player

- `GET /api/Player/GetAllPlayers` (Admin)
- `GET /api/Player/PlayerById?playerId=...` (Admin, Manager)
- `GET /api/Player/PlayerByPosition?position=...` (Admin, Manager)
- `GET /api/Player/PlayerByTeamId?teamId=...` (Admin, Manager)
- `POST /api/Player/CreatePlayer` (Admin)
- `PUT /api/Player/UpdatePlayer` (Admin, Player)
- `DELETE /api/Player/DeletePlayer?playerId=...` (Admin)

### Manager

- `GET /api/Manager/GetAllManagers` (Admin, Player)
- `GET /api/Manager/ManagerById?managerId=...` (Admin, Player)
- `GET /api/Manager/ManagerPlayersByManagerId?managerId=...` (Admin, Manager, Player)
- `POST /api/Manager/CreateManager` (Admin)
- `DELETE /api/Manager/DeleteManager?managerId=...` (Admin)

### Team

- `GET /api/Team/GetAllTeams`
- `GET /api/Team/TeamById?teamId=...`
- `GET /api/Team/TeamsByLeagueId?leagueId=...`

### Offer

- `GET /api/Offer/GetAllOffers` (Admin)
- `GET /api/Offer/OfferById?offerId=...` (Admin)
- `GET /api/Offer/OffersByPlayerId?playerId=...` (Admin, Player)
- `GET /api/Offer/OffersByManagerId?managerId=...` (Admin, Manager)
- `POST /api/Offer/CreateOffer` (Manager, Admin)
- `POST /api/Offer/{offerId}/accept` (Player, Admin)
- `POST /api/Offer/{offerId}/reject` (Player, Admin)
- `PUT /api/Offer/UpdateOffer` (Admin, Manager)
- `DELETE /api/Offer/{offerId}` (Admin, Manager)

### Contract

- `GET /api/Contract/GetAllContracts` (Admin)
- `GET /api/Contract/ContractsManagerId?managerId=...` (Admin, Manager)
- `GET /api/Contract/ContractsByPlayerId?playerId=...` (Admin, Player)
- `GET /api/Contract/ContractById?id=...` (Admin, Manager)
- `POST /api/Contract/CreateContract` (Admin, Manager)
- `PUT /api/Contract/UpdateContract` (Admin, Manager)
- `DELETE /api/Contract/DeleteContract?id=...` (Admin, Manager)

### Import

- `POST /api/PlayerImport/ImportPlayers` (Admin)

## Validation

FluentValidation is registered in `Program.cs` and validators are discovered from the Application assembly (e.g. user registration, player, manager, offer validators).

## Notes for Production Hardening

- Move all secrets to environment variables or secret manager
- Enforce HTTPS only
- Add structured logging and centralized error handling middleware
- Add rate limiting and request auditing
- Add integration tests for critical flows (auth, offer accept/reject, contract creation)


