

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var servicesConfig = builder.Configuration.GetSection(ServicesConfiguration.SectionName).Get<ServicesConfiguration>();

builder.Services.AddScoped<IAssetsService, AssetsService>(sp => new AssetsService(new HttpClient() {
    BaseAddress = new Uri(servicesConfig.AssetsService!.URI!)
}));

builder.Services.AddScoped<ICurrencyService, CurrencyService>(sp => new CurrencyService(new HttpClient() {
    BaseAddress = new Uri(servicesConfig.CurrencyService!.URI!)
}));

await builder.Build().RunAsync();
