using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.UpdateRestautantChain
{
    public class UpdateRestautantChainCommandHandler : IRequestHandler<UpdateRestautantChainCommand, string>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateRestautantChainCommandHandler(IRestaurantChainRepository restaurantChainRepository, ICurrentUserService currentUserService, IEmployeeRepository employeeRepository)
        {
            _currentUserService = currentUserService;
            _restaurantChainRepository = restaurantChainRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<string> Handle(UpdateRestautantChainCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resChain = await this._restaurantChainRepository.FindAsync(x => x.ID.Equals(request.resChainID) && x.IsDeleted == false, cancellationToken);
                if (resChain == null)
                    throw new NotFoundException($"Not found restaurantChain ID {request.resChainID}");
                if (request.RestaurantChain_AdminID != null)
                {
                    if ((await this._employeeRepository.FindAsync(x => x.ID.Equals(request.RestaurantChain_AdminID) && x.NgayXoa == null, cancellationToken)).RoleID == 1)
                    {
                        throw new NotFoundException($"Not found admin ID {request.RestaurantChain_AdminID}");
                    }
                }
                resChain.NgayCapNhatCuoi = DateTime.UtcNow;
                resChain.NguoiCapNhatID = this._currentUserService.UserId;
                resChain.RestaurantChainHQAddress = request.RestaurantChainHQAddress ?? resChain.RestaurantChainHQAddress;
                resChain.RestaurantChainName = request.RestaurantChainName ?? resChain.RestaurantChainName;
                resChain.RestaurantChainType = request.RestaurantChainType ?? resChain.RestaurantChainType;
                resChain.RestaurantChain_AdminID = request.RestaurantChain_AdminID ?? resChain.RestaurantChain_AdminID;
                this._restaurantChainRepository.Update(resChain);
                await this._restaurantChainRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return $"Updated restaurantChain ID {request.resChainID}";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
