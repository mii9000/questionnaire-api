## Note
- API Spec : http://localhost:56133/swagger/index.html
- Demo video in this directory
- Need to wait at least 60 seconds for SQL Server to complete intialization

## Debugging/Running
- Visual Studio
- Visual Studio Code (.NET Core Debugger)
- `dotnet run src/Questionnaire.API/Questionnaire.API.csproj`
- `docker-compose up --build` (**Recommended**)

## Development
- Mac OS X
- Visual Studio Code
- Visual Studio
- ASP.NET Core 3.1
- Azure Data Studio
- docker

## Tests
- `dotnet test ./tests/Questionnaire.Tests`
- Visual Studio Test Explorer
- Visual Studio Code Test Explorer


# Solution

#### Questionnaire.Data
This .NET Standard library encapsulates data layer and its logic so that if its required to move from one storage to a different kind of storage then very few changes would be required.
- Repository Pattern
- Async APIs
- Micro-ORM (Dapper)
- SQL Server

#### Questionnaire.Services
This .NET Standard library contains business logic to present data from repositories to controllers.
- DI
- Async APIs
- Unit Tested

#### Questionnaire.API
This is the ASP.NET Core Web API project that is serving REST endpoints and Swagger for documentation.
- REST
- DI
- API Versioning
- Swagger
- Exception Handling Middleware

#### Improvements for future
- API Validation e.g. FluentValidation
- Convention based DI Registration
- Integration Testing
- Authentication and Authorization
- Caching
