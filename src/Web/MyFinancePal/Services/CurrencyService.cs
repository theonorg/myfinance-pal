using System.Net.Http.Json;
using Tiberna.MyFinancePal.Web.Models;

namespace Tiberna.MyFinancePal.Web.Services;


public class CurrencyService : ICurrencyService
{
    private readonly HttpClient httpClient;

    private const string CurrencyEndpoint = "/currency";

    public CurrencyService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<CurrencyDTO>> GetAllCurrenciesAsync()
    {
        var currencies = await httpClient.GetFromJsonAsync<List<CurrencyDTO>>(CurrencyEndpoint);
        return currencies ?? new List<CurrencyDTO>();
    }

    public async Task<CurrencyDTO> GetCurrencyByIdAsync(int id)
    {
        var currency = await httpClient.GetFromJsonAsync<CurrencyDTO>($"{CurrencyEndpoint}/{id}");
        return currency ?? new CurrencyDTO();
    }

    public async Task<CurrencyDTO> GetCurrencyByCodeAsync(string code)
    {
        var currency = await httpClient.GetFromJsonAsync<CurrencyDTO>($"{CurrencyEndpoint}/{code}");
        return currency ?? new CurrencyDTO();
    }
}