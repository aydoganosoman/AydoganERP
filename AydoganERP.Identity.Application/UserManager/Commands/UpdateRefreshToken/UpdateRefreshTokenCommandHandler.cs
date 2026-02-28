using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Identity.Application.UserManager.Commands.UpdateRefreshToken;

public record UpdateRefreshTokenCommand(string ApiKey, string RefreshToken) : IRequest;

public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public UpdateRefreshTokenCommandHandler(IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var entity = await _baseDbContext
            .Users
            .FirstOrDefaultAsync(x => x.ApiKey == request.ApiKey);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.ApiKey);
        }

        entity.UpdateRefreshToken(request.RefreshToken);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

    }
}
