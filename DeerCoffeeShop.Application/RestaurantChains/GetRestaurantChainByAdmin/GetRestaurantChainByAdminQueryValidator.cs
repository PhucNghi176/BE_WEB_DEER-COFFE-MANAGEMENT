using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RuleFor(x => x.pageNumber)
                .NotEmpty().NotNull().WithMessage("please fill in pageNumber");
            RuleFor(x => x.pageSize)
                .NotEmpty().NotNull().WithMessage("please fill in pageSize");
            RuleFor(x => x.adminID)
                .NotNull().NotEmpty().WithMessage("please chose a admin");
        }
    }
}
