using AydoganERP.Identity.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Identity.Infrastructure.Repositories;

public static class RepositoryModule
{
    public static IServiceCollection LoadRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}