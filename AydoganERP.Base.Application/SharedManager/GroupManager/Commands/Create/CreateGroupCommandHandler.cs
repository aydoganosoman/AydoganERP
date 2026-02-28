using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using MediatR;

namespace AydoganERP.Base.Application.SharedManager.GroupManager.Commands.Create;

public record CreateGroupCommand(Guid CompanyId, string Code, string Name, UsageAreaEnum UsageAreas) : IRequest<GroupDto>;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, GroupDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGroupCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GroupDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = Group.Create(Guid.NewGuid(), request.CompanyId, request.Code, request.Name, request.UsageAreas);
        await _context.Groups.AddAsync(group, cancellationToken);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<GroupDto>(group);
    }
}