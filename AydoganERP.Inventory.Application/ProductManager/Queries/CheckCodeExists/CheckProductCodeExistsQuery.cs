using AydoganERP.Base.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Queries.CheckCodeExists;

public record CheckProductCodeExistsQuery(
    string Code,
    Guid CompanyId,
    Guid? ExcludeProductId = null) : IRequest<CheckProductCodeExistsResult>;

public class CheckProductCodeExistsResult
{
    public bool Exists { get; set; }
    public string? ExistingProductName { get; set; }
}

public class CheckProductCodeExistsQueryHandler : IRequestHandler<CheckProductCodeExistsQuery, CheckProductCodeExistsResult>
{
    private readonly IBaseDbContext _dbContext;

    public CheckProductCodeExistsQueryHandler(IBaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CheckProductCodeExistsResult> Handle(CheckProductCodeExistsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Products
            .Where(p => p.CompanyId == request.CompanyId)
            .Where(p => p.Code.ToLower() == request.Code.ToLower());

        // Güncelleme durumunda mevcut ürünü hariç tut
        if (request.ExcludeProductId.HasValue)
        {
            query = query.Where(p => p.Id != request.ExcludeProductId.Value);
        }

        var existing = await query
            .Select(p => new { p.Code, p.Name })
            .FirstOrDefaultAsync(cancellationToken);

        return new CheckProductCodeExistsResult
        {
            Exists = existing != null,
            ExistingProductName = existing?.Name
        };
    }
}
