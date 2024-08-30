using AydoganERP.Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Customer.Infrastructure.Persistence.Configurations;

public class AuthorizePersonConfiguration : IEntityTypeConfiguration<AuthorizePerson>
{
    public void Configure(EntityTypeBuilder<AuthorizePerson> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
