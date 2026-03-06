using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class CompanyBankAccountConfiguration : IEntityTypeConfiguration<CompanyBankAccount>
{
    public void Configure(EntityTypeBuilder<CompanyBankAccount> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("CompanyBankAccounts");

        builder.Property(x => x.CompanyId)
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.BankName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.BranchName)
            .HasMaxLength(100);

        builder.Property(x => x.AccountNo)
            .HasMaxLength(50);

        builder.Property(x => x.AccountName)
            .HasMaxLength(200);

        builder.Property(x => x.Iban)
            .IsRequired()
            .HasMaxLength(34);

        builder.Property(x => x.SwiftCode)
            .HasMaxLength(11);

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Unique index: CompanyId + Iban
        builder.HasIndex(x => new { x.CompanyId, x.Iban })
            .IsUnique();
    }
}
