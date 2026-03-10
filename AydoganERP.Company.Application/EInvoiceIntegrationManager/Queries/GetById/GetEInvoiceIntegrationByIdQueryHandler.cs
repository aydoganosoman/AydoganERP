using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.EInvoiceIntegrationManager.Queries.GetById;

public record GetEInvoiceIntegrationByIdQuery(Guid Id) : IRequest<EInvoiceIntegrationDto>;

public class GetEInvoiceIntegrationByIdQueryHandler : IRequestHandler<GetEInvoiceIntegrationByIdQuery, EInvoiceIntegrationDto>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetEInvoiceIntegrationByIdQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EInvoiceIntegrationDto> Handle(GetEInvoiceIntegrationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EInvoiceIntegrations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(EInvoiceIntegration), request.Id);

        return _mapper.Map<EInvoiceIntegrationDto>(entity);
    }
}
