namespace Tiberna.MyFinancePal.AssetsService.API.Infrastructure.Validators;

public class InvestmentAccountValidator : AbstractValidator<InvestmentAccountDTO>
{
    public InvestmentAccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name_Required");
        RuleFor(x => x.Name).MaximumLength(200).WithMessage("Name_MaxLength_200");
        RuleFor(x => x.Name).MinimumLength(2).WithMessage("Name_MinLength_2");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description_MaxLength_500");
        RuleFor(x => x.InitialBalance).NotEmpty().WithMessage("InitialBalance_Required");
        RuleFor(x => x.InitialBalanceDate).NotEmpty().WithMessage("InitialBalanceDate_Required");
        RuleFor(x => x.InitialBalanceDate).LessThanOrEqualTo(DateTime.Now).WithMessage("InitialBalanceDate_NotInThePast");
        RuleFor(x => x.InvestedBalance).NotEmpty().WithMessage("InvestedBalance_Required");
        RuleFor(x => x.AvailableBalance).NotEmpty().WithMessage("AvailableBalance_Required");
    }
}