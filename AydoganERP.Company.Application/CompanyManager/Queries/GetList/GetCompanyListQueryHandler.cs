using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.CompanyManager.Queries.GetList;

public record GetCompanyListQuery() : IRequest<List<CompanyDto>>;

public class GetCompanyListQueryHandler : IRequestHandler<GetCompanyListQuery, List<CompanyDto>>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetCompanyListQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<List<CompanyDto>> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
    {
        var companies = await _baseDbContext.Companies
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        return _mapper.Map<List<CompanyDto>>(companies);
    }
}
