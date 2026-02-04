using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(s => s.City)
                    .WithMany(ad => ad.Districts)
                    .HasForeignKey(s => s.CityId);

    }
}
