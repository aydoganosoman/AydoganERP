using AydoganERP.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.User.Infrastructure.Persistence.Configurations;

public class CompanyUserAuthorizationConfiguration : IEntityTypeConfiguration<CompanyUserAuthorization>
{
    public void Configure(EntityTypeBuilder<CompanyUserAuthorization> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
