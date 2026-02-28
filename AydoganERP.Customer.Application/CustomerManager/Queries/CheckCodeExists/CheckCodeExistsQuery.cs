using AydoganERP.Base.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Customer.Application.CustomerManager.Queries.CheckCodeExists;

public record CheckCodeExistsQuery(
    string Code,
    Guid CompanyId,
    Guid? ExcludeCustomerId = null) : IRequest<CheckCodeExistsResult>;

public class CheckCodeExistsResult
{
    public bool Exists { get; set; }
    public string? ExistingCustomerName { get; set; }
}

public class CheckCodeExistsQueryHandler : IRequestHandler<CheckCodeExistsQuery, CheckCodeExistsResult>
{
    private readonly IBaseDbContext _dbContext;

    public CheckCodeExistsQueryHandler(IBaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CheckCodeExistsResult> Handle(CheckCodeExistsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Customers
            .Where(c => c.CompanyId == request.CompanyId)
            .Where(c => c.Code.ToLower() == request.Code.ToLower());

        // Güncelleme durumunda mevcut müşteriyi hariç tut
        if (request.ExcludeCustomerId.HasValue)
        {
            query = query.Where(c => c.Id != request.ExcludeCustomerId.Value);
        }

        var existing = await query
            .Select(c => new { c.Code, c.Name })
            .FirstOrDefaultAsync(cancellationToken);

        return new CheckCodeExistsResult
        {
            Exists = existing != null,
            ExistingCustomerName = existing?.Name
        };
    }
}
