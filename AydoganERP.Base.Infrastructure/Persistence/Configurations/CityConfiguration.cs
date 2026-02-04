using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(s => s.Country)
            .WithMany(ad => ad.Cities)
            .HasForeignKey(s => s.CountryId);
    }
}
