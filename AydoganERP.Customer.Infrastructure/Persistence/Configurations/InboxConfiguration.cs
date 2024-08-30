using AydoganERP.Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Customer.Infrastructure.Persistence.Configurations;

public class InboxConfiguration : IEntityTypeConfiguration<Inbox>
{
    public void Configure(EntityTypeBuilder<Inbox> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .OwnsOne(c => c.EntryId);

        //builder
        //    .ComplexProperty(e => e.EntryId);
    }
}
