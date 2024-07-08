using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.UpdateRestautantChain
{
    public class UpdateRestautantChainCommand : IRequest<string>, ICommand
    {
        public string resChainID { get; set; }
        public string RestaurantChain_AdminID { get; set; }
        public string RestaurantChainName { get; set; }
        public string RestaurantChainHQAddress { get; set; }
        public string RestaurantChainType { get; set; }
        public UpdateRestautantChainCommand(string resChainID, string restaurantChain_AdminID, string restaurantChainName, string restaurantChainHQAddress, string restaurantChainType)
        {
            this.resChainID = resChainID;
            this.RestaurantChain_AdminID = restaurantChain_AdminID;
            this.RestaurantChainName = restaurantChainName;
            this.RestaurantChainHQAddress = restaurantChainHQAddress;
            this.RestaurantChainType = restaurantChainType;
        }
    }
}
