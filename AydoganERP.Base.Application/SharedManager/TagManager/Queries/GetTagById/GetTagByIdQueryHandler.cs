using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.TagManager.Queries.GetTagById;

public record GetTagByIdQuery(Guid Id) : IRequest<TagDto>;

public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagDto>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetTagByIdQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TagDto> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags
            .AsNoTracking()
            .Include(t => t.TagGroup)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (tag == null) throw new NotFoundException(nameof(Tag), request.Id);
        return _mapper.Map<TagDto>(tag);
    }
}