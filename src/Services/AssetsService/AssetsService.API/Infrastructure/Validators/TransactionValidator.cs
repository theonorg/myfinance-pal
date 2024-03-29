using FluentValidation;

namespace Tiberna.MyFinancePal.AssetsService.API.Infrastructure.Validators;

public class TransactionValidator : AbstractValidator<TransactionDTO>
{
    public TransactionValidator()
    {
        RuleFor(x => x.Amount).ScalePrecision(2, 10).WithMessage("Amount_With_Precision_10_Scale_2");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description_MaxLength_500");
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("AccountId_Required");
        RuleFor(x => x.CurrencyId).NotEmpty().WithMessage("CurrencyId_Required");
        RuleFor(x => x.Date).NotEmpty().LessThanOrEqualTo(DateTime.Now).WithMessage("Date_Required");
    }
}