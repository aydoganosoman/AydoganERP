using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using AydoganERP.Finance.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Commands.Create;

public record CreateInvoiceLineItem(
    Guid? ProductId,
    string ProductCode,
    string ProductName,
    string? UnitName,
    decimal Quantity,
    decimal UnitPrice,
    float VatRate,
    float DiscountRate = 0,
    string? Description = null,
    Guid? SerialNumberId = null);

public record CreateInvoiceCommand(
    Guid CompanyId,
    string InvoiceNumber,
    DateTime InvoiceDate,
    int InvoiceType,
    Guid CustomerId,
    int Currency = 0,
    decimal ExchangeRate = 1,
    int PaymentTermDays = 0,
    string? Description = null,
    string? Notes = null,
    bool IsEInvoice = false,
    List<CreateInvoiceLineItem>? Lines = null) : IRequest<InvoiceDto>;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public CreateInvoiceCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = Invoice.Create(
            Guid.NewGuid(),
            request.CompanyId,
            request.InvoiceNumber,
            request.InvoiceDate,
            request.InvoiceType,
            request.CustomerId,
            request.Currency,
            request.ExchangeRate,
            request.PaymentTermDays,
            request.Description,
            request.Notes,
            request.IsEInvoice);

        await _baseDbContext.Invoices.AddAsync(invoice, cancellationToken);

        // Satırları ekle
        if (request.Lines != null && request.Lines.Count > 0)
        {
            var lineNumber = 1;
            foreach (var lineItem in request.Lines)
            {
                var line = InvoiceLine.Create(
                    Guid.NewGuid(),
                    invoice.Id,
                    lineNumber++,
                    lineItem.ProductId,
                    lineItem.ProductCode,
                    lineItem.ProductName,
                    lineItem.UnitName,
                    lineItem.Quantity,
                    lineItem.UnitPrice,
                    lineItem.VatRate,
                    lineItem.DiscountRate,
                    lineItem.Description,
                    lineItem.SerialNumberId);

                invoice.AddLine(line);
                await _baseDbContext.InvoiceLines.AddAsync(line, cancellationToken);
            }
        }

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        // Faturayı ilişkileriyle birlikte çek
        var createdInvoice = await _baseDbContext.Invoices
            .Include(i => i.Customer)
            .Include(i => i.Lines)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == invoice.Id, cancellationToken);

        return _mapper.Map<InvoiceDto>(createdInvoice);
    }
}
