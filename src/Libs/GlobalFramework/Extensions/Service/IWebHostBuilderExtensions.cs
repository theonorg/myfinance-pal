namespace Tiberna.MyFinancePal.GlobalFramework.Extensions.Service;

public static class IWebHostBuilderExtensions
{
    public static void RemoveKestrelHeader(this IWebHostBuilder webHost)
    {
        webHost.ConfigureKestrel(serverOptions => serverOptions.AddServerHeader = false);
    }
}
