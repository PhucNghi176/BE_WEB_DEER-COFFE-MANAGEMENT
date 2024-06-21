using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.GetRestaurantIsLowEmp
{
    public class GetRestaurantIsLowEmpQuery : IRequest<PagedResult<RestaurantDTO>>, IQuery
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public GetRestaurantIsLowEmpQuery(int pageSize, int pageNumber)
        {
            this.pageSize = pageSize;
            this.pageNumber = pageNumber;
        }
        public GetRestaurantIsLowEmpQuery() { }
    }
}
