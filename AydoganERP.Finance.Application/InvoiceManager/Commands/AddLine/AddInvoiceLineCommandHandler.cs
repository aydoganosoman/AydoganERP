using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using AydoganERP.Finance.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Commands.AddLine;

public record AddInvoiceLineCommand(
    Guid InvoiceId,
    Guid? ProductId,
    string ProductCode,
    string ProductName,
    string? UnitName,
    decimal Quantity,
    decimal UnitPrice,
    float VatRate,
    float DiscountRate = 0,
    string? Description = null,
    Guid? SerialNumberId = null) : IRequest<InvoiceDto>;

public class AddInvoiceLineCommandHandler : IRequestHandler<AddInvoiceLineCommand, InvoiceDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public AddInvoiceLineCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(AddInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _baseDbContext.Invoices
            .Include(i => i.Customer)
            .Include(i => i.Lines)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);

        if (invoice == null)
            throw new Exception("Fatura bulunamadı.");

        var nextLineNumber = invoice.Lines.Any() ? invoice.Lines.Max(l => l.LineNumber) + 1 : 1;

        var line = InvoiceLine.Create(
            Guid.NewGuid(),
            invoice.Id,
            nextLineNumber,
            request.ProductId,
            request.ProductCode,
            request.ProductName,
            request.UnitName,
            request.Quantity,
            request.UnitPrice,
            request.VatRate,
            request.DiscountRate,
            request.Description,
            request.SerialNumberId);

        invoice.AddLine(line);
        await _baseDbContext.InvoiceLines.AddAsync(line, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<InvoiceDto>(invoice);
    }
}
