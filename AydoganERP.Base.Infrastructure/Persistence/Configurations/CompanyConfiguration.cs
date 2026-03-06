using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.CompanyModule.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Companies");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(CompanyStatusEnum.Active);

        // Firma Bilgileri
        builder.Property(x => x.CompanyType)
            .IsRequired()
            .HasDefaultValue(CompanyTypeEnum.Corporate);

        builder.Property(x => x.ShortName)
            .HasMaxLength(200);

        builder.Property(x => x.TradeRegisterNo)
            .HasMaxLength(50);

        builder.Property(x => x.TradeRegisterTitle)
            .HasMaxLength(300);

        builder.Property(x => x.MersisNo)
            .HasMaxLength(20);

        builder.Property(x => x.TapdkNo)
            .HasMaxLength(50);

        builder.Property(x => x.HeadquartersAddress)
            .HasMaxLength(500);

        builder.Property(x => x.Currency)
            .HasDefaultValue(0);

        builder.Property(x => x.Capital)
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.Property(x => x.EstablishmentDate);

        // ValueObjects
        builder.OwnsOne(x => x.TaxInfo, taxInfo =>
        {
            taxInfo.Property(t => t.TaxNumber).HasMaxLength(20).HasColumnName("TaxNumber");
            taxInfo.Property(t => t.TaxOffice).HasMaxLength(100).HasColumnName("TaxOffice");
        });

        builder.OwnsOne(x => x.Contact, contact =>
        {
            contact.Property(c => c.Email).HasMaxLength(200).HasColumnName("Email");
            contact.Property(c => c.Phone).HasMaxLength(30).HasColumnName("Phone");
            contact.Property(c => c.Fax).HasMaxLength(30).HasColumnName("Fax");
            contact.Property(c => c.Website).HasMaxLength(200).HasColumnName("Website");
        });

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(a => a.Country).HasColumnName("CountryId");
            address.Property(a => a.City).HasColumnName("CityId");
            address.Property(a => a.District).HasColumnName("DistrictId");
            address.Property(a => a.Line).HasMaxLength(500).HasColumnName("AddressLine");
        });
        builder.Navigation(x => x.Address).IsRequired(false);
    }
}
