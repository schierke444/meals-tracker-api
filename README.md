# bookconnect api
Backend services for **`meal tracker`**

An App that can track ur meals on the daily basis

## Tech

Tech and Some architecture that has been used for this project
- [MassTransit](https://masstransit.io/) - For routing messages or events with abstraction that helps communication from other services.
- [RabbitMQ](https://www.rabbitmq.com/) - Message broker and for handling Events under `MassTransit`
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/)  - ORM used for this project.
- [Postgresql/Npgsql](https://www.npgsql.org/) - Postgresql for .Net and Database used for this project.
- [FluentValidation](https://fluentvalidation.net/) - A package that used for object validation.
- [Isopoh.Cryptography.Argon2](https://www.nuget.org/packages/Isopoh.Cryptography.Argon2) - For password hashing using Argon2.
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/8.0.0-preview.3.23177.8) - A external package or a middleware that receives a JWT Bearer Token.
- [System.IdentityModel.Tokens.Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt) - For Serializing and Validating the JWT Tokens.
- [Microsoft.AspNetCore.JsonPatch](https://www.nuget.org/packages/Microsoft.AspNetCore.JsonPatch/8.0.0-preview.3.23177.8) - .NET core support for Json Patch
- [Microsoft.AspNetCore.Mvc.NewtonsoftJson](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.NewtonsoftJson/8.0.0-preview.3.23177.8) - For input and output formatters using Json and Json patch.
- [Docker](https://www.docker.com/) - For App Containerization.

Soon to be Added
- [Automapper](https://automapper.org/) - For mapping objects
- [MedatR](https://www.nuget.org/packages/MediatR) - For Implementing Mediator Pattern and used for communicating internal Services (like an in-memory pub-sub pattern).

## Installation
TBA


## Initializing/Starting
TBA
