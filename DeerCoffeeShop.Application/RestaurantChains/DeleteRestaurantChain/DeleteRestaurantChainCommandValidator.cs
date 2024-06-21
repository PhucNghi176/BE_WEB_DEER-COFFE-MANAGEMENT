using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.RestaurantChains.DeleteRestaurantChain
{
    public class DeleteRestaurantChainCommandValidator : AbstractValidator<DeleteRestaurantChainCommand>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public DeleteRestaurantChainCommandValidator(IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantChainRepository = restaurantChainRepository;
            configureValidatorRule();
        }
        private void configureValidatorRule()
        {
            RuleFor(x => x.resChainID)
                .NotEmpty().NotNull().WithMessage("please chose restaurantChain");
        }

    }
}
