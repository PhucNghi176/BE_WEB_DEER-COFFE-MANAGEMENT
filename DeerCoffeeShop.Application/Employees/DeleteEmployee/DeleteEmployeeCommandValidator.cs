using FluentValidation;

namespace DeerCoffeeShop.Application.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            Configure();
        }

        public void Configure()
        {
            RuleFor(x => x.EmployeeID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Employee must not be empty!");
        }
    }
}
