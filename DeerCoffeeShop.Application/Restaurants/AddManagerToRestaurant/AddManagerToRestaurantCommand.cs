using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.AddManagerToRestaurant
{
    public class AddManagerToRestaurantCommand : IRequest<string>, ICommand
    {
        public string resID { get; set; }
        public string ManagerID { get; set; }
        public AddManagerToRestaurantCommand() { }
        public AddManagerToRestaurantCommand(string resID, string ManagerID)
        {
            this.resID = resID;
            this.ManagerID = ManagerID;
        }
    }
}
