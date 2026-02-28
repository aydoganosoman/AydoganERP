using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.CompanyManager.Queries.GetPaged;

public record GetPagedQuery(int CurrentPage, int PageSize) : IRequest<PaginatedList<CompanyDto>>;

public class GetPagedQueryHandler : IRequestHandler<GetPagedQuery, PaginatedList<CompanyDto>>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetPagedQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CompanyDto>> Handle(GetPagedQuery request, CancellationToken cancellationToken)
    {
        var query = _baseDbContext
            .Companies
            .AsNoTracking()
            .AsQueryable();
        
        var queryList = await query
            .ToPagedListAsync(request.CurrentPage, request.PageSize, null, null);

        var mapped = _mapper.Map<PaginatedList<CompanyDto>>(queryList);

        return mapped;
    }
}
