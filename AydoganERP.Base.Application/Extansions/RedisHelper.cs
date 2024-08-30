using AydoganERP.Base.Application.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AydoganERP.Base.Application.Extansions;

public class RedisHelper : IRedisHelper
{
    private readonly ConfigurationOptions _configuration;
    private Lazy<IConnectionMultiplexer> _connection;

    public RedisHelper(string connectionString)
    {
        _configuration = new ConfigurationOptions()
        {
            //for the redis pool so you can extent later if needed
            AllowAdmin = false,
            //Password = "", //to the security for the production
            ClientName = "ERP Redis Client",
            ReconnectRetryPolicy = new LinearRetry(5000),
            AbortOnConnectFail = false,
        };
        _configuration.EndPoints.Add(connectionString);

        _connection = new Lazy<IConnectionMultiplexer>(() =>
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_configuration);
            //redis.ErrorMessage += _Connection_ErrorMessage;
            //redis.InternalError += _Connection_InternalError;
            //redis.ConnectionFailed += _Connection_ConnectionFailed;
            //redis.ConnectionRestored += _Connection_ConnectionRestored;
            return redis;
        });
    }

    public IConnectionMultiplexer Connection { get { return _connection.Value; } }

    public IDatabase Database() => Connection.GetDatabase();

    public T JsonGet<T>(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        RedisValue rv = Database().StringGet(key, flags);
        if (!rv.HasValue)
            return default;
        T rgv = JsonConvert.DeserializeObject<T>(rv);
        return rgv;
    }

    public bool JsonSet(RedisKey key, object value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        if (value == null) return false;
        return Database().StringSet(key, JsonConvert.SerializeObject(value), expiry, when, flags);
    }

    private void _Connection_ErrorMessage(object sender, RedisErrorEventArgs e)
    {
        throw new NotImplementedException();
    }
}
