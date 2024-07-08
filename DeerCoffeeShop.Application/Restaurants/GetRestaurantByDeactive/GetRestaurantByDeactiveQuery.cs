using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.GetRestaurantByDeactive
{
    public class GetRestaurantByDeactiveQuery : IRequest<PagedResult<RestaurantDTO>>, IQuery
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public GetRestaurantByDeactiveQuery(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }


    }
}
