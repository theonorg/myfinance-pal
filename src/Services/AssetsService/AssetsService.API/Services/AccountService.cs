namespace Tiberna.MyFinancePal.AssetsService.API.Services;

public class AccountService : IAccountService
{
    private readonly AssetsDbContext _context;
    private readonly ILogger _logger;

    public AccountService(AssetsDbContext context, ILogger<AccountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AccountDTO> CreateAccountAsync(AccountDTO newAccount)
    {
        Account account = newAccount.ToAccount();
        _context.Accounts.Add(account);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception adding account {newAccount.Id}, {newAccount.Name}");
            throw;
        }

        AccountDTO? returnValue = new AccountDTO(account);
        return returnValue;
    }

    public async Task<AccountDTO> DeleteAccountAsync(int id)
    {
        Account? account = await _context.Accounts.FindAsync(id);
        if (account is not Account)
        {
            throw new ItemNotFoundException($"Account not found with id {id}");
        }

        _context.Accounts.Remove(account);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception removing account {account.Name}");
            throw;
        }

        AccountDTO returnValue = new AccountDTO(account);
        return returnValue;
    }

    public async Task<AccountDTO> GetAccountAsync(int id)
    {
        Account? account = await _context.Accounts.FindAsync(id);
        if (account is not Account)
        {
            throw new ItemNotFoundException($"Account not found with id {id}");
        }

        AccountDTO returnValue = new AccountDTO(account);
        return returnValue;
    }

    public async Task<AccountDTO> GetAccountByBankAccountIdAsync(string bankAccountId)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(acc => acc.BankAccountId == bankAccountId);
        if (account is not Account)
        {
            throw new ItemNotFoundException($"Account not found with bank account id {bankAccountId}");
        }

        AccountDTO returnValue = new AccountDTO(account);
        return returnValue;
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAccountsAsync()
    {
        List<Account> allAccounts = await _context.Accounts.OrderBy(acc => acc.Id).ToListAsync();
        IEnumerable<AccountDTO> returnList = allAccounts.Select(t => new AccountDTO(t));
        return returnList;
    }

    public async Task<AccountDTO> UpdateAccountAsync(AccountDTO account)
    {
        Account? fromDB = await _context.Accounts.FindAsync(account.Id);

        if (fromDB is not Account)
        {
            throw new ItemNotFoundException($"Account not found with id {account.Id}");
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

        AccountDTO returnValue = new AccountDTO(fromDB);
        return returnValue;
    }
}

