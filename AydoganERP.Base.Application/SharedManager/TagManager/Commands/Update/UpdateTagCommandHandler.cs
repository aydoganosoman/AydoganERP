using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.TagManager.Commands.Update;

public record UpdateTagCommand(Guid Id, string Name, Guid TagGroupId, string? Color, bool IsActive) : IRequest<TagDto>;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, TagDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTagCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TagDto> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (tag == null) throw new NotFoundException(nameof(Tag), request.Id);
        tag.Update(request.Name, request.Color);
        tag.ChangeGroup(request.TagGroupId);
        tag.SetActive(request.IsActive);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<TagDto>(tag);
    }
}