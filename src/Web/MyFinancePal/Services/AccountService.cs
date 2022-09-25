using System.Net.Http.Json;
using Tiberna.MyFinancePal.Web.Models;

namespace Tiberna.MyFinancePal.Web.Services;


public class AccountService : IAccountService
{
    private readonly HttpClient httpClient;

    private const string AccountEndpoint = "/account";

    public AccountService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<AccountDTO> CreateAccountAsync(AccountDTO account)
    {
        var newAccount = new AccountDTO();
        var response = await httpClient.PostAsJsonAsync<AccountDTO>(AccountEndpoint, account);
        if (response.IsSuccessStatusCode)
        {
            newAccount = await response.Content.ReadFromJsonAsync<AccountDTO>();
        }
        return newAccount ?? new AccountDTO();
    }

    public async Task<AccountDTO> DeleteAccountAsync(int id)
    {
        var account = new AccountDTO();
        var response = await httpClient.DeleteAsync($"{AccountEndpoint}/{id}");
        if (response.IsSuccessStatusCode)
        {
            account = await response.Content.ReadFromJsonAsync<AccountDTO>();
        }
        return account ?? new AccountDTO();
    }

    public async Task<AccountDTO> GetAccountAsync(int id)
    {
        var account = await httpClient.GetFromJsonAsync<AccountDTO>($"{AccountEndpoint}/{id}");
        return account ?? new AccountDTO();
    }

    public Task<AccountDTO> GetAccountByBankAccountIdAsync(string bankAccountId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAccountsAsync()
    {
        var accounts = await httpClient.GetFromJsonAsync<List<AccountDTO>>(AccountEndpoint);
        return accounts ?? new List<AccountDTO>();
    }

    public async Task<AccountDTO> UpdateAccountAsync(int id, AccountDTO account)
    {
        var updatedAccount = new AccountDTO();
        var response = await httpClient.PutAsJsonAsync<AccountDTO>($"{AccountEndpoint}/{id}", account);
        if (response.IsSuccessStatusCode)
        {
            updatedAccount = await response.Content.ReadFromJsonAsync<AccountDTO>();
        }
        return updatedAccount ?? new AccountDTO();
    }
}