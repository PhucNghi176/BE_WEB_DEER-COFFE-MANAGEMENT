using AutoMapper;
using DeerCoffeeShop.Domain.Entities;

namespace DeerCoffeeShop.Application.Restaurants
{
    public static class RestaurantDTOMappingExstension
    {
        public static RestaurantDTO MapToRestaurantDTO(this Restaurant projectFrom, IMapper mapper)
           => mapper.Map<RestaurantDTO>(projectFrom);

        public static List<RestaurantDTO> MapToRestaurantDTOList(this IEnumerable<Restaurant> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToRestaurantDTO(mapper)).ToList();
    }
}
