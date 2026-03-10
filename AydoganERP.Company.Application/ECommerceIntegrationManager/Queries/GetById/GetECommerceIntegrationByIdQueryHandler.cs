using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.ECommerceIntegrationManager.Queries.GetById;

public record GetECommerceIntegrationByIdQuery(Guid Id) : IRequest<ECommerceIntegrationDto>;

public class GetECommerceIntegrationByIdQueryHandler : IRequestHandler<GetECommerceIntegrationByIdQuery, ECommerceIntegrationDto>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetECommerceIntegrationByIdQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ECommerceIntegrationDto> Handle(GetECommerceIntegrationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ECommerceIntegrations
            .Include(x => x.Defaults)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(ECommerceIntegration), request.Id);

        return _mapper.Map<ECommerceIntegrationDto>(entity);
    }
}
