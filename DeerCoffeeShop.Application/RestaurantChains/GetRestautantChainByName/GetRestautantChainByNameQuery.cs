using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestautantChainByName
{
    public class GetRestautantChainByNameQuery : IRequest<PagedResult<RestaurantChainDTO>>, IQuery
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string resChainName { get; set; }
        public GetRestautantChainByNameQuery(int pageNumber, int pageSize, string resChainName)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.resChainName = resChainName;
        }
    }
}
