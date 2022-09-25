using MyFinancePal.Pages;

namespace Tiberna.MyFinancePal.Web.Models;


public class AccountDTO
{
    public AccountDTO()
    {
        Name = string.Empty;
        IsActive = true;
        InitialBalance = 0m;
        InitialBalanceDate = default(DateTime);
        CurrencyId = 1;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? BankAccountId { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public DateTime InitialBalanceDate { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal ActualBalance { get; set; }
    public int CurrencyId { get; set; }

}

