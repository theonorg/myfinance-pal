namespace Tiberna.MyFinancePal.AssetsService.API.Integration.Models;

public class BankAPITransaction
{
    public string? Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Currency { get; set; }
}

