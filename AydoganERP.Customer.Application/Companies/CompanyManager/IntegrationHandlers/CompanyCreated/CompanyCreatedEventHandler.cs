using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Domain.Events.UserModule.Company;
using AydoganERP.Customer.Application.Common.Repositories;
using AydoganERP.Customer.Domain.Entities;
using MediatR;

namespace AydoganERP.Customer.Application.Companies.CompanyManager.IntegrationHandlers.CompanyCreated;

public class CompanyCreatedEventHandler : INotificationHandler<DomainEventNotification<CompanyCreatedEvent>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CompanyCreatedEventHandler(ICompanyRepository companyRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _companyRepository = companyRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(DomainEventNotification<CompanyCreatedEvent> notification, CancellationToken cancellationToken)
    {
        Company company = await _companyRepository
             .GetAsync(x => x.Id == notification.DomainEvent.CompanyId);

        if (company == null)
        {
            company = new Company(notification.DomainEvent.CompanyId, notification.DomainEvent.CompanyName);

            await _companyRepository.AddAsync(new Company(notification.DomainEvent.CompanyId, notification.DomainEvent.CompanyName));

            await _domainEventUnitOfWork.CommitAsync(cancellationToken);
        }
    }
}
