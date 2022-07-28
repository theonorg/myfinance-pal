
namespace Tiberna.MyFinancePal.AssetsService.API.Services;

public class CurrencyService : ICurrencyService
{
    private readonly AssetsDbContext _context;
    private readonly ILogger _logger;

    public CurrencyService(AssetsDbContext context, ILogger<AccountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CurrencyDTO>> GetAllCurrenciesAsync()
    {
        List<Currency> allTenants = await _context.Currencies.ToListAsync();
        IEnumerable<CurrencyDTO> returnList = allTenants.Select(t => new CurrencyDTO(t));
        return returnList;
    }

    public async Task<CurrencyDTO> GetCurrencyByCodeAsync(string code)
    {
        Currency? currency = await _context.Currencies.FirstOrDefaultAsync(cur => cur.Code == code);
        if (currency is not Currency)
        {
            throw new ItemNotFoundException($"Currency not found with code {code}");
        }

        CurrencyDTO returnValue = new CurrencyDTO(currency);
        return returnValue;
    }

    public async Task<CurrencyDTO> GetCurrencyByIdAsync(int id)
    {
        Currency? currency = await _context.Currencies.FindAsync(id);
        if (currency is not Currency)
        {
            throw new ItemNotFoundException($"Currency not found with id {id}");
        }

        CurrencyDTO returnValue = new CurrencyDTO(currency);
        return returnValue;
    }
}


