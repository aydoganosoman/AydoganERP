using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;

namespace AydoganERP.Base.Application.SharedManager.TagGroupManager.Commands.Create;

public record CreateTagGroupCommand(Guid CompanyId, string Name) : IRequest<TagGroupDto>;

public class CreateTagGroupCommandHandler : IRequestHandler<CreateTagGroupCommand, TagGroupDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTagGroupCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TagGroupDto> Handle(CreateTagGroupCommand request, CancellationToken cancellationToken)
    {
        var tagGroup = TagGroup.Create(Guid.NewGuid(), request.CompanyId, request.Name);
        await _context.TagGroups.AddAsync(tagGroup, cancellationToken);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<TagGroupDto>(tagGroup);
    }
}