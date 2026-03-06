using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.CompanyBankAccountManager.Commands.Update;

public record UpdateCompanyBankAccountCommand(
    Guid Id,
    string BankName,
    string Iban,
    int Currency,
    string? BranchName,
    string? AccountNo,
    string? AccountName,
    string? SwiftCode,
    bool IsActive) : IRequest<CompanyBankAccountDto>;

public class UpdateCompanyBankAccountCommandHandler : IRequestHandler<UpdateCompanyBankAccountCommand, CompanyBankAccountDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCompanyBankAccountCommandHandler(
        IBaseDbContext context,
        IDomainEventUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CompanyBankAccountDto> Handle(UpdateCompanyBankAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CompanyBankAccounts
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(CompanyBankAccount), request.Id);

        entity.Update(
            request.BankName,
            request.Iban,
            request.Currency,
            request.BranchName,
            request.AccountNo,
            request.AccountName,
            request.SwiftCode,
            request.IsActive);

        await _unitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<CompanyBankAccountDto>(entity);
    }
}
