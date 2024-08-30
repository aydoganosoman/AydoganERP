using AydoganERP.Base.Application.Models;
using RabbitMQ.Client;

namespace AydoganERP.User.Infrastructure.RabbitMq;

public abstract class BaseConnection
{
    public readonly IConnection _connection;
    public BaseConnection(RabbitMqOptions rabbitMq)
    {
        var factory = new ConnectionFactory() { HostName = rabbitMq.Host, UserName = rabbitMq.UserName, Password = rabbitMq.Password };
        _connection = factory.CreateConnection();
    }
}
