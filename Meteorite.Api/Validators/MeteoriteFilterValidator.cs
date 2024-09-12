using FluentValidation;
using Meteorite.Api.Models;

namespace Meteorite.Api.Validators
{
    public class MeteoriteFilterValidator : AbstractValidator<MeteoriteFilter>
    {
        public MeteoriteFilterValidator()
        {
            RuleFor(x => x.YearFrom)
                .GreaterThanOrEqualTo(0).When(x => x.YearFrom != null).WithMessage("Value of YearFrom can't be negative")
                .LessThanOrEqualTo(DateTime.Now.Year).When(x => x.YearFrom != null).WithMessage("Year should be less that current.");
            RuleFor(x => x.YearTo)
                .GreaterThanOrEqualTo(0).When(x => x.YearTo != null).WithMessage("Value of YearTo can't be negative")
                .LessThanOrEqualTo(DateTime.Now.Year).When(x => x.YearTo != null).WithMessage("Year should be less or equal to current.");
            RuleFor(x => x.YearFrom)
                .LessThanOrEqualTo(x => x.YearTo).When(x => x.YearFrom != null).WithMessage("Start of the date range should be less than end of the range.");
        }
    }
}
