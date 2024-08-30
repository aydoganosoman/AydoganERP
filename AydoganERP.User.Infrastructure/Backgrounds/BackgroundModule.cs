using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.User.Infrastructure.Backgrounds;

public static class BackgroundModule
{
    public static IServiceCollection LoadBackground(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<OutboxBackgroundService>();

        return services;
    }
}
