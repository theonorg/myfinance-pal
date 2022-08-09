namespace Tiberna.MyFinancePal.AssetsService.API.Tests.Theory.Account;

public class UpdateBadAccountTheoryData : TheoryData<AccountDTO>
{
    public UpdateBadAccountTheoryData()
    {
        Add(new AccountDTO()
        {
            Id = 20,
            Name = "Update Account 1",
            BankAccountId = "789456123",
            Description = "Update Account description",
            InitialBalance = 5000,
            InitialBalanceDate = DateTime.UtcNow,
            IsActive = true
        });

        Add(new AccountDTO()
        {
            Id = 21,
            Name = "Update Account 3",
            Description = "Update Account description",
            InitialBalance = -5000,
            InitialBalanceDate = DateTime.UtcNow,
            IsActive = false
        });
    }
}