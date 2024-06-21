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
            RuleFor(x => x.RestaurantChainHQAddress)
               .NotEmpty().NotNull().WithMessage("please fill in the restaurantChainHQAddress");
            RuleFor(x => x.RestaurantChainName)
                .NotNull().NotEmpty().WithMessage("please fill in the restaurantChainName");
            RuleFor(x => x.RestaurantChain_AdminID)
                .NotEmpty().NotNull().WithMessage("please chose a admin");
            RuleFor(x => x.RestaurantChainType)
                .NotEmpty().NotNull().WithMessage("please chose a restaurnatType");
        }
    }
}
