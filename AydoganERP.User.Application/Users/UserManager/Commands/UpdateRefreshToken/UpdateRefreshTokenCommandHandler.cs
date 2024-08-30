using AydoganERP.Base.Application.Exceptions;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.User.Application.Common.Repositories;
using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Commands.UpdateRefreshToken;

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

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

    }
}
