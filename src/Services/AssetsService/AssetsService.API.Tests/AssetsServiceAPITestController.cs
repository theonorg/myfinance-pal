using System.Linq;

namespace Tiberna.MyFinancePal.AssetsService.API.Tests;

public class AssetsServiceAPITestController
{
    internal readonly AssetsAPIMock AssetsServiceAPI;
    internal readonly Currency[] CurrencyBaseList;

    public AssetsServiceAPITestController()
    {
        AssetsServiceAPI = new AssetsAPIMock();

        using (var scope = AssetsServiceAPI.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var dbContext = provider.GetRequiredService<AssetsDbContext>())
            {
                dbContext.Database.EnsureCreated();
                CurrencyBaseList = dbContext.Currencies.ToArray<Currency>();
                var toAdd = AssetsAPIConstants.GenerateMockData(CurrencyBaseList);
                dbContext.Accounts.AddRange(toAdd);
                dbContext.SaveChanges();
            }
        }
    }
}