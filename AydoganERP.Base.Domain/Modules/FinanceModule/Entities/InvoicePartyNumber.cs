using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Entities;

/// <summary>
/// Alıcı/Satıcı numaraları - Abone No, Bayi No, VKN, TCKN, EPDK No vb.
/// </summary>
public class InvoicePartyNumber : Entity
{
    // For EF
    public InvoicePartyNumber() { }

    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;
    
    public bool IsBuyer { get; private set; } // true = Alıcı, false = Satıcı
    public int NumberType { get; private set; } // PartyNumberTypeEnum
    public string Value { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public static InvoicePartyNumber Create(
        Guid id,
        Guid invoiceId,
        bool isBuyer,
        int numberType,
        string value,
        string? description = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId cannot be empty.");
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value is required.");

        return new InvoicePartyNumber
        {
            Id = id,
            InvoiceId = invoiceId,
            IsBuyer = isBuyer,
            NumberType = numberType,
            Value = value.Trim(),
            Description = description
        };
    }

    public void Update(bool isBuyer, int numberType, string value, string? description)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value is required.");

        IsBuyer = isBuyer;
        NumberType = numberType;
        Value = value.Trim();
        Description = description;
    }
}
