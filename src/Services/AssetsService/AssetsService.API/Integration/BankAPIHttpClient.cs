namespace Tiberna.MyFinancePal.AssetsService.API.Integration;

public class BankAPIHttpClient
{
    private HttpClient _httpClient;
    private const string SYNC_URL = "/bank/sync";
    private readonly ILogger<BankAPIHttpClient> _logger;
    public BankAPIHttpClient(HttpClient httpClient, ILogger<BankAPIHttpClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<BankAPITransaction>?> SyncAccount(string accountId)
    {
        var url = $"{SYNC_URL}/{accountId}";
        _logger.LogInformation($"Syncing account {accountId} with bank API at {url}");
        return await _httpClient.GetFromJsonAsync<List<BankAPITransaction>>(url);
    }

}

