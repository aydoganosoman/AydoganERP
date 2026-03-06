using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.CompanyBankAccountManager.Queries.GetList;

public record GetCompanyBankAccountListQuery(Guid CompanyId) : IRequest<List<CompanyBankAccountDto>>;

public class GetCompanyBankAccountListQueryHandler : IRequestHandler<GetCompanyBankAccountListQuery, List<CompanyBankAccountDto>>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetCompanyBankAccountListQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CompanyBankAccountDto>> Handle(GetCompanyBankAccountListQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.CompanyBankAccounts
            .Where(x => x.CompanyId == request.CompanyId)
            .OrderBy(x => x.BankName)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CompanyBankAccountDto>>(items);
    }
}
