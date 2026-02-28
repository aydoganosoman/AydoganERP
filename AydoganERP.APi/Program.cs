using AydoganERP.Api.Filters;
using AydoganERP.Api.Services;
using AydoganERP.APi.Services;
using AydoganERP.Base.Application;
using AydoganERP.Base.Application.Common.EMail;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Infrastructure;
using AydoganERP.Base.Infrastructure.Persistence;
using AydoganERP.Company.Application;
using AydoganERP.Company.Infrastructure;
using AydoganERP.Customer.Application;
using AydoganERP.Customer.Infrastructure;
using AydoganERP.Identity.Application;
using AydoganERP.Identity.Infrastructure;
using AydoganERP.Inventory.Application;
using AydoganERP.Inventory.Infrastructure;
using AydoganERP.Finance.Application;
using AydoganERP.Identity.Infrastructure.Models;
using AydoganERP.Identity.Infrastructure.Persistence;
using Carter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddBaseApplication(builder.Configuration);
builder.Services.AddBaseInfrastructure(builder.Configuration);

builder.Services.AddCompanyApplication(builder.Configuration);
builder.Services.AddCompanyInfrastructure(builder.Configuration);

builder.Services.AddCustomerApplication(builder.Configuration);
builder.Services.AddCustomerInfrastructure(builder.Configuration);

builder.Services.AddIdentityApplication(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);

builder.Services.AddInventoryApplication(builder.Configuration);
builder.Services.AddInventoryInfrastructure(builder.Configuration);

builder.Services.AddFinanceApplication(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();

builder.Services.AddHttpContextAccessor();

var settings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecurityKey)),
            ValidateLifetime = true //here we are saying that we don't care about the token's expiration date
        };
    });

builder.Services
    .AddAuthorization();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.CustomSchemaIds(type => type.FullName); 
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JSON Web Token based security",
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCarter();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.MapCarter();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var md5Helper = services.GetRequiredService<IMD5Helper>();
        var _generatePasswordUtil = services.GetRequiredService<IGeneratePasswordUtil>();

        logger.LogInformation($"Connection String: {services.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection").ToString()}");
        logger.LogInformation("Applying migrations...");

        context.Database.Migrate();

        #region Seed
        await ApplicationDbContextSeed.SeedDefaultValuesAsync(context);
        await ApplicationDbContextUserSeed.SeedDefaultValuesAsync(md5Helper, _generatePasswordUtil, context);
        #endregion
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

        throw;
    }
}

app.Run();
