# AGENTS.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

AydoganERP is a modular ERP system built with .NET 8 following Clean Architecture, Domain-Driven Design (DDD), and CQRS patterns. The system uses PostgreSQL as its database and is organized into a Base layer with multiple business modules.

## Architecture

### Layered Structure

The codebase follows a modular monolith architecture with three layers per module:

1. **Domain Layer** (`*.Domain`) - Pure business logic, entities, value objects, domain events, and business rules
2. **Application Layer** (`*.Application`) - Use cases, commands/queries (CQRS), interfaces, DTOs, and validation
3. **Infrastructure Layer** (`*.Infrastructure`) - Data access, external services, and infrastructure concerns

### Base Layer

The Base layer provides shared infrastructure for all modules:

- `AydoganERP.Base.Domain`: Core domain primitives (`Entity`, `ValueObject`, `AuditableEntity`, `IDomainEvent`, `IBusinessRule`)
- `AydoganERP.Base.Application`: Shared application concerns (MediatR behaviors, interfaces, repositories, validation)
- `AydoganERP.Base.Infrastructure`: Shared infrastructure (`ApplicationDbContext`, `DomainEventUnitOfWork`, email services)

All entities in `AydoganERP.Base.Domain/Modules/` are shared across the system and stored in a single database context.

### Business Modules

Each module follows the same three-layer structure:

- **Identity Module**: User authentication, authorization, and API key management
- **Company Module**: Company entity management
- **Customer Module**: Party (customer/supplier) and ledger entry management with value objects (Address, ContactInfo, Money, TaxInfo)
- **Inventory Module**: Product and stock movement tracking
- **Finance Module**: (Placeholder for future implementation)

### Key Patterns

**Domain Events**: Entities inherit from `Entity` base class and can publish domain events via `PublishEvent()`. Events are queued in `DomainEvents` collection and dispatched by `DomainEventUnitOfWork` before database commit.

**Business Rules**: Domain logic is enforced via `IBusinessRule` interface. Use `CheckRule()` method in entities to validate rules (throws `BusinessRuleValidationException` if broken).

**CQRS with MediatR**: Commands and queries are organized under `*/Application/*/Commands` and `*/Application/*/Queries` directories. Each has a handler implementing `IRequestHandler<TRequest, TResponse>`.

**MediatR Pipeline Behaviors** (applied in order):
- `UnhandledExceptionBehaviour`: Global exception handling
- `AuthorizationBehaviour`: Command authorization via `[Authorize]` attribute
- `ValidationBehaviour`: FluentValidation integration
- `PerformanceBehaviour`: Performance monitoring
- `LoggingBehaviour`: Request logging

**Repository Pattern**: `BaseRepository<TEntity>` provides generic data access. Module-specific repositories extend this for custom queries.

**Audit Logging**: `ApplicationDbContext.SaveChangesAsync()` automatically tracks Insert/Update/Delete operations and stores them in `AuditLogs` table.

**Current User Context**: `ICurrentUserService` provides current user's email for audit trails. Set by `AuditableEntity` properties (`CreatedBy`, `LastModifiedBy`).

## Development Commands

### Building and Running

```bash
# Restore dependencies
dotnet restore

# Build the entire solution
dotnet build

# Build in Release mode
dotnet build -c Release

# Run the API project
dotnet run --project AydoganERP.APi/AydoganERP.APi.csproj

# Watch mode (auto-rebuild on changes)
dotnet watch --project AydoganERP.APi/AydoganERP.APi.csproj
```

The API runs on:
- HTTP: http://localhost:5073
- HTTPS: https://localhost:7103
- Swagger UI: Available at `/swagger` when running in Development mode

### Database Migrations

Entity Framework Core migrations are managed from the Base.Infrastructure project:

```bash
# Add a new migration
dotnet ef migrations add <MigrationName> \
  --startup-project AydoganERP.APi/AydoganERP.APi.csproj \
  --project AydoganERP.Base.Infrastructure/AydoganERP.Base.Infrastructure.csproj \
  --context ApplicationDbContext \
  --output-dir Persistence/Migrations

# Update database
dotnet ef database update \
  --startup-project AydoganERP.APi/AydoganERP.APi.csproj \
  --project AydoganERP.Base.Infrastructure/AydoganERP.Base.Infrastructure.csproj \
  --context ApplicationDbContext

# Remove last migration
dotnet ef migrations remove \
  --startup-project AydoganERP.APi/AydoganERP.APi.csproj \
  --project AydoganERP.Base.Infrastructure/AydoganERP.Base.Infrastructure.csproj \
  --context ApplicationDbContext

# Generate SQL script from migrations
dotnet ef migrations script \
  --startup-project AydoganERP.APi/AydoganERP.APi.csproj \
  --project AydoganERP.Base.Infrastructure/AydoganERP.Base.Infrastructure.csproj \
  --context ApplicationDbContext
```

