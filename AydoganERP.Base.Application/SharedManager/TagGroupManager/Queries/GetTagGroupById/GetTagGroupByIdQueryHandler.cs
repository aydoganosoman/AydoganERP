using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.TagGroupManager.Queries.GetTagGroupById;

public record GetTagGroupByIdQuery(Guid Id) : IRequest<TagGroupDto>;

public class GetTagGroupByIdQueryHandler : IRequestHandler<GetTagGroupByIdQuery, TagGroupDto>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetTagGroupByIdQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TagGroupDto> Handle(GetTagGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var tagGroup = await _context.TagGroups
            .AsNoTracking()
            .Include(tg => tg.Tags)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (tagGroup == null) throw new NotFoundException(nameof(TagGroup), request.Id);
        return _mapper.Map<TagGroupDto>(tagGroup);
    }
}