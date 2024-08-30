using AydoganERP.User.Infrastructure.Backgrounds;
using AydoganERP.User.Infrastructure.Email;
using AydoganERP.User.Infrastructure.MassTransit;
using AydoganERP.User.Infrastructure.Persistence;
using AydoganERP.User.Infrastructure.Proccessing;
using AydoganERP.User.Infrastructure.Redis;
using AydoganERP.User.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.User.Infrastructure;

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

        services.LoadEmail(configuration);

        return services;
    }

}
