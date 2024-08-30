using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Application.Repositories.TownRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Base.Domain.Events.UserModule.Shared;
using MediatR;

namespace AydoganERP.Customer.Application.Shared.CategoryManager.IntegrationHandlers.TownCreated;

public class TownCreatedEventHandler : INotificationHandler<DomainEventNotification<TownCreatedEvent>>
{
    private readonly ITownRepositroy _townRepositroy;
    private readonly ICityRepositroy _cityRepositroy;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public TownCreatedEventHandler(ITownRepositroy townRepositroy,
        ICityRepositroy cityRepositroy,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _townRepositroy = townRepositroy;
        _cityRepositroy = cityRepositroy;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(DomainEventNotification<TownCreatedEvent> notification, CancellationToken cancellationToken)
    {
        Town town = await _townRepositroy
             .GetAsync(x => x.Id == notification.DomainEvent.TownId);

        if (town == null)
        {
            var _city = await _cityRepositroy.GetAsync(x => x.Id == notification.DomainEvent.CityId);

            town = Town.Create(notification.DomainEvent.TownId,
                _city,
                notification.DomainEvent.TownName);

            await _townRepositroy.AddAsync(town);

            await _domainEventUnitOfWork.CommitAsync(cancellationToken);
        }
    }
}
