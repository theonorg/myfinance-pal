using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tiberna.MyFinancePal.AssetsService.API.Models.Configuration;

public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(acc => acc.Id);

        builder.HasIndex(acc => acc.BankTransactionId)
            .IsUnique(true)
            .HasFilter("bank_transaction_id IS NOT NULL");

        builder.Property(acc => acc.Date)
            .IsRequired();

        builder.Property(acc => acc.CurrencyId)
            .IsRequired();

        builder.Property(acc => acc.AccountId)
            .IsRequired();

        builder.Property(acc => acc.Description)
            .HasMaxLength(500);

        builder.Property(acc => acc.Amount)
            .IsRequired()
            .HasPrecision(10, 2);
    }
}
