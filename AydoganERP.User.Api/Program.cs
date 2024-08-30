using AydoganERP.Base.Application.EMail;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.User.Api.Services;
using AydoganERP.User.Api.Services.JWT_Authorize;
using AydoganERP.User.Application;
using AydoganERP.User.Infrastructure;
using AydoganERP.User.Infrastructure.Models;
using AydoganERP.User.Infrastructure.Persistence;
using FastEndpoints;
using FastEndpoints.Security;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

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

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ICurrentRequestService, CurrentRequestService>();

builder.Services.AddHttpContextAccessor();

var settings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services
    .AddJWTAuthorize(settings);

builder.Services
   .AddAuthenticationJwtBearer(s => s.SigningKey = settings.SecurityKey);

builder.Services
    .AddAuthorization();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

builder.Services
   .AddFastEndpoints()
   .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors();

app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthentication()
   .UseAuthorization()
   .UseFastEndpoints((c) =>
   {
       c.Endpoints.RoutePrefix = "api";
       c.Serializer.Options.ReferenceHandler = ReferenceHandler.IgnoreCycles;   
   });

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
