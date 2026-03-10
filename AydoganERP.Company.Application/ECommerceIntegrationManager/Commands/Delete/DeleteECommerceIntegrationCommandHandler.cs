using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.ECommerceIntegrationManager.Commands.Delete;

public record DeleteECommerceIntegrationCommand(Guid Id) : IRequest;

public class DeleteECommerceIntegrationCommandHandler : IRequestHandler<DeleteECommerceIntegrationCommand>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;

    public DeleteECommerceIntegrationCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteECommerceIntegrationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ECommerceIntegrations
            .Include(x => x.Defaults)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(ECommerceIntegration), request.Id);

        _context.ECommerceIntegrations.Remove(entity);
        await _unitOfWork.CommitAsync(null, cancellationToken);
    }
}
