namespace Tiberna.MyFinancePal.AssetsService.API.Services.DTO;

public class InvestmentAccountTypeDTO
{
    public InvestmentAccountTypeDTO()
    {
        Name = string.Empty;
    }

    public InvestmentAccountTypeDTO(InvestmentAccountType investmentAccountType) {
        this.Id = investmentAccountType.Id;
        this.Name = investmentAccountType.Name;
    }

    public InvestmentAccountType ToInvestmentAccountType() {
        return new InvestmentAccountType() {
            Id = this.Id,
            Name = this.Name
        };
    }

    public void CopyTo(InvestmentAccountType investmentAccountType) {
        investmentAccountType.Name = this.Name;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
}

