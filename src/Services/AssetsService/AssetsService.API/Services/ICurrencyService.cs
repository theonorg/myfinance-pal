namespace Tiberna.MyFinancePal.AssetsService.API.Services;

public interface ICurrencyService
{
    Task<IEnumerable<CurrencyDTO>> GetAllCurrenciesAsync();
    Task<CurrencyDTO> GetCurrencyByIdAsync(int id);
    Task<CurrencyDTO> GetCurrencyByCodeAsync(string code);
}