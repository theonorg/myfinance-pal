using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tiberna.MyFinancePal.AssetsService.API.Models.Configuration;

public class ExchangeRateEntityTypeConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(exr => exr.Id);
        builder.HasIndex(exr => new { exr.SourceCurrencyId, exr.TargetCurrencyId, exr.ExchangeDate }).IsUnique(true);

        builder.Property(exr => exr.SourceCurrencyId)
            .IsRequired();

        builder.Property(exr => exr.TargetCurrencyId)
            .IsRequired();

        builder.Property(exr => exr.Rate)
            .IsRequired()
            .HasPrecision(12, 6);

        builder.Property(exr => exr.ExchangeDate)
            .IsRequired();
    }
}
