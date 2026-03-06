using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;

namespace AydoganERP.Company.Application.CompanyBankAccountManager.Commands.Create;

public record CreateCompanyBankAccountCommand(
    Guid CompanyId,
    string BankName,
    string Iban,
    int Currency,
    string? BranchName,
    string? AccountNo,
    string? AccountName,
    string? SwiftCode) : IRequest<CompanyBankAccountDto>;

public class CreateCompanyBankAccountCommandHandler : IRequestHandler<CreateCompanyBankAccountCommand, CompanyBankAccountDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCompanyBankAccountCommandHandler(
        IBaseDbContext context,
        IDomainEventUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CompanyBankAccountDto> Handle(CreateCompanyBankAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = CompanyBankAccount.Create(
            Guid.NewGuid(),
            request.CompanyId,
            request.BankName,
            request.Iban,
            request.Currency,
            request.BranchName,
            request.AccountNo,
            request.AccountName,
            request.SwiftCode);

        _context.CompanyBankAccounts.Add(entity);
        await _unitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<CompanyBankAccountDto>(entity);
    }
}
