
namespace Tiberna.MyFinancePal.AssetsService.API.Models;

public class InvestmentTransaction
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public Currency? Currency { get; set; }
    public int CurrencyId { get; set; }
    public InvestmentAccount? InvestmentAccount { get; set; }
    public int InvestmentAccountId { get; set; }
    public Transaction? Transaction { get; set; }
    public int? TransactionId { get; set; }
}

