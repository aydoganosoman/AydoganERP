using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Finance.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Commands.RemoveLine;

public record RemoveInvoiceLineCommand(Guid InvoiceId, Guid LineId) : IRequest<InvoiceDto>;

public class RemoveInvoiceLineCommandHandler : IRequestHandler<RemoveInvoiceLineCommand, InvoiceDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public RemoveInvoiceLineCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(RemoveInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _baseDbContext.Invoices
            .Include(i => i.Customer)
            .Include(i => i.Lines)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);

        if (invoice == null)
            throw new Exception("Fatura bulunamadı.");

        var line = invoice.Lines.FirstOrDefault(l => l.Id == request.LineId);
        if (line == null)
            throw new Exception("Satır bulunamadı.");

        invoice.RemoveLine(request.LineId);
        _baseDbContext.InvoiceLines.Remove(line);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<InvoiceDto>(invoice);
    }
}
