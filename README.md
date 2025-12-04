\# CNAB Razor Pages Application



A .NET 8 Razor Pages app that parses CNAB files, persists data with Entity Framework Core, and displays store transaction details and balances.



\## Features



\- Razor Pages UI:

&nbsp;- `Index` page for overview.

&nbsp;- `Stores/Details` shows store owner, transactions, and computed balance.

\- CNAB domain:

&nbsp;- `CnabRecord` with `SignedAmount` derived from CNAB type codes.

&nbsp;- `CnabParser` to read and translate CNAB lines into domain records.

\- Persistence:

&nbsp;- EF Core with `CnabDbContext`, `Store`, `Transaction`.

\- Tests:

&nbsp;- Unit tests for parsing and balance calculations using EF Core InMemory provider.

\- Docker:

&nbsp;- Containerization via `src/Web/Dockerfile`.



\## Getting Started



\### Prerequisites



\- .NET SDK 8.0+

\- Visual Studio 2022 (17.8+) with .NET workloads

\- Optional: Docker Desktop



\### Build and Run



\- Using Visual Studio:

&nbsp;- Open the solution, set `src/Web` as startup, press \_\_Start Debugging\_\_.

\- Using CLI:





Navigate to `http://localhost:5000` (or the port shown in the console).





Navigate to `http://localhost:5000/swagger` or the port shown in the console) to see API Documentation.





\### Run Tests





Key tests:

\- `BalanceTests` validate store balances via EF Core InMemory.

\- `CnabParserTests` validate CNAB parsing and signed amounts.



\## Project Structure Vertical Slice Architecture



\- `src/Domain`

&nbsp;- `CnabRecord.cs`, `CnabParseTests.cs`



&nbsp;- `Entities` with `ParseUploadedFile`, `Store`, and `TransactionStore`

\- `src/Web`

&nbsp;- Razor Pages: `Pages/Upload.cshtml`, 

&nbsp;- `Program.cs` for app setup

&nbsp;- `Dockerfile` for container builds

\- `tests/Web`

&nbsp;- `BalanceTests.cs`, `CnabParserTests.cs`



\## Data and EF Core



\- Tests use EF Core InMemory (`UseInMemoryDatabase(Guid.NewGuid().ToString())`) for fast, isolated runs.

\- Production storage can be configured by updating `ApplicationDbContext` options in `Program.cs` (e.g., SQL Server or SQLite).



\## Docker



Build and run:

\- Build images:

&nbsp;  - `docker compose buld`

\- Run containers:

&nbsp;- `docker compose up`

\- Stop containers:

`docker compose down`



These rules are enforced by `ParseUploadedFile` and validated by tests.



