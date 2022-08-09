using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tiberna.MyFinancePal.AssetsService.API.Models.Configuration;

public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(acc => acc.Id);
        builder.HasIndex(acc => acc.Name).IsUnique(true);
        builder.HasIndex(acc => acc.BankAccountId)
            .IsUnique(true)
            .HasFilter("bank_account_id IS NOT NULL");

        builder.Property(acc => acc.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(acc => acc.BankAccountId)
            .HasColumnName("bank_account_id")
            .HasMaxLength(128);

        builder.Property(acc => acc.IsActive)
            .HasDefaultValue(false);  

        builder.Property(acc => acc.Description)
            .HasMaxLength(500);

        builder.Property(acc => acc.InitialBalance)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(acc => acc.InitialBalanceDate)
            .IsRequired();
    }
}
