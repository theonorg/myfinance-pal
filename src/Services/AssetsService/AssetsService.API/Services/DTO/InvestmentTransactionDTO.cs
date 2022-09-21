namespace Tiberna.MyFinancePal.AssetsService.API.Services.DTO;


public class InvestmentTransactionDTO
{
    public InvestmentTransactionDTO()
    {
        Description = string.Empty;
    }

    public InvestmentTransactionDTO(InvestmentTransaction transaction) {
        this.Id = transaction.Id;
        this.Date = transaction.Date;
        this.Amount = transaction.Amount;
        this.Description = transaction.Description;
        this.CurrencyId = transaction.CurrencyId;
        this.CurrencyCode = transaction.Currency?.Code;
        this.InvestmentAccountId = transaction.InvestmentAccountId;
        this.AccountName = transaction.InvestmentAccount?.Name;
        this.IsEditable = transaction.Transaction is null;
    }

    public InvestmentTransaction ToInvestmentTransaction() {
        return new InvestmentTransaction() {
            Id = this.Id,
            InvestmentAccountId = this.InvestmentAccountId,
            CurrencyId = this.CurrencyId,
            Date = this.Date,
            Amount = this.Amount,
            Description = this.Description,
            TransactionId = this.TransactionId
        };
    }

    public void CopyTo(InvestmentTransaction transaction) {
        transaction.Date = this.Date;
        transaction.Amount = this.Amount;
        transaction.Description = this.Description;
        transaction.CurrencyId = this.CurrencyId;
        transaction.InvestmentAccountId = this.InvestmentAccountId;
    }

    public long Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public int CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public int InvestmentAccountId { get; set; }
    public string? AccountName { get; set; }
    public int? TransactionId { get; set; }
    public bool IsEditable { get; set; }
}

