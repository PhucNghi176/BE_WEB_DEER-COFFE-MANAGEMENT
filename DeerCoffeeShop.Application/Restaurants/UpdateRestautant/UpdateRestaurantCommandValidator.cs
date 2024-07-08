using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.Restaurants.UpdateRestautant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public UpdateRestaurantCommandValidator(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
            configuraValidatorRule();
        }
        private void configuraValidatorRule()
        {
            _ = RuleFor(x => x.resID)
                .NotEmpty().NotNull().WithMessage("please chose a restaurant during updating.");
        }
    }
}
