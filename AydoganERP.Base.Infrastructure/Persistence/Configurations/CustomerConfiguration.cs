using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Customers");

        builder.Property(x => x.CompanyId)
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.CustomerName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Property(x => x.SurName)
            .HasMaxLength(100);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(CustomerStatusEnum.Active);
        
        builder.OwnsOne(x => x.TaxInfo, taxInfo =>
        {
            taxInfo.Property(t => t.TaxNumber).HasMaxLength(50).HasColumnName("TaxNumber");
            taxInfo.Property(t => t.TaxOffice).HasMaxLength(100).HasColumnName("TaxOffice");
        });

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
