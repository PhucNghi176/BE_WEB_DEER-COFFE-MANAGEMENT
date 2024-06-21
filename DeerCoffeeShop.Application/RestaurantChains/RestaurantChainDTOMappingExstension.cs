using AutoMapper;
using DeerCoffeeShop.Domain.Entities;

namespace DeerCoffeeShop.Application.RestaurantChains
{
    public static class RestaurantChainDTOMappingExstension
    {
        public static RestaurantChainDTO MapToRestaurantChainDTO(this RestaurantChain projectFrom, IMapper mapper)
           => mapper.Map<RestaurantChainDTO>(projectFrom);

        public static List<RestaurantChainDTO> MapToRestaurantChainDTOList(this IEnumerable<RestaurantChain> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToRestaurantChainDTO(mapper)).ToList();
    }
}
