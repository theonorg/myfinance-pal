
var appName = "AssetsService API";
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.RemoveKestrelHeader();

builder.AddCustomLogger()
    .AddCustomCORS()
    .AddCustomSwagger(appName, "v1");

var dbConfig = builder.Configuration.GetSection(DataSourcesConfiguration.SectionName).Get<DataSourcesConfiguration>();
var integrationConfig = builder.Configuration.GetSection(IntegrationConfiguration.SectionName).Get<IntegrationConfiguration>();

builder.Services.AddAntiforgery();
builder.Services.AddPostgreSQLContext<AssetsDbContext>(dbConfig.AssetsDB!.ConnectionString!);

builder.Services
.AddHttpClient<IBankAPIHttpClient, BankAPIHttpClient>(client =>
{
    client.BaseAddress = new Uri(integrationConfig.BankAPI);
});

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<IValidator<AccountDTO>, AccountValidator>();
builder.Services.AddScoped<IValidator<TransactionDTO>, TransactionValidator>();

var app = builder.Build();

app.UseCustomSwagger(appName)
    .SetExceptionPage()
    .UseCustomCORS();

app.Logger.LogInformation($"Starting web api ({appName})...");

app.InitializePostgreSQLDatabase<AssetsDbContext>(force: dbConfig.AssetsDB.ForceInit);

#region Accounts
app.MapGet("/account", async (IAccountService accountService) =>
{
    try
    {
        app.Logger.LogDebug("Requesting all accounts.");

        List<AccountDTO> allAccounts = (await accountService.GetAllAccountsAsync()).ToList();

        app.Logger.LogDebug($"Returning {allAccounts.Count} accounts");

        return Results.Ok(allAccounts);
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "Problem retrieving all accounts");
        throw;
    }

})
.WithName("GetAllAccounts")
.WithTags(new[] { "Account" })
.Produces<AccountDTO[]>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapGet("/account/{id}", async ([FromRoute] int id, IAccountService accountService) =>
{
    app.Logger.LogDebug($"Requested account with ID {id}");
    try
    {
        AccountDTO account = await accountService.GetAccountAsync(id);
        app.Logger.LogDebug($"Found {account.Name} with ID {id}");

        return Results.Ok(account);
    }
    catch (ItemNotFoundException)
    {
        app.Logger.LogInformation($"Was not able to find account with ID {id}");
        return Results.NotFound();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem retrieving account with ID {id}");
        throw;
    }
})
.WithName("GetAccountById")
.WithTags(new[] { "Account" })
.Produces<AccountDTO>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapPost("/account", async ([FromBody] AccountDTO newAccount, IAccountService accountService, IValidator<AccountDTO> validator) =>
{
    var validationResult = await validator.ValidateAsync(newAccount);

    if (!validationResult.IsValid)
    {
        app.Logger.LogWarning($"Account {newAccount} is not valid");
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    try
    {
        app.Logger.LogInformation($"Creating a new account: {newAccount.Name}");
        AccountDTO account = await accountService.CreateAccountAsync(newAccount);

        app.Logger.LogInformation($"Creating a new account: {account.Name} with ID {account.Id}");
        return Results.Created($"/account/{account.Id}", account);
    }
    catch (DbUpdateException ex)
    {
        app.Logger.LogWarning(ex, $"Problem creating account {newAccount.Name}");
        return Results.BadRequest((ex.InnerException ?? ex).Message);
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem creating account {newAccount.Name}");
        throw;
    }
})
.WithName("CreateAccount")
.WithTags(new[] { "Account" })
.Produces<AccountDTO>(StatusCodes.Status201Created)
.ProducesProblem(StatusCodes.Status400BadRequest)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapGet("/account/{accountId}/transaction", async ([FromRoute] int accountId, ITransactionService transactionService) =>
{
    app.Logger.LogDebug($"Requested transactions from account with ID {accountId}");
    try
    {
        List<TransactionDTO> transactions = (await transactionService.GetTransactionsByAccountIdAsync(accountId)).ToList();
        app.Logger.LogDebug($"Returning {transactions.Count} transactions");

        return Results.Ok(transactions);
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem retrieving transactions ffrom account with ID {accountId}");
        throw;
    }
})
.WithName("GetTransactionsByAccountId")
.WithTags(new[] { "Account" })
.Produces<TransactionDTO[]>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapPut("/account/{id}", async ([FromRoute] int id, [FromBody] AccountDTO account, IAccountService accountService, IValidator<AccountDTO> validator) =>
{
    var validationResult = await validator.ValidateAsync(account);

    if (!validationResult.IsValid)
    {
        app.Logger.LogWarning($"Account {account} is not valid");
        return Results.ValidationProblem(validationResult.ToDictionary());
    } 

    app.Logger.LogDebug($"Updating account {account.Name} with id {id}");
    if (id != account.Id)
    {
        app.Logger.LogInformation($"Requested Id {id} did not match request data {account.Id}");
        return Results.BadRequest($"Requested Id {id} did not match request data {account.Id}");
    }

    try
    {
        var updatedAccount = await accountService.UpdateAccountAsync(account);
        app.Logger.LogInformation($"Updated account {account.Name} with id {id}");
        return Results.Ok(updatedAccount);
    }
    catch (ItemNotFoundException ex)
    {
        app.Logger.LogWarning(ex, $"Unable to find account with id {id}");
        return Results.NotFound();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem updating account with id {id}");
        throw;
    }
})
.WithName("UpdateAccount")
.WithTags(new[] { "Account" })
.Produces<AccountDTO>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.ProducesProblem(StatusCodes.Status400BadRequest)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapDelete("/account/{id}", async ([FromRoute] int id, IAccountService accountService) =>
{
    app.Logger.LogDebug($"Deleting account with id {id}");

    try
    {
        var deletedAccount = await accountService.DeleteAccountAsync(id);
        app.Logger.LogInformation($"Deleted account {deletedAccount.Name} with id {id}");
        return Results.Ok(deletedAccount);
    }
    catch (ItemNotFoundException ex)
    {
        app.Logger.LogWarning(ex, $"Unable to find account with id {id}");
        return Results.NotFound();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem updating account with id {id}");
        throw;
    }
})
.WithName("DeleteAccount")
.WithTags(new[] { "Account" })
.Produces<AccountDTO>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapGet("/account/{accountId}/transaction/sync", async ([FromRoute] int accountId, ITransactionService transactionService, IAccountService accountService, IBankAPIHttpClient bankAPIHttpClient) =>
{
    try
    {
        app.Logger.LogDebug("Getting 90 days transactions from bank account.");

        try
        {
            AccountDTO account = await accountService.GetAccountAsync(accountId);

            if (string.IsNullOrEmpty(account.BankAccountId)) 
            {
                app.Logger.LogWarning($"Account with Id {accountId} does not have a bank account id");
                return Results.BadRequest($"Account with Id {accountId} does not have a bank account id");
            }

            List<BankAPITransaction>? allBankTransactions = await bankAPIHttpClient.SyncAccount(account.BankAccountId);

            int count = await transactionService.SyncTransactionsfromBankAsync(accountId, allBankTransactions!);

            app.Logger.LogDebug($"Synced {count} transactions from bank account {account.BankAccountId} to account {accountId}");

            return Results.Ok(count);
        }
        catch (ItemNotFoundException ex)
        {
            app.Logger.LogWarning(ex, $"Unable to find account with id {accountId}");
            return Results.NotFound();
        }
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "Problem retrieving all transactions");
        throw;
    }

})
.WithName("SyncBankTransactions")
.WithTags(new[] { "Account" })
.Produces<int>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.ProducesProblem(StatusCodes.Status500InternalServerError);

