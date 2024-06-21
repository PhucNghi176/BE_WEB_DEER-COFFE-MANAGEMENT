using FluentValidation;

namespace DeerCoffeeShop.Application.Shift.Create
{
    public class CreateShiftCommandValidator : AbstractValidator<CreateShiftCommand>
    {
        public CreateShiftCommandValidator()
        {
            Configure();
        }

        public void Configure()
        {
            RuleFor(x => x.shift_end)
                 .GreaterThan(x => x.shift_start)
                 .WithMessage("Shift end must later than shift start!");

            RuleFor(x => x.shift_start)
                .NotNull()
                .NotEmpty()
                .WithMessage("Shift's start times must not be empty!");

            RuleFor(x => x.shift_end)
                .NotNull()
                .NotEmpty()
                .WithMessage("Shift's end times must not be empty!");

            RuleFor(x => x.shift_name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Shift's name must not be empty!");

            RuleFor(x => x.shift_description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Shift's description must not be empty!");
        }
    }
}
