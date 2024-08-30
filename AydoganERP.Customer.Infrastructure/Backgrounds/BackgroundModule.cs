using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Customer.Infrastructure.Backgrounds;

public static class BackgroundModule
{
    public static IServiceCollection LoadBackground(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<InboxBackgroundService>();
        services.AddHostedService<OutboxBackgroundService>();

        return services;
    }
}
