using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tiberna.MyFinancePal.AssetsService.API.Models.Configuration;

public class InvestmentAccountTypeEntityTypeConfiguration : IEntityTypeConfiguration<InvestmentAccountType>
{
    public void Configure(EntityTypeBuilder<InvestmentAccountType> builder)
    {
        builder.HasKey(acc => acc.Id);
        builder.HasIndex(acc => acc.Name).IsUnique(true);

        builder.Property(acc => acc.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasData(
            new { Id = 1, Name = "Stocks", Code = "STOCKS" },
            new { Id = 2, Name = "Cryptocurrency", Code = "CRYPTO" },
            new { Id = 3, Name = "Retirement Plans", Code = "PPR"  },
            new { Id = 4, Name = "Savings", Code = "SAVE" },
            new { Id = 5, Name = "Real Estate", Code = "REAL" },
            new { Id = 6, Name = "Collections", Code = "COLLECT" }
        );

    }
}
