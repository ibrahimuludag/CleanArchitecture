# .Net Clean Architecture Solution
This solution is a basic, incomplete solution demonstrating the basics of Clean Architecture. I was mainly inspired by [Json Taylor](https://github.com/jasontaylordev/CleanArchitecture) and [Gill Cleeren](https://app.pluralsight.com/library/courses/asp-dot-net-core-6-clean-architecture/table-of-contents). To use this solution, clone this repository and start coding.

##### Table of Contents  
[Headers](#headers)  
[Emphasis](#emphasis)  
...snip...    
<a name="headers"/>
## Headers

# Clean Architecture
Clean architecture is a software design philosophy that separates the elements of a design into ring levels. An essential goal of clean architecture is to provide developers with a way to organize code so that it encapsulates the business logic but keeps it separate from the delivery mechanism. 

The main rule of clean architecture is that code dependencies can only move from the outer levels inward. Code on the inner layers can have no knowledge of functions on the outer layers. The variables, functions and classes (any entities) that exist in the outer layers can not be mentioned in the more inward levels. It is recommended that data formats also stay separate between levels.

Clean architecture was created by Robert C. Martin and promoted on his blog, Uncle Bob. Like other software design philosophies, clean architecture attempts to provide a cost-effective methodology that makes it easier to develop quality code that will perform better is easier to change and has fewer dependencies.
For more information, visit [Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html),

# Technologies
- [ASP.NET Core 7.0](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0)
  ASPNET Core is a framework to create web apps and services that are fast, secure, cross-platform, and cloud-based.	
- [Entity Framework Core 7](https://learn.microsoft.com/en-us/ef/core/)
Entity Framework (EF) Core is a lightweight, extensible, open source, and cross-platform version of the widespread Entity Framework data access technology.
- [MediatR](https://github.com/jbogard/MediatR)
In-process messaging with no dependencies.
Supports request/response, commands, queries, notifications and events, synchronous and async with intelligent dispatching via C# generic variance.
- [Mapster](https://github.com/MapsterMapper/Mapster)
Fast and lightweight mapper having code generation feature. 
- [FluentValidation](https://fluentvalidation.net/)
FluentValidation is a .NET library for building strongly-typed validation rules.
- [FluentResult](https://github.com/altmann/FluentResults)
FluentResults is a lightweight .NET library developed to solve a common problem. It returns an object indicating success or failure of an operation instead of throwing/using exceptions.
- [Serilog](https://serilog.net/)
Serilog is a diagnostic logging library for .NET applications. It is easy to set up, has a clean API, and runs on all recent .NET platforms. While it's useful even in the simplest applications, Serilog's support for structured logging shines when instrumenting complex, distributed, and asynchronous applications and systems.
- [NSwag](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-7.0&tabs=visual-studio)
 NSwag is a Swagger/OpenAPI 2.0 and 3.0 toolchain for .NET, .NET Core, Web API, ASP.NET Core, TypeScript (jQuery, AngularJS, Angular 2+, Aurelia, KnockoutJS and more) and other platforms, written in C#. The OpenAPI/Swagger specification uses JSON and JSON Schema to describe a RESTful web API. The NSwag project provides tools to generate OpenAPI specifications from existing ASP.NET Web API controllers and client code from these OpenAPI specifications.
- [xUnit](https://xunit.net/)
xUnit is a free, open source, community-focused unit testing tool for the .NET Framework.
- [AutoFixture](https://github.com/AutoFixture/AutoFixture)
AutoFixture makes it easier for developers to do Test-Driven Development by automating non-relevant Test Fixture Setup, allowing the Test Developer to focus on the essentials of each test case.
- [Shouldly](https://github.com/shouldly/shouldly)
Shouldly is an assertion framework which focuses on giving great error messages when the assertion fails while being simple and terse.

# Solution Structure
## Domain
The *Domain* Project contains Entities and Domain Events. 

### Entities
Every entity should inherit from **BaseEntity**. This way *Creation* and *Update* information can be tracked for an entity.

I have chosen the Guid type for the identifier field. Although this has some disadvantages over integers, it makes it easy for synchronization and able to generate identifiers anywhere.

### Domain Events
Every domain event should inherit from **BaseEvent**. I added general *EntityCreatedEvent*, *EntityDeletedEvent*, and *EntityUpdatedEvent*. These are not fired automatically. You need to fire at MediatR handlers.

Also, you can add your domain events under the *Events* folder,

### Value Objects
A value object is a small object that represents a simple entity whose equality is not based on identity.
Examples of value objects are objects representing an amount of money or a date range.

I used *record* for Value Objects. You can check [Weight](https://github.com/ibrahimuludag/CleanArchitecture/blob/main/src/CleanArchitecture.Domain/ValueObjects/Weight.cs) record on [Product](https://github.com/ibrahimuludag/CleanArchitecture/blob/main/src/CleanArchitecture.Domain/Entities/Product.cs) entity.

## Application
The *Application* project contains Contracts and MediatR handlers. Implementation of the contracts should be implemented on Infrastructure.

### Validation
[ValidationBehavior](https://github.com/ibrahimuludag/CleanArchitecture/blob/main/src/CleanArchitecture.Application/Common/Behaviours/ValidationBehaviour.cs) validates input commands and queries using MediatR pipeline and FluentValidation.

### Contracts
Contracts for authorization, email, file, and database (repository) exist under the *Contracts* folder. 

### Repository
I implemented the *Generic Repository* pattern for data access. Each entity should have a separate repository inherited from [IAsyncRepository](https://github.com/ibrahimuludag/CleanArchitecture/blob/main/src/CleanArchitecture.Application/Contracts/Persistence/IAsyncRepository.cs). 

### Exceptions
All custom exceptions should be in the folder and inherit from [CleanArchitectureApplicationException](https://github.com/ibrahimuludag/CleanArchitecture/blob/main/src/CleanArchitecture.Application/Exceptions/CleanArchitectureApplicationException.cs).
By default, I added the below exceptions.
 - BadRequestException
 - ForbiddenAccessException
 - NotFoundException
 - UnauthorizedException
 - ValidationException
 
### Fetaures
This folder contains MediatR implementations.

The structure of the solutiuon is as below
 - Entity1
	 - Use Case 1
		 - UserCase1Command/UserCase1Query
		 - UserCase1CommandResponse/UserCase1QueryResponse
		 - UserCase1CommandValidator/UserCase1QueryValidator
	 - Use Case 2
		 - UserCase2Command/UserCase2Query
		 - UserCase2CommandResponse/UserCase2QueryResponse
		 - UserCase2CommandValidator/UserCase2QueryValidator
 - Entity2
	 - Use Case 1
		 - UserCase1Command/UserCase1Query
		 - UserCase1CommandResponse/UserCase1QueryResponse
		 - UserCase1CommandValidator/UserCase1QueryValidator
	 - Use Case 2
		 - UserCase2Command/UserCase2Query
		 - UserCase2CommandResponse/UserCase2QueryResponse
		 - UserCase2CommandValidator/UserCase2QueryValidator

I did not create a subfolder for separating commands & queries. Adding a command/query suffix is enough.

UserCase1Command/UserCase1Query contains the command/query and handler. Every handler returns a FluentResult. 

I have chosen using *FluentResult* over *Exceptions* for the below reasons.
 - Exceptions are for exceptional cases
 - To have better control over the program flow
 - To deal with exceptions in a better way
 - Cleaner code
 
I used *Mapster* for mapping between entities and commands/queries. Mapster has a code generation feature. My initial idea was to use this feature. However, I had problems while implementing code generation. The code generation feature for .Net Core 7 is not ready yet.

Additionally, you can add any domain event here.

### Models
This folder contains common models like configuration settings, paginated list, and email. You can add any common models here.

### Validation
This folder contains custom fluent errors.

## Infrastructure
This project/layer contains data access, caching, authorization, and external services like email.
For data access, I created a separate project, *Persistence*. Data access deserves a different project for easy maintenance.

### Authorization & Authentication
Authorization is a separate domain. And there are many ways of implementing it. For this reason, I did not implement authentication & authorization. Instead, I added a fake [authentication handler](https://github.com/ibrahimuludag/CleanArchitecture/blob/main/src/CleanArchitecture.Infrastructure/Authorization/FakeAuthHandler.cs).

### ApiConventions
I added ApiConventions to reuse ProducesResponseType for OpenApi. 

### ExceptionHandling
ExceptionHandlingMiddleware catches unhandled exceptions and returns [standard](https://www.rfc-editor.org/rfc/rfc7231) response. 

## Persistence

This project handles data access. It uses Entity Framework Core 7. 

### Configurations
I used FluentApi to configure domain entities. Each entity should have a separate configuration class.

### Seed Data
I implemented custom seed logic. Required data depend on the running environment. For example, you may need sample data on the dev or test environment. But, some data like lookup is required on every environment.

To seed a table, you must create a class inheriting the *ICustomSeeder* interface. 

If it is required only for development or testing purposes, set IsDevelopmentData to true.

    IsDevelopmentData = true;

Else, if it is required on all environments, set it to false.

    IsDevelopmentData = false;

You can also specify in which order the seeding is performed.

     Order = 1

### Repositories

Repository implementations are in this folder. Every repository should inherit from *BaseRepository*

### Migrations

To add a migration, go to *Persistence* folder and run the below command.

    dotnet ef migrations add "SampleMigration"

## Api

This is the Api endpoint project. Gets commands/queries from client, sends to MediatR and returns the response via *Controllers*.

### Integration Tests
By the help of WebApplicationFactory, in-memory testing is performed. Endpoints are called, and responses are asserted.

Integration test uses "It" environment and [appsettings.It.json](https://github.com/ibrahimuludag/CleanArchitecture/blob/main/src/CleanArchitecture.Api/appsettings.It.json) configuration. By the below configuration, Entity Framework in-memory database is used.

    "UseInMemoryDatabase": true


### Unit Tests
AutoFixture, Moq, and Shouldly libraries are used.

# Support

If you are having problems, please let me know by  [raising a new issue](https://github.com/ibrahimuludag/CleanArchitecture/issues/new)

# Licence
This project is licensed with the  [MIT license](https://github.com/ibrahimuludag/CleanArchitecture/blob/main/LICENSE)


# References & Glossary
- [Clean Architecture](https://www.techtarget.com/whatis/definition/clean-architecture)
- [Value Objects](https://en.wikipedia.org/wiki/Value_object)
- [Domain Events](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation)

# Todo
- Docker Integration
- Add UI projects