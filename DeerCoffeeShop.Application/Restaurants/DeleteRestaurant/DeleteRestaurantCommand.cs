using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.DeleteRestaurant
{
    public class DeleteRestaurantCommand : IRequest<string>, ICommand
    {
        public string resID { get; set; }
        public DeleteRestaurantCommand() { }
        public DeleteRestaurantCommand(string resID)
        {
            this.resID = resID;
        }
    }
}
