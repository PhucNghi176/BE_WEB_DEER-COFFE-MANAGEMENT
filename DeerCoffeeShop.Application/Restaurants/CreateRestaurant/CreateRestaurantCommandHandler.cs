using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, string>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ICurrentUserService currentUserService, IRestaurantChainRepository restaurantChainRepository, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _restaurantChainRepository = restaurantChainRepository;
            _restaurantRepository = restaurantRepository;
            _currentUserService = currentUserService;
            _employeeRepository = employeeRepository;
        }
        public async Task<string> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {

            if (await _restaurantRepository.FindAsync(x => x.RestaurantName.Equals(request.RestaurantName) && x.RestaurantChainID.Equals(request.RestaurantChainID), cancellationToken) != null)
                throw new NotFoundException("dulicate restaurant name in this restaurantChain.");
            RestaurantChain? resChain = await _restaurantChainRepository.FindAsync(x => x.ID.Equals(request.RestaurantChainID), cancellationToken);
            if (resChain == null)
                throw new NotFoundException("Not found restaurantChain that had been chosen.");

            Employee? emp = await _employeeRepository.FindAsync(x => x.ID.Equals(request.ManagerID), cancellationToken);
            if (emp.RoleID == 2) //2 là manager đúng hong ta :vv
                throw new NotFoundException("Not found manager had been chosen.");
            if (await _restaurantRepository.AnyAsync(x => x.ManagerID.Equals(request.ManagerID), cancellationToken))
                throw new NotFoundException("Manager had been chosen is manager of another restaurant.");
            resChain.RestaurantChainTotalBranches += 1;
            resChain.RestaurantChainTotalEmployees += 1;
            _restaurantChainRepository.Update(resChain);
            _ = await _restaurantChainRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            Restaurant restaurant = new()
            {

                IsDeleted = false,
                ManagerID = request.ManagerID,
                NguoiTaoID = _currentUserService.UserId,
                NgayTao = DateTime.UtcNow,
                RestaurantAddress = request.RestaurantAddress,
                RestaurantChainID = request.RestaurantChainID,
                RestaurantName = request.RestaurantName,
                TotalEmployees = 1,
            };
            _restaurantRepository.Add(restaurant);
            _ = await _restaurantRepository.UnitOfWork.SaveChangesAsync();

            return $"Create new restaurantName: {request.RestaurantName} of restaurantChain: {(await _restaurantChainRepository.FindAsync(x => x.ID.Equals(request.RestaurantChainID), cancellationToken)).RestaurantChainName} successful.";

        }
    }
}
