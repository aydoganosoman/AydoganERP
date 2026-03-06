using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.DocumentNumberingManager.Commands.Update;

public record UpdateDocumentNumberingCommand(
    Guid Id,
    string Prefix,
    bool IsDefault,
    bool IsActive) : IRequest<DocumentNumberingDto>;

public class UpdateDocumentNumberingCommandHandler : IRequestHandler<UpdateDocumentNumberingCommand, DocumentNumberingDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDocumentNumberingCommandHandler(
        IBaseDbContext context,
        IDomainEventUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DocumentNumberingDto> Handle(UpdateDocumentNumberingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.DocumentNumberings
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(DocumentNumbering), request.Id);

        entity.Update(request.Prefix, request.IsDefault, request.IsActive);
        await _unitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<DocumentNumberingDto>(entity);
    }
}
