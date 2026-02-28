using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.GroupManager.Queries.GetGroupById;

public record GetGroupByIdQuery(Guid Id) : IRequest<GroupDto>;

public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, GroupDto>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetGroupByIdQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GroupDto> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups
            .AsNoTracking()
            .Include(g => g.Categories)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (group == null) throw new NotFoundException(nameof(Group), request.Id);
        return _mapper.Map<GroupDto>(group);
    }
}