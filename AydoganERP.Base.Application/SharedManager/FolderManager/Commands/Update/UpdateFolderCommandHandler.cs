using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.FolderManager.Commands.Update;

public record UpdateFolderCommand(Guid Id, string Code, string Name, string? Color, DocumentTypeEnum DocumentTypes, bool IsActive)
    : IRequest<FolderDto>;

public class UpdateFolderCommandHandler : IRequestHandler<UpdateFolderCommand, FolderDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateFolderCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FolderDto> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
    {
        var folder = await _context.Folders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (folder == null) throw new NotFoundException(nameof(Folder), request.Id);
        folder.Update(request.Name, request.DocumentTypes, request.Color);
        folder.SetActive(request.IsActive);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<FolderDto>(folder);
    }
}