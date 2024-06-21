using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.RestaurantChains.InactiveRestaurantChain
{
    public class InactiveRestaurantChainCommandValidator : AbstractValidator<InactiveRestaurantChainCommand>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public InactiveRestaurantChainCommandValidator(IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantChainRepository = restaurantChainRepository;
            RuleFor(x => x.ID).NotEmpty().NotNull().WithMessage("Please chose restaurantChain.");
        }
    }
}
