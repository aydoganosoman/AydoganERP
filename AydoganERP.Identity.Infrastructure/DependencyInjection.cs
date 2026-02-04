using AydoganERP.Identity.Infrastructure.Repositories;
using AydoganERP.Identity.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.LoadRepositories(configuration);
        
        services.LoadServices(configuration);

        return services;
    }

}
