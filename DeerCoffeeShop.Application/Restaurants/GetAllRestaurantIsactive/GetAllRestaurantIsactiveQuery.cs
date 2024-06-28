using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.GetAllRestaurantIsactive
{
    public class GetAllRestaurantIsactiveQuery : IRequest<PagedResult<RestaurantDTO>>, IQuery
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public GetAllRestaurantIsactiveQuery(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
    }
}
