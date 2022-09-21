
namespace Tiberna.MyFinancePal.AssetsService.API.Services.Impl;

public class InvestmentAccountTypeService : IInvestmentAccountTypeService
{
    private readonly AssetsDbContext _context;
    private readonly ILogger _logger;

    public InvestmentAccountTypeService(AssetsDbContext context, ILogger<InvestmentAccountTypeService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InvestmentAccountTypeDTO> CreateAsync(InvestmentAccountTypeDTO newType)
    {
        InvestmentAccountType type = newType.ToInvestmentAccountType();
        _context.InvestmentAccountTypes.Add(type);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception adding InvestmentAccountType {newType.Id}, {newType.Name}");
            throw;
        }

        InvestmentAccountTypeDTO? returnValue = new InvestmentAccountTypeDTO(type);
        return returnValue;
    }

    public async Task<InvestmentAccountTypeDTO> DeleteAsync(int id)
    {
        InvestmentAccountType? type = await _context.InvestmentAccountTypes.FindAsync(id);
        if (type is not InvestmentAccountType)
        {
            throw new ItemNotFoundException($"InvestmentAccountType not found with id {id}");
        }

        _context.InvestmentAccountTypes.Remove(type);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception removing InvestmentAccountType {type.Name}");
            throw;
        }

        InvestmentAccountTypeDTO returnValue = new InvestmentAccountTypeDTO(type);
        return returnValue;
    }
    public async Task<IEnumerable<InvestmentAccountTypeDTO>> GetAllAsync()
    {
        List<InvestmentAccountType> allRecords = await _context.InvestmentAccountTypes.ToListAsync();
        IEnumerable<InvestmentAccountTypeDTO> returnList = allRecords.Select(t => new InvestmentAccountTypeDTO(t));
        return returnList;
    }


    public async Task<InvestmentAccountTypeDTO> GetByIdAsync(int id)
    {
        InvestmentAccountType? type = await _context.InvestmentAccountTypes.FindAsync(id);
        if (type is not InvestmentAccountType)
        {
            throw new ItemNotFoundException($"InvestmentAccountType not found with id {id}");
        }

        InvestmentAccountTypeDTO returnValue = new InvestmentAccountTypeDTO(type);
        return returnValue;
    }

    public async Task<InvestmentAccountTypeDTO> UpdateAsync(InvestmentAccountTypeDTO type)
    {
        InvestmentAccountType? fromDB = await _context.InvestmentAccountTypes.FindAsync(type.Id);

        if (fromDB is not InvestmentAccountType)
        {
            throw new ItemNotFoundException($"InvestmentAccountType not found with id {type.Id}");
        }

        var existAccount = await _context.InvestmentAccounts.FirstAsync();
        if (existAccount is not null)
        {
            throw new OperationNotAllowedException($"Cannot delete InvestmentAccountType {type.Id} as it is linked to 1+ accounts");
        }

        type.CopyTo(fromDB);
        _context.Entry(fromDB).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Exception saving changes to InvestmentAccountType {type.Id}, {type.Name}");
            throw;
        }

        InvestmentAccountTypeDTO returnValue = new InvestmentAccountTypeDTO(fromDB);
        return returnValue;
    }
}


