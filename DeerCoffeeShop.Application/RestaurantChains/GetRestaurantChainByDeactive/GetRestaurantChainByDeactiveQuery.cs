using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByDeactive
{
    public class GetRestaurantChainByDeactiveQuery : IRequest<PagedResult<RestaurantChainDTO>>, IQuery
    {
        public int pageNumber {  get; set; }
        public int pageSize { get; set; }
        public GetRestaurantChainByDeactiveQuery(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }   
    }
}
