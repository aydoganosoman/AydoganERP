using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.DocumentNumberingManager.Commands.Delete;

public record DeleteDocumentNumberingCommand(Guid Id) : IRequest;

public class DeleteDocumentNumberingCommandHandler : IRequestHandler<DeleteDocumentNumberingCommand>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;

    public DeleteDocumentNumberingCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDocumentNumberingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.DocumentNumberings
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(DocumentNumbering), request.Id);

        _context.DocumentNumberings.Remove(entity);
        await _unitOfWork.CommitAsync(null, cancellationToken);
    }
}
