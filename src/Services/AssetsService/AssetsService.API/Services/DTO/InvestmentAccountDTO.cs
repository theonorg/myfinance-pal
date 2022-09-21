namespace Tiberna.MyFinancePal.AssetsService.API.Services.DTO;

public class InvestmentAccountDTO
{
    public InvestmentAccountDTO()
    {
        Name = string.Empty;
        InitialBalance = 0m;
        InitialBalanceDate = default(DateTime);
        InvestedBalance = 0m;
        AvailableBalance = 0m;
    }

    public InvestmentAccountDTO(InvestmentAccount acc) {
        this.Id = acc.Id;
        this.Name = acc.Name;
        this.Description = acc.Description;
        this.InitialBalance = acc.InitialBalance;
        this.InitialBalanceDate = acc.InitialBalanceDate;
        this.InvestedBalance = acc.InvestedBalance;
        this.AvailableBalance = acc.AvailableBalance;
        this.TypeId = acc.InvestmentTypeId;
        this.TypeName = acc.InvestmentType!.Name;
        this.CurrencyId = acc.AccountCurrencyId;
        this.CurrencyCode = acc.AccountCurrency!.Code;
        this.AccountId = acc.AccountId;
    }

    public InvestmentAccount ToInvestmentAccount() {
        return new InvestmentAccount() {
            Id = this.Id,
            Name = this.Name,
            Description = this.Description,
            InitialBalance = this.InitialBalance,
            InitialBalanceDate = this.InitialBalanceDate,
            InvestedBalance = this.InvestedBalance,
            AvailableBalance = this.AvailableBalance,
            InvestmentTypeId = this.TypeId,
            AccountCurrencyId = this.CurrencyId,
            AccountId = this.AccountId
        };
    }

    public void CopyTo(InvestmentAccount account) {
        account.Name = this.Name;
        account.Description = this.Description;
        account.InitialBalance = this.InitialBalance;
        account.InitialBalanceDate = this.InitialBalanceDate;
        account.InvestedBalance = this.InvestedBalance;
        account.AvailableBalance = this.AvailableBalance;
        account.InvestmentTypeId = this.TypeId;
        account.AccountCurrencyId = this.CurrencyId;
        account.AccountId = this.AccountId;

    }

    public int Id { get; set; }
    public Guid AccountId { get; set; }
    public string? Name { get; set; }
    public int TypeId { get; set; }
    public string? TypeName { get; set; }
    public int CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public string? Description { get; set; }
    public DateTime InitialBalanceDate { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal InvestedBalance { get; set; }
    public decimal AvailableBalance { get; set; }
}

