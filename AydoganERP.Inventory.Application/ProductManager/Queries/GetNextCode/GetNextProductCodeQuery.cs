using AydoganERP.Base.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Queries.GetNextCode;

public record GetNextProductCodeQuery(
    Guid CompanyId,
    string Prefix = "PRD") : IRequest<string>;

public class GetNextProductCodeQueryHandler : IRequestHandler<GetNextProductCodeQuery, string>
{
    private readonly IBaseDbContext _dbContext;

    public GetNextProductCodeQueryHandler(IBaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(GetNextProductCodeQuery request, CancellationToken cancellationToken)
    {
        var prefix = request.Prefix.ToUpper();
        var pattern = $"{prefix}-";

        // Aynı prefix ile başlayan en son kodu bul
        var lastCode = await _dbContext.Products
            .Where(p => p.CompanyId == request.CompanyId)
            .Where(p => p.Code.StartsWith(pattern))
            .OrderByDescending(p => p.Code)
            .Select(p => p.Code)
            .FirstOrDefaultAsync(cancellationToken);

        int nextNumber = 1;

        if (!string.IsNullOrEmpty(lastCode))
        {
            // "PRD-00001" formatından numarayı çıkar
            var numberPart = lastCode.Replace(pattern, "");
            if (int.TryParse(numberPart, out var currentNumber))
            {
                nextNumber = currentNumber + 1;
            }
        }

        // 5 haneli format: PRD-00001
        return $"{prefix}-{nextNumber:D5}";
    }
}
