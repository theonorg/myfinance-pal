namespace Tiberna.MyFinancePal.AssetsService.API.Services;

public interface IInvestmentAccountService
{
    Task<IEnumerable<InvestmentAccountDTO>> GetAllAsync();
    Task<InvestmentAccountDTO> GetAsync(int id);
    Task<InvestmentAccountDTO> CreateAsync(InvestmentAccountDTO newAccount);
    Task<InvestmentAccountDTO> UpdateAsync(InvestmentAccountDTO account);
    Task<InvestmentAccountDTO> DeleteAsync(int id);
}