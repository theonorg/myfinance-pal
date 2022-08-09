namespace Tiberna.MyFinancePal.AssetsService.API.Tests.Theory.Account;

public class CreateBadAccountTheoryData : TheoryData<AccountDTO>
{
    public CreateBadAccountTheoryData()
    {
        // Error: without name
        Add(new AccountDTO()
        {
            Id = 30,
            BankAccountId = "789456123",
            Description = "UT Account description",
            InitialBalance = 5000,
            InitialBalanceDate = DateTime.Now,
            IsActive = true
        });

        // Error: name bigger than 200 characters
        Add(new AccountDTO()
        {
            Id = 30,
            Name = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                Morbi porta placerat nisl, sit amet venenatis sem vulputate ac.
                Maecenas at consectetur libero, sed convallis justo. 
                Suspendisse vulputate, lectus vel gravida varius, odio ex condimentum dolor.",
            Description = "UT Account 2 description",
            InitialBalance = -5000,
            InitialBalanceDate = DateTime.Now,
            IsActive = false
        });

        // Error: name smaller than 2 characters
        Add(new AccountDTO()
        {
            Id = 30,
            Name = @"A",
            Description = "UT Account 2 description",
            InitialBalance = -5000,
            InitialBalanceDate = DateTime.Now,
            IsActive = false
        });

        // Error: description bigger than 500 characters
        Add(new AccountDTO()
        {
            Id = 30,
            Name = @"Account",
            Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
                Sed leo metus, porttitor volutpat vulputate ac, lacinia sed magna. 
                Integer elementum ipsum sit amet leo auctor suscipit sit amet sed nibh. 
                Etiam dictum venenatis dolor, eu pulvinar tortor bibendum ultricies. 
                Fusce vel ultrices tellus, ultricies imperdiet ante. 
                Sed lacinia sit amet libero nec mattis. 
                Nunc sollicitudin placerat nibh id bibendum. 
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla facilisi. 
                Ut congue eu est eu malesuada. Curabitur vulputate viverra ex, eu aliquam.",
            InitialBalance = -5000,
            InitialBalanceDate = DateTime.Now,
            IsActive = false
        });

        // Error: initialBalance null
        Add(new AccountDTO()
        {
            Id = 30,
            Name = @"Account",
            Description = @"Good description",
            InitialBalanceDate = DateTime.Now,
            IsActive = false
        });

        // Error: initialBalanceDate null
        Add(new AccountDTO()
        {
            Id = 30,
            Name = @"Account",
            Description = @"Good description",
            InitialBalance = -5000,
            IsActive = false
        });

        // Error: initialBalanceDate null
        Add(new AccountDTO()
        {
            Id = 30,
            Name = @"Account",
            Description = @"Good description",
            InitialBalance = -5000,
            IsActive = false
        });

        // Error: initialBalanceDate bigger than today
        Add(new AccountDTO()
        {
            Id = 30,
            Name = @"Account",
            Description = @"Good description",
            InitialBalance = -5000,
            InitialBalanceDate = DateTime.Now.AddDays(10),
            IsActive = false
        });
    }
}