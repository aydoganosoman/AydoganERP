using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Finance.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Queries.GetById;

public record GetInvoiceByIdQuery(Guid Id) : IRequest<InvoiceDto?>;

public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto?>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetInvoiceByIdQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<InvoiceDto?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _baseDbContext.Invoices
            .AsNoTracking()
            .Include(i => i.Customer)
            .Include(i => i.Lines.OrderBy(l => l.LineNumber))
                .ThenInclude(l => l.SerialNumber)
            .Include(i => i.Payments.OrderBy(p => p.PaymentDate))
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (invoice == null)
            return null;

        return _mapper.Map<InvoiceDto>(invoice);
    }
}
