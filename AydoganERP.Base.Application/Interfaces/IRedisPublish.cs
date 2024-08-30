namespace AydoganERP.Base.Application.Interfaces;

public interface IRedisPublish
{
    Task PublishAsync(string name, string data);
}
