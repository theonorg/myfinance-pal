namespace Tiberna.MyFinancePal.AssetsService.API.Tests.Mock;

public class BankAPIHttpClientMock : IBankAPIHttpClient
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    public BankAPIHttpClientMock()
    {
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    public async Task<List<BankAPITransaction>?> SyncAccount(string accountId)
    {
        using var fs = new FileStream("Mock/response.json", FileMode.Open, FileAccess.Read);
        using var contentStream = new StreamReader(fs, Encoding.UTF8).BaseStream;

        return await JsonSerializer.DeserializeAsync<List<BankAPITransaction>>(contentStream, _jsonSerializerOptions);
    }
}

