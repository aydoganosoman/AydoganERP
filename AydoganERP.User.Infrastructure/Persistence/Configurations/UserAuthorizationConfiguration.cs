using AydoganERP.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.User.Infrastructure.Persistence.Configurations;

public class UserAuthorizationConfiguration : IEntityTypeConfiguration<UserAuthorization>
{
    public void Configure(EntityTypeBuilder<UserAuthorization> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
