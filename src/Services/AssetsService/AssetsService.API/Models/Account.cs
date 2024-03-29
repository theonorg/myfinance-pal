namespace Tiberna.MyFinancePal.AssetsService.API.Models;

public class Account
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? BankAccountId { get; set; }
    public bool IsActive { get; set; } = true;
    public int AccountCurrencyCurrencyId { get; set; }
    public Currency? AccountCurrency { get; set; }
    public string? Description { get; set; }
    public DateTime InitialBalanceDate { get; set; }
    public decimal InitialBalance { get; set; }
    public List<Transaction>? Transactions { get; set; }
}

