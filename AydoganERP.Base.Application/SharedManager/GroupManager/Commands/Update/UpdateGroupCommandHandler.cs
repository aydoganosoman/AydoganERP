using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.GroupManager.Commands.Update;

public record UpdateGroupCommand(Guid Id, string Code, string Name, UsageAreaEnum UsageAreas, bool IsActive) : IRequest<GroupDto>;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, GroupDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateGroupCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GroupDto> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (group == null) throw new NotFoundException(nameof(Group), request.Id);
        group.Update(request.Name, request.UsageAreas);
        group.SetActive(request.IsActive);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<GroupDto>(group);
    }
}