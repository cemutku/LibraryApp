# LibraryApp

Sample API with ASP.NET Core (3.1) API

It contains basic async HTTP methods:

- GET
- POST
- PUT
- DELETE

## Development

Library API is using PostgreSQL. For local development environment run the docker command below:

`docker run --name local-postgres -e POSTGRES_PASSWORD=P@123 -p 5432:5432 -d postgres`

After docker run command check postgres instance on PowerShell with `docker ps` command.

When database instance ready, in LibraryApp.Data path `update-database` in package manager console or `dotnet ef database update` in CLI

After all these steps, the application is ready for use.

## Alternative development environment (docker-compose)

For development and dotnet watcher run the `docker-compose -f docker-compose.dev.yml up` in /LibraryApp directory, than hit the http://localhost:5000/index.html and check the API methods in swaggerui
