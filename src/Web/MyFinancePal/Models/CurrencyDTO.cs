namespace Tiberna.MyFinancePal.Web.Models;


public class CurrencyDTO
{
    public CurrencyDTO()
    {
        Name = string.Empty;
        Code = string.Empty;
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

