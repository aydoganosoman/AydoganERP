using AydoganERP.Base.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Queries.GetByBarcode;

public record ProductWithStockDto(
    Guid Id,
    string Code,
    string Name,
    string? UnitName,
    decimal CurrentStock);

public record GetProductByBarcodeQuery(
    string Barcode,
    Guid? CompanyId = null) : IRequest<ProductWithStockDto?>;

public class GetProductByBarcodeQueryHandler : IRequestHandler<GetProductByBarcodeQuery, ProductWithStockDto?>
{
    private readonly IBaseDbContext _baseDbContext;

    public GetProductByBarcodeQueryHandler(IBaseDbContext baseDbContext)
    {
        _baseDbContext = baseDbContext;
    }

    public async Task<ProductWithStockDto?> Handle(GetProductByBarcodeQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Barcode))
            return null;

        var barcodeLower = request.Barcode.Trim().ToLower();

        // Barkod ile ürünü bul
        var query = _baseDbContext.ProductBarcodes
            .AsNoTracking()
            .Include(pb => pb.Product)
                .ThenInclude(p => p.Unit)
            .Where(pb => pb.Barcode.ToLower() == barcodeLower);

        if (request.CompanyId.HasValue)
            query = query.Where(pb => pb.Product.CompanyId == request.CompanyId.Value);

        var productBarcode = await query.FirstOrDefaultAsync(cancellationToken);

        if (productBarcode == null)
            return null;

        var product = productBarcode.Product;

        // Stok seviyesini hesapla
        var currentStock = await _baseDbContext.StockMovements
            .AsNoTracking()
            .Where(sm => sm.ProductId == product.Id)
            .SumAsync(sm => sm.QuantityDelta, cancellationToken);

        return new ProductWithStockDto(
            product.Id,
            product.Code,
            product.Name,
            product.Unit?.Name,
            currentStock);
    }
}
