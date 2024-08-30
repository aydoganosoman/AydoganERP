using AydoganERP.Base.Application.Extansions;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Customer.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Customer.Infrastructure.Redis;

public static class RedisModule
{
    public static IServiceCollection LoadRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IRedisPublish, RedisPublish>();
        //services.AddTransient<IRedisSubscribe, RedisSubscribe>();

        services.AddHostedService<RedisStreamBackground>();

        services.AddSingleton<IRedisHelper>(new RedisHelper(configuration.GetConnectionString("Redis")));

        return services;
    }

}
