namespace Tiberna.MyFinancePal.BankIntegration.API.Nordigen;

public class NordigenApiOptions
{
    public const string SectionName = "NordigenAPI";
    public string Url { get; set; } = "https://ob.nordigen.com/";
}