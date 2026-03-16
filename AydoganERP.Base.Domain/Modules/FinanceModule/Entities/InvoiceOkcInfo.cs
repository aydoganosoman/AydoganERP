using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Entities;

/// <summary>
/// ÖKC (Ödeme Kaydedici Cihaz / Yazar Kasa) fiş bilgileri
/// </summary>
public class InvoiceOkcInfo : Entity
{
    // For EF
    public InvoiceOkcInfo() { }

    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;
    
    public string? FisNo { get; private set; } // Fiş numarası
    public DateTime? FisDate { get; private set; } // Fiş tarihi
    public TimeSpan? FisTime { get; private set; } // Fiş saati
    public int? FisType { get; private set; } // OkcFisTypeEnum: Satış(1), İade(2)
    public string? ZReportNo { get; private set; } // Z Raporu No
    public string? OkcSerialNo { get; private set; } // ÖKC Seri No

    public static InvoiceOkcInfo Create(
        Guid id,
        Guid invoiceId,
        string? fisNo = null,
        DateTime? fisDate = null,
        TimeSpan? fisTime = null,
        int? fisType = null,
        string? zReportNo = null,
        string? okcSerialNo = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId cannot be empty.");

        return new InvoiceOkcInfo
        {
            Id = id,
            InvoiceId = invoiceId,
            FisNo = fisNo?.Trim(),
            FisDate = fisDate,
            FisTime = fisTime,
            FisType = fisType,
            ZReportNo = zReportNo?.Trim(),
            OkcSerialNo = okcSerialNo?.Trim()
        };
    }

    public void Update(
        string? fisNo,
        DateTime? fisDate,
        TimeSpan? fisTime,
        int? fisType,
        string? zReportNo,
        string? okcSerialNo)
    {
        FisNo = fisNo?.Trim();
        FisDate = fisDate;
        FisTime = fisTime;
        FisType = fisType;
        ZReportNo = zReportNo?.Trim();
        OkcSerialNo = okcSerialNo?.Trim();
    }
}
