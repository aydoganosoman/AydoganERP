using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Base.Domain.Modules.IdentityModule.Enums;
using AydoganERP.Identity.Application.Models;
using AydoganERP.Identity.Application.Repositories;
using AydoganERP.Identity.Domain.Exceptions;
using AydoganERP.Identity.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Identity.Application.UserManager.Queries.VerifyToken;

public record VerifyTokenQuery(string RefreshToken) : IRequest<UserAuthModel>;

public class VerifyTokenQueryHandler : IRequestHandler<VerifyTokenQuery, UserAuthModel>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public VerifyTokenQueryHandler(IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<UserAuthModel> Handle(VerifyTokenQuery request, CancellationToken cancellationToken)
    {
        var dbContext = _userRepository.GetDbContext();
        
        var userEntity = await dbContext
            .Set<User>()
            .Include(x => x.Company)
            .FirstOrDefaultAsync(x => x.Email == _currentUserService.UserEmail && x.Status == UserStatusEnum.Active);

        UserAuthModel user = null;
        if (userEntity != null)
        {
            user = new UserAuthModel(userEntity.Id,
                userEntity.Role,
                userEntity.Name,
                userEntity.Title,
                userEntity.Email,
                userEntity.ApiKey,
                userEntity.Status);
        }
        else
        {
            throw new UserNotFoundException("Kullanıcı bulunamadı.");
        }
        
        userEntity.TokenIsValid(request.RefreshToken);

        return user;
    }
}