using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Base.Domain.Events.UserModule.Shared;
using MediatR;

namespace AydoganERP.Customer.Application.Shared.CategoryManager.IntegrationHandlers.CategoryUpdated;

public class CategoryUpdatedEventHandler : INotificationHandler<DomainEventNotification<CategoryUpdatedEvent>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CategoryUpdatedEventHandler(ICategoryRepository categoryRepository,
         IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _categoryRepository = categoryRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(DomainEventNotification<CategoryUpdatedEvent> notification, CancellationToken cancellationToken)
    {
        Category category = await _categoryRepository
             .GetAsync(x => x.Id == notification.DomainEvent.CategoryId);

        if (category != null)
        {
            category.UpdateFromIntegratedHandler(notification.DomainEvent.CategoryName, notification.DomainEvent.CategoryColor);

            await _categoryRepository.UpdateAsync(category, cancellationToken);

            await _domainEventUnitOfWork.CommitAsync(cancellationToken);
        }
    }
}
