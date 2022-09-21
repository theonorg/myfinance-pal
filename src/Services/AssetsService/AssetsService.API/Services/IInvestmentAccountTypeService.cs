namespace Tiberna.MyFinancePal.AssetsService.API.Services;

public interface IInvestmentAccountTypeService
{
    Task<IEnumerable<InvestmentAccountTypeDTO>> GetAllAsync();
    Task<InvestmentAccountTypeDTO> GetByIdAsync(int id);
    Task<InvestmentAccountTypeDTO> CreateAsync(InvestmentAccountTypeDTO newType);
    Task<InvestmentAccountTypeDTO> UpdateAsync(InvestmentAccountTypeDTO type);
    Task<InvestmentAccountTypeDTO> DeleteAsync(int id);
}