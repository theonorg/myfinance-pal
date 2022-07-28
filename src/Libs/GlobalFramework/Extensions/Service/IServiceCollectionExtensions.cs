

namespace Tiberna.MyFinancePal.GlobalFramework.Extensions.Service;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddPostgreSQLContext<T>(this IServiceCollection services, string connectionString) where T : DbContext
    {
        services.AddDbContext<T>(opt =>
            opt.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention());

        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }
}

