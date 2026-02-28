using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.InventoryModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductSerialNumberManager.Commands.UpdateStatus;

public record UpdateSerialNumberStatusCommand(
    Guid Id,
    int Status,
    string? Notes = null) : IRequest<bool>;

public class UpdateSerialNumberStatusCommandHandler : IRequestHandler<UpdateSerialNumberStatusCommand, bool>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;

    public UpdateSerialNumberStatusCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task<bool> Handle(UpdateSerialNumberStatusCommand request, CancellationToken cancellationToken)
    {
        var serialNumber = await _baseDbContext.ProductSerialNumbers
            .FirstOrDefaultAsync(sn => sn.Id == request.Id, cancellationToken);

        if (serialNumber == null)
            return false;

        // Durum güncelle
        serialNumber.UpdateStatus((SerialNumberStatusEnum)request.Status);

        // Not güncelle (varsa)
        if (request.Notes != null)
            serialNumber.SetNotes(request.Notes);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return true;
    }
}
