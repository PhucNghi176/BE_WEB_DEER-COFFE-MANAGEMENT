using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByAdmin
{
    public class GetRestaurantChainByAdminQuery : IRequest<PagedResult<RestaurantChainDTO>>, IQuery
    {
        public int pageNumber {  get; set; }
        public int pageSize { get; set; }
        public string adminID {  get; set; }
        public GetRestaurantChainByAdminQuery() { }
        public GetRestaurantChainByAdminQuery(int pageNumber, int pageSize, string adminID) 
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.adminID = adminID;
        }
    }
}
