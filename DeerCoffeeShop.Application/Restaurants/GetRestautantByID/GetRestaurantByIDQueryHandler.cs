using AutoMapper;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.GetRestautantByID
{
    public class GetRestaurantByIDQueryHandler : IRequestHandler<GetRestaurantByIDQuery, RestaurantDTO>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public GetRestaurantByIDQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<RestaurantDTO> Handle(GetRestaurantByIDQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var restaurant = await this._restaurantRepository.FindAsync(x => x.ID.Equals(request.resID) && x.IsDeleted == false, cancellationToken);
                if (restaurant == null) 
                    throw new NotFoundException($"Not found restaurant that had ID {request.resID}");
                return restaurant.MapToRestaurantDTO(_mapper);
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
