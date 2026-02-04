using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Identity.Application.Repositories;
using MediatR;

namespace AydoganERP.Identity.Application.UserManager.Commands.UpdateRefreshToken;

public record UpdateRefreshTokenCommand(string ApiKey, string RefreshToken) : IRequest;

public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public UpdateRefreshTokenCommandHandler(IUserRepository userRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _userRepository = userRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetAsync(x => x.ApiKey == request.ApiKey);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.ApiKey);
        }

        entity.UpdateRefreshToken(request.RefreshToken);

        await _userRepository.UpdateAsync(entity, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

    }
}
