namespace Tiberna.MyFinancePal.AssetsService.API.Models;

public class ExchangeRate
{
    public long Id { get; set; }
    public int SourceCurrencyId { get; set; }
    public Currency? Source { get; set; }
    public int TargetCurrencyId { get; set; }
    public Currency? Target { get; set; }
    public DateTime ExchangeDate { get; set; }
    public decimal Rate { get; set; }
}

