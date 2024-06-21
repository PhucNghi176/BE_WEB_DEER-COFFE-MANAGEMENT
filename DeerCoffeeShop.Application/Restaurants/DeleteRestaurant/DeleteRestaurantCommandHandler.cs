using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, string>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICurrentUserService _currentUserService;
        public DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IEmployeeRepository employeeRepository, ICurrentUserService currentUserService)
        {
            _restaurantRepository = restaurantRepository;
            _employeeRepository = employeeRepository;
            _currentUserService = currentUserService;
        }
        public DeleteRestaurantCommandHandler() { }
        public async Task<string> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var restaurant = await _restaurantRepository.FindAsync(x => x.ID.Equals(request.resID) && !x.IsDeleted, cancellationToken) ?? throw new NotFoundException($"Not found restaurant ID: {request.resID}");
                //var manager = await this._employeeRepository.FindAsync(x => x.ID.Equals(restaurant.ManagerID),cancellationToken);
                var listStaff = await _employeeRepository.FindAllAsync(x => x.ManagerID.Equals(restaurant.ManagerID), cancellationToken);
                foreach (var employee in listStaff)
                {
                    employee.ManagerID = null;
                    _employeeRepository.Update(employee);
                }
                await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                restaurant.NgayXoa = DateTime.UtcNow;
                restaurant.NguoiXoaID = _currentUserService.UserId;
                restaurant.IsDeleted = true;
                _restaurantRepository.Update(restaurant);
                await _restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return $"success delete restaurant ID : {request.resID}";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
