using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using AydoganERP.Inventory.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Commands.Update;

public record UpdateProductBarcodeItem(
    Guid? Id,
    string Barcode,
    decimal Quantity = 1,
    string Unit = "Adet");

public record UpdateProductSupplierItem(
    Guid? Id,
    Guid CustomerId,
    string Code,
    string Name);

public record UpdateProductCommand(
    Guid Id,
    string Name,
    Guid UnitId,
    bool IsLotTracked,
    bool IsSerialTracked,
    bool IsActive,
    decimal PurchaseUnitPrice = 0,
    int PurchaseUnitPriceCurrency = 0,
    bool PurchaseUnitPriceVatInclude = false,
    float PurchaseVatRate = 0,
    decimal SaleUnitPrice = 0,
    int SaleUnitPriceCurrency = 0,
    bool SaleUnitPriceVatInclude = false,
    float SaleVatRate = 0,
    Guid? CategoryId = null,
    List<UpdateProductBarcodeItem>? Barcodes = null,
    List<UpdateProductSupplierItem>? Suppliers = null) : IRequest<ProductDto>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _baseDbContext.Products
            .Include(p => p.ProductBarcodes)
            .Include(p => p.ProductSuppliers)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product == null)
            throw new NotFoundException(nameof(Product), request.Id);

        product.Rename(request.Name);
        product.SetUnit(request.UnitId);
        product.SetActive(request.IsActive);
        product.SetLotTracked(request.IsLotTracked);
        product.SetSerialTracked(request.IsSerialTracked);
        product.SetCategory(request.CategoryId);
        product.SetPurchasePricing(
            request.PurchaseUnitPrice,
            request.PurchaseUnitPriceCurrency,
            request.PurchaseUnitPriceVatInclude,
            request.PurchaseVatRate);
        product.SetSalePricing(
            request.SaleUnitPrice,
            request.SaleUnitPriceCurrency,
            request.SaleUnitPriceVatInclude,
            request.SaleVatRate);

        // Barkodları güncelle
        await UpdateBarcodesAsync(product, request.Barcodes, cancellationToken);

        // Tedarikçileri güncelle
        await UpdateSuppliersAsync(product, request.Suppliers, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        // Ürünü ilişkileriyle birlikte çek
        var updatedProduct = await _baseDbContext.Products
            .Include(p => p.ProductBarcodes)
            .Include(p => p.ProductSuppliers)
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

        return _mapper.Map<ProductDto>(updatedProduct);
    }

    private async Task UpdateBarcodesAsync(
        Product product,
        List<UpdateProductBarcodeItem>? requestBarcodes,
        CancellationToken cancellationToken)
    {
        if (requestBarcodes == null)
            return;

        var existingBarcodes = product.ProductBarcodes.ToList();
        var requestBarcodeIds = requestBarcodes
            .Where(b => b.Id.HasValue && b.Id != Guid.Empty)
            .Select(b => b.Id!.Value)
            .ToHashSet();

        // Silinecekleri bul ve sil
        var toDelete = existingBarcodes.Where(e => !requestBarcodeIds.Contains(e.Id)).ToList();
        foreach (var barcode in toDelete)
        {
            _baseDbContext.ProductBarcodes.Remove(barcode);
        }

        // Yeni eklenecekleri ekle
        var newBarcodes = requestBarcodes.Where(b => !b.Id.HasValue || b.Id == Guid.Empty).ToList();
        foreach (var item in newBarcodes)
        {
            var barcode = ProductBarcode.Create(
                Guid.NewGuid(),
                product.Id,
                item.Barcode,
                item.Quantity,
                item.Unit);

            await _baseDbContext.ProductBarcodes.AddAsync(barcode, cancellationToken);
        }
    }

    private async Task UpdateSuppliersAsync(
        Product product,
        List<UpdateProductSupplierItem>? requestSuppliers,
        CancellationToken cancellationToken)
    {
        if (requestSuppliers == null)
            return;

        var existingSuppliers = product.ProductSuppliers.ToList();
        var requestSupplierIds = requestSuppliers
            .Where(s => s.Id.HasValue && s.Id != Guid.Empty)
            .Select(s => s.Id!.Value)
            .ToHashSet();

        // Silinecekleri bul ve sil
        var toDelete = existingSuppliers.Where(e => !requestSupplierIds.Contains(e.Id)).ToList();
        foreach (var supplier in toDelete)
        {
            _baseDbContext.ProductSuppliers.Remove(supplier);
        }

        // Yeni eklenecekleri ekle
        var newSuppliers = requestSuppliers.Where(s => !s.Id.HasValue || s.Id == Guid.Empty).ToList();
        foreach (var item in newSuppliers)
        {
            var supplier = ProductSupplier.Create(
                Guid.NewGuid(),
                product.Id,
                item.CustomerId,
                item.Code,
                item.Name);

            await _baseDbContext.ProductSuppliers.AddAsync(supplier, cancellationToken);
        }
    }
}
