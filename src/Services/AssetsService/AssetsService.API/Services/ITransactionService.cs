namespace Tiberna.MyFinancePal.AssetsService.API.Services;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDTO>> GetAllTransactionAsync();
    Task<TransactionDTO> GetTransactionByIdAsync(int id);
    Task<IEnumerable<TransactionDTO>> GetTransactionsByAccountIdAsync(int accountId);
    Task<TransactionDTO> CreateTransactionAsync(TransactionDTO newTransaction);
    Task<TransactionDTO> UpdateTransactionAsync(TransactionDTO transaction);
    Task<TransactionDTO> DeleteTransactionAsync(int id);
    Task<int> SyncTransactionsfromBankAsync(int accountId, List<BankAPITransaction> bankTransactions);
}