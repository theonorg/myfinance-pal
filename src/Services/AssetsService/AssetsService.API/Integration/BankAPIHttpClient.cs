namespace Tiberna.MyFinancePal.AssetsService.API.Integration;

public class BankAPIHttpClient
{
    private HttpClient _httpClient;
    private const string SYNC_URL = "/bank/sync/";
    public BankAPIHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BankAPITransaction>?> SyncAccount(string accountId)
    {
        return await _httpClient.GetFromJsonAsync<List<BankAPITransaction>>($"{SYNC_URL}/{accountId}");
    }

}

