using AydoganERP.Base.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Customer.Infrastructure.Services;

public static class ServiceModule
{
    public static IServiceCollection LoadServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IMD5Helper, MD5Helper>();

        return services;
    }
}
