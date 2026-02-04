using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.IdentityModule.Rules;
using AydoganERP.Identity.Application.DomainServices;
using AydoganERP.Identity.Application.Extansions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserUniquenessChecker, UserUniquenessChecker>();
        services.AddScoped<IGeneratePasswordUtil, GeneratePasswordUtil>();
        
        return services;
    }
}
