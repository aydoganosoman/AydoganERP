using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Inventory.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Queries.GetList;

public record GetProductListQuery(
    Guid? CompanyId = null,
    Guid? CategoryId = null,
    string? SearchText = null,
    bool? IsActive = null,
    int PageNumber = 1,
    int PageSize = 20) : IRequest<PaginatedList<ProductDto>>;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, PaginatedList<ProductDto>>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetProductListQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var query = _baseDbContext.Products
            .AsNoTracking()
            .Include(p => p.Unit)
            .Include(p => p.Category)
            .Include(p => p.UnitPrices)
                .ThenInclude(up => up.Unit)
            .Include(p => p.ProductSuppliers)
            .AsQueryable();

        if (request.CompanyId != null)
            query = query.Where(x => x.CompanyId == request.CompanyId);

        if (request.CategoryId != null)
            query = query.Where(x => x.CategoryId == request.CategoryId);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var searchLower = request.SearchText.ToLower();
            query = query.Where(x =>
                x.Code.ToLower().Contains(searchLower) ||
                x.Name.ToLower().Contains(searchLower));
        }

        if (request.IsActive != null)
            query = query.Where(x => x.IsActive == request.IsActive);

        query = query.OrderBy(x => x.Code);

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<ProductDto>>(items);

        return new PaginatedList<ProductDto>(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}
