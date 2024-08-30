namespace AydoganERP.User.Application.Common.Interfaces;

public interface IRedisSubscribe
{
    Task SubscribeAccountRegisteredAsync();
}
