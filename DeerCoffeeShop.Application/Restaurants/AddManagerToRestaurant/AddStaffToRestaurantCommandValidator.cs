using DeerCoffeeShop.Domain.Repositories;
using FluentValidation;

namespace DeerCoffeeShop.Application.Restaurants.AddManagerToRestaurant
{
    public class AddStaffToRestaurantCommandValidator : AbstractValidator<AddManagerToRestaurantCommand>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public AddStaffToRestaurantCommandValidator(IRestaurantRepository restaurantRepository, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _restaurantRepository = restaurantRepository;
            configuraValidatorRule();
        }
        private void configuraValidatorRule()
        {
            RuleFor(x => x.ManagerID).NotEmpty().NotNull().WithMessage("please chose staff.");
            RuleFor(x => x.resID).NotEmpty().NotNull().WithMessage("please chose restaurant.");
        }
    }
}
