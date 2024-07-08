using AutoMapper;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

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
                Domain.Entities.RestaurantChain? resChain = await this._restaurantChainRepository.FindAsync(x => x.ID.Equals(request.resChainID) && x.IsDeleted == false, cancellationToken);
                return resChain == null
                    ? throw new NotFoundException($"Not found restaurantChain with ID {request.resChainID}")
                    : resChain.MapToRestaurantChainDTO(_mapper);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
