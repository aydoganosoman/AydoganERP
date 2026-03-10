using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.ECommerceIntegrationManager.Queries.GetList;

public record GetECommerceIntegrationListQuery(Guid CompanyId, int? IntegrationType = null) : IRequest<List<ECommerceIntegrationDto>>;

public class GetECommerceIntegrationListQueryHandler : IRequestHandler<GetECommerceIntegrationListQuery, List<ECommerceIntegrationDto>>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetECommerceIntegrationListQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ECommerceIntegrationDto>> Handle(GetECommerceIntegrationListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ECommerceIntegrations
            .Include(x => x.Defaults)
            .Where(x => x.CompanyId == request.CompanyId);

        if (request.IntegrationType.HasValue)
            query = query.Where(x => x.IntegrationType == request.IntegrationType.Value);

        var items = await query
            .OrderBy(x => x.StoreName)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ECommerceIntegrationDto>>(items);
    }
}
