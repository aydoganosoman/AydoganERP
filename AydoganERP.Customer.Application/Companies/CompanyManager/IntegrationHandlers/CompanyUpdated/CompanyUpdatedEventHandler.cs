using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Domain.Events.UserModule.Company;
using AydoganERP.Customer.Application.Common.Repositories;
using AydoganERP.Customer.Domain.Entities;
using MediatR;

namespace AydoganERP.Customer.Application.Companies.CompanyManager.IntegrationHandlers.CompanyUpdated;

public class CompanyUpdatedEventHandler : INotificationHandler<DomainEventNotification<CompanyUpdatedEvent>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CompanyUpdatedEventHandler(ICompanyRepository companyRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _companyRepository = companyRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(DomainEventNotification<CompanyUpdatedEvent> notification, CancellationToken cancellationToken)
    {
        Company company = await _companyRepository
             .GetAsync(x => x.Id == notification.DomainEvent.CompanyId);

        if (company != null)
        {
            company.Update(notification.DomainEvent.CompanyName);

            await _companyRepository.UpdateAsync(company, cancellationToken);

            await _domainEventUnitOfWork.CommitAsync(cancellationToken);
        }
    }
}
