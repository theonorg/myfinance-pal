namespace Tiberna.MyFinancePal.AssetsService.API.Services.DTO;


public class AccountDTO
{
    public AccountDTO()
    {
        Name = string.Empty;
        IsActive = true;
        InitialBalance = 0m;
        InitialBalanceDate = default(DateTime);
    }

    public AccountDTO(Account acc) {
        this.Id = acc.Id;
        this.Name = acc.Name;
        this.BankAccountId = acc.BankAccountId;
        this.IsActive = acc.IsActive;
        this.Description = acc.Description;
        this.InitialBalance = acc.InitialBalance;
        this.ActualBalance = acc.ActualBalance;
        this.InitialBalanceDate = acc.InitialBalanceDate;
    }

    public Account ToAccount() {
        return new Account() {
            Id = this.Id,
            Name = this.Name,
            BankAccountId = this.BankAccountId,
            IsActive = this.IsActive,
            Description = this.Description,
            InitialBalance = this.InitialBalance,
            InitialBalanceDate = DateTime.SpecifyKind(this.InitialBalanceDate, DateTimeKind.Utc),
            ActualBalance = this.ActualBalance
        };
    }

    public void CopyTo(Account account) {
        account.Name = this.Name;
        account.BankAccountId = this.BankAccountId;
        account.IsActive = this.IsActive;
        account.Description = this.Description;
        account.InitialBalance = this.InitialBalance;
        account.InitialBalanceDate = this.InitialBalanceDate;
        account.ActualBalance = this.ActualBalance;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? BankAccountId { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public DateTime InitialBalanceDate { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal ActualBalance { get; set; }
}

