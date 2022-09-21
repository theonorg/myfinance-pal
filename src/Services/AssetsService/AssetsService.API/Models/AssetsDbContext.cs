namespace Tiberna.MyFinancePal.AssetsService.API.Models;

public class AssetsDbContext : DbContext
{
    public AssetsDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<InvestmentAccountType> InvestmentAccountTypes => Set<InvestmentAccountType>();
    public DbSet<InvestmentAccount> InvestmentAccounts => Set<InvestmentAccount>();
    public DbSet<InvestmentTransaction> InvestmentTransactions => Set<InvestmentTransaction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new AccountEntityTypeConfiguration().Configure(builder.Entity<Account>());
        new CurrencyEntityTypeConfiguration().Configure(builder.Entity<Currency>());
        new ExchangeRateEntityTypeConfiguration().Configure(builder.Entity<ExchangeRate>());
        new TransactionEntityTypeConfiguration().Configure(builder.Entity<Transaction>());
        new InvestmentAccountTypeEntityTypeConfiguration().Configure(builder.Entity<InvestmentAccountType>());
        new InvestmentAccountEntityTypeConfiguration().Configure(builder.Entity<InvestmentAccount>());
        new InvestmentTransactionEntityTypeConfiguration().Configure(builder.Entity<InvestmentTransaction>());
    }
}

