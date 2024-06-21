using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.UpdateRestautant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, string>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IRestaurantChainRepository restaurantChainRepository, IEmployeeRepository employeeRepository)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantChainRepository = restaurantChainRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<string> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var restaurant = await _restaurantRepository.FindAsync(x => x.ID.Equals(request.resID) && !x.IsDeleted, cancellationToken) ?? throw new NotFoundException($"Not found restaurant had ID : {request.resID} or it had been deleted .");
                if ((await _employeeRepository.FindAsync(x => x.ID.Equals(request.manageID) && x.IsDeleted == false, cancellationToken)).RoleID != 2)
                    throw new NotFoundException($"Not found manager with ID {request.manageID}");
                restaurant.ManagerID = request.manageID ?? restaurant.ManagerID;
                if (request.resChainID != null)
                {
                    restaurant.RestaurantChainID = request.resChainID;
                    var curResChain = await _restaurantChainRepository.FindAsync(x => x.ID.Equals(restaurant.RestaurantChainID) && !x.IsDeleted, cancellationToken);
                    var newResChain = await _restaurantChainRepository.FindAsync(x => x.ID.Equals(request.resChainID) && !x.IsDeleted, cancellationToken);
                    curResChain.RestaurantChainTotalBranches -= 1;
                    curResChain.RestaurantChainTotalEmployees -= restaurant.TotalEmployees;
                    newResChain.RestaurantChainTotalBranches += 1;
                    newResChain.RestaurantChainTotalEmployees += restaurant.TotalEmployees;
                    _restaurantChainRepository.Update(newResChain);
                    _restaurantChainRepository.Update(curResChain);
                    await _restaurantChainRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    restaurant.RestaurantChainID = restaurant.RestaurantChainID;
                }
                restaurant.RestaurantAddress = request.resAddress ?? restaurant.RestaurantAddress;
                restaurant.RestaurantName = request.resName ?? restaurant.RestaurantName;
                _restaurantRepository.Update(restaurant);
                await _restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return $"Update restaurant ID {request.resID} successful.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
