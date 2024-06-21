using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.DeleteRestaurantChain
{
    public class DeleteRestaurantChainCommand : IRequest<string>, ICommand
    {
        public string resChainID { get; set; }
        public DeleteRestaurantChainCommand(string resChainID)
        {
            this.resChainID = resChainID;
        }
    }
}
