
namespace Tiberna.MyFinancePal.Libs.Nordigen.Net.Responses;

internal class Token
{
    public string? Access { get; set;}

    [JsonPropertyName("access_expires")]
    public int AccessExpires { get; set;}

    public string? Refresh { get; set;}

    [JsonPropertyName("refresh_expires")]
    public int RefreshExpires { get; set;}
}
