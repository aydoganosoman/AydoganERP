using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Customer.Infrastructure.Persistence.Configurations;

public class CustomerNoteConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.CustomerModule.Entities.CustomerNote>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.CustomerModule.Entities.CustomerNote> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
