using AydoganERP.Base.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Queries.GetByBarcode;

/// <summary>
/// Barkod ile bulunan ürün ve birim bilgisi
/// </summary>
public record ProductWithStockDto(
    Guid Id,
    string Code,
    string Name,
    Guid UnitId,
    string? UnitName,
    decimal ConversionRate,
    decimal SaleUnitPrice,
    int SaleUnitPriceCurrency,
    float SaleVatRate,
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

        // Barkod ile ürün birim fiyatını bul
        var query = _baseDbContext.ProductUnitPrices
            .AsNoTracking()
            .Include(up => up.Product)
            .Include(up => up.Unit)
            .Where(up => up.Barcode != null && up.Barcode.ToLower() == barcodeLower);

        if (request.CompanyId.HasValue)
            query = query.Where(up => up.Product.CompanyId == request.CompanyId.Value);

        var unitPrice = await query.FirstOrDefaultAsync(cancellationToken);

        if (unitPrice == null)
            return null;

        var product = unitPrice.Product;

        // Stok seviyesini hesapla (ana birimde)
        var currentStock = await _baseDbContext.StockMovements
            .AsNoTracking()
            .Where(sm => sm.ProductId == product.Id)
            .SumAsync(sm => sm.QuantityDelta, cancellationToken);

        // Seçilen birime dönüştür
        var stockInUnit = currentStock / unitPrice.ConversionRate;

        return new ProductWithStockDto(
            product.Id,
            product.Code,
            product.Name,
            unitPrice.UnitId,
            unitPrice.Unit?.Name,
            unitPrice.ConversionRate,
            unitPrice.SaleUnitPrice,
            unitPrice.SaleUnitPriceCurrency,
            unitPrice.SaleVatRate,
            stockInUnit);
    }
}
