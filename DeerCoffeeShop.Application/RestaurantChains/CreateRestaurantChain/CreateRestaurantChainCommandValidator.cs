using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.RestaurantChains.CreateRestaurantChain
{
    public class CreateRestaurantChainCommandValidator : AbstractValidator<CreateRestaurantChainCommand>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public CreateRestaurantChainCommandValidator(IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantChainRepository = restaurantChainRepository;
            configureValidatorRule();
        }
        private void configureValidatorRule()
        {
            _ = RuleFor(x => x.RestaurantChainHQAddress)
               .NotEmpty().NotNull().WithMessage("please fill in the restaurantChainHQAddress");
            _ = RuleFor(x => x.RestaurantChainName)
                .NotNull().NotEmpty().WithMessage("please fill in the restaurantChainName");
            _ = RuleFor(x => x.RestaurantChain_AdminID)
                .NotEmpty().NotNull().WithMessage("please chose a admin");
            _ = RuleFor(x => x.RestaurantChainType)
                .NotEmpty().NotNull().WithMessage("please chose a restaurnatType");
        }
    }
}
