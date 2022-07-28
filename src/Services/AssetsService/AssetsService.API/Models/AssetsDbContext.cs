namespace Tiberna.MyFinancePal.AssetsService.API.Models;

public class AssetsDbContext : DbContext
{
    public AssetsDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new AccountEntityTypeConfiguration().Configure(builder.Entity<Account>());
        new CurrencyEntityTypeConfiguration().Configure(builder.Entity<Currency>());
        new ExchangeRateEntityTypeConfiguration().Configure(builder.Entity<ExchangeRate>());
        new TransactionEntityTypeConfiguration().Configure(builder.Entity<Transaction>());
    }
}

