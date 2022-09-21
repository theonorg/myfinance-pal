namespace Tiberna.MyFinancePal.AssetsService.API.Services.Impl;

public class TransactionService : ITransactionService
{
    private readonly AssetsDbContext _context;
    private readonly ILogger _logger;

    public TransactionService(AssetsDbContext context, ILogger<TransactionService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<TransactionDTO> CreateTransactionAsync(TransactionDTO newTransaction)
    {
        Transaction transaction = newTransaction.ToTransaction();
        _context.Transactions.Add(transaction);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception adding transaction {newTransaction.Description} for account {newTransaction.AccountId}");
            throw;
        }

        TransactionDTO? returnValue = new TransactionDTO(transaction);
        return returnValue;
    }

    public async Task<TransactionDTO> DeleteTransactionAsync(int id)
    {
        Transaction? transaction = await _context.Transactions.FindAsync(id);
        if (transaction is not Transaction)
        {
            throw new ItemNotFoundException($"Transaction not found with id {id}");
        }

        if (transaction.BankTransactionId is not null)
        {
            throw new OperationNotAllowedException($"Cannot delete transaction {id} as it is linked to a bank transaction");
        }

        _context.Transactions.Remove(transaction);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception removing transaction {transaction.Id}, {transaction.Description} on account {transaction.AccountId}");
            throw;
        }

        TransactionDTO returnValue = new TransactionDTO(transaction);
        return returnValue;
    }

    public async Task<IEnumerable<TransactionDTO>> GetAllTransactionAsync()
    {
        List<Transaction> allTransactions = await _context.Transactions.OrderBy(tr => tr.Id).ToListAsync();
        IEnumerable<TransactionDTO> returnList = allTransactions.Select(t => new TransactionDTO(t));
        return returnList;
    }

    public async Task<IEnumerable<TransactionDTO>> GetTransactionsByAccountIdAsync(int accountId)
    {
        List<Transaction> transactions = await _context.Transactions.Where(tr => tr.AccountId == accountId).OrderBy(tr => tr.Id).ToListAsync();
        IEnumerable<TransactionDTO> returnList = transactions.Select(t => new TransactionDTO(t));
        return returnList;
    }

    public async Task<TransactionDTO> GetTransactionByIdAsync(int id)
    {
        Transaction? transaction = await _context.Transactions.FindAsync(id);
        if (transaction is not Transaction)
        {
            throw new ItemNotFoundException($"Transaction not found with id {id}");
        }

        TransactionDTO returnValue = new TransactionDTO(transaction);
        return returnValue;
    }

    public async Task<TransactionDTO> UpdateTransactionAsync(TransactionDTO transaction)
    {
        Transaction? fromDB = await _context.Transactions.FindAsync(transaction.Id);

        if (fromDB is not Transaction)
        {
            throw new ItemNotFoundException($"Transaction not found with id {transaction.Id}");
        }

        if (fromDB.BankTransactionId is not null)
        {
            throw new OperationNotAllowedException($"Cannot delete transaction {transaction.Id} as it is linked to a bank transaction");
        }

        transaction.CopyTo(fromDB);
        _context.Entry(fromDB).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception saving changes to Transaction {transaction.Id}, {transaction.Description}");
            throw;
        }

        TransactionDTO returnValue = new TransactionDTO(fromDB);
        return returnValue;
    }

    public async Task<int> SyncTransactionsfromBankAsync(int accountId, List<BankAPITransaction> bankTransactions)
    {
        List<Transaction> transactionsToSync = new List<Transaction>();
        Dictionary<string, int> currencyLookup = new Dictionary<string, int>();
        foreach (BankAPITransaction bankTransaction in bankTransactions)
        {
            if (!(await CheckBankTransactionExists(accountId, bankTransaction.Id!)))
            {
                if (!currencyLookup.ContainsKey(bankTransaction.Currency!))
                {
                    Currency? currency = await _context.Currencies.FirstOrDefaultAsync(c => c.Code == bankTransaction.Currency);
                    if (currency is null)
                    {
                        _logger.LogWarning($"Currency {bankTransaction.Currency} not found. Bank transaction {bankTransaction.Id} will not be synced");
                        continue;
                    }

                    currencyLookup.Add(bankTransaction.Currency!, currency.Id);
                }

                Transaction newTransaction = new Transaction
                {
                    AccountId = accountId,
                    BankTransactionId = bankTransaction.Id,
                    Amount = bankTransaction.Amount,
                    Description = bankTransaction.Description,
                    Date = bankTransaction.Date,
                    CurrencyId = currencyLookup[bankTransaction.Currency!],
                };
                transactionsToSync.Add(newTransaction);
            }
        }
        
        await _context.AddRangeAsync(transactionsToSync);
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception adding transactions for account {accountId}");
            throw;
        }

        return transactionsToSync.Count;
    }

    private async Task<bool> CheckBankTransactionExists(int accountId, string bankTransactionId)
    {
        try
        {
            bool exists = await _context.Transactions.AnyAsync(t => string.Equals(t.BankTransactionId, bankTransactionId) && t.AccountId == accountId);
            return exists;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while checking for valid path");
            throw;
        }
    }
}

