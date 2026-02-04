using AydoganERP.Base.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Base.Infrastructure.Persistence;

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

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddTransient<IBaseDbContext>(provider => provider.GetService<ApplicationDbContext>());
        
        return services;
    }
}
