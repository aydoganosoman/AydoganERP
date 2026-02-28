using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.CompanyManager.Commands.Suspend;

public record SuspendCompanyCommand(Guid Id) : IRequest;

public class SuspendCompanyCommandHandler : IRequestHandler<SuspendCompanyCommand>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;

    public SuspendCompanyCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(SuspendCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _baseDbContext.Companies
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (company == null)
        {
            throw new NotFoundException(nameof(Company), request.Id);
        }

        company.Suspend();

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);
    }
}
