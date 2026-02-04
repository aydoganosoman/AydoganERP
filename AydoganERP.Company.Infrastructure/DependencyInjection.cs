using AydoganERP.Company.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Company.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCompanyInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.LoadRepositories(configuration);
        
        return services;
    }

}
