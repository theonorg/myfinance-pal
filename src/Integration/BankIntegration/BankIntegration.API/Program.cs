
var appName = "BankIntegration API";
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.RemoveKestrelHeader();

builder.AddCustomLogger()
    .AddCustomCORS()
    .AddCustomSwagger(appName, "v1");

var nordigenApiOptions = builder.Configuration.GetSection(NordigenApiOptions.SectionName).Get<NordigenApiOptions>();
builder.Services
.AddHttpClient<NordigenApi>(client =>
{
    client.BaseAddress = new Uri(nordigenApiOptions.Url);
});

var app = builder.Build();

app.UseCustomSwagger(appName)
    .SetExceptionPage()
    .UseCustomCORS();

app.MapGet("/bank/sync/{id:length(36)}", async (string id, IHttpClientFactory httpClientFactory, NordigenApi nordigenApi) =>
{
    app.Logger.LogInformation("Syncing bank account with id {id}", id);
    var transactions = await nordigenApi.GetTransactions(id);
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
