using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tiberna.MyFinancePal.AssetsService.API.Models.Configuration;

public class InvestmentTransactionEntityTypeConfiguration : IEntityTypeConfiguration<InvestmentTransaction>
{
    public void Configure(EntityTypeBuilder<InvestmentTransaction> builder)
    {
        builder.HasKey(acc => acc.Id);

        builder.Property(acc => acc.Date)
            .IsRequired();

        builder.Property(acc => acc.CurrencyId)
            .IsRequired();

        builder.Property(acc => acc.InvestmentAccountId)
            .IsRequired();

        builder.Property(acc => acc.Description)
            .HasMaxLength(500);

        builder.Property(acc => acc.Amount)
            .IsRequired()
            .HasPrecision(10, 2);

    }
}
