using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tiberna.MyFinancePal.AssetsService.API.Models.Configuration;

public class InvestmentAccountEntityTypeConfiguration : IEntityTypeConfiguration<InvestmentAccount>
{
    public void Configure(EntityTypeBuilder<InvestmentAccount> builder)
    {
        builder.HasKey(acc => acc.Id);
        builder.HasIndex(acc => acc.Name).IsUnique(true);

        builder.Property(acc => acc.Name)
            .IsRequired()
            .HasMaxLength(200);

        // builder.Property(acc => acc.InvestmentType)
        //     .IsRequired();  

        builder.Property(acc => acc.Description)
            .HasMaxLength(500);

        builder.Property(acc => acc.InitialBalance)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(acc => acc.InitialBalanceDate)
            .IsRequired();

        builder.Property(acc => acc.InvestedBalance)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(acc => acc.AvailableBalance)
            .IsRequired()
            .HasPrecision(10, 2)
            .HasDefaultValue(0);
    }
}
