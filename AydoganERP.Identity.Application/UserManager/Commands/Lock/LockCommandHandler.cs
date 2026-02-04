using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Identity.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Identity.Application.UserManager.Commands.Lock;

public record LockCommand(Guid Id) : IRequest;

public class LockCommandHandler : IRequestHandler<LockCommand>
{
    private readonly IUserRepository _userRepository;
    public LockCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(LockCommand request, CancellationToken cancellationToken)
    {
        var dbContext = _userRepository.GetDbContext();
        
        User currentUser = await dbContext
            .Set<User>()
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (currentUser == null)
            throw new Exception("User not found!");

        currentUser.Lock();

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
