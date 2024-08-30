using AydoganERP.Base.Application.EMail;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Customer.Api.Filters;
using AydoganERP.Customer.Api.GraphQL.Mutations;
using AydoganERP.Customer.Api.GraphQL.Queries;
using AydoganERP.Customer.Api.Services;
using AydoganERP.Customer.Api.Services.JWT_Authorize;
using AydoganERP.Customer.Application;
using AydoganERP.Customer.Application.Common.Interfaces;
using AydoganERP.Customer.Infrastructure;
using AydoganERP.Customer.Infrastructure.Models;
using AydoganERP.Customer.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ICurrentRequestService, CurrentRequestService>();


builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddFiltering()
    .AddQueryType<CustomerQuery>()
    .AddMutationType<CustomerMutation>();

builder.Services.AddHttpContextAccessor();

var settings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddJWTAuthorize(settings);

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

builder.Services.AddControllersWithViews(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>())
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGraphQL();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Redis Subscribe
//await app.Services.GetRequiredService<IRedisSubscribe>().SubscribeCompanyAsync();
#endregion

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();

        await ApplicationDbContextSeed.SeedDefaultValuesAsync(services.GetRequiredService<IDomainEventUnitOfWork>(), context);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

        throw;
    }
}

app.Run();
