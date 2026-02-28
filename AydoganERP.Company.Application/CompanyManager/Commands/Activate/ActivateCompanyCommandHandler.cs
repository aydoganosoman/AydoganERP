using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.CompanyManager.Commands.Activate;

public record ActivateCompanyCommand(Guid Id) : IRequest;

public class ActivateCompanyCommandHandler : IRequestHandler<ActivateCompanyCommand>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;

    public ActivateCompanyCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(ActivateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _baseDbContext.Companies
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (company == null)
        {
            throw new NotFoundException(nameof(Company), request.Id);
        }

        company.Activate();

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);
    }
}
