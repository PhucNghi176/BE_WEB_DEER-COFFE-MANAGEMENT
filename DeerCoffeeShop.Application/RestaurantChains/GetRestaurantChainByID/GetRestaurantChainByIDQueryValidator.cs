using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByID
{
    public class GetRestaurantChainByIDQueryValidator : AbstractValidator<GetRestaurantChainByIDQuery>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public GetRestaurantChainByIDQueryValidator(IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantChainRepository = restaurantChainRepository;
            confuguraValidatorRule();
        }
        private void confuguraValidatorRule()
        {
            _ = RuleFor(x => x.resChainID)
                .NotEmpty().NotNull().WithMessage("please fill in an restaurantChainID");
        }
    }
}
