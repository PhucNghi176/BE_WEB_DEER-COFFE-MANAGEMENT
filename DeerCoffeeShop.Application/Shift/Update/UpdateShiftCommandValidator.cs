using FluentValidation;

namespace DeerCoffeeShop.Application.Shift.Update
{
    public class UpdateShiftCommandValidator : AbstractValidator<UpdateShiftCommand>
    {
        public UpdateShiftCommandValidator()
        {
            Configure();
        }

        public void Configure()
        {
            _ = RuleFor(x => x.shift_end)
                 .GreaterThan(x => x.shift_start)
                 .WithMessage("Shift end must later than shift start!");

            _ = RuleFor(x => x.shift_start)
                .NotNull()
                .NotEmpty()
                .WithMessage("Shift's start times must not be empty!");

            _ = RuleFor(x => x.shift_end)
                .NotNull()
                .NotEmpty()
                .WithMessage("Shift's end times must not be empty!");
        }
    }
}
