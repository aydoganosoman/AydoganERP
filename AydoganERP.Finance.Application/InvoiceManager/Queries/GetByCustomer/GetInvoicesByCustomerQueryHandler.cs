using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Finance.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Queries.GetByCustomer;

public record GetInvoicesByCustomerQuery(
    Guid CustomerId,
    int? InvoiceType = null,
    bool? UnpaidOnly = null) : IRequest<List<InvoiceListDto>>;

public class GetInvoicesByCustomerQueryHandler : IRequestHandler<GetInvoicesByCustomerQuery, List<InvoiceListDto>>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetInvoicesByCustomerQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<List<InvoiceListDto>> Handle(GetInvoicesByCustomerQuery request, CancellationToken cancellationToken)
    {
        var query = _baseDbContext.Invoices
            .AsNoTracking()
            .Include(i => i.Customer)
            .Include(i => i.Payments)
            .Where(i => i.CustomerId == request.CustomerId);

        if (request.InvoiceType.HasValue)
            query = query.Where(i => i.InvoiceType == request.InvoiceType.Value);

        var invoices = await query
            .OrderByDescending(i => i.InvoiceDate)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<InvoiceListDto>>(invoices);

        if (request.UnpaidOnly == true)
        {
            dtos = dtos.Where(d => !d.IsPaid).ToList();
        }

        return dtos;
    }
}
