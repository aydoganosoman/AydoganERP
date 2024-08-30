using AydoganERP.Base.Application.Interfaces;
using StackExchange.Redis;

namespace AydoganERP.Customer.Infrastructure.Redis;

public class RedisPublish : IRedisPublish
{
    private readonly IRedisHelper _redisHelper;
    public RedisPublish(IRedisHelper redisHelper)
    {
        _redisHelper = redisHelper;
    }

    public async Task PublishAsync(string name, string data)
    {
        var @event = new[] { new NameValueEntry(name, data) };

        await _redisHelper.Database().StreamAddAsync(name, @event);
    }
}
