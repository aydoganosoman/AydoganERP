using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.GroupManager.Queries.GetGroupList;

public record GetGroupListQuery(Guid? CompanyId = null, UsageAreaEnum? UsageArea = null, bool? IsActive = null) : IRequest<List<GroupDto>>;

public class GetGroupListQueryHandler : IRequestHandler<GetGroupListQuery, List<GroupDto>>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetGroupListQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GroupDto>> Handle(GetGroupListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Groups.AsNoTracking().Include(g => g.Categories).AsQueryable();
        if (request.CompanyId.HasValue) query = query.Where(x => x.CompanyId == request.CompanyId.Value);
        if (request.UsageArea.HasValue) query = query.Where(x => (x.UsageAreas & request.UsageArea.Value) != 0);
        if (request.IsActive.HasValue) query = query.Where(x => x.IsActive == request.IsActive.Value);
        return _mapper.Map<List<GroupDto>>(await query.OrderBy(x => x.Code).ToListAsync(cancellationToken));
    }
}