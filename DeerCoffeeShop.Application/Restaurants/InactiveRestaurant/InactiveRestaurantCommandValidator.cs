using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.Restaurants.InactiveRestaurant
{
    public class InactiveRestaurantCommandValidator : AbstractValidator<InactiveRestaurantCommand>
    {
        private readonly IRestaurantRepository repository;
        public InactiveRestaurantCommandValidator(IRestaurantRepository repository)
        {
            this.repository = repository;
            _ = RuleFor(x => x.ID).NotEmpty().NotNull().WithMessage("Please chose a restaurant");
        }
    }
}
