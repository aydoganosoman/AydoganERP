using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using AydoganERP.Inventory.Application.Models;
using MediatR;

namespace AydoganERP.Inventory.Application.StockMovementManager.Commands.Create;

public record CreateStockMovementCommand(
    Guid ProductId,
    DateOnly Date,
    int Type,
    decimal QuantityDelta,
    string? Description = null,
    string? ReferenceType = null,
    Guid? ReferenceId = null,
    string? ReferenceNo = null) : IRequest<StockMovementDto>;

public class CreateStockMovementCommandHandler : IRequestHandler<CreateStockMovementCommand, StockMovementDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public CreateStockMovementCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<StockMovementDto> Handle(CreateStockMovementCommand request, CancellationToken cancellationToken)
    {
        var movement = StockMovement.Create(
            Guid.NewGuid(),
            request.ProductId,
            request.Date,
            request.Type,
            request.QuantityDelta,
            request.Description,
            request.ReferenceType,
            request.ReferenceId,
            request.ReferenceNo);

        await _baseDbContext.StockMovements.AddAsync(movement, cancellationToken);
        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<StockMovementDto>(movement);
    }
}
