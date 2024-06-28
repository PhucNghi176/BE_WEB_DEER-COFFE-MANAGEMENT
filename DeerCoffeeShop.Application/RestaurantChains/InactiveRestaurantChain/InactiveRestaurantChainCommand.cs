using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.InactiveRestaurantChain
{
    public class InactiveRestaurantChainCommand : IRequest<string>, ICommand
    {
        public string ID { get; set; }
        public InactiveRestaurantChainCommand(string iD)
        {
            ID = iD;
        }
    }
}
