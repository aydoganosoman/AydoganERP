using StackExchange.Redis;

namespace AydoganERP.Base.Application.Interfaces;

public interface IRedisHelper
{
    T JsonGet<T>(RedisKey key, CommandFlags flags = CommandFlags.None);
    bool JsonSet(RedisKey key, object value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None);
    IDatabase Database();
    IConnectionMultiplexer Connection { get; }
}
