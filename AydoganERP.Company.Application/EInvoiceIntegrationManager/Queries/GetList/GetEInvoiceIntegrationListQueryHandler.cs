using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.EInvoiceIntegrationManager.Queries.GetList;

public record GetEInvoiceIntegrationListQuery(Guid CompanyId, int? IntegrationType = null) : IRequest<List<EInvoiceIntegrationDto>>;

public class GetEInvoiceIntegrationListQueryHandler : IRequestHandler<GetEInvoiceIntegrationListQuery, List<EInvoiceIntegrationDto>>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetEInvoiceIntegrationListQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EInvoiceIntegrationDto>> Handle(GetEInvoiceIntegrationListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.EInvoiceIntegrations
            .Where(x => x.CompanyId == request.CompanyId);

        if (request.IntegrationType.HasValue)
            query = query.Where(x => x.IntegrationType == request.IntegrationType.Value);

        var items = await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EInvoiceIntegrationDto>>(items);
    }
}
