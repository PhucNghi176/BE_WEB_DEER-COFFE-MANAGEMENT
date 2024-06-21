using AutoMapper;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByID
{
    public class GetRestaurantChainByIDQueryHandler : IRequestHandler<GetRestaurantChainByIDQuery, RestaurantChainDTO>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        private readonly IMapper _mapper;
        public GetRestaurantChainByIDQueryHandler(IRestaurantChainRepository restaurantChainRepository, IMapper mapper)
        {
            _mapper = mapper;
            _restaurantChainRepository = restaurantChainRepository;
        }
        public async Task<RestaurantChainDTO> Handle(GetRestaurantChainByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resChain = await this._restaurantChainRepository.FindAsync(x => x.ID.Equals(request.resChainID) && x.IsDeleted == false, cancellationToken);
                if (resChain == null)
                    throw new NotFoundException($"Not found restaurantChain with ID {request.resChainID}");
                return resChain.MapToRestaurantChainDTO(_mapper);
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
