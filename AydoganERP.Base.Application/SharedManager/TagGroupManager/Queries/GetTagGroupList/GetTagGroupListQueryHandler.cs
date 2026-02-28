using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.TagGroupManager.Queries.GetTagGroupList;

public record GetTagGroupListQuery(Guid? CompanyId = null, bool? IsActive = null) : IRequest<List<TagGroupDto>>;

public class GetTagGroupListQueryHandler : IRequestHandler<GetTagGroupListQuery, List<TagGroupDto>>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetTagGroupListQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TagGroupDto>> Handle(GetTagGroupListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TagGroups.AsNoTracking().Include(tg => tg.Tags).AsQueryable();
        if (request.CompanyId.HasValue) query = query.Where(x => x.CompanyId == request.CompanyId.Value);
        if (request.IsActive.HasValue) query = query.Where(x => x.IsActive == request.IsActive.Value);
        return _mapper.Map<List<TagGroupDto>>(await query.OrderBy(x => x.Name).ToListAsync(cancellationToken));
    }
}