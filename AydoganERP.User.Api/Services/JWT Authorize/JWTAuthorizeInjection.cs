using AydoganERP.User.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AydoganERP.User.Api.Services.JWT_Authorize;

public static class JWTAuthorizeInjection
{
    public static IServiceCollection AddJWTAuthorize(this IServiceCollection services, JwtSettings settings)
    {
        #region JWT Configure
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        #endregion

        return services;
    }

}
