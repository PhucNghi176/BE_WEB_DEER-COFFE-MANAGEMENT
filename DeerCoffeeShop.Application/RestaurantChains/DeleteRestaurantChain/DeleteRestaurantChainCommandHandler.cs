using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Restaurants.DeleteRestaurant;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.DeleteRestaurantChain
{
    public class DeleteRestaurantChainCommandHandler : IRequestHandler<DeleteRestaurantChainCommand, string>
    {
        private readonly ISender _metiator;
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteRestaurantChainCommandHandler(IRestaurantChainRepository restaurantChainRepository, ICurrentUserService currentUserService, 
                                                   IRestaurantRepository restaurantRepository, IEmployeeRepository employeeRepository,
                                                   ISender mediator)
        {
            _metiator = mediator;
            _currentUserService = currentUserService;
            _restaurantChainRepository = restaurantChainRepository;
            _restaurantRepository = restaurantRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<string> Handle(DeleteRestaurantChainCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resChain = await this._restaurantChainRepository.FindAsync(x => x.ID.Equals(request.resChainID) && x.IsDeleted == false, cancellationToken);
                if (resChain == null)
                    throw new NotFoundException($"Not found restaurantChain ID {request.resChainID}");
                var resList = await this._restaurantRepository.FindAllAsync(x => x.RestaurantChainID.Equals(resChain.ID), cancellationToken);
                foreach (var res in resList)
                {
                   await _metiator.Send(new DeleteRestaurantCommand(res.ID), cancellationToken);
                }
                resChain.NgayXoa = DateTime.UtcNow;
                resChain.NguoiXoaID = this._currentUserService.UserId;
                resChain.IsDeleted = true;
                this._restaurantChainRepository.Update(resChain);
                await this._restaurantChainRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return $"Deleted restaurantChain ID {request.resChainID}.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
