using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using AydoganERP.Inventory.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Commands.Create;

public record CreateProductUnitPriceItem(
    Guid UnitId,
    decimal ConversionRate = 1,
    string? Barcode = null,
    decimal SaleUnitPrice = 0,
    int SaleUnitPriceCurrency = 0,
    bool SaleUnitPriceVatInclude = false,
    float SaleVatRate = 0,
    bool IsBaseUnit = false);

public record CreateProductSupplierItem(
    Guid CustomerId,
    string Code,
    string Name);

public record CreateProductCommand(
    Guid CompanyId,
    string Code,
    string Name,
    Guid? UnitId = null, // Opsiyonel - boşsa varsayılan birim kullanılır
    Guid? CategoryId = null,
    decimal PurchaseUnitPrice = 0,
    int PurchaseUnitPriceCurrency = 0,
    bool PurchaseUnitPriceVatInclude = false,
    float PurchaseVatRate = 0,
    bool IsLotTracked = false,
    bool IsSerialTracked = false,
    List<CreateProductUnitPriceItem>? UnitPrices = null,
    List<CreateProductSupplierItem>? Suppliers = null) : IRequest<ProductDto>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Kod benzersizliği kontrolü
        var existingCode = await _baseDbContext.Products
            .AnyAsync(p => p.CompanyId == request.CompanyId && p.Code.ToLower() == request.Code.ToLower(), cancellationToken);

        if (existingCode)
        {
            throw new InvalidOperationException($"'{request.Code}' kodu zaten kullanılıyor. Lütfen farklı bir kod giriniz.");
        }

        // UnitId boşsa varsayılan birimi (ADET) al
        var unitId = request.UnitId;
        if (!unitId.HasValue || unitId == Guid.Empty)
        {
            var defaultUnit = await _baseDbContext.ProductUnits
                .FirstOrDefaultAsync(u => u.Code == "ADET" || u.Name == "Adet", cancellationToken);
            
            if (defaultUnit == null)
            {
                defaultUnit = await _baseDbContext.ProductUnits.FirstOrDefaultAsync(cancellationToken);
            }
            
            if (defaultUnit == null)
            {
                throw new InvalidOperationException("Sistemde tanımlı birim bulunamadı. Lütfen önce birim tanımlayın.");
            }
            
            unitId = defaultUnit.Id;
        }

        var product = Product.Create(
            Guid.NewGuid(),
            request.CompanyId,
            request.Code,
            request.Name,
            unitId.Value,
            request.CategoryId,
            request.PurchaseUnitPrice,
            request.PurchaseUnitPriceCurrency,
            request.PurchaseUnitPriceVatInclude,
            request.PurchaseVatRate,
            request.IsLotTracked,
            request.IsSerialTracked);

        await _baseDbContext.Products.AddAsync(product, cancellationToken);

        // Birim fiyatları ekle
        if (request.UnitPrices != null && request.UnitPrices.Count > 0)
        {
            foreach (var unitPriceItem in request.UnitPrices)
            {
                var unitPrice = ProductUnitPrice.Create(
                    Guid.NewGuid(),
                    product.Id,
                    unitPriceItem.UnitId,
                    unitPriceItem.ConversionRate,
                    unitPriceItem.Barcode,
                    unitPriceItem.SaleUnitPrice,
                    unitPriceItem.SaleUnitPriceCurrency,
                    unitPriceItem.SaleUnitPriceVatInclude,
                    unitPriceItem.SaleVatRate,
                    unitPriceItem.IsBaseUnit);

                await _baseDbContext.ProductUnitPrices.AddAsync(unitPrice, cancellationToken);
            }
        }

        // Tedarikçileri ekle
        if (request.Suppliers != null && request.Suppliers.Count > 0)
        {
            foreach (var supplierItem in request.Suppliers)
            {
                var supplier = ProductSupplier.Create(
                    Guid.NewGuid(),
                    product.Id,
                    supplierItem.CustomerId,
                    supplierItem.Code,
                    supplierItem.Name);

                await _baseDbContext.ProductSuppliers.AddAsync(supplier, cancellationToken);
            }
        }

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        // Ürünü ilişkileriyle birlikte çek
        var createdProduct = await _baseDbContext.Products
            .Include(p => p.UnitPrices)
                .ThenInclude(up => up.Unit)
            .Include(p => p.ProductSuppliers)
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

        return _mapper.Map<ProductDto>(createdProduct);
    }
}
