using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Inventory.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.StockMovementManager.Queries.GetByProduct;

public record GetStockMovementsByProductQuery(Guid ProductId) : IRequest<List<StockMovementDto>>;

public class GetStockMovementsByProductQueryHandler : IRequestHandler<GetStockMovementsByProductQuery, List<StockMovementDto>>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetStockMovementsByProductQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<List<StockMovementDto>> Handle(GetStockMovementsByProductQuery request, CancellationToken cancellationToken)
    {
        var movements = await _baseDbContext.StockMovements
            .AsNoTracking()
            .Include(m => m.Product)
            .Where(m => m.ProductId == request.ProductId)
            .OrderByDescending(m => m.Date)
            .ThenByDescending(m => m.Created)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<StockMovementDto>>(movements);
    }
}
