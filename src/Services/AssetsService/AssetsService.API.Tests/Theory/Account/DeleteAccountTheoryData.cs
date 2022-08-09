namespace Tiberna.MyFinancePal.AssetsService.API.Tests.Theory.Account;

public class DeleteAccountTheoryData : TheoryData<AccountDTO>
{
    public DeleteAccountTheoryData()
    {
        Add(new AccountDTO()
        {
            Id = 30,
            Name = "Delete Account 1",
            BankAccountId = "789456123",
            Description = "Update Account description",
            InitialBalance = 5000,
            InitialBalanceDate = DateTime.UtcNow,
            IsActive = true
        });

        Add(new AccountDTO()
        {
            Id = 31,
            Name = "Delete Account 1",
            Description = "Update Account description",
            InitialBalance = -5000,
            InitialBalanceDate = DateTime.UtcNow,
            IsActive = false
        });
    }
}