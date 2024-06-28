using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByAdmin
{
    public class GetRestaurantChainByAdminQueryValidator : AbstractValidator<GetRestaurantChainByAdminQuery>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public GetRestaurantChainByAdminQueryValidator(IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantChainRepository = restaurantChainRepository;
            configureValidatorRule();
        }
        private void configureValidatorRule()
        {
            _ = RuleFor(x => x.pageNumber)
                .NotEmpty().NotNull().WithMessage("please fill in pageNumber");
            _ = RuleFor(x => x.pageSize)
                .NotEmpty().NotNull().WithMessage("please fill in pageSize");
            _ = RuleFor(x => x.adminID)
                .NotNull().NotEmpty().WithMessage("please chose a admin");
        }
    }
}
