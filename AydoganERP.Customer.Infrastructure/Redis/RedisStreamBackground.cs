using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Domain;
using AydoganERP.Base.Domain.Events.UserModule.Company;
using AydoganERP.Base.Domain.Events.UserModule.Shared;
using AydoganERP.Base.Domain.ValueObjects;
using AydoganERP.Customer.Application.Outboxes_Inboxes.InboxManager.Commands.Create;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace AydoganERP.Customer.Infrastructure.Redis;
public class RedisStreamBackground : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRedisHelper _redisHelper;
    private readonly IMediator _mediator;
    private readonly IInboxRepository _inboxRepository;
    public RedisStreamBackground(IServiceProvider serviceProvider,
        IRedisHelper redisHelper)
    {
        _serviceProvider = serviceProvider;
        _redisHelper = redisHelper;

        _mediator = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
        _inboxRepository = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IInboxRepository>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CreatedStream(typeof(CategoryCreatedEvent).AssemblyQualifiedName);
            await CreatedStream(typeof(CategoryUpdatedEvent).AssemblyQualifiedName);

            await CreatedStream(typeof(CityCreatedEvent).AssemblyQualifiedName);

            await CreatedStream(typeof(TownCreatedEvent).AssemblyQualifiedName);

            await CreatedStream(typeof(CompanyCreatedEvent).AssemblyQualifiedName);
            await CreatedStream(typeof(CompanyUpdatedEvent).AssemblyQualifiedName);

            Thread.Sleep(10000);
        }
    }

    private async Task CreatedStream(string key)
    {
        var lastInbox = await _inboxRepository.GetAsync(predicate: x => x.Event == key, orderBy: (x) => x.OrderByDescending(o => o.EntryId.Timestamp));

        var lastId = "-";

        if (lastInbox != null)
            lastId = lastInbox.EntryId.ToString();

        var result = await _redisHelper
            .Database()
            .StreamRangeAsync(key, lastId, "+");

        if (!result.Any() || lastId == result.Last().Id) return;

        lastId = result.Last().Id;

        foreach (var entry in result)
            foreach (var field in entry.Values)
            {
                var type = Type.GetType(field.Name!);

                var body = JsonConvert.DeserializeObject(field.Value!, type!)!;

                await _mediator.Send(new CreateCommand()
                {
                    Event = key,
                    Data = JsonConvert.SerializeObject(body),
                    SendType = (int)OutboxInboxSendTypeEnum.Redis,
                    EntryId = new RedisEntryId(lastId)
                });
            }
    }
}
