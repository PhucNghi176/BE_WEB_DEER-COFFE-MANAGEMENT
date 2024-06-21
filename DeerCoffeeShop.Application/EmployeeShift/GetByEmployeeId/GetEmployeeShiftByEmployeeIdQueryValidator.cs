using FluentValidation;

namespace DeerCoffeeShop.Application.EmployeeShift.GetByEmployeeId
{
    public class GetEmployeeShiftByEmployeeIdQueryValidator : AbstractValidator<GetEmployeeShiftByEmployeeIdQuery>
    {
        public GetEmployeeShiftByEmployeeIdQueryValidator()
        {
            Configure();
        }

        public void Configure()
        {
            RuleFor(x => x.EmployeeId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Employee must be not empty!");
        }
    }
}
