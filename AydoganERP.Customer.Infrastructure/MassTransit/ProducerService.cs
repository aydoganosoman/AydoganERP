using AydoganERP.Base.Application.Interfaces;
using MassTransit;

namespace AydoganERP.Customer.Infrastructure.MassTransit;

public class ProducerService : IProducerService
{
    private readonly IPublishEndpoint _publishEndpoint;
    public ProducerService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync(string name, string data)
    {
        await _publishEndpoint.Publish(data);
    }
}
