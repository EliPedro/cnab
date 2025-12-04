# CNAB Razor Pages Application

A .NET 8 Razor Pages app that parses CNAB files, persists data with Entity Framework Core, and displays store transaction details and balances.

## Features

- Razor Pages UI:
 - `Index` page for overview.
 - `Stores/Details` shows store owner, transactions, and computed balance.
- CNAB domain:
 - `CnabRecord` with `SignedAmount` derived from CNAB type codes.
 - `CnabParser` to read and translate CNAB lines into domain records.
- Persistence:
 - EF Core with `CnabDbContext`, `Store`, `Transaction`.
- Tests:
 - Unit tests for parsing and balance calculations using EF Core InMemory provider.
- Docker:
 - Containerization via `Dockerfile`.

## Getting Started

### Prerequisites

- .NET SDK 8.0+
- Visual Studio 2022 (17.8+) with .NET workloads
- Optional: Docker Desktop

### Build and Run

- Using Visual Studio:
 - Open the solution, set `src/Web` as startup, press __Start Debugging__.

Navigate to `http://localhost:5000` (or the port shown in the console).


Navigate to `http://localhost:5000/swagger` or the port shown in the console) to see API Documentation.


### Run Tests


Key tests:
- `BalanceTests` validate store balances via EF Core InMemory.
- `CnabParserTests` validate CNAB parsing and signed amounts.

## Project Structure Vertical Slice Architecture

- `src/Domain`
 - `CnabRecord.cs`, `CnabParseTests.cs`

 - `Entities` with `ParseUploadedFile`, `Store`, and `TransactionStore`
- `src/Web`
 - Razor Pages: `Pages/Upload.cshtml`, 
 - `Program.cs` for app setup
 - `Dockerfile` for container builds
- `tests/Web`
 - `BalanceTests.cs`, `CnabParserTests.cs`

## Data and EF Core

- Tests use EF Core InMemory (`UseInMemoryDatabase(Guid.NewGuid().ToString())`) for fast, isolated runs.
- Production storage can be configured by updating `ApplicationDbContext` options in `Program.cs` (e.g., SQL Server or SQLite).

## Docker

Build and run:

From the solution root?

- Build images:
  - `docker compose build`
- Run containers:
  - `docker compose up`
- Stop containers:
  `docker compose down`

These rules are enforced by `ParseUploadedFile` and validated by tests.
