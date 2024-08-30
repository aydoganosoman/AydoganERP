using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Persistence;

namespace AydoganERP.User.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    public static async Task SeedDefaultValuesAsync(IDomainEventUnitOfWork domainEventUnitOfWork, ApplicationDbContext context)
    {
        await BaseDbContextSeed.BaseSeedDefaultValuesAsync(domainEventUnitOfWork, context);

        await context.SaveChangesAsync();

        //if (!context.Users.Any(x => x.EMail == "stronger_osman@hotmail.com"))
        //{
        //    User.Domain.Entities.User spAdmin = User.Domain.Entities.User.Create("Super Admin",
        //        "stronger_osman@hotmail.com",
        //        "b19fa5231ea031ffa7f87ca2697ddc58",
        //        Domain.Enums.RoleEnum.SuperAdmin);

        //    spAdmin.AddAuthorization(Domain.Enums.AuthorizationTransactionTypeEnum.Full, PermissionEnum.AccessAlkdl);

        //    await context.Users.AddAsync(spAdmin);

        //    await context.SaveChangesAsync();
        //}
    }
}
