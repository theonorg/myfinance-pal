namespace Tiberna.MyFinancePal.AssetsService.API.Services.Impl;

public class InvestmentAccountService : IInvestmentAccountService
{
    private readonly AssetsDbContext _context;
    private readonly ILogger _logger;

    public InvestmentAccountService(AssetsDbContext context, ILogger<InvestmentAccountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InvestmentAccountDTO> CreateAsync(InvestmentAccountDTO newAccount)
    {
        InvestmentAccount account = newAccount.ToInvestmentAccount();
        _context.InvestmentAccounts.Add(account);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception adding investment account {newAccount.Id}, {newAccount.Name}");
            throw;
        }

        InvestmentAccountDTO? returnValue = new InvestmentAccountDTO(account);
        return returnValue;
    }

    public async Task<InvestmentAccountDTO> DeleteAsync(int id)
    {
        InvestmentAccount? account = await _context.InvestmentAccounts.FindAsync(id);
        if (account is not InvestmentAccount)
        {
            throw new ItemNotFoundException($"InvestmentAccount not found with id {id}");
        }

        _context.InvestmentAccounts.Remove(account);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception removing investment account {account.Name}");
            throw;
        }

        InvestmentAccountDTO returnValue = new InvestmentAccountDTO(account);
        return returnValue;
    }

    public async Task<IEnumerable<InvestmentAccountDTO>> GetAllAsync()
    {
        List<InvestmentAccount> allAccounts = await _context.InvestmentAccounts.OrderBy(acc => acc.Id).ToListAsync();
        IEnumerable<InvestmentAccountDTO> returnList = allAccounts.Select(t => new InvestmentAccountDTO(t));
        return returnList;
    }

    public async Task<InvestmentAccountDTO> GetAsync(int id)
    {
        InvestmentAccount? account = await _context.InvestmentAccounts.FindAsync(id);
        if (account is not InvestmentAccount)
        {
            throw new ItemNotFoundException($"InvestmentAccount not found with id {id}");
        }

        InvestmentAccountDTO returnValue = new InvestmentAccountDTO(account);
        return returnValue;
    }

    public async Task<InvestmentAccountDTO> UpdateAsync(InvestmentAccountDTO account)
    {
        InvestmentAccount? fromDB = await _context.InvestmentAccounts.FindAsync(account.Id);

        if (fromDB is not InvestmentAccount)
        {
            throw new ItemNotFoundException($"InvestmentAccount not found with id {account.Id}");
        }

        account.CopyTo(fromDB);
        _context.Entry(fromDB).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception saving changes to account {account.Id}, {account.Name}");
            throw;
        }

        InvestmentAccountDTO returnValue = new InvestmentAccountDTO(fromDB);
        return returnValue;
    }
}

