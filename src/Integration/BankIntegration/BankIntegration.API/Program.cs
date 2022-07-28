
var appName = "BankIntegration API";
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.RemoveKestrelHeader();

builder.AddCustomLogger()
    .AddCustomCORS()
    .AddCustomSwagger(appName, "v1");

builder.Services.AddHttpClient();

builder.Services.Configure<NordigenApiOptions>(
    builder.Configuration.GetSection(NordigenApiOptions.SectionName));

var app = builder.Build();

app.UseCustomSwagger(appName)
    .SetExceptionPage()
    .UseCustomCORS();

app.MapGet("/bank/sync/{id:length(36)}", async (string id, IHttpClientFactory httpClientFactory, IOptions<NordigenApiOptions> nordigenApiOptions) =>
{
    app.Logger.LogInformation("Syncing bank account with id {id}", id);
    NordigenApi nordegen = new NordigenApi(httpClientFactory, nordigenApiOptions.Value.Url);
    var transactions = await nordegen.GetTransactions(id);
    app.Logger.LogInformation("Received {count} transactions", transactions.Transactions!.Booked!.Count());
    var result = new List<BankTransaction>();

    foreach (var transaction in transactions.Transactions!.Booked!)
    {
        result.Add(BankTransaction.FromNordigen(transaction));
    }

    return result;
})
.WithName("SyncBankAccount")
.WithTags(new[] { "Bank" })
.Produces<BankTransaction[]>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.Run();