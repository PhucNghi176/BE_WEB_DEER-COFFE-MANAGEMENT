using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.Restaurants.InactiveRestaurant
{
    public class InactiveRestaurantCommandHandler : IRequestHandler<InactiveRestaurantCommand, string>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public InactiveRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<string> Handle(InactiveRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var restaurant = await this._restaurantRepository.FindAsync(x => x.ID.Equals(request.ID) && x.IsDeleted == true, cancellationToken);
                if (restaurant == null)
                    throw new NotFoundException($"restaurant ID {request.ID} was not found.");
                restaurant.IsDeleted = false;
                this._restaurantRepository.Update(restaurant);
                await this._restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return $"restaurant ID {request.ID} is Active now.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
