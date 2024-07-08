using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.InactiveRestaurant
{
    public class InactiveRestaurantCommand : IRequest<string>, ICommand
    {
        public string ID { get; set; }
        public InactiveRestaurantCommand(string iD)
        {
            ID = iD;
        }
    }
}
