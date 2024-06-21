using FluentValidation;


namespace DeerCoffeeShop.Application.Employees.GetEmployee
{
    public class GetEmployeeQueryValidator : AbstractValidator<GetEmployeeQuery>
    {
        public GetEmployeeQueryValidator()
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
