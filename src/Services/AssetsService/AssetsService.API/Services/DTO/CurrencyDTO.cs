namespace Tiberna.MyFinancePal.AssetsService.API.Services.DTO;


public class CurrencyDTO
{
    public CurrencyDTO()
    {
        Name = string.Empty;
        Code = string.Empty;
        Symbol = string.Empty;
    }

    public CurrencyDTO(Currency cur) {
        this.Id = cur.Id;
        this.Name = cur.Name;
        this.Code = cur.Code;
        this.Symbol = cur.Symbol;
    }

    public Currency ToCurrency() {
        return new Currency() {
            Id = this.Id,
            Name = this.Name,
            Code = this.Code,
            Symbol = this.Symbol
        };
    }

    public void CopyTo(Currency currency) {
        currency.Name = this.Name;
        currency.Code = this.Code;
        currency.Symbol = this.Symbol;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Symbol { get; set; }
}

