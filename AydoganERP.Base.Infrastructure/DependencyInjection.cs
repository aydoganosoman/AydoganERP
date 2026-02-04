using AydoganERP.Base.Infrastructure.Email;
using AydoganERP.Base.Infrastructure.Persistence;
using AydoganERP.Base.Infrastructure.Proccessing;
using AydoganERP.Base.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Base.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.LoadPersistance(configuration);
        
        services.LoadProccessing(configuration);
        
        services.LoadServices(configuration);

        services.LoadEmail(configuration);

        return services;
    }

}
