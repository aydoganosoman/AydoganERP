using AydoganERP.Inventory.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Inventory.Infrastructure.Repositories;

public static class RepositoryModule
{
    public static IServiceCollection LoadRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IInventoryRepository, InventoryRepository>();

        return services;
    }
}