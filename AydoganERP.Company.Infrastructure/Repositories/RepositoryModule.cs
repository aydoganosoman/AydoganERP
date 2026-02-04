using AydoganERP.Company.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Company.Infrastructure.Repositories;

public static class RepositoryModule
{
    public static IServiceCollection LoadRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }
}