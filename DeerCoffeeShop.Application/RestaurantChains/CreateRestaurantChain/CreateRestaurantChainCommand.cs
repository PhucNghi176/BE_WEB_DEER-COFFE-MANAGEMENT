using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.CreateRestaurantChain
{
    public class CreateRestaurantChainCommand : IRequest<string>, ICommand
    {
        public string RestaurantChain_AdminID { get; set; }
        public string RestaurantChainName { get; set; }
        public string RestaurantChainHQAddress { get; set; }
        public string RestaurantChainType { get; set; }

        public CreateRestaurantChainCommand(string restaurantChain_AdminID, string restaurantChainName, string restaurantChainHQAddress, string restaurantChainType)
        {
            RestaurantChain_AdminID = restaurantChain_AdminID;
            RestaurantChainName = restaurantChainName;
            RestaurantChainHQAddress = restaurantChainHQAddress;
            RestaurantChainType = restaurantChainType;
        }
        public CreateRestaurantChainCommand() { }
    }
}
