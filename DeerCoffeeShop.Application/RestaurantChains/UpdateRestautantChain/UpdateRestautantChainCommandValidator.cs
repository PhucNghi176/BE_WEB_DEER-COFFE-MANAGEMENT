using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.RestaurantChains.UpdateRestautantChain
{
    public class UpdateRestautantChainCommandValidator : AbstractValidator<UpdateRestautantChainCommand>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public UpdateRestautantChainCommandValidator(IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantChainRepository = restaurantChainRepository;
            configuraValidatorRule();
        }
        private void configuraValidatorRule()
        {
            _ = RuleFor(x => x.resChainID)
                .NotEmpty().NotNull().WithMessage("please chose an restaurantChain");
        }
    }
}
