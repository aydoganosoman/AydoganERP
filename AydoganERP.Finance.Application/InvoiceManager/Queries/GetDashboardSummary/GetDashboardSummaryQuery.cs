using MediatR;

namespace AydoganERP.Finance.Application.InvoiceManager.Queries.GetDashboardSummary;

public record GetDashboardSummaryQuery(
    Guid? CompanyId,
    DateTime? StartDate,
    DateTime? EndDate) : IRequest<FinanceDashboardDto>;

public class FinanceDashboardDto
{
    // Satış Özet
    public decimal TotalSalesAmount { get; set; }
    public int TotalSalesCount { get; set; }
    public decimal TotalSalesCollected { get; set; }
    public decimal TotalSalesOutstanding { get; set; }

    // Alış Özet
    public decimal TotalPurchaseAmount { get; set; }
    public int TotalPurchaseCount { get; set; }
    public decimal TotalPurchasePaid { get; set; }
    public decimal TotalPurchaseOutstanding { get; set; }

    // Durum Dağılımı
    public int DraftCount { get; set; }
    public int ApprovedCount { get; set; }
    public int CancelledCount { get; set; }
    public int EInvoiceSentCount { get; set; }

    // Aylık Özet
    public List<MonthlyInvoiceSummary> MonthlySales { get; set; } = new();
    public List<MonthlyInvoiceSummary> MonthlyPurchases { get; set; } = new();

    // Vadesi Geçmiş
    public decimal OverdueSalesAmount { get; set; }
    public int OverdueSalesCount { get; set; }
    public decimal OverduePurchaseAmount { get; set; }
    public int OverduePurchaseCount { get; set; }

    // En Çok Borçlu Müşteriler
    public List<CustomerDebtSummary> TopDebtors { get; set; } = new();
}

public class MonthlyInvoiceSummary
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Amount { get; set; }
    public int Count { get; set; }
}

public class CustomerDebtSummary
{
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalDebt { get; set; }
    public int InvoiceCount { get; set; }
}
