using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Identity.Application.UserManager.Commands.Active;

public record ActiveCommand(Guid Id) : IRequest;

public class ActiveCommandHandler : IRequestHandler<ActiveCommand>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;

    public ActiveCommandHandler(IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(ActiveCommand request, CancellationToken cancellationToken)
    {
        User currentUser = await _baseDbContext
            .Users
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (currentUser == null)
            throw new Exception("User not found!");

        currentUser.Activate();

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);
    }
}