using AydoganERP.Customer.Infrastructure.Backgrounds;
using AydoganERP.Customer.Infrastructure.CAP;
using AydoganERP.Customer.Infrastructure.MassTransit;
using AydoganERP.Customer.Infrastructure.Persistence;
using AydoganERP.Customer.Infrastructure.Proccessing;
using AydoganERP.Customer.Infrastructure.RabbitMq;
using AydoganERP.Customer.Infrastructure.Redis;
using AydoganERP.Customer.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Customer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.LoadMassTransit(configuration);

        //services.LoadCAP(configuration);

        //services.LoadRabbitMq(configuration);

        services.LoadBackground(configuration);

        services.LoadRedis(configuration);

        services.LoadPersistance(configuration);

        services.LoadProccessing(configuration);

        services.LoadServices(configuration);

        return services;
    }

}
