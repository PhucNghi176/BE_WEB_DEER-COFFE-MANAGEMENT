using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.GetRestautantByID
{
    public class GetRestaurantByIDQuery : IRequest<RestaurantDTO>, IQuery
    {
        public Guid resID {  get; set; }
        public GetRestaurantByIDQuery(Guid resID)
        {
            this.resID = resID;
        }
        public GetRestaurantByIDQuery() { }
    }
}
