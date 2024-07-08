using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByDeactive
{
    public class GetRestaurantChainByDeactiveQuery : IRequest<PagedResult<RestaurantChainDTO>>, IQuery
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public GetRestaurantChainByDeactiveQuery(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
    }
}
