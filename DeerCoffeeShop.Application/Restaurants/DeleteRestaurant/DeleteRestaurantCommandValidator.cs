using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.Restaurants.DeleteRestaurant
{
    public class DeleteRestaurantCommandValidator : AbstractValidator<DeleteRestaurantCommand>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public DeleteRestaurantCommandValidator(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
            configuraValidatorRule();
        }
        private void configuraValidatorRule()
        {
            _ = RuleFor(x => x.resID)
                .NotEmpty().NotNull().WithMessage("please chose a restaurant.");
        }

    }
}
