using FluentValidation;

namespace DeerCoffeeShop.Application.EmployeeShift.GetAll
{
    public class GetAllEmployeeShiftQueryValidator : AbstractValidator<GetAllEmployeeShiftQuery>
    {
        public GetAllEmployeeShiftQueryValidator()
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

            _ = RuleFor(x => x.RestaurantId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Restaurant must not be null!");
        }
    }
}
