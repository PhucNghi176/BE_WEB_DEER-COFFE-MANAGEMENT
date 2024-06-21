using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.RestaurantChains.InactiveRestaurantChain
{
    public class InactiveRestaurantChainCommandHandler : IRequestHandler<InactiveRestaurantChainCommand, string>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public InactiveRestaurantChainCommandHandler(IRestaurantChainRepository restaurantChainRepository)
        {
            _restaurantChainRepository = restaurantChainRepository;
        }
        public async Task<string> Handle(InactiveRestaurantChainCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var restaurantChain = await this._restaurantChainRepository.FindAsync(x => x.ID.Equals(request.ID) && x.IsDeleted == true, cancellationToken);
                if(restaurantChain == null) 
                    throw new NotFoundException($"RestaurantChain ID {request.ID} was not found");
                restaurantChain.IsDeleted = false;
                this._restaurantChainRepository.Update(restaurantChain);
                await this._restaurantChainRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return $"RestaurantChain ID {request.ID} is Active now";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
