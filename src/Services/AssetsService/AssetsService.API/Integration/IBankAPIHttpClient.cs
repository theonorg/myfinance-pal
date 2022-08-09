namespace Tiberna.MyFinancePal.AssetsService.API.Integration;
public interface IBankAPIHttpClient
{
    Task<List<BankAPITransaction>?> SyncAccount(string accountId);
}

