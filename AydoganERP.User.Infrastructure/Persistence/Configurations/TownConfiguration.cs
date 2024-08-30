using AydoganERP.Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.User.Infrastructure.Persistence.Configurations;

public class TownConfiguration : IEntityTypeConfiguration<Town>
{
    public void Configure(EntityTypeBuilder<Town> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(s => s.City)
                    .WithMany(ad => ad.Towns);

    }
}
