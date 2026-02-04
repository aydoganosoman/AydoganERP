using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models;
using AydoganERP.Base.Domain.Modules.IdentityModule.Events;
using MediatR;

namespace AydoganERP.Company.Application.CompanyManager.IntegrationHandlers.UserCreated;

public class UserCreatedEventHandler : INotificationHandler<DomainEventNotification<UserCreatedEvent>>
{
    private readonly IBaseDbContext _baseDbContext;
    public UserCreatedEventHandler(IBaseDbContext baseDbContext)
    {
        _baseDbContext = baseDbContext;
    }
    
    public async Task Handle(DomainEventNotification<UserCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var newCompany = Base.Domain.Modules.CompanyModule.Entities.Company.Create(Guid.NewGuid(), notification.DomainEvent.User.Name);
        
        await _baseDbContext.Companies.AddAsync(newCompany);
        
        notification.DomainEvent.User.SetCompany(newCompany.Id);
    }
}