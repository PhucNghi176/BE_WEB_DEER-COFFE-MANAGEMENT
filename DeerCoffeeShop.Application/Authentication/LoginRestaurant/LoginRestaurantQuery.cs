using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Authentication.LoginRestaurant;

public record LoginRestaurantQuery : IRequest<string>
{
    public string RetaurantID { get; set; }
    public string Password { get; set; }
}
internal class LoginRestaurantQueryHandler(IRestaurantRepository restaurantRepository, IEmployeeRepository employeeRepository) : IRequestHandler<LoginRestaurantQuery, string>
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<string> Handle(LoginRestaurantQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.Restaurant restaurant = await _restaurantRepository.FindAsync(_ => _.ID == request.RetaurantID, cancellationToken) ?? throw new NotFoundException("Restaurant not found");
        Domain.Entities.Employee? manager = await _employeeRepository.FindAsync(_ => _.ID == restaurant.ManagerID, cancellationToken);
        return manager == null
            ? throw new NotFoundException("Manager not found")
            : _employeeRepository.VerifyPassword(request.Password, manager.Password)
            ? restaurant.ID
            : throw new IncorrectPasswordException("Password is incorrect");
    }
}
