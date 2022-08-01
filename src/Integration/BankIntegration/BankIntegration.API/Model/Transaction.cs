namespace Tiberna.MyFinancePal.BankIntegration.API.Model;

public class BankTransaction
{
    public string? Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Currency { get; set; }

    public static BankTransaction FromNordigen(Transaction nordigenTransaction)
    {
        BankTransaction newBankTransaction = new BankTransaction();
        newBankTransaction.Id = nordigenTransaction.TransactionId;
        newBankTransaction.Date = DateTime.SpecifyKind(nordigenTransaction.ValueDate!.Value, DateTimeKind.Utc);
        newBankTransaction.Amount = Convert.ToDecimal(nordigenTransaction.TransactionAmount!.Amount);
        newBankTransaction.Description = nordigenTransaction.RemittanceInformationUnstructured;
        newBankTransaction.Currency = nordigenTransaction.TransactionAmount.Currency;

        return newBankTransaction;
    }
}

