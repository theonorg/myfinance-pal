using System.Net.Http.Json;
using Tiberna.MyFinancePal.Web.Models;

namespace Tiberna.MyFinancePal.Web.Services;


public class AssetsService : IAssetsService
{
    private readonly HttpClient httpClient;

    private const string AssetsEndpoint = "/account";

    public AssetsService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public Task<AccountDTO> CreateAccountAsync(AccountDTO account)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDTO> DeleteAccountAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDTO> GetAccountAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDTO> GetAccountByBankAccountIdAsync(string bankAccountId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAccountsAsync()
    {
        var accounts = await httpClient.GetFromJsonAsync<List<AccountDTO>>(AssetsEndpoint);
        return accounts ?? new List<AccountDTO>();
    }

    public Task<AccountDTO> UpdateAccountAsync(AccountDTO account)
    {
        throw new NotImplementedException();
    }
}