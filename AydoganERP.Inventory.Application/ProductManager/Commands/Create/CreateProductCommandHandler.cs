using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using AydoganERP.Inventory.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Commands.Create;

public record CreateProductBarcodeItem(
    string Barcode,
    decimal Quantity = 1,
    string Unit = "Adet");

public record CreateProductSupplierItem(
    Guid CustomerId,
    string Code,
    string Name);

public record CreateProductCommand(
    Guid CompanyId,
    string Code,
    string Name,
    Guid UnitId,
    Guid? CategoryId = null,
    decimal PurchaseUnitPrice = 0,
    int PurchaseUnitPriceCurrency = 0,
    bool PurchaseUnitPriceVatInculde = false,
    decimal SaleUnitPrice = 0,
    int SaleUnitPriceCurrency = 0,
    bool SaleUnitPriceVatInculde = false,
    float PurchaseVatRate = 0,
    float SaleVatRate = 0,
    bool IsLotTracked = false,
    bool IsSerialTracked = false,
    List<CreateProductBarcodeItem>? Barcodes = null,
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

        var product = Product.Create(
            Guid.NewGuid(),
            request.CompanyId,
            request.Code,
            request.Name,
            request.UnitId,
            request.CategoryId,
            request.PurchaseUnitPrice,
            request.PurchaseUnitPriceCurrency,
            request.PurchaseUnitPriceVatInculde,
            request.SaleUnitPrice,
            request.SaleUnitPriceCurrency,
            request.SaleUnitPriceVatInculde,
            request.PurchaseVatRate,
            request.SaleVatRate,
            request.IsLotTracked,
            request.IsSerialTracked);

        await _baseDbContext.Products.AddAsync(product, cancellationToken);

        // Barkodları ekle
        if (request.Barcodes != null && request.Barcodes.Count > 0)
        {
            foreach (var barcodeItem in request.Barcodes)
            {
                var barcode = ProductBarcode.Create(
                    Guid.NewGuid(),
                    product.Id,
                    barcodeItem.Barcode,
                    barcodeItem.Quantity,
                    barcodeItem.Unit);

                await _baseDbContext.ProductBarcodes.AddAsync(barcode, cancellationToken);
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
            .Include(p => p.ProductBarcodes)
            .Include(p => p.ProductSuppliers)
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

        return _mapper.Map<ProductDto>(createdProduct);
    }
}
