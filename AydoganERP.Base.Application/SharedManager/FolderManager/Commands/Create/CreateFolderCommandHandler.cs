using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using MediatR;

namespace AydoganERP.Base.Application.SharedManager.FolderManager.Commands.Create;

public record CreateFolderCommand(Guid CompanyId, string Code, string Name, string? Color, FolderDocumentTypeEnum DocumentTypes)
    : IRequest<FolderDto>;

public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, FolderDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateFolderCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FolderDto> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        var folder = Folder.Create(
            Guid.NewGuid(),
            request.CompanyId,
            request.Code,
            request.Name,
            request.DocumentTypes,
            request.Color
        );
        await _context.Folders.AddAsync(folder, cancellationToken);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<FolderDto>(folder);
    }
}