
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Tiberna.MyFinancePal.Libs.Nordigen.Net;
public class NordigenApi
{
    private readonly string _url;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public NordigenApi(IHttpClientFactory httpClientFactory, string url)
    {
        _httpClientFactory = httpClientFactory;
        _url = url.TrimEnd('/');
        _jsonSerializerOptions = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<TransactionsList> GetTransactions(string accountId, CancellationToken cancellationToken = default)
    {
        var token = await GetAccessToken(cancellationToken);

        var accountUrl = $"{Constants.AccountsUrl}{accountId}/transactions/";
        var httpClient = CreateHTTPClient(token);

        var message = await httpClient.GetAsync(accountUrl, cancellationToken);

        if (!message.IsSuccessStatusCode)
        {
            var error = await message.Content.ReadFromJsonAsync<Error>(_jsonSerializerOptions);
            throw new Exception(error!.ToString());
        }

        using var contentStream = message.Content.ReadAsStream();
        //using var fs = new FileStream("result.tmp", FileMode.Open, FileAccess.Read);
        //using var contentStream = new StreamReader(fs, Encoding.UTF8).BaseStream;

        //var accounts = JsonSerializer.Deserialize<TransactionsList>(contentStream, _jsonSerializerOptions);

        var accounts = await message.Content.ReadFromJsonAsync<TransactionsList>(_jsonSerializerOptions);
        return accounts!;
    }

    private async Task<string> GetAccessToken(CancellationToken cancellationToken = default)
    {
        var secretId = Environment.GetEnvironmentVariable(Constants.NordigenSecretId);
        var secretKey = Environment.GetEnvironmentVariable(Constants.NordigenSecretKey);

        if (string.IsNullOrEmpty(secretId) || string.IsNullOrEmpty(secretKey))
        {
            throw new ArgumentNullException($"Nordigen secret id or key is not defined as environment variables ({Constants.NordigenSecretId}, {Constants.NordigenSecretKey}");
        }

        var credentials = new Dictionary<string, string>
        {
            ["secret_id"] = secretId,
            ["secret_key"] = secretKey,
        };

        var content = new FormUrlEncodedContent(credentials);
        var httpClient = CreateHTTPClient();

        var message = await httpClient.PostAsync(Constants.TokenUrl, content, cancellationToken);

        if (!message.IsSuccessStatusCode)
        {
            var error = await message.Content.ReadFromJsonAsync<Error>(_jsonSerializerOptions);
            throw new Exception(error!.ToString());
        }

        var token = await message.Content.ReadFromJsonAsync<Token>(_jsonSerializerOptions);
        return token!.Access!;
    }

    private HttpClient CreateHTTPClient(string? accessToken = null)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_url);
        client.DefaultRequestHeaders.Add("Accept", Constants.AcceptedMediaType);
        client.DefaultRequestHeaders.Add("User-Agent", "MyFinancePal.Nordigen.Net");

        if (!string.IsNullOrEmpty(accessToken))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return client;
    }
}
