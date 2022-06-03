using FluentValidation;

namespace InvestmentCalculator.Validators;

public class InvestmentValidator: AbstractValidator<Investment>
{
    public InvestmentValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Investment {PropertyName} must me a positive number");
        
        RuleFor(x => x.Years)
            .GreaterThan(0).WithMessage("Investment {PropertyName} must me a positive number");
        
        RuleFor(x => x.Rate)
            .InclusiveBetween(0, 1).WithMessage("Investment {PropertyName} must me a decimal number between 0 and 1");
        
        RuleFor(x => x.CalculationDate)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(x => x.AgreementDate).WithMessage("Investment {PropertyName} must be after agreement date")
            .LessThanOrEqualTo(x => x.AgreementDate.AddYears(x.Years)).WithMessage("Investment {PropertyName} must be before the end of the agreement");
    }
}