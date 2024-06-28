using FluentValidation;

namespace DeerCoffeeShop.Application.Shift.Delete
{
    public class DeleteShiftCommandValidator : AbstractValidator<DeleteShiftCommand>
    {
        public DeleteShiftCommandValidator()
        {
            Configure();
        }

        public void Configure()
        {
            _ = RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Shift must not be empty!");
        }
    }
}
