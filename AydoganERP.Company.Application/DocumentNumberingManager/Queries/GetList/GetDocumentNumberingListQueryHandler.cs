using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.DocumentNumberingManager.Queries.GetList;

public record GetDocumentNumberingListQuery(Guid CompanyId) : IRequest<List<DocumentNumberingDto>>;

public class GetDocumentNumberingListQueryHandler : IRequestHandler<GetDocumentNumberingListQuery, List<DocumentNumberingDto>>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetDocumentNumberingListQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DocumentNumberingDto>> Handle(GetDocumentNumberingListQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.DocumentNumberings
            .Where(x => x.CompanyId == request.CompanyId)
            .OrderBy(x => x.DocumentType)
            .ThenBy(x => x.Prefix)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<DocumentNumberingDto>>(items);
    }
}
