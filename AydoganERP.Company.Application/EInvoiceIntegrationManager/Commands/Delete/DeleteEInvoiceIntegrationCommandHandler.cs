using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.EInvoiceIntegrationManager.Commands.Delete;

public record DeleteEInvoiceIntegrationCommand(Guid Id) : IRequest;

public class DeleteEInvoiceIntegrationCommandHandler : IRequestHandler<DeleteEInvoiceIntegrationCommand>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;

    public DeleteEInvoiceIntegrationCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteEInvoiceIntegrationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EInvoiceIntegrations
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(ECommerceIntegration), request.Id);

        _context.EInvoiceIntegrations.Remove(entity);
        await _unitOfWork.CommitAsync(null, cancellationToken);
    }
}
