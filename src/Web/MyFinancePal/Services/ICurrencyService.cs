using Tiberna.MyFinancePal.Web.Models;

namespace Tiberna.MyFinancePal.Web.Services;

public interface ICurrencyService {
    Task<IEnumerable<CurrencyDTO>> GetAllCurrenciesAsync();
    Task<CurrencyDTO> GetCurrencyByIdAsync(int id);
    Task<CurrencyDTO> GetCurrencyByCodeAsync(string code);
}