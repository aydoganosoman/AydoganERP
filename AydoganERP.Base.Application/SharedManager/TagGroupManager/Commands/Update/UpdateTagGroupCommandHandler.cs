using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.TagGroupManager.Commands.Update;

public record UpdateTagGroupCommand(Guid Id, string Name, bool IsActive) : IRequest<TagGroupDto>;

public class UpdateTagGroupCommandHandler : IRequestHandler<UpdateTagGroupCommand, TagGroupDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTagGroupCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TagGroupDto> Handle(UpdateTagGroupCommand request, CancellationToken cancellationToken)
    {
        var tagGroup = await _context.TagGroups.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (tagGroup == null) throw new NotFoundException(nameof(TagGroup), request.Id);
        tagGroup.Rename(request.Name);
        tagGroup.SetActive(request.IsActive);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<TagGroupDto>(tagGroup);
    }
}