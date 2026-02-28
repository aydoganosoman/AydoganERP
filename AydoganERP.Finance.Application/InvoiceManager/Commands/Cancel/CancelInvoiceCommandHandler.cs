using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Finance.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Commands.Cancel;

public record CancelInvoiceCommand(Guid Id, string? Reason = null) : IRequest<InvoiceDto>;

public class CancelInvoiceCommandHandler : IRequestHandler<CancelInvoiceCommand, InvoiceDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public CancelInvoiceCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(CancelInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _baseDbContext.Invoices
            .Include(i => i.Customer)
            .Include(i => i.Lines)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (invoice == null)
            throw new Exception("Fatura bulunamadı.");

        invoice.Cancel(request.Reason);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<InvoiceDto>(invoice);
    }
}
