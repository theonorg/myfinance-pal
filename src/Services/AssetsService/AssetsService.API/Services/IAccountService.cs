namespace Tiberna.MyFinancePal.AssetsService.API.Services;

public interface IAccountService
{
    Task<IEnumerable<AccountDTO>> GetAllAccountsAsync();
    Task<AccountDTO> GetAccountAsync(int id);
    Task<AccountDTO> GetAccountByBankAccountIdAsync(string bankAccountId);
    Task<AccountDTO> CreateAccountAsync(AccountDTO account);
    Task<AccountDTO> UpdateAccountAsync(AccountDTO account);
    Task<AccountDTO> DeleteAccountAsync(int id);
}