#endregion

#region Currency
app.MapGet("/currency", async (ICurrencyService currencyService) =>
{
    try
    {
        app.Logger.LogDebug("Requesting all currencies.");

        List<CurrencyDTO> allCurrencies = (await currencyService.GetAllCurrenciesAsync()).ToList();

        app.Logger.LogDebug($"Returning {allCurrencies.Count} currencies");

        return Results.Ok(allCurrencies);
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "Problem retrieving all currencies");
        throw;
    }

})
.WithName("GetAllCurrencies")
.WithTags(new[] { "Currency" })
.Produces<CurrencyDTO[]>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapGet("/currency/{id:int}", async ([FromRoute] int id, ICurrencyService currencyService) =>
{
    app.Logger.LogDebug($"Requested currency with ID {id}");
    try
    {
        CurrencyDTO currency = await currencyService.GetCurrencyByIdAsync(id);
        app.Logger.LogDebug($"Found {currency.Name} with ID {id}");

        return Results.Ok(currency);
    }
    catch (ItemNotFoundException)
    {
        app.Logger.LogInformation($"Was not able to find currency with ID {id}");
        return Results.NotFound();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem retrieving currency with ID {id}");
        throw;
    }
})
.WithName("GetCurrencyById")
.WithTags(new[] { "Currency" })
.Produces<CurrencyDTO>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapGet("/currency/{code}", async ([FromRoute] string code, ICurrencyService currencyService) =>
{
    app.Logger.LogDebug($"Requested currency with code {code}");
    try
    {
        CurrencyDTO currency = await currencyService.GetCurrencyByCodeAsync(code);
        app.Logger.LogDebug($"Found {currency.Name} with code {code}");

        return Results.Ok(currency);
    }
    catch (ItemNotFoundException)
    {
        app.Logger.LogInformation($"Was not able to find currency with code {code}");
        return Results.NotFound();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem retrieving currency with code {code}");
        throw;
    }
})
.WithName("GetCurrencyByCode")
.WithTags(new[] { "Currency" })
.Produces<CurrencyDTO>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.ProducesProblem(StatusCodes.Status500InternalServerError);

#endregion

#region Transaction

