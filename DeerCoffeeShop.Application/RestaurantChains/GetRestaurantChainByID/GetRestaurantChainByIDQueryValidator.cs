using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RuleFor(x => x.resChainID)
                .NotEmpty().NotNull().WithMessage("please fill in an restaurantChainID");
        }
    }
}
