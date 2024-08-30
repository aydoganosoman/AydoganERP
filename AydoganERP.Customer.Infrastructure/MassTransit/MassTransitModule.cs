using AydoganERP.Base.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Customer.Infrastructure.MassTransit;

public static class MassTransitModule
{
    public static IServiceCollection LoadMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProducerService, ProducerService>();

        services.AddMassTransit(mt =>
        {
            mt.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMq"));
            });
        });

        return services;
    }

}
