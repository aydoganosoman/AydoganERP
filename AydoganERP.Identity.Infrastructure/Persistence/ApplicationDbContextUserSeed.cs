using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Base.Domain.Modules.IdentityModule.Enums;
using AydoganERP.Base.Infrastructure.Persistence;

namespace AydoganERP.Identity.Infrastructure.Persistence;

public class ApplicationDbContextUserSeed
{
    public static async Task SeedDefaultValuesAsync(IMD5Helper md5Helper,
        IGeneratePasswordUtil generatePasswordUtil,
        ApplicationDbContext context)
    {
        if (!context.Users.Any(x => x.Email == "stronger_osman@hotmail.com"))
        {
            User _user = User.Register(null,
                generatePasswordUtil,
                UserRoleEnum.SuperAdmin,
                string.Empty,
                "developer@mail.com",
                "<<12111211>>",
                md5Helper.GenerateMD5($"<<12111211>>"));

            context.Users.Add(_user);
        }

        await context.SaveChangesAsync();
    }
}