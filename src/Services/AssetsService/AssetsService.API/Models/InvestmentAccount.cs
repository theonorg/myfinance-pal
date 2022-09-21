namespace Tiberna.MyFinancePal.AssetsService.API.Models;

public class InvestmentAccount
{
    public int Id { get; set; }
    public Guid AccountId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int InvestmentTypeId { get; set; }
    public InvestmentAccountType? InvestmentType { get; set; }
    public int AccountCurrencyId { get; set; }
    public Currency? AccountCurrency { get; set; }
    public DateTime InitialBalanceDate { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal InvestedBalance { get; set; }
    public decimal AvailableBalance { get; set; }
    public List<InvestmentTransaction>? InvestmentTransactions { get; set; }
}