using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.RestaurantChains.InactiveRestaurantChain
{
    public class InactiveRestaurantChainCommandValidator : AbstractValidator<InactiveRestaurantChainCommand>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public InactiveRestaurantChainCommandValidator(IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantChainRepository = restaurantChainRepository;
            _ = RuleFor(x => x.ID).NotEmpty().NotNull().WithMessage("Please chose restaurantChain.");
        }
    }
}
