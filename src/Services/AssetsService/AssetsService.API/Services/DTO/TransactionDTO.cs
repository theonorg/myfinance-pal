namespace Tiberna.MyFinancePal.AssetsService.API.Services.DTO;


public class TransactionDTO
{
    public TransactionDTO()
    {
        Description = string.Empty;
    }

    public TransactionDTO(Transaction transaction) {
        this.Id = transaction.Id;
        this.Date = transaction.Date;
        this.Amount = transaction.Amount;
        this.Description = transaction.Description;
        this.CurrencyId = transaction.CurrencyId;
        this.CurrencyCode = transaction.Currency?.Code;
        this.AccountId = transaction.AccountId;
        this.AccountName = transaction.Account?.Name;
        this.IsEditable = !string.IsNullOrEmpty(transaction.BankTransactionId);
    }

    public Transaction ToTransaction() {
        return new Transaction() {
            Id = this.Id,
            AccountId = this.AccountId,
            CurrencyId = this.CurrencyId,
            Date = this.Date,
            Amount = this.Amount,
            Description = this.Description
        };
    }

    public void CopyTo(Transaction transaction) {
        transaction.Date = this.Date;
        transaction.Amount = this.Amount;
        transaction.Description = this.Description;
        transaction.CurrencyId = this.CurrencyId;
        transaction.AccountId = this.AccountId;
    }

    public long Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public int CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public int AccountId { get; set; }
    public string? AccountName { get; set; }
    public bool IsEditable { get; set; }
}

