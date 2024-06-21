using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.RestaurantChains.InactiveRestaurantChain
{
    public class InactiveRestaurantChainCommand : IRequest<string>, ICommand
    {
        public string ID {  get; set; }
        public InactiveRestaurantChainCommand(string iD)
        {
            ID = iD;
        }
    }
}
