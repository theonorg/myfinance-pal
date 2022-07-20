namespace Tiberna.MyFinancePal.Libs.Nordigen.Net.Responses;

internal class Error
{
    public string? Summary { get; set; }

    public string? Detail { get; set;}

    public string? Type { get; set;}

    public int StatusCode { get; set;}

    public override string ToString()
    {
        return $"{StatusCode} : {Summary} - {Detail}";
    }
}

