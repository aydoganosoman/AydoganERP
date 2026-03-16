using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Finance.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Commands.Update;

public record UpdateInvoiceCommand(
    Guid Id,
    DateTime InvoiceDate,
    int PaymentTermDays,
    int Currency,
    decimal ExchangeRate,
    string? Description,
    int EInvoiceScenario = 0,
    string? PostboxAlias = null,
    TimeSpan? InvoiceTime = null,
    string? SeriesPrefix = null,
    int? InvoiceSerial = null,
    bool ReplacesInvoiceRef = false,
    decimal RoundingAmount = 0,
    decimal InvoiceSubDiscount = 0) : IRequest<InvoiceDto>;

public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, InvoiceDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateInvoiceCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _baseDbContext.Invoices
            .Include(i => i.Customer)
            .Include(i => i.Lines)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (invoice == null)
            throw new Exception("Fatura bulunamadı.");

        invoice.UpdateDetails(
            request.InvoiceDate,
            request.PaymentTermDays,
            request.Currency,
            request.ExchangeRate,
            request.Description,
            request.EInvoiceScenario,
            request.PostboxAlias,
            request.InvoiceTime,
            request.SeriesPrefix,
            request.InvoiceSerial,
            request.ReplacesInvoiceRef,
            request.RoundingAmount,
            request.InvoiceSubDiscount);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<InvoiceDto>(invoice);
    }
}
