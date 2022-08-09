using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tiberna.MyFinancePal.AssetsService.API.Tests;

class AssetsAPIMock : WebApplicationFactory<Program>
{
    private readonly string _dbName = Guid.NewGuid().ToString();
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddXUnit();
        });
        builder.ConfigureServices(services =>
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            services.AddScoped(sp =>
            {
                // Replace PostgreSQL with the in memory provider for tests
                return new DbContextOptionsBuilder<AssetsDbContext>()
                            .UseInMemoryDatabase(_dbName)
                            //.UseApplicationServiceProvider(sp)
                            .Options;
            });

            services.AddScoped<IBankAPIHttpClient, BankAPIHttpClientMock>();
        });

        return base.CreateHost(builder);
    }
}
