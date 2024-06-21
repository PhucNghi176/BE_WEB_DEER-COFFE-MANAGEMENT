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
            RuleFor(x => x.pageNumber)
                .NotEmpty().NotNull().WithMessage("please fill in pageNumber");
            RuleFor(x => x.pageSize)
                .NotEmpty().NotNull().WithMessage("please fill in pageSize");
        }
    }
}
