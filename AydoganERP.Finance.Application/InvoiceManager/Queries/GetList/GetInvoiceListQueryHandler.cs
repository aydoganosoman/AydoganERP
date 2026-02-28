using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Finance.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Finance.Application.InvoiceManager.Queries.GetList;

public record GetInvoiceListQuery(
    Guid? CompanyId = null,
    int? InvoiceType = null,
    int? Status = null,
    Guid? CustomerId = null,
    DateTime? StartDate = null,
    DateTime? EndDate = null,
    string? SearchText = null,
    bool? IsPaid = null,
    int PageNumber = 1,
    int PageSize = 20) : IRequest<PaginatedList<InvoiceListDto>>;

public class GetInvoiceListQueryHandler : IRequestHandler<GetInvoiceListQuery, PaginatedList<InvoiceListDto>>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetInvoiceListQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<InvoiceListDto>> Handle(GetInvoiceListQuery request, CancellationToken cancellationToken)
    {
        var query = _baseDbContext.Invoices
            .AsNoTracking()
            .Include(i => i.Customer)
            .Include(i => i.Payments)
            .AsQueryable();

        // Filters
        if (request.CompanyId.HasValue)
            query = query.Where(i => i.CompanyId == request.CompanyId.Value);

        if (request.InvoiceType.HasValue)
            query = query.Where(i => i.InvoiceType == request.InvoiceType.Value);

        if (request.Status.HasValue)
            query = query.Where(i => i.Status == request.Status.Value);

        if (request.CustomerId.HasValue)
            query = query.Where(i => i.CustomerId == request.CustomerId.Value);

        if (request.StartDate.HasValue)
            query = query.Where(i => i.InvoiceDate >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(i => i.InvoiceDate <= request.EndDate.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var search = request.SearchText.ToLower();
            query = query.Where(i =>
                i.InvoiceNumber.ToLower().Contains(search) ||
                i.Customer.CustomerName.ToLower().Contains(search) ||
                i.Customer.Code.ToLower().Contains(search));
        }

        // Order by date desc
        query = query.OrderByDescending(i => i.InvoiceDate).ThenByDescending(i => i.Created);

        // Total count
        var totalCount = await query.CountAsync(cancellationToken);

        // Paginate
        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<InvoiceListDto>>(items);

        // Filter by IsPaid if specified (must be done after mapping due to calculated field)
        if (request.IsPaid.HasValue)
        {
            dtos = dtos.Where(d => d.IsPaid == request.IsPaid.Value).ToList();
        }

        return new PaginatedList<InvoiceListDto>(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}
