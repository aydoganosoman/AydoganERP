using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Customer.Application.Outboxes_Inboxes.InboxManager.Commands.UpdateResult;
using AydoganERP.Customer.Application.Outboxes_Inboxes.InboxManager.Queries.GetAllUnDone;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace AydoganERP.Customer.Infrastructure.Backgrounds;

public class InboxBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDomainEventService _domainEventService;
    private readonly IMediator _mediator;
    public InboxBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        _domainEventService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IDomainEventService>();
        _mediator = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await SubscribeOutstandingIntegrationEvents(stoppingToken);

            Thread.Sleep(10000);
        }
    }

    private async Task SubscribeOutstandingIntegrationEvents(CancellationToken stoppingToken)
    {
        GetListResponse<InboxDto> undone = await _mediator.Send(new GetAllUnDoneQuery(), stoppingToken);

        foreach (InboxDto inboxItem in undone.Items)
        {
            try
            {
                var type = Type.GetType(inboxItem.Event!);

                var obj = JsonConvert.DeserializeObject(inboxItem.Data, type);

                await _domainEventService.Publish((IDomainEvent)obj);

                await _mediator.Send(new UpdateResultCommand()
                {
                    Id = inboxItem.Id,
                    IsDone = true,
                    Result = ""
                }, stoppingToken);

            }
            catch (Exception ex)
            {
                await _mediator.Send(new UpdateResultCommand()
                {
                    Id = inboxItem.Id,
                    IsDone = false,
                    Result = ex.Message
                }, stoppingToken);
            }
        }
    }

}
