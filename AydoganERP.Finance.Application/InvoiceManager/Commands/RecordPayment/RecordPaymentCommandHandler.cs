using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using AydoganERP.Finance.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Commands.RecordPayment;

public record RecordPaymentCommand(
    Guid InvoiceId,
    DateTime PaymentDate,
    decimal Amount,
    int PaymentMethod = PaymentMethodEnum.Cash,
    string? Reference = null,
    string? Notes = null) : IRequest<InvoiceDto>;

public class RecordPaymentCommandHandler : IRequestHandler<RecordPaymentCommand, InvoiceDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public RecordPaymentCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(RecordPaymentCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _baseDbContext.Invoices
            .Include(i => i.Customer)
            .Include(i => i.Lines)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);

        if (invoice == null)
            throw new Exception("Fatura bulunamadı.");

        var payment = InvoicePayment.Create(
            Guid.NewGuid(),
            invoice.Id,
            request.PaymentDate,
            request.Amount,
            request.PaymentMethod,
            request.Reference,
            request.Notes);

        invoice.Payments.Add(payment);
        await _baseDbContext.InvoicePayments.AddAsync(payment, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<InvoiceDto>(invoice);
    }
}
