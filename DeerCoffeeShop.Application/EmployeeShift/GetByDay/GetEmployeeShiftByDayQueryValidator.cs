using FluentValidation;

namespace DeerCoffeeShop.Application.EmployeeShift.GetByDay
{
    public class GetEmployeeShiftByDayQueryValidator : AbstractValidator<GetEmployeeShiftByDayQuery>
    {
        public GetEmployeeShiftByDayQueryValidator()
        {
            Configure();
        }

        public void Configure()
        {
            _ = RuleFor(x => x.PageNo)
                .NotNull()
                .NotEmpty()
                .WithMessage("Page number must not be null!");

            _ = RuleFor(x => x.PageSize)
                .NotNull()
                .NotEmpty()
                .WithMessage("Page size must not be null!");


            _ = RuleFor(x => x.DateOfWork)
                .NotNull()
                .NotEmpty()
                .WithMessage("Date of work must not be null!");
        }
    }
}
