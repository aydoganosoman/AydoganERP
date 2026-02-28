using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.CategoryManager.Queries.GetCategoryList;

public record GetCategoryListQuery(Guid? CompanyId = null, Guid? GroupId = null, ProcessTypeEnum? ProcessType = null, bool? IsActive = null)
    : IRequest<List<CategoryDto>>;

public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<CategoryDto>>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryListQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Categories.AsNoTracking().Include(c => c.Group).AsQueryable();
        if (request.CompanyId.HasValue) query = query.Where(x => x.CompanyId == request.CompanyId.Value);
        if (request.GroupId.HasValue) query = query.Where(x => x.GroupId == request.GroupId.Value);
        if (request.ProcessType.HasValue) query = query.Where(x => (x.ProcessTypes & request.ProcessType.Value) != 0);
        if (request.IsActive.HasValue) query = query.Where(x => x.IsActive == request.IsActive.Value);
        return _mapper.Map<List<CategoryDto>>(await query.OrderBy(x => x.Code).ToListAsync(cancellationToken));
    }
}