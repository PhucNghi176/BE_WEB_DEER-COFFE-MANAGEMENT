using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByID
{
    public class GetRestaurantChainByIDQuery : IRequest<RestaurantChainDTO>, IQuery
    {
        public string resChainID {  get; set; }
        public GetRestaurantChainByIDQuery(string resChainID)
        {
            this.resChainID = resChainID;
        }
    }
}
