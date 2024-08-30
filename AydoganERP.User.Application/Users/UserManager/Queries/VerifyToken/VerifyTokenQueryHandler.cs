using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.User.Application.Common.Repositories;
using AydoganERP.User.Domain.Entities;
using AydoganERP.User.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.User.Application.Users.UserManager.Queries.VerifyToken;

public class VerifyTokenQuery : IRequest<UserAuthModel>
{
    public string RefreshToken { get; set; }
}

public class VerifyTokenQueryHandler : IRequestHandler<VerifyTokenQuery, UserAuthModel>
{
    private readonly IUserToRoleRepository _userToRoleRepository;
    private readonly ICurrentUserService _currentUserService;
    public VerifyTokenQueryHandler(IUserToRoleRepository userToRoleRepository, ICurrentUserService currentUserService)
    {
        _userToRoleRepository = userToRoleRepository;
        _currentUserService = currentUserService;
    }

    public async Task<UserAuthModel> Handle(VerifyTokenQuery request, CancellationToken cancellationToken)
    {
        IPaginate<UserToRole> userToRoles = await _userToRoleRepository
             .GetListAsync(predicate: x => x.User.ApiKey == _currentUserService.UserApiKey, include: (x) => x.Include(b => b.User).Include(b => b.Company).Include(b => b.UserAuthorizations));

        var res2 = userToRoles.Items
            .ToLookup(x => x.User, x => x.Company)
            .FirstOrDefault();

        res2.Key.TokenIsValid(request.RefreshToken);

        UserAuthModel user = null;
        if (res2 != null && res2.Key != null)
        {
            user = new UserAuthModel();
            user.Id = res2.Key.Id;
            user.Name = res2.Key.Name;
            user.Title = res2.Key.Title;
            user.EMail = res2.Key.EMail;
            user.ApiKey = res2.Key.ApiKey;
            user.ProfilePicture = res2.Key.ProfilePicture;
            user.Verification = res2.Key.Verification;
            user.IsEnable = res2.Key.IsEnable;

            foreach (var companyItem in res2.ToList())
            {
                CompanyAuthModel company = new CompanyAuthModel { Id = companyItem.Id, Name = companyItem.Name };

                user.Companies.Add(company);

                var userToRole = companyItem.AccessibleUser
                     .FirstOrDefault(x => x.User.Id == res2.Key.Id);

                foreach (var authItem in userToRole.UserAuthorizations)
                {
                    company.CompanyUserAuthorizations.Add(new CompanyUserAuthModel()
                    {
                        Module = (int)authItem.Module,
                        AuthorizationTransactionType = (int)authItem.AuthorizationTransactionType
                    });
                }
            }
        }
        else
        {
            throw new UserNotFoundException("Kullanıcı bulunamadı.");
        }

        return user;
    }
}
