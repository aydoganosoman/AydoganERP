using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.StockMovementManager.Commands.BulkCreate;

public record BulkStockMovementItem(
    Guid ProductId,
    decimal QuantityDelta,
    string? Description);

public record BulkCreateStockMovementCommand(
    DateOnly Date,
    int Type,
    List<BulkStockMovementItem> Items) : IRequest<BulkCreateStockMovementResult>;

public record BulkCreateStockMovementResult(
    int SuccessCount,
    int FailedCount,
    List<string> Errors);

public class
    BulkCreateStockMovementCommandHandler : IRequestHandler<BulkCreateStockMovementCommand,
    BulkCreateStockMovementResult>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;

    public BulkCreateStockMovementCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task<BulkCreateStockMovementResult> Handle(BulkCreateStockMovementCommand request,
        CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        var successCount = 0;

        // Validate all product IDs exist
        var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
        var existingProductIds = await _baseDbContext.Products
            .Where(p => productIds.Any(a => a == p.Id))
            .Select(p => p.Id)
            .ToListAsync(cancellationToken);

        var missingProductIds = productIds.Except(existingProductIds).ToList();
        if (missingProductIds.Any())
        {
            foreach (var missingId in missingProductIds)
            {
                errors.Add($"Ürün bulunamadı: {missingId}");
            }
        }

        // Process valid items
        foreach (var item in request.Items)
        {
            if (!existingProductIds.Contains(item.ProductId))
            {
                continue; // Skip items with missing products
            }

            try
            {
                var movement = StockMovement.Create(
                    Guid.NewGuid(),
                    item.ProductId,
                    request.Date,
                    request.Type,
                    item.QuantityDelta,
                    item.Description);

                await _baseDbContext.StockMovements.AddAsync(movement, cancellationToken);
                successCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"Ürün {item.ProductId}: {ex.Message}");
            }
        }

        if (successCount > 0)
        {
            await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);
        }

        return new BulkCreateStockMovementResult(
            successCount,
            request.Items.Count - successCount,
            errors);
    }
}