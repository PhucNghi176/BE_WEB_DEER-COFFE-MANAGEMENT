using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.Restaurants.InactiveRestaurant
{
    public class InactiveRestaurantCommand : IRequest<string>, ICommand
    {
        public string ID {  get; set; }
        public InactiveRestaurantCommand(string iD)
        {
            ID = iD;
        }
    }
}
