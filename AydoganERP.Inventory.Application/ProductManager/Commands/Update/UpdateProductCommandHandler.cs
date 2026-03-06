using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using AydoganERP.Inventory.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Commands.Update;

public record UpdateProductUnitPriceItem(
    Guid? Id,
    Guid UnitId,
    decimal ConversionRate = 1,
    string? Barcode = null,
    decimal SaleUnitPrice = 0,
    int SaleUnitPriceCurrency = 0,
    bool SaleUnitPriceVatInclude = false,
    float SaleVatRate = 0,
    bool IsBaseUnit = false);

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
    Guid? CategoryId = null,
    List<UpdateProductUnitPriceItem>? UnitPrices = null,
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
            .Include(p => p.UnitPrices)
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

        // Birim fiyatları güncelle
        await UpdateUnitPricesAsync(product, request.UnitPrices, cancellationToken);

        // Tedarişçileri güncelle
        await UpdateSuppliersAsync(product, request.Suppliers, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        // Ürünü ilişkileriyle birlikte çek
        var updatedProduct = await _baseDbContext.Products
            .Include(p => p.UnitPrices)
                .ThenInclude(up => up.Unit)
            .Include(p => p.ProductSuppliers)
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

        return _mapper.Map<ProductDto>(updatedProduct);
    }

    private async Task UpdateUnitPricesAsync(
        Product product,
        List<UpdateProductUnitPriceItem>? requestUnitPrices,
        CancellationToken cancellationToken)
    {
        if (requestUnitPrices == null)
            return;

        var existingUnitPrices = product.UnitPrices.ToList();
        var requestUnitPriceIds = requestUnitPrices
            .Where(u => u.Id.HasValue && u.Id != Guid.Empty)
            .Select(u => u.Id!.Value)
            .ToHashSet();

        // Silinecekleri bul ve sil
        var toDelete = existingUnitPrices.Where(e => !requestUnitPriceIds.Contains(e.Id)).ToList();
        foreach (var unitPrice in toDelete)
        {
            _baseDbContext.ProductUnitPrices.Remove(unitPrice);
        }

        // Mevcut olanları güncelle
        foreach (var item in requestUnitPrices.Where(u => u.Id.HasValue && u.Id != Guid.Empty))
        {
            var existing = existingUnitPrices.FirstOrDefault(e => e.Id == item.Id!.Value);
            if (existing != null)
            {
                existing.Update(
                    item.ConversionRate,
                    item.Barcode,
                    item.SaleUnitPrice,
                    item.SaleUnitPriceCurrency,
                    item.SaleUnitPriceVatInclude,
                    item.SaleVatRate,
                    item.IsBaseUnit);
            }
        }

        // Yeni eklenecekleri ekle
        var newUnitPrices = requestUnitPrices.Where(u => !u.Id.HasValue || u.Id == Guid.Empty).ToList();
        foreach (var item in newUnitPrices)
        {
            var unitPrice = ProductUnitPrice.Create(
                Guid.NewGuid(),
                product.Id,
                item.UnitId,
                item.ConversionRate,
                item.Barcode,
                item.SaleUnitPrice,
                item.SaleUnitPriceCurrency,
                item.SaleUnitPriceVatInclude,
                item.SaleVatRate,
                item.IsBaseUnit);

            await _baseDbContext.ProductUnitPrices.AddAsync(unitPrice, cancellationToken);
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
