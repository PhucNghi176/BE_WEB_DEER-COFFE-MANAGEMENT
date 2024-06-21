using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.AddManagerToRestaurant
{
    public class AddManagerToRestaurantCommandHandler : IRequestHandler<AddManagerToRestaurantCommand, string>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public AddManagerToRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IEmployeeRepository employeeRepository, IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantRepository = restaurantRepository;
            _employeeRepository = employeeRepository;
            _restaurantChainRepository = restaurantChainRepository;
        }
        public async Task<string> Handle(AddManagerToRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Employee = await _employeeRepository.FindAsync(x => x.ID == request.ManagerID, cancellationToken) ?? throw new NotFoundException("Employee not found");
                var Restaurant = await _restaurantRepository.FindAsync(x => x.ID == request.resID, cancellationToken) ?? throw new NotFoundException("Restaurant not found");
                var isManager = await _restaurantRepository.AnyAsync(x => x.ManagerID == Employee.ID, cancellationToken);
                if (isManager)
                {
                    return ("Employee is already a manager of another restaurant");
                }
                var RestaurantChain = await _restaurantChainRepository.FindAsync(x => x.ID == Restaurant.RestaurantChainID, cancellationToken);
                if (RestaurantChain == null)
                {
                    throw new NotFoundException("Restaurant Chain not found");
                }
                if (Employee.RoleID != 2)
                {
                    return ("Employee is not a manager");
                }
                Restaurant.ManagerID = Employee.ID;
                _restaurantRepository.Update(Restaurant);
                await _restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return "Add manager to restaurant successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
