using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class CustomerBranchConfiguration : IEntityTypeConfiguration<CustomerBranch>
{
    public void Configure(EntityTypeBuilder<CustomerBranch> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("CustomerBranches");

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(x => x.Contact, contact =>
        {
            contact.Property(c => c.Email).HasMaxLength(200).HasColumnName("ContactEmail");
            contact.Property(c => c.Phone).HasMaxLength(30).HasColumnName("ContactPhone");
        });

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(a => a.Country).HasColumnName("CountryId");
            address.Property(a => a.City).HasColumnName("CityId");
            address.Property(a => a.District).HasColumnName("DistrictId");
            address.Property(a => a.Line).HasMaxLength(300).HasColumnName("AddressLine");
        });
        builder.Navigation(x => x.Address).IsRequired(false);
    }
}
