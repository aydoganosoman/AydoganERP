using AydoganERP.Base.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Customer.Application.CustomerManager.Queries.GetNextCode;

public record GetNextCodeQuery(
    Guid CompanyId,
    string Prefix = "CRI") : IRequest<string>;

public class GetNextCodeQueryHandler : IRequestHandler<GetNextCodeQuery, string>
{
    private readonly IBaseDbContext _dbContext;

    public GetNextCodeQueryHandler(IBaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(GetNextCodeQuery request, CancellationToken cancellationToken)
    {
        var prefix = request.Prefix.ToUpper();
        var pattern = $"{prefix}-";

        // Aynı prefix ile başlayan en son kodu bul
        var lastCode = await _dbContext.Customers
            .Where(c => c.CompanyId == request.CompanyId)
            .Where(c => c.Code.StartsWith(pattern))
            .OrderByDescending(c => c.Code)
            .Select(c => c.Code)
            .FirstOrDefaultAsync(cancellationToken);

        int nextNumber = 1;

        if (!string.IsNullOrEmpty(lastCode))
        {
            // "CRI-00001" formatından numarayı çıkar
            var numberPart = lastCode.Replace(pattern, "");
            if (int.TryParse(numberPart, out var currentNumber))
            {
                nextNumber = currentNumber + 1;
            }
        }

        // 5 haneli format: CRI-00001
        return $"{prefix}-{nextNumber:D5}";
    }
}
