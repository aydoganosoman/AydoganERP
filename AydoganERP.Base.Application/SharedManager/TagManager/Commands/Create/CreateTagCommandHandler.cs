using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.TagManager.Commands.Create;

public record CreateTagCommand(Guid CompanyId, string Name, Guid TagGroupId, string? Color) : IRequest<TagDto>;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTagCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TagDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tagGroup = await _context.TagGroups.FirstOrDefaultAsync(x => x.Id == request.TagGroupId, cancellationToken);
        if (tagGroup == null) throw new NotFoundException(nameof(TagGroup), request.TagGroupId);

        var tag = Tag.Create(Guid.NewGuid(), request.CompanyId, request.TagGroupId, request.Name, request.Color);
        await _context.Tags.AddAsync(tag, cancellationToken);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<TagDto>(tag);
    }
}