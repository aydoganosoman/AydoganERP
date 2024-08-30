using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Domain;
using AydoganERP.Base.Domain.Events.UserModule.Company;
using AydoganERP.Customer.Application.Common.Interfaces;
using AydoganERP.Customer.Application.Outboxes_Inboxes.InboxManager.Commands.Create;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AydoganERP.Customer.Infrastructure.Redis;

public class RedisSubscribe : IRedisSubscribe
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRedisHelper _redisHelper;
    private readonly IMediator _mediator;
    public RedisSubscribe(IServiceProvider serviceProvider,
        IRedisHelper redisHelper)
    {
        _serviceProvider = serviceProvider;
        _redisHelper = redisHelper;

        _mediator = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
    }

    #region Company Created Subscribe
    public async Task SubscribeCompanyAsync()
    {
        ISubscriber sub = _redisHelper.Connection.GetSubscriber();

        await sub.SubscribeAsync("AydoganERP.Base.Domain.Events.UserModule.CompanyCreatedEvent", SubscribeCompanyCreated);
        await sub.SubscribeAsync("AydoganERP.Base.Domain.Events.UserModule.CompanyUpdatedEvent", SubscribeCompanyUpdated);

    }

    private void SubscribeCompanyCreated(RedisChannel channel, RedisValue value)
    {
        CompanyCreatedEvent company = JsonConvert.DeserializeObject<CompanyCreatedEvent>(value.ToString());

        _mediator.Send(new CreateCommand()
        {
            Event = company.GetType().AssemblyQualifiedName,
            Data = JsonConvert.SerializeObject(company, Formatting.Indented),
            SendType = (int)OutboxInboxSendTypeEnum.Redis,
        });
    }

    private void SubscribeCompanyUpdated(RedisChannel channel, RedisValue value)
    {
        CompanyUpdatedEvent company = JsonConvert.DeserializeObject<CompanyUpdatedEvent>(value.ToString());

        _mediator.Send(new CreateCommand()
        {
            Event = company.GetType().AssemblyQualifiedName,
            Data = JsonConvert.SerializeObject(company, Formatting.Indented),
            SendType = (int)OutboxInboxSendTypeEnum.Redis,
        });
    }

    #endregion

}
