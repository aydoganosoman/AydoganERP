using AydoganERP.Base.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.User.Infrastructure.Proccessing;

public static class ProccessingModule
{
    public static IServiceCollection LoadProccessing(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDomainEventUnitOfWork, DomainEventUnitOfWork>();

        return services;
    }

}
