namespace Tiberna.MyFinancePal.AssetsService.API.Tests.Fixtures;

public class AccountEndpointsTest : IClassFixture<AssetsServiceAPITestController>
{
    private readonly AssetsAPIMock AssetsServiceAPI;
    private readonly Currency[] CurrencyBaseList;
    private readonly Account[] AccountExpectedList;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public AccountEndpointsTest(AssetsServiceAPITestController controller)
    {
        AssetsServiceAPI = controller.AssetsServiceAPI;
        CurrencyBaseList = controller.CurrencyBaseList;
        AccountExpectedList = AssetsAPIConstants.GenerateMockData(CurrencyBaseList).ToArray();
        _jsonSerializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };
    }

    [Fact]
    public async Task GetAccounts()
    {
        var client = AssetsServiceAPI.CreateClient();
        var accounts = await client.GetFromJsonAsync<List<AccountDTO>>(AssetsAPIConstants.AccountEndpoints.Get);

        Assert.NotNull(accounts);
        Assert.NotEmpty(accounts);
        //Assert.Equal(2, accounts!.Count);
        foreach (var (account, expected) in accounts!.Zip(AccountExpectedList))
        {
            Assert.Equal(expected.Id, account.Id);
            Assert.Equal(expected.Name, account.Name);
            Assert.Equal(expected.Description, account.Description);
            Assert.Equal(expected.BankAccountId, account.BankAccountId);
            Assert.Equal(expected.InitialBalance, account.InitialBalance);
            Assert.Equal(expected.InitialBalanceDate, account.InitialBalanceDate);
            Assert.Equal(expected.IsActive, account.IsActive);
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetAccountById(int id)
    {
        var client = AssetsServiceAPI.CreateClient();
        var expected = AccountExpectedList.First(a => a.Id == id);
        var account = await client.GetFromJsonAsync<AccountDTO>(AssetsAPIConstants.AccountEndpoints.GetById(id));

        Assert.NotNull(account);
        Assert.Equal(expected.Id, account!.Id);
        Assert.Equal(expected.Name, account.Name);
        Assert.Equal(expected.Description, account.Description);
        Assert.Equal(expected.BankAccountId, account.BankAccountId);
        Assert.Equal(expected.InitialBalance, account.InitialBalance);
        Assert.Equal(expected.InitialBalanceDate, account.InitialBalanceDate);
        Assert.Equal(expected.IsActive, account.IsActive);
    }

    [Fact]
    public async Task GetAccountById_NotFound()
    {
        var client = AssetsServiceAPI.CreateClient();
        var response = await client.GetAsync(AssetsAPIConstants.AccountEndpoints.GetById(1000));
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Theory]
    [ClassData(typeof(CreateAccountTheoryData))]
    public async Task CreateAccount(AccountDTO expected)
    {
        var client = AssetsServiceAPI.CreateClient();
        var response = await client.PostAsJsonAsync(AssetsAPIConstants.AccountEndpoints.Create, expected);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdAccount = await response.Content.ReadFromJsonAsync<AccountDTO>();
        Assert.Equal(JsonSerializer.Serialize(expected, _jsonSerializerOptions),
            JsonSerializer.Serialize(createdAccount, _jsonSerializerOptions));
    }

    [Theory]
    [ClassData(typeof(CreateBadAccountTheoryData))]
    public async Task CreateAccount_BadRequest(AccountDTO badAccount)
    {
        var client = AssetsServiceAPI.CreateClient();
        var response = await client.PostAsJsonAsync(AssetsAPIConstants.AccountEndpoints.Create, badAccount);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [ClassData(typeof(DeleteAccountTheoryData))]
    public async Task DeleteAccount(AccountDTO expected)
    {
        var client = AssetsServiceAPI.CreateClient();
        var response = await client.PostAsJsonAsync(AssetsAPIConstants.AccountEndpoints.Create, expected);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdAccount = await response.Content.ReadFromJsonAsync<AccountDTO>();
        Assert.Equal(JsonSerializer.Serialize(expected, _jsonSerializerOptions),
            JsonSerializer.Serialize(createdAccount, _jsonSerializerOptions));

        response = await client.DeleteAsync(AssetsAPIConstants.AccountEndpoints.Delete(expected.Id));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var deletedAccount = await response.Content.ReadFromJsonAsync<AccountDTO>();
        Assert.Equal(JsonSerializer.Serialize(expected, _jsonSerializerOptions),
            JsonSerializer.Serialize(deletedAccount, _jsonSerializerOptions));
    }

    [Fact]
    public async Task DeleteAccount_NotFound()
    {
        var client = AssetsServiceAPI.CreateClient();
        var response = await client.DeleteAsync(AssetsAPIConstants.AccountEndpoints.GetById(1000));
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [ClassData(typeof(UpdateAccountTheoryData))]
    public async Task Update(AccountDTO expected)
    {
        var client = AssetsServiceAPI.CreateClient();
        var response = await client.PostAsJsonAsync(AssetsAPIConstants.AccountEndpoints.Create, expected);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdAccount = await response.Content.ReadFromJsonAsync<AccountDTO>();
        Assert.Equal(JsonSerializer.Serialize(expected, _jsonSerializerOptions),
            JsonSerializer.Serialize(createdAccount, _jsonSerializerOptions));

        var random = RandomNumberGenerator.GetInt32(1, 99999);

        createdAccount!.Name = $"Updated Name {random}";
        createdAccount.Description = $"Updated Description {random}";
        createdAccount.BankAccountId = $"qwerty-{random}";
        createdAccount.InitialBalance = random;
        createdAccount.InitialBalanceDate = DateTime.UtcNow.AddHours(-random);
        createdAccount.IsActive = !createdAccount.IsActive;

        response = await client.PutAsJsonAsync(AssetsAPIConstants.AccountEndpoints.Update(expected.Id), createdAccount);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updatedAccount = await response.Content.ReadFromJsonAsync<AccountDTO>();
        
        Assert.NotNull(updatedAccount);
        Assert.Equal(createdAccount.Id, updatedAccount!.Id);
        Assert.Equal(createdAccount.Name, updatedAccount.Name);
        Assert.Equal(createdAccount.Description, updatedAccount.Description);
        Assert.Equal(createdAccount.BankAccountId, updatedAccount.BankAccountId);
        Assert.Equal(createdAccount.InitialBalance, updatedAccount.InitialBalance);
        Assert.Equal(createdAccount.InitialBalanceDate, updatedAccount.InitialBalanceDate);
        Assert.Equal(createdAccount.IsActive, updatedAccount.IsActive);
    }

    [Theory]
    [ClassData(typeof(UpdateBadAccountTheoryData))]
    public async Task Update_BadRequest(AccountDTO expected)
    {
        var client = AssetsServiceAPI.CreateClient();
        var response = await client.PostAsJsonAsync(AssetsAPIConstants.AccountEndpoints.Create, expected);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdAccount = await response.Content.ReadFromJsonAsync<AccountDTO>();
        Assert.Equal(JsonSerializer.Serialize(expected, _jsonSerializerOptions),
            JsonSerializer.Serialize(createdAccount, _jsonSerializerOptions));

        var random = RandomNumberGenerator.GetInt32(1, 99999);

        createdAccount!.Name = $"Updated Name {random}";
        createdAccount.Description = $"Updated Description {random}";
        createdAccount.BankAccountId = $"qwerty-{random}";
        createdAccount.InitialBalance = random;
        createdAccount.InitialBalanceDate = DateTime.UtcNow.AddHours(-random);
        createdAccount.IsActive = !createdAccount.IsActive;

        response = await client.PutAsJsonAsync(AssetsAPIConstants.AccountEndpoints.Update(expected.Id+100), createdAccount);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAccount_NotFound()
    {
        var client = AssetsServiceAPI.CreateClient();

        var account = new AccountDTO();
        account.Id = 1000;
        account.Name = "Test Account";
        account.Description = "Test Description";
        account.InitialBalance = 100;
        account.InitialBalanceDate = DateTime.UtcNow.AddHours(-100);

        var response = await client.PutAsJsonAsync(AssetsAPIConstants.AccountEndpoints.Update(1000), account);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}