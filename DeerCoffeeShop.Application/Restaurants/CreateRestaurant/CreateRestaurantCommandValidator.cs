using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.Restaurants.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public CreateRestaurantCommandValidator(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
            ConfiguraValidatorRule();
        }
        private void ConfiguraValidatorRule()
        {
            _ = RuleFor(x => x.RestaurantAddress)
                .NotEmpty().NotNull().WithMessage("please fill in a HQAdress.")
                .MaximumLength(300).WithMessage("the restairant was too long try again.");
            _ = RuleFor(x => x.RestaurantName)
                .NotEmpty().NotNull().WithMessage("please fill in a restaurant's name.")
                .MaximumLength(50).WithMessage("the restaurant's name was too long try again.");
            _ = RuleFor(x => x.ManagerID)
                .NotEmpty().NotNull().WithMessage("please chose your restaurant owner.");
            _ = RuleFor(x => x.RestaurantChainID)
                .NotEmpty().NotNull().WithMessage("please chose your restaurant chain owner.");
        }
    }
}
