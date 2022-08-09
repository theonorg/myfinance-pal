namespace Tiberna.MyFinancePal.AssetsService.API.Tests.Theory.Account;

public class CreateAccountTheoryData : TheoryData<AccountDTO>
{
    public CreateAccountTheoryData()
    {
        Add(new AccountDTO()
        {
            Id = 3,
            Name = "UT Account",
            BankAccountId = "789456123",
            Description = "UT Account description",
            InitialBalance = 5000,
            InitialBalanceDate = DateTime.UtcNow,
            IsActive = true
        });

        Add(new AccountDTO()
        {
            Id = 4,
            Name = "UT Account 2",
            Description = "UT Account 2 description",
            InitialBalance = -5000,
            InitialBalanceDate = DateTime.UtcNow,
            IsActive = false
        });
    }
}