using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByID
{
    public class GetRestaurantChainByIDQuery : IRequest<RestaurantChainDTO>, IQuery
    {
        public string resChainID { get; set; }
        public GetRestaurantChainByIDQuery(string resChainID)
        {
            this.resChainID = resChainID;
        }
    }
}
