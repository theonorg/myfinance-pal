
namespace Tiberna.MyFinancePal.Web.Services.Configuration;
public class ServicesConfiguration
{
    public const string SectionName = "Services";
    public ServiceConfig? AssetsService { get; set; }
    public ServiceConfig? CurrencyService { get; set; }
}

public class ServiceConfig
{
    public string? URI { get; set; }
}