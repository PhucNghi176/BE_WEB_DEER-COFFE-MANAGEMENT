using FluentValidation;

namespace DeerCoffeeShop.Application.EmployeeShift.UpdateEmployeeShift
{
    public class UpdateEmployeeShiftCommandValidator : AbstractValidator<UpdateEmployeeShiftCommand>
    {
        public UpdateEmployeeShiftCommandValidator()
        {
            Configure();
        }

        public void Configure()
        {
            _ = RuleFor(x => x.RestaurantID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Restaurant must not be empty!");

            _ = RuleFor(x => x.ShiftID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Shift must not be empty!");

            _ = RuleFor(x => x.DateOfWork)
                .NotEmpty()
                .NotNull()
                .WithMessage("Date of work must not be empty!");

            _ = RuleFor(x => x.EmployeeID)
              .NotEmpty()
              .NotNull()
              .WithMessage("Employee must not be empty!");
        }
    }
}
