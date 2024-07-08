using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.FillterByReschainAndManagerID
{
    public class FillterByReschainAndManagerIDQuery : IRequest<PagedResult<RestaurantDTO>>, IQuery
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string managerID { get; set; }
        public string resChainID { get; set; }
        public FillterByReschainAndManagerIDQuery() { }
        public FillterByReschainAndManagerIDQuery(int pageNumber, int pageSize,
                                                  string managerID, string resChainID)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.managerID = managerID;
            this.resChainID = resChainID;
        }
    }
}
