namespace Tiberna.MyFinancePal.AssetsService.API.Infrastructure.Configuration;
public class IntegrationConfiguration
{
    public const string SectionName = "Integration";
    public string BankAPI { get; set; } = "https://ob.nordigen.com/";
}
