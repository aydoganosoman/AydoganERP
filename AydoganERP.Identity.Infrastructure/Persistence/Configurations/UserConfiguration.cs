using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Identity.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(a => a.Company)
            .WithMany(b => b.Users)
            .HasForeignKey(b => b.CompanyId)
            .IsRequired(false);
    }
}
