using AydoganERP.Base.Application.Interfaces;
using AydoganERP.User.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.User.Infrastructure.Services;

public static class ServiceModule
{
    public static IServiceCollection LoadServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IMD5Helper, MD5Helper>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
