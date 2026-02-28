using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using AydoganERP.Base.Domain.Modules.InventoryModule.Helpers;
using AydoganERP.Inventory.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.StockMovementManager.Queries.GetStockLevel;

public record GetStockLevelQuery(Guid ProductId) : IRequest<StockLevelDto>;

public class GetStockLevelQueryHandler : IRequestHandler<GetStockLevelQuery, StockLevelDto>
{
    private readonly IBaseDbContext _baseDbContext;

    public GetStockLevelQueryHandler(IBaseDbContext baseDbContext)
    {
        _baseDbContext = baseDbContext;
    }

    public async Task<StockLevelDto> Handle(GetStockLevelQuery request, CancellationToken cancellationToken)
    {
        var product = await _baseDbContext.Products
            .AsNoTracking()
            .Include(p => p.Unit)
            .Include(p => p.Movements)
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

        if (product == null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        var onHand = StockCalculator.CalculateOnHand(product.Movements);

        return new StockLevelDto
        {
            ProductId = product.Id,
            ProductCode = product.Code,
            ProductName = product.Name,
            OnHand = onHand,
            UnitName = product.Unit?.Name ?? string.Empty
        };
    }
}
