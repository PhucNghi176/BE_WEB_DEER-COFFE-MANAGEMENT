using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System.Data;

namespace DeerCoffeeShop.Application.RestaurantChains.CreateRestaurantChain
{
    public class CreateRestaurantChainCommandHandler : IRequestHandler<CreateRestaurantChainCommand, string>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public CreateRestaurantChainCommandHandler(IRestaurantChainRepository restaurantChainRepository, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _restaurantChainRepository = restaurantChainRepository;
        }

        public async Task<string> Handle(CreateRestaurantChainCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RestaurantChain? restaurantChain = await this._restaurantChainRepository.FindAsync(x => x.RestaurantChainName.Equals(request.RestaurantChainName)
                                                                                        && x.RestaurantChainType.Equals(request.RestaurantChainType)
                                                                                        && x.RestaurantChainHQAddress.Equals(request.RestaurantChainHQAddress)
                                                                                        , cancellationToken);
                if (restaurantChain != null)
                    throw new DuplicateNameException($"restaurantChain existed ID is {restaurantChain.ID}");

                if ((await this._employeeRepository.FindAsync(x => x.ID.Equals(request.RestaurantChain_AdminID), cancellationToken)).RoleID != 1)
                    throw new NotFoundException($"Not found admin by ID {request.RestaurantChain_AdminID}");

                this._restaurantChainRepository.Add(new RestaurantChain()
                {
                    RestaurantChainHQAddress = request.RestaurantChainHQAddress,
                    RestaurantChainName = request.RestaurantChainName,
                    RestaurantChainTotalBranches = 0,
                    RestaurantChainTotalEmployees = 0,
                    RestaurantChainType = request.RestaurantChainType,
                    RestaurantChain_AdminID = request.RestaurantChain_AdminID,
                });
                _ = await this._restaurantChainRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return $"Successfully created RestaurantChain Name {request.RestaurantChainName}";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
