namespace Tiberna.MyFinancePal.Libs.Nordigen.Net.Responses;

public class Transaction
{
    public string? TransactionId { get; set;}
    public TransactionAmount? TransactionAmount { get; set;}
    public DateTime? BookingDate { get; set;}
    public DateTime? ValueDate { get; set;}
    public string? RemittanceInformationUnstructured { get; set;}
}

