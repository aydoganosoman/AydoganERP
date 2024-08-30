using AydoganERP.Gateway.Models;
using AydoganERP.Gateway.Services.JWT_Authorize;
using Ocelot.DependencyInjection;
using Ocelot.Errors.Middleware;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("ocelot.json");

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();

var settings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddJWTAuthorize(settings);

builder.Services.AddOcelot(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseOcelot().Wait();

app.UseExceptionHandlerMiddleware();
app.UseMiddleware<AydoganERP.Gateway.Middlewares.ResponseCheckMiddleware>();

app.MapControllers();

app.Run();
