using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Application.Repositories.TownRepository;
using AydoganERP.Customer.Application.Common.Repositories;
using AydoganERP.Customer.Application.Repositories.InboxRepository;
using AydoganERP.Customer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Customer.Infrastructure.Persistence;

public static class PersistanceModule
{
    public static IServiceCollection LoadPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

            options.EnableSensitiveDataLogging();
        });

        services.AddScoped<ICityRepositroy, CityRepositroy>();
        services.AddScoped<ITownRepositroy, TownRepositroy>();
        services.AddScoped<IInboxRepository, InboxRepository>();
        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerSegmentationRepository, CustomerSegmentationRepository>();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}
