namespace Tiberna.MyFinancePal.AssetsService.API.Infrastructure.Validators;

public class InvestmentAccountTypeValidator : AbstractValidator<InvestmentAccountTypeDTO>
{
    public InvestmentAccountTypeValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name_Required");
        RuleFor(x => x.Name).MaximumLength(200).WithMessage("Name_MaxLength_200");
        RuleFor(x => x.Name).MinimumLength(2).WithMessage("Name_MinLength_2");
    }
}