using MediatR;

namespace AydoganERP.Finance.Application.InvoiceManager.Commands.SendEInvoice;

/// <summary>
/// E-Fatura gönderme komutu
/// </summary>
public record SendEInvoiceCommand(Guid InvoiceId) : IRequest<SendEInvoiceResult>;

public class SendEInvoiceResult
{
    public bool Success { get; set; }
    public Guid? EInvoiceLogId { get; set; }
    public string? EInvoiceUUID { get; set; }
    public string? ErrorMessage { get; set; }
}
