namespace Tiberna.MyFinancePal.Libs.Nordigen.Net;
public class NordigenApi
{
    private HttpClient _httpClient;
    private readonly ILogger<NordigenApi> _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    // public NordigenApi(IHttpClientFactory httpClientFactory, string url)
    // {
    //     _httpClientFactory = httpClientFactory;
    //     _url = url.TrimEnd('/');
    //     _jsonSerializerOptions = new JsonSerializerOptions {
    //         PropertyNameCaseInsensitive = true
    //     };
    // }

    public NordigenApi(HttpClient httpClient, ILogger<NordigenApi> logger)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Accept", Constants.AcceptedMediaType);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<TransactionsList> GetTransactions(string accountId, CancellationToken cancellationToken = default)
    {
        var token = await GetAccessToken(cancellationToken);

        var accountUrl = $"{Constants.AccountsUrl}{accountId}/transactions/";
        SetHTTPAutorization(token);

        var message = await _httpClient.GetAsync(accountUrl, cancellationToken);

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

        var message = await _httpClient.PostAsync(Constants.TokenUrl, content, cancellationToken);

        if (!message.IsSuccessStatusCode)
        {
            var error = await message.Content.ReadFromJsonAsync<Error>(_jsonSerializerOptions);
            throw new Exception(error!.ToString());
        }

        var token = await message.Content.ReadFromJsonAsync<Token>(_jsonSerializerOptions);
        return token!.Access!;
    }

    private void SetHTTPAutorization(string accessToken)
    {
        if (!string.IsNullOrEmpty(accessToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
