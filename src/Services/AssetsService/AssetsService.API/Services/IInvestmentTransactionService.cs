namespace Tiberna.MyFinancePal.AssetsService.API.Services;

public interface IInvestmentTransactionService
{
    Task<IEnumerable<InvestmentTransactionDTO>> GetAllAsync();
    Task<InvestmentTransactionDTO> GetByIdAsync(int id);
    Task<IEnumerable<InvestmentTransactionDTO>> GetByAccountIdAsync(int accountId);
    Task<InvestmentTransactionDTO> CreateAsync(InvestmentTransactionDTO newTransaction);
    Task<InvestmentTransactionDTO> UpdateAsync(InvestmentTransactionDTO transaction);
    Task<InvestmentTransactionDTO> DeleteAsync(int id);
}