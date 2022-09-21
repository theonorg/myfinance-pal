namespace Tiberna.MyFinancePal.AssetsService.API.Services.Impl;

public class InvestmentTransactionService : IInvestmentTransactionService
{
    private readonly AssetsDbContext _context;
    private readonly ILogger _logger;

    public InvestmentTransactionService(AssetsDbContext context, ILogger<InvestmentTransactionService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<InvestmentTransactionDTO> CreateAsync(InvestmentTransactionDTO newTransaction)
    {
        InvestmentTransaction transaction = newTransaction.ToInvestmentTransaction();
        _context.InvestmentTransactions.Add(transaction);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception adding transaction {newTransaction.Description} for account {newTransaction.InvestmentAccountId}");
            throw;
        }

        InvestmentTransactionDTO? returnValue = new InvestmentTransactionDTO(transaction);
        return returnValue;
    }

    public async Task<InvestmentTransactionDTO> DeleteAsync(int id)
    {
        InvestmentTransaction? transaction = await _context.InvestmentTransactions.FindAsync(id);
        if (transaction is not InvestmentTransaction)
        {
            throw new ItemNotFoundException($"InvestmentTransaction not found with id {id}");
        }

        if (transaction.TransactionId is not null)
        {
            throw new OperationNotAllowedException($"Cannot delete InvestmentTransaction {id} as it is linked to a bank transaction");
        }

        _context.InvestmentTransactions.Remove(transaction);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception removing transaction {transaction.Id}, {transaction.Description} on account {transaction.InvestmentAccountId}");
            throw;
        }

        InvestmentTransactionDTO returnValue = new InvestmentTransactionDTO(transaction);
        return returnValue;
    }

    public async Task<IEnumerable<InvestmentTransactionDTO>> GetAllAsync()
    {
        List<InvestmentTransaction> allTransactions = await _context.InvestmentTransactions.OrderBy(tr => tr.Id).ToListAsync();
        IEnumerable<InvestmentTransactionDTO> returnList = allTransactions.Select(t => new InvestmentTransactionDTO(t));
        return returnList;
    }

    public async Task<IEnumerable<InvestmentTransactionDTO>> GetByAccountIdAsync(int accountId)
    {
        List<InvestmentTransaction> transactions = await _context.InvestmentTransactions
            .Where(tr => tr.InvestmentAccountId == accountId).OrderBy(tr => tr.Id).ToListAsync();

        IEnumerable<InvestmentTransactionDTO> returnList = transactions.Select(t => new InvestmentTransactionDTO(t));
        return returnList;
    }

    public async Task<InvestmentTransactionDTO> GetByIdAsync(int id)
    {
        InvestmentTransaction? transaction = await _context.InvestmentTransactions.FindAsync(id);
        if (transaction is not InvestmentTransaction)
        {
            throw new ItemNotFoundException($"InvestmentTransaction not found with id {id}");
        }

        InvestmentTransactionDTO returnValue = new InvestmentTransactionDTO(transaction);
        return returnValue;
    }

    public async Task<InvestmentTransactionDTO> UpdateAsync(InvestmentTransactionDTO transaction)
    {
        InvestmentTransaction? fromDB = await _context.InvestmentTransactions.FindAsync(transaction.Id);

        if (fromDB is not InvestmentTransaction)
        {
            throw new ItemNotFoundException($"InvestmentTransaction not found with id {transaction.Id}");
        }

        if (fromDB.TransactionId is not null)
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

        InvestmentTransactionDTO returnValue = new InvestmentTransactionDTO(fromDB);
        return returnValue;
    }
}

