using AydoganERP.Base.Application.Common.Services;
using AydoganERP.Identity.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Identity.Infrastructure.Services;

public static class ServiceModule
{
    public static IServiceCollection LoadServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}