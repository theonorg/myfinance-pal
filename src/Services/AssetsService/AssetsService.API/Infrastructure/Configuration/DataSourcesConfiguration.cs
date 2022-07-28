namespace Tiberna.MyFinancePal.AssetsService.API.Infrastructure.Configuration;
public class DataSourcesConfiguration
{
    public const string SectionName = "Databases";
    public DbConfiguration? AssetsDB { get; set; }
}

public class DbConfiguration
{
    public string? ConnectionString { get; set; }
    public bool ForceInit { get; set; } = false;
}
