using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;

namespace AydoganERP.Company.Application.DocumentNumberingManager.Commands.Create;

public record CreateDocumentNumberingCommand(
    Guid CompanyId,
    int DocumentType,
    string Prefix,
    bool IsDefault) : IRequest<DocumentNumberingDto>;

public class CreateDocumentNumberingCommandHandler : IRequestHandler<CreateDocumentNumberingCommand, DocumentNumberingDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDocumentNumberingCommandHandler(
        IBaseDbContext context,
        IDomainEventUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DocumentNumberingDto> Handle(CreateDocumentNumberingCommand request, CancellationToken cancellationToken)
    {
        var entity = DocumentNumbering.Create(
            Guid.NewGuid(),
            request.CompanyId,
            request.DocumentType,
            request.Prefix,
            request.IsDefault);

        _context.DocumentNumberings.Add(entity);
        await _unitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<DocumentNumberingDto>(entity);
    }
}
