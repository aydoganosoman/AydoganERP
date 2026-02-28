using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Queries.GetDashboardSummary;

public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, FinanceDashboardDto>
{
    private readonly IBaseDbContext _dbContext;

    public GetDashboardSummaryQueryHandler(IBaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FinanceDashboardDto> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Invoices.AsNoTracking();

        if (request.CompanyId.HasValue)
            query = query.Where(i => i.CompanyId == request.CompanyId.Value);

        if (request.StartDate.HasValue)
            query = query.Where(i => i.InvoiceDate >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(i => i.InvoiceDate <= request.EndDate.Value);

        var today = DateTime.UtcNow.Date;

        // Tüm faturaları çek
        var invoices = await query
            .Include(i => i.Payments)
            .ToListAsync(cancellationToken);

        // Müşteri bilgilerini ayrı çek
        var customerIds = invoices.Select(i => i.CustomerId).Distinct().ToList();
        var customers = await _dbContext.Customers
            .Where(c => customerIds.Contains(c.Id))
            .Select(c => new { c.Id, c.CustomerName })
            .ToDictionaryAsync(c => c.Id, c => c.CustomerName, cancellationToken);

        // Satış faturaları
        var salesInvoices = invoices.Where(i => i.InvoiceType == InvoiceTypeEnum.SalesInvoice).ToList();
        var purchaseInvoices = invoices.Where(i => i.InvoiceType == InvoiceTypeEnum.PurchaseInvoice).ToList();

        var dto = new FinanceDashboardDto
        {
            // Satış Özet
            TotalSalesAmount = salesInvoices.Sum(i => i.GrandTotal),
            TotalSalesCount = salesInvoices.Count,
            TotalSalesCollected = salesInvoices.Sum(i => i.Payments.Sum(p => p.Amount)),
            TotalSalesOutstanding = salesInvoices.Sum(i => i.GrandTotal - i.Payments.Sum(p => p.Amount)),

            // Alış Özet
            TotalPurchaseAmount = purchaseInvoices.Sum(i => i.GrandTotal),
            TotalPurchaseCount = purchaseInvoices.Count,
            TotalPurchasePaid = purchaseInvoices.Sum(i => i.Payments.Sum(p => p.Amount)),
            TotalPurchaseOutstanding = purchaseInvoices.Sum(i => i.GrandTotal - i.Payments.Sum(p => p.Amount)),

            // Durum Dağılımı
            DraftCount = invoices.Count(i => i.Status == InvoiceStatusEnum.Draft),
            ApprovedCount = invoices.Count(i => i.Status == InvoiceStatusEnum.Approved),
            CancelledCount = invoices.Count(i => i.Status == InvoiceStatusEnum.Cancelled),
            EInvoiceSentCount = invoices.Count(i => i.Status == InvoiceStatusEnum.EInvoiceSent || i.Status == InvoiceStatusEnum.EInvoiceAccepted),

            // Vadesi Geçmiş Satışlar
            OverdueSalesAmount = salesInvoices
                .Where(i => i.DueDate.HasValue && i.DueDate.Value < today &&
                           i.GrandTotal > i.Payments.Sum(p => p.Amount))
                .Sum(i => i.GrandTotal - i.Payments.Sum(p => p.Amount)),
            OverdueSalesCount = salesInvoices
                .Count(i => i.DueDate.HasValue && i.DueDate.Value < today &&
                           i.GrandTotal > i.Payments.Sum(p => p.Amount)),

            // Vadesi Geçmiş Alışlar
            OverduePurchaseAmount = purchaseInvoices
                .Where(i => i.DueDate.HasValue && i.DueDate.Value < today &&
                           i.GrandTotal > i.Payments.Sum(p => p.Amount))
                .Sum(i => i.GrandTotal - i.Payments.Sum(p => p.Amount)),
            OverduePurchaseCount = purchaseInvoices
                .Count(i => i.DueDate.HasValue && i.DueDate.Value < today &&
                           i.GrandTotal > i.Payments.Sum(p => p.Amount)),

            // Aylık Satışlar (son 12 ay)
            MonthlySales = salesInvoices
                .Where(i => i.Status != InvoiceStatusEnum.Cancelled)
                .GroupBy(i => new { i.InvoiceDate.Year, i.InvoiceDate.Month })
                .Select(g => new MonthlyInvoiceSummary
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Amount = g.Sum(i => i.GrandTotal),
                    Count = g.Count()
                })
                .OrderByDescending(m => m.Year)
                .ThenByDescending(m => m.Month)
                .Take(12)
                .ToList(),

            // Aylık Alışlar (son 12 ay)
            MonthlyPurchases = purchaseInvoices
                .Where(i => i.Status != InvoiceStatusEnum.Cancelled)
                .GroupBy(i => new { i.InvoiceDate.Year, i.InvoiceDate.Month })
                .Select(g => new MonthlyInvoiceSummary
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Amount = g.Sum(i => i.GrandTotal),
                    Count = g.Count()
                })
                .OrderByDescending(m => m.Year)
                .ThenByDescending(m => m.Month)
                .Take(12)
                .ToList(),

            // En Çok Borçlu Müşteriler
            TopDebtors = salesInvoices
                .Where(i => i.Status != InvoiceStatusEnum.Cancelled)
                .GroupBy(i => i.CustomerId)
                .Select(g => new CustomerDebtSummary
                {
                    CustomerId = g.Key,
                    CustomerName = customers.TryGetValue(g.Key, out var name) ? name : "Bilinmiyor",
                    TotalDebt = g.Sum(i => i.GrandTotal - i.Payments.Sum(p => p.Amount)),
                    InvoiceCount = g.Count()
                })
                .Where(c => c.TotalDebt > 0)
                .OrderByDescending(c => c.TotalDebt)
                .Take(10)
                .ToList()
        };

        return dto;
    }
}
