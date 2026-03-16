using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Entities;

/// <summary>
/// Fatura notları - birden fazla not eklenebilir
/// </summary>
public class InvoiceNote : Entity
{
    // For EF
    public InvoiceNote() { }

    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;
    public string NoteText { get; private set; } = string.Empty;
    public int SortOrder { get; private set; }

    public static InvoiceNote Create(Guid id, Guid invoiceId, string noteText, int sortOrder = 0)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId cannot be empty.");
        if (string.IsNullOrWhiteSpace(noteText)) throw new ArgumentException("NoteText is required.");

        return new InvoiceNote
        {
            Id = id,
            InvoiceId = invoiceId,
            NoteText = noteText.Trim(),
            SortOrder = sortOrder
        };
    }

    public void Update(string noteText, int sortOrder)
    {
        if (string.IsNullOrWhiteSpace(noteText)) throw new ArgumentException("NoteText is required.");
        NoteText = noteText.Trim();
        SortOrder = sortOrder;
    }
}
