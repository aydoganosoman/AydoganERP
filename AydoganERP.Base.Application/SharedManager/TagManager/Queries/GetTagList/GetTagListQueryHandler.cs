using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.TagManager.Queries.GetTagList;

public record GetTagListQuery(Guid? CompanyId = null, Guid? TagGroupId = null, bool? IsActive = null) : IRequest<List<TagDto>>;

public class GetTagListQueryHandler : IRequestHandler<GetTagListQuery, List<TagDto>>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetTagListQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TagDto>> Handle(GetTagListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Tags.AsNoTracking().Include(t => t.TagGroup).AsQueryable();
        if (request.CompanyId.HasValue) query = query.Where(x => x.CompanyId == request.CompanyId.Value);
        if (request.TagGroupId.HasValue) query = query.Where(x => x.TagGroupId == request.TagGroupId.Value);
        if (request.IsActive.HasValue) query = query.Where(x => x.IsActive == request.IsActive.Value);
        return _mapper.Map<List<TagDto>>(await query.OrderBy(x => x.Name).ToListAsync(cancellationToken));
    }
}