### Code Formatting

The project uses CSharpier for code formatting with configuration in `.csharpierrc`:

```bash
# Format all files (requires CSharpier to be installed globally)
dotnet csharpier .

# Check formatting without making changes
dotnet csharpier --check .
```

**CSharpier Settings**: 130 character line width, 4-space indentation (spaces not tabs)

### Cleaning Build Artifacts

```bash
# Clean build outputs
dotnet clean

# Clean and rebuild
dotnet clean && dotnet build
```

## Module Development Guidelines

### Adding a New Module

When adding a new business module (e.g., Sales, Purchasing):

1. **Domain Layer**: Create entities in `AydoganERP.Base.Domain/Modules/<ModuleName>Module/Entities/` since all entities share the same DbContext
2. **Application Layer**: Create `AydoganERP.<ModuleName>.Application` project with:
   - Commands and Queries under `<EntityName>Manager/`
   - DTOs in `Models/`
   - AutoMapper profile in `Mappings/MappingProfile.cs`
   - Repository interfaces in `Repositories/`
   - Domain services in `DomainServices/`
3. **Infrastructure Layer**: Create `AydoganERP.<ModuleName>.Infrastructure` project with:
   - Repository implementations
   - EF Core configurations in `Persistence/Configurations/`
   - `DependencyInjection.cs` for service registration
4. **API Integration**: 
   - Add project references to `AydoganERP.APi.csproj`
   - Register module services via DI in `Program.cs`
   - Create Carter endpoints in `AydoganERP.APi/Modules/<ModuleName>/`

### Adding a New Entity

1. Define entity class in `AydoganERP.Base.Domain/Modules/<Module>/Entities/`
2. Inherit from `Entity` (or `AuditableEntity` directly if not using domain events)
3. Add private constructor and static factory method(s) for entity creation
4. Implement business logic as methods, use `CheckRule()` for validation
5. Publish domain events via `PublishEvent()` when appropriate
6. Add `DbSet<TEntity>` property to `IBaseDbContext` interface
7. Add corresponding property to `ApplicationDbContext`
8. Create EF Core configuration in `AydoganERP.Base.Infrastructure/Persistence/Configurations/`
9. Create and apply migration

### Adding a New Command/Query

1. Define command/query record in `<Module>.Application/<EntityName>Manager/Commands|Queries/<ActionName>/`
2. Implement handler in same directory: `<ActionName>CommandHandler.cs` or `<ActionName>QueryHandler.cs`
3. Add FluentValidation validator if needed: `<ActionName>CommandValidator.cs`
4. Handler receives dependencies via constructor (e.g., `IBaseDbContext`, `IDomainEventUnitOfWork`, `IMapper`)
5. For commands that modify data, use `await _domainEventUnitOfWork.CommitAsync()` to save changes and dispatch events
6. For queries, use repository or DbContext directly with `.AsNoTracking()` for read-only operations

### Domain Events

Domain events enable loose coupling between modules:

1. Create event class implementing `IDomainEvent` in `<Module>/Events/` directory
2. Publish event in entity method: `this.PublishEvent(new EntityCreatedEvent(this));`
3. Create event handler implementing `INotificationHandler<TEvent>` in Application layer
4. Events are automatically dispatched before database commit via `DomainEventUnitOfWork`

## Code Style

Follow the `.editorconfig` conventions:
- File-scoped namespaces
- 4-space indentation
- No `this.` qualification
- Expression-bodied members for properties and accessors
- Private fields without underscore prefix (properties use PascalCase)
- Pattern matching preferred over `as` with null checks

## Database Configuration

The `ApplicationDbContext` requires connection string configuration. Check `appsettings.json` and `appsettings.Development.json` for PostgreSQL connection settings.

Entity configurations should be placed in `AydoganERP.Base.Infrastructure/Persistence/Configurations/` and will be auto-discovered via `builder.ApplyConfigurationsFromAssembly()`.

## Important Notes

- All shared entities live in `AydoganERP.Base.Domain/Modules/` - do NOT create separate domain entities in individual module Domain projects
- The `ApplicationDbContext` is the single source of truth for all entities
- Always use `IDomainEventUnitOfWork.CommitAsync()` instead of direct `SaveChangesAsync()` when domain events are involved
- Repository methods create new DbContext instances per operation (using pattern)
- FluentValidation validators are automatically discovered and executed via `ValidationBehaviour`
- Use `Carter` library for minimal API endpoint definitions
- Password hashing uses MD5 with salt pattern: `<<password>>`
