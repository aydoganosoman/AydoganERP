using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Customer.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Customer.Application.CustomerManager.Queries.GetPaged;

public record GetPagedQuery(int CurrentPage, int PageSize, Guid? CompanyId = null) : IRequest<PaginatedList<CustomerDto>>;

public class GetPagedQueryHandler : IRequestHandler<GetPagedQuery, PaginatedList<CustomerDto>>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetPagedQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CustomerDto>> Handle(GetPagedQuery request, CancellationToken cancellationToken)
    {
        var query = _baseDbContext
            .Customers
            .AsNoTracking()
            .AsQueryable();

        if (request.CompanyId != null)
            query = query.Where(x => x.CompanyId == request.CompanyId);
        
        var queryList = await query
            .ToPagedListAsync(request.CurrentPage, request.PageSize, null, null);

        var mapped = _mapper.Map<PaginatedList<CustomerDto>>(queryList);

        return mapped;
    }
}
