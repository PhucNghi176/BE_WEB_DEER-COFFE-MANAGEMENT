using FluentValidation;

namespace DeerCoffeeShop.Application.EmployeeShift.Delete
{
    public class DeleteEmployeeShiftCommandValidator : AbstractValidator<DeleteEmployeeShiftCommand>
    {
        public DeleteEmployeeShiftCommandValidator()
        {
            Configure();
        }

        public void Configure()
        {
            _ = RuleFor(x => x.EmployeeID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Employee must not be empty!");

            _ = RuleFor(x => x.RestaurantID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Restaurant must not be empty!");

            _ = RuleFor(x => x.ShiftID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Shift must not be empty!");
        }
    }
}
