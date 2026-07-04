using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.FolderManager.Queries.GetFolderList;

public record GetFolderListQuery(Guid? CompanyId = null, FolderDocumentTypeEnum? DocumentTypes = null, bool? IsActive = null)
    : IRequest<List<FolderDto>>;

public class GetFolderListQueryHandler : IRequestHandler<GetFolderListQuery, List<FolderDto>>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetFolderListQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FolderDto>> Handle(GetFolderListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Folders.AsNoTracking().AsQueryable();
        if (request.CompanyId.HasValue) query = query.Where(x => x.CompanyId == request.CompanyId.Value);
        if (request.DocumentTypes.HasValue)
            query = query.Where(x => (x.DocumentTypes & request.DocumentTypes.Value) != 0);
        if (request.IsActive.HasValue) query = query.Where(x => x.IsActive == request.IsActive.Value);
        return _mapper.Map<List<FolderDto>>(await query.OrderBy(x => x.Code).ToListAsync(cancellationToken));
    }
}