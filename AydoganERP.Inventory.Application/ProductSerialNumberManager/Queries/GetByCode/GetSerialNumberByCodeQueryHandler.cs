using AydoganERP.Base.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductSerialNumberManager.Queries.GetByCode;

public record SerialNumberDetailDto(
    Guid Id,
    string SerialNumber,
    int Status,
    Guid ProductId,
    string ProductCode,
    string ProductName,
    DateTime? PurchaseDate,
    decimal? PurchasePrice,
    string? PurchaseCustomerName,
    DateTime? SaleDate,
    decimal? SalePrice,
    string? SaleCustomerName,
    DateTime? WarrantyEndDate,
    string? Notes);

public record GetSerialNumberByCodeQuery(
    string SerialNumber,
    Guid? CompanyId = null) : IRequest<SerialNumberDetailDto?>;

public class GetSerialNumberByCodeQueryHandler : IRequestHandler<GetSerialNumberByCodeQuery, SerialNumberDetailDto?>
{
    private readonly IBaseDbContext _baseDbContext;

    public GetSerialNumberByCodeQueryHandler(IBaseDbContext baseDbContext)
    {
        _baseDbContext = baseDbContext;
    }

    public async Task<SerialNumberDetailDto?> Handle(GetSerialNumberByCodeQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SerialNumber))
            return null;

        var serialLower = request.SerialNumber.Trim().ToLower();

        var query = _baseDbContext.ProductSerialNumbers
            .AsNoTracking()
            .Include(sn => sn.Product)
            .Include(sn => sn.PurchaseCustomer)
            .Include(sn => sn.SaleCustomer)
            .Where(sn => sn.SerialNumber.ToLower() == serialLower);

        if (request.CompanyId.HasValue)
            query = query.Where(sn => sn.Product.CompanyId == request.CompanyId.Value);

        var serialNumber = await query.FirstOrDefaultAsync(cancellationToken);

        if (serialNumber == null)
            return null;

        return new SerialNumberDetailDto(
            serialNumber.Id,
            serialNumber.SerialNumber,
            (int)serialNumber.Status,
            serialNumber.ProductId,
            serialNumber.Product.Code,
            serialNumber.Product.Name,
            serialNumber.PurchaseDate,
            serialNumber.PurchasePrice,
            serialNumber.PurchaseCustomer?.CustomerName,
            serialNumber.SaleDate,
            serialNumber.SalePrice,
            serialNumber.SaleCustomer?.CustomerName,
            serialNumber.WarrantyEndDate,
            serialNumber.Notes);
    }
}
