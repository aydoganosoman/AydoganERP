using AydoganERP.Finance.Application.EInvoice;
using AydoganERP.Finance.Infrastructure.EInvoice;
using AydoganERP.Finance.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Finance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddFinanceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEInvoiceService, EInvoiceService>();
        
        // Background Services
        services.AddHostedService<EInvoiceStatusCheckerService>();
        
        return services;
    }
}
