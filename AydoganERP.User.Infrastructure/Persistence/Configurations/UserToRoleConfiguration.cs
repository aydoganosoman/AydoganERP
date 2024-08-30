using AydoganERP.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.User.Infrastructure.Persistence.Configurations;

public class UserToRoleConfiguration : IEntityTypeConfiguration<UserToRole>
{
    public void Configure(EntityTypeBuilder<UserToRole> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
