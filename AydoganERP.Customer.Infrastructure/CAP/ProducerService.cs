using AydoganERP.Base.Application.Interfaces;
using DotNetCore.CAP;

namespace AydoganERP.Customer.Infrastructure.CAP;

public class ProducerService : IProducerService
{
    private readonly ICapPublisher _capPublisher;
    //private readonly RabbitMqPublish _rabbitMqPublish;
    //public ProducerService(ICapPublisher capPublisher, RabbitMqPublish rabbitMqPublish)
    //{
    //    _capPublisher = capPublisher;
    //    _rabbitMqPublish = rabbitMqPublish;
    //}

    public ProducerService(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }

    public async Task PublishAsync(string name, string data)
    {
        await _capPublisher.PublishAsync(name, DateTime.Now.ToString());

        //_rabbitMqPublish.Publish(name, data);
    }
}
