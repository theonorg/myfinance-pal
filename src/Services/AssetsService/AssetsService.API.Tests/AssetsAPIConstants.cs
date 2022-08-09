namespace Tiberna.MyFinancePal.AssetsService.API.Tests;

internal static class AssetsAPIConstants
{
    internal static List<Account> GenerateMockData(Currency[] currencies)
    {
        var account = new Account
        {
            Id = 1,
            Name = "Test #1",
            Description = "Desc",
            BankAccountId = "1234567890",
            InitialBalance = 100.0M,
            InitialBalanceDate = new DateTime(2022, 8, 1),
            IsActive = true
        };

        account.Transactions = new List<Transaction> {
            new Transaction {
                Id = 1,
                Amount = 100.0M,
                Date = DateTime.Now,
                Account = account,
                Description = "Test #1" ,
                Currency = currencies[0]
            },
            new Transaction {
                Id = 2,
                Amount = 200.0M,
                Date = DateTime.Now,
                Account = account,
                Description = "Test #2" ,
                Currency = currencies[0]
            },
            new Transaction {
                Id = 3,
                Amount = -300.0M,
                Date = DateTime.Now,
                Account = account,
                Description = "Test #3" ,
                Currency = currencies[1]
            }
        };

        var account2 = new Account
        {
            Id = 2,
            Name = "Test #2",
            Description = "Desc",
            InitialBalance = 1000.0M,
            InitialBalanceDate = new DateTime(2022, 8, 2),
            IsActive = false
        };

        return new List<Account> { account, account2 };

    }
    
    internal static class AccountEndpoints
    {
        internal const string Get = "/account";
        internal static string GetById(int id) => $"/account/{id}";
        internal const string Create = "/account";
        internal static string Update(int id) => $"/account/{id}";
        internal static string Delete(int id) => $"/account/{id}";
        internal const string GetTransaction = "/account/{0}/transaction";
        internal const string SyncTransactions = "/account/{0}/transaction/sync";
    }
}
