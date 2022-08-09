
namespace Tiberna.MyFinancePal.AssetsService.API.Tests.Fixtures;

public class CurrencyEndpointsTest : IClassFixture<AssetsServiceAPITestController>
{
    private readonly AssetsAPIMock AssetsServiceAPI;

    public CurrencyEndpointsTest(AssetsServiceAPITestController controller)
    {
        AssetsServiceAPI = controller.AssetsServiceAPI;
    }

    [Fact]
    public async Task GetCurrencies()
    {
        var client = AssetsServiceAPI.CreateClient();
        var currencies = await client.GetFromJsonAsync<List<CurrencyDTO>>("/currency");

        Assert.NotNull(currencies);
        Assert.NotEmpty(currencies);
        Assert.Equal(4, currencies!.Count);
        Assert.Equal("Euro", currencies[0].Name);
        Assert.Equal("EUR", currencies[0].Code);
        Assert.Equal("Dollar", currencies[1].Name);
        Assert.Equal("USD", currencies[1].Code);
    }

    [Theory]
    [InlineData(1, "EUR", "Euro")]
    [InlineData(2, "USD", "Dollar")]
    [InlineData(3, "GBP", "British Pound")]
    [InlineData(4, "BRL", "Brazilian Real")]
    public async Task GetCurrencyById(int id, string expectedCode, string expectedName)
    {
        var client = AssetsServiceAPI.CreateClient();
        var currency = await client.GetFromJsonAsync<CurrencyDTO>($"/currency/{id}");

        Assert.NotNull(currency);
        Assert.Equal(currency!.Code, expectedCode);
        Assert.Equal(currency.Name, expectedName);
    }

    [Theory]
    [InlineData("EUR", "Euro")]
    [InlineData("USD", "Dollar")]
    [InlineData("GBP", "British Pound")]
    [InlineData("BRL", "Brazilian Real")]
    public async Task GetCurrencyByCode(string code, string expectedName)
    {
        var client = AssetsServiceAPI.CreateClient();
        var currency = await client.GetFromJsonAsync<CurrencyDTO>($"/currency/{code}");

        Assert.NotNull(currency);
        Assert.Equal(currency!.Name, expectedName);
    }

    [Fact]
    public async Task GetCurrencyById_IdNotFound()
    {
        var client = AssetsServiceAPI.CreateClient();
        var response = await client.GetAsync($"/currency/10");

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetCurrencyByCode_CodeNotFound()
    {
        var client = AssetsServiceAPI.CreateClient();
        var response = await client.GetAsync($"/currency/XPTO");

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

}