app.MapGet("/transaction", async (ITransactionService transactionService) =>
{
    try
    {
        app.Logger.LogDebug("Requesting all transactions.");

        List<TransactionDTO> allTransactions = (await transactionService.GetAllTransactionAsync()).ToList();

        app.Logger.LogDebug($"Returning {allTransactions.Count} transactions");

        return Results.Ok(allTransactions);
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "Problem retrieving all transactions");
        throw;
    }

})
.WithName("GetAllTransactions")
.WithTags(new[] { "Transaction" })
.Produces<TransactionDTO[]>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapGet("/transaction/{id}", async ([FromRoute] int id, ITransactionService transactionService) =>
{
    app.Logger.LogDebug($"Requested transaction with ID {id}");
    try
    {
        TransactionDTO transaction = await transactionService.GetTransactionByIdAsync(id);
        app.Logger.LogDebug($"Found transaction with ID {id}");

        return Results.Ok(transaction);
    }
    catch (ItemNotFoundException)
    {
        app.Logger.LogInformation($"Was not able to find transactiopn with ID {id}");
        return Results.NotFound();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem retrieving transactiopn with ID {id}");
        throw;
    }
})
.WithName("GetTransactionById")
.WithTags(new[] { "Transaction" })
.Produces<TransactionDTO>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapPost("/transaction", async ([FromBody] TransactionDTO newTransaction, ITransactionService transactionService, IValidator<TransactionDTO> validator) =>
{
    var validationResult = await validator.ValidateAsync(newTransaction);

    if (!validationResult.IsValid)
    {
        app.Logger.LogWarning($"Transaction {newTransaction} is not valid");
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    try
    {
        app.Logger.LogInformation($"Creating a new transaction: {newTransaction.Description}, {newTransaction.Date} on account {newTransaction.AccountId}");
        TransactionDTO transaction = await transactionService.CreateTransactionAsync(newTransaction);

        app.Logger.LogInformation($"Created a new transaction: {newTransaction.Description}, {newTransaction.Date} on account {newTransaction.AccountId}");
        return Results.Created($"/transaction/{transaction.Id}", transaction);
    }
    catch (DbUpdateException ex)
    {
        app.Logger.LogWarning(ex, $"Problem creating transaction {newTransaction.Description}");
        return Results.BadRequest((ex.InnerException ?? ex).Message);
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem creating transaction {newTransaction.Description}");
        throw;
    }
})
.WithName("CreateTransaction")
.WithTags(new[] { "Transaction" })
.Produces<TransactionDTO>(StatusCodes.Status201Created)
.ProducesProblem(StatusCodes.Status400BadRequest)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapPut("/transaction/{id}", async ([FromRoute] int id, [FromBody] TransactionDTO transaction, ITransactionService transactionService, IValidator<TransactionDTO> validator) =>
{
    var validationResult = await validator.ValidateAsync(transaction);

    if (!validationResult.IsValid)
    {
        app.Logger.LogWarning($"Transaction {transaction} is not valid");
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    app.Logger.LogDebug($"Updating transaction with id {id}");
    if (id != transaction.Id)
    {
        app.Logger.LogInformation($"Requested Id {id} did not match request data {transaction.Id}");
        return Results.BadRequest($"Requested Id {id} did not match request data {transaction.Id}");
    }

    try
    {
        var updatedTransaction = await transactionService.UpdateTransactionAsync(transaction);
        app.Logger.LogInformation($"Updated transaction with id {id}");
        return Results.Ok(updatedTransaction);
    }
    catch (OperationNotAllowedException ex)
    {
        app.Logger.LogWarning(ex, $"Transaction with Id {id} cannot be updated since is a synced transaction");
        return Results.BadRequest($"Transaction with Id {id} cannot be updated since is a synced transaction");
    }
    catch (ItemNotFoundException ex)
    {
        app.Logger.LogWarning(ex, $"Unable to find transaction with id {id}");
        return Results.NotFound();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem updating transaction with id {id}");
        throw;
    }
})
.WithName("UpdateTransaction")
.WithTags(new[] { "Transaction" })
.Produces<TransactionDTO>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.ProducesProblem(StatusCodes.Status400BadRequest)
.ProducesProblem(StatusCodes.Status500InternalServerError);

app.MapDelete("/transaction/{id}", async ([FromRoute] int id, ITransactionService transactionService) =>
{
    app.Logger.LogDebug($"Deleting transaction with id {id}");

    try
    {
        var deletedTransaction = await transactionService.DeleteTransactionAsync(id);
        app.Logger.LogInformation($"Deleted transaction with id {id}");
        return Results.Ok(deletedTransaction);
    }
    catch (OperationNotAllowedException ex)
    {
        app.Logger.LogWarning(ex, $"Transaction with Id {id} cannot be deleted since is a synced transaction");
        return Results.BadRequest($"Transaction with Id {id} cannot be deleted since is a synced transaction");
    }
    catch (ItemNotFoundException ex)
    {
        app.Logger.LogWarning(ex, $"Unable to find account with id {id}");
        return Results.NotFound();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, $"Problem updating account with id {id}");
        throw;
    }
})
.WithName("DeleteTransaction")
.WithTags(new[] { "Transaction" })
.Produces<TransactionDTO>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status404NotFound)
.ProducesProblem(StatusCodes.Status500InternalServerError);


#endregion

app.Run();