using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Identity.Application.UserManager.Commands.Lock;

public record LockCommand(Guid Id) : IRequest;

public class LockCommandHandler : IRequestHandler<LockCommand>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;

    public LockCommandHandler(IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(LockCommand request, CancellationToken cancellationToken)
    {
        User currentUser = await _baseDbContext
            .Users
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (currentUser == null)
            throw new Exception("User not found!");

        currentUser.Lock();

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);
    }
}