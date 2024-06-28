using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByAdmin
{
    public class GetRestaurantChainByAdminQuery : IRequest<PagedResult<RestaurantChainDTO>>, IQuery
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string adminID { get; set; }
        public GetRestaurantChainByAdminQuery() { }
        public GetRestaurantChainByAdminQuery(int pageNumber, int pageSize, string adminID)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.adminID = adminID;
        }
    }
}
