using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Base.Domain.Events.UserModule.Shared;
using MediatR;

namespace AydoganERP.Customer.Application.Shared.CategoryManager.IntegrationHandlers.CityCreated;

public class CityCreatedEventHandler : INotificationHandler<DomainEventNotification<CityCreatedEvent>>
{
    private readonly ICityRepositroy _cityRepositroy;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CityCreatedEventHandler(ICityRepositroy cityRepositroy,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _cityRepositroy = cityRepositroy;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(DomainEventNotification<CityCreatedEvent> notification, CancellationToken cancellationToken)
    {
        City city = await _cityRepositroy
             .GetAsync(x => x.Id == notification.DomainEvent.CityId);

        if (city == null)
        {
            city = City.CreateFromIntegratedHandler(notification.DomainEvent.CityId,
                notification.DomainEvent.CityName);

            await _cityRepositroy.AddAsync(city);

            await _domainEventUnitOfWork.CommitAsync(cancellationToken);
        }
    }
}
