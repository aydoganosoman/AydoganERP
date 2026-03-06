using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.CompanyBankAccountManager.Commands.Delete;

public record DeleteCompanyBankAccountCommand(Guid Id) : IRequest;

public class DeleteCompanyBankAccountCommandHandler : IRequestHandler<DeleteCompanyBankAccountCommand>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;

    public DeleteCompanyBankAccountCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteCompanyBankAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CompanyBankAccounts
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(CompanyBankAccount), request.Id);

        _context.CompanyBankAccounts.Remove(entity);
        await _unitOfWork.CommitAsync(null, cancellationToken);
    }
}
