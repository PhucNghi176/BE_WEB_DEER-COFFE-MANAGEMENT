using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.Restaurants.InactiveRestaurant
{
    public class InactiveRestaurantCommandValidator : AbstractValidator<InactiveRestaurantCommand>
    {
        private readonly IRestaurantRepository repository;
        public InactiveRestaurantCommandValidator(IRestaurantRepository repository)
        {
            this.repository = repository;
            RuleFor(x => x.ID).NotEmpty().NotNull().WithMessage("Please chose a restaurant");
        }
    }
}
