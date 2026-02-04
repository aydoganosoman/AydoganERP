using AydoganERP.Base.Application.Common.EMail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Base.Infrastructure.Email;

public static class EmailModule
{
    public static IServiceCollection LoadEmail(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }
}