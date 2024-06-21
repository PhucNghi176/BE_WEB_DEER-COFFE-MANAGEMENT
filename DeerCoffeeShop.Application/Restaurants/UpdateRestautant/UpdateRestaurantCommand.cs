using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.UpdateRestautant
{
    public class UpdateRestaurantCommand : IRequest<string>, ICommand
    {
        public string resID {  get; set; }
        public string manageID {  get; set; }
        public string resName {  get; set; }
        public string resAddress {  get; set; }
        public string resChainID {  get; set; }
        public UpdateRestaurantCommand(string resID, string manageID, string resName, string resAddress, string resChainID)
        {
            this.resID = resID;
            this.manageID = manageID;
            this.resName = resName;
            this.resAddress = resAddress;
            this.resChainID = resChainID;
        }
    }
}
