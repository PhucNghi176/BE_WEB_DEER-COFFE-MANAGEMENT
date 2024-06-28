using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestautantChainByName
{
    public class GetRestautantChainByNameQueryValidator : AbstractValidator<GetRestautantChainByNameQuery>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public GetRestautantChainByNameQueryValidator(IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantChainRepository = restaurantChainRepository;
            configuraValidatorRule();
        }
        private void configuraValidatorRule()
        {
            _ = RuleFor(x => x.pageNumber)
                .NotEmpty().NotNull().WithMessage("please fill in pageNumber");
            _ = RuleFor(x => x.pageSize)
                .NotEmpty().NotNull().WithMessage("please fill in pageSize");
        }
    }
}
