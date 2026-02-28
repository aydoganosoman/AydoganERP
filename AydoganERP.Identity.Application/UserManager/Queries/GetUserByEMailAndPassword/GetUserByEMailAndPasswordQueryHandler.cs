using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.IdentityModule.Enums;
using AydoganERP.Identity.Application.Models;
using AydoganERP.Identity.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Identity.Application.UserManager.Queries.GetUserByEMailAndPassword;

public record GetUserByEMailAndPasswordQuery(string Email, string Password) : IRequest<UserAuthModel>;

public class GetUserByEMailAndPasswordQueryHandler : IRequestHandler<GetUserByEMailAndPasswordQuery, UserAuthModel>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMD5Helper _md5Helper;

    public GetUserByEMailAndPasswordQueryHandler(IBaseDbContext baseDbContext,
        IMD5Helper md5Helper)
    {
        _baseDbContext = baseDbContext;
        _md5Helper = md5Helper;
    }

    public async Task<UserAuthModel> Handle(GetUserByEMailAndPasswordQuery request, CancellationToken cancellationToken)
    {
        var generatedPasswordSalted = $"<<{request.Password}>>";

        var generatedPasswordHashed = _md5Helper
            .GenerateMD5(generatedPasswordSalted);

        var userEntity = await _baseDbContext
            .Users
            .Include(x => x.Company)
            .FirstOrDefaultAsync(x =>
                x.Email == request.Email && x.Password == generatedPasswordSalted &&
                x.HashPassword == generatedPasswordHashed && x.Status == UserStatusEnum.Active);

        UserAuthModel user = null;
        if (userEntity != null)
        {
            user = new UserAuthModel(userEntity.Id,
                userEntity.Role,
                userEntity.CompanyId.HasValue ? userEntity.CompanyId.Value : null,
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

        return user;
    }
}