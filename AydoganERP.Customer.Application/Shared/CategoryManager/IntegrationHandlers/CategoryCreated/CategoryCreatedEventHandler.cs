using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Base.Domain.Events.UserModule.Shared;
using MediatR;

namespace AydoganERP.Customer.Application.Shared.CategoryManager.IntegrationHandlers.CategoryCreated;

public class CategoryCreatedEventHandler : INotificationHandler<DomainEventNotification<CategoryCreatedEvent>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CategoryCreatedEventHandler(ICategoryRepository categoryRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _categoryRepository = categoryRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(DomainEventNotification<CategoryCreatedEvent> notification, CancellationToken cancellationToken)
    {
        Category category = await _categoryRepository
             .GetAsync(x => x.Id == notification.DomainEvent.CategoryId);

        if (category == null)
        {
            category = Category.CreateFromIntegratedHandler(notification.DomainEvent.CategoryId,
                notification.DomainEvent.CompanyId,
                notification.DomainEvent.CategoryName,
                notification.DomainEvent.CategoryColor);

            await _categoryRepository.AddAsync(category);

            await _domainEventUnitOfWork.CommitAsync(cancellationToken);
        }
    }
}
