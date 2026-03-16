using AydoganERP.Base.Infrastructure.EInvoice;
using AydoganERP.Base.Infrastructure.Email;
using AydoganERP.Base.Infrastructure.Persistence;
using AydoganERP.Base.Infrastructure.Proccessing;
using AydoganERP.Base.Infrastructure.Services;
using AydoganERP.EInvoice.Abstractions.Services;
using AydoganERP.Finance.Application.EInvoice;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Base.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.LoadPersistance(configuration);
        
        services.LoadProccessing(configuration);
        
        services.LoadServices(configuration);

        services.LoadEmail(configuration);

        services.LoadEInvoice(configuration);

        return services;
    }

    private static IServiceCollection LoadEInvoice(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IntegratorFactory>();
        services.AddScoped<IEInvoiceService, EInvoiceService>();
        return services;
    }
}
