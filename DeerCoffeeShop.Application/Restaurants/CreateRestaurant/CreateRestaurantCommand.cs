using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.CreateRestaurant
{
    public class CreateRestaurantCommand : IRequest<string>, ICommand
    {
        public string RestaurantChainID { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string ManagerID { get; set; }

        public CreateRestaurantCommand(string restaurantChainID, string restaurantName, string restaurantAddress, string managerID)
        {
            RestaurantChainID = restaurantChainID;
            RestaurantName = restaurantName;
            RestaurantAddress = restaurantAddress;
            ManagerID = managerID;
        }

        public CreateRestaurantCommand() { }
    }
}
