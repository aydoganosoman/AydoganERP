using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain;
using AydoganERP.User.Application.Outboxes_Inboxes.OutboxManager.Commands.UpdateResult;
using AydoganERP.User.Application.Outboxes_Inboxes.OutboxManager.Queries.GetAllUnDone;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AydoganERP.User.Infrastructure.Backgrounds;

public class OutboxBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMediator _mediator;
    private readonly IRedisPublish _redisPublish;
    private readonly IProducerService _producerService;
    public OutboxBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        _mediator = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
        _redisPublish = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IRedisPublish>();
        _producerService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IProducerService>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await PublishOutstandingIntegrationEvents(stoppingToken);

            Thread.Sleep(10000);
        }
    }

    private async Task PublishOutstandingIntegrationEvents(CancellationToken stoppingToken)
    {
        GetListResponse<OutboxDto> undone = await _mediator.Send(new GetAllUnDoneQuery(), stoppingToken);

        foreach (OutboxDto outboxItem in undone.Items)
        {
            try
            {
                switch ((OutboxInboxSendTypeEnum)outboxItem.SendType)
                {
                    case OutboxInboxSendTypeEnum.CAP:
                        break;
                    case OutboxInboxSendTypeEnum.Masstransit:
                        {
                            await _producerService.PublishAsync(outboxItem.Event, outboxItem.Data);
                        }
                        break;
                    case OutboxInboxSendTypeEnum.Redis:
                        {
                            await _redisPublish.PublishAsync(outboxItem.Event, outboxItem.Data);
                        }
                        break;
                    default:
                        break;
                }

                await _mediator.Send(new UpdateResultCommand()
                {
                    Id = outboxItem.Id,
                    IsDone = true,
                    Result = ""
                }, stoppingToken);

            }
            catch (Exception ex)
            {
                await _mediator.Send(new UpdateResultCommand()
                {
                    Id = outboxItem.Id,
                    IsDone = false,
                    Result = ex.Message
                }, stoppingToken);
            }
        }
    }
}
