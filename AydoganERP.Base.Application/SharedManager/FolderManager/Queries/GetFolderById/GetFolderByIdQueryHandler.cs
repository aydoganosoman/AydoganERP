using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.FolderManager.Queries.GetFolderById;

public record GetFolderByIdQuery(Guid Id) : IRequest<FolderDto>;

public class GetFolderByIdQueryHandler : IRequestHandler<GetFolderByIdQuery, FolderDto>
{
    private readonly IBaseDbContext _context;
    private readonly IMapper _mapper;

    public GetFolderByIdQueryHandler(IBaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FolderDto> Handle(GetFolderByIdQuery request, CancellationToken cancellationToken)
    {
        var folder = await _context.Folders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (folder == null) throw new NotFoundException(nameof(Folder), request.Id);
        return _mapper.Map<FolderDto>(folder);
    }
}