using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Identity.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Identity.Application.UserManager.Commands.Active;

public record ActiveCommand(Guid Id) : IRequest;

public class ActiveCommandHandler : IRequestHandler<ActiveCommand>
{
    private readonly IUserRepository _userRepository;
    public ActiveCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(ActiveCommand request, CancellationToken cancellationToken)
    {
        var dbContext = _userRepository.GetDbContext();
        
        User currentUser = await dbContext
            .Set<User>()
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (currentUser == null)
            throw new Exception("User not found!");

        currentUser.Activate();

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
