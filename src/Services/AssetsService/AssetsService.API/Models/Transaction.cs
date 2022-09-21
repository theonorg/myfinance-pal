
namespace Tiberna.MyFinancePal.AssetsService.API.Models;

public class Transaction
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public Currency? Currency { get; set; }
    public int CurrencyId { get; set; }
    public Account? Account { get; set; }
    public int AccountId { get; set; }
    public string? BankTransactionId { get; set; }
    public bool IsIncome { get; set; }
}

