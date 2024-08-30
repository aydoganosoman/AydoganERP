using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.User.Infrastructure.RabbitMq;

public static class RabbitMqModule
{
    public static IServiceCollection LoadRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<RabbitMqPublish>();

        return services;
    }
}
