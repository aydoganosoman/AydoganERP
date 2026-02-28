using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Inventory.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.StockMovementManager.Queries.GetList;

public record GetStockMovementListQuery(
    Guid? CompanyId = null,
    Guid? ProductId = null,
    int? Type = null,
    DateOnly? DateFrom = null,
    DateOnly? DateTo = null,
    string? SearchText = null,
    int PageNumber = 1,
    int PageSize = 20) : IRequest<PaginatedList<StockMovementDto>>;

public class GetStockMovementListQueryHandler : IRequestHandler<GetStockMovementListQuery, PaginatedList<StockMovementDto>>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetStockMovementListQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<StockMovementDto>> Handle(GetStockMovementListQuery request, CancellationToken cancellationToken)
    {
        var query = _baseDbContext.StockMovements
            .AsNoTracking()
            .Include(m => m.Product)
            .AsQueryable();

        // CompanyId filtresi (ürün üzerinden)
        if (request.CompanyId != null)
            query = query.Where(x => x.Product.CompanyId == request.CompanyId);

        // ProductId filtresi
        if (request.ProductId != null)
            query = query.Where(x => x.ProductId == request.ProductId);

        // Type filtresi
        if (request.Type != null)
            query = query.Where(x => x.Type == request.Type);

        // Tarih filtresi
        if (request.DateFrom != null)
            query = query.Where(x => x.Date >= request.DateFrom);

        if (request.DateTo != null)
            query = query.Where(x => x.Date <= request.DateTo);

        // Arama (ürün kodu, adı, referans no veya açıklama)
        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var searchLower = request.SearchText.ToLower();
            query = query.Where(x =>
                x.Product.Code.ToLower().Contains(searchLower) ||
                x.Product.Name.ToLower().Contains(searchLower) ||
                (x.ReferenceNo != null && x.ReferenceNo.ToLower().Contains(searchLower)) ||
                x.Description.ToLower().Contains(searchLower));
        }

        // Tarihe göre azalan sırala
        query = query.OrderByDescending(x => x.Date).ThenByDescending(x => x.Created);

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<StockMovementDto>>(items);

        return new PaginatedList<StockMovementDto>(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}
