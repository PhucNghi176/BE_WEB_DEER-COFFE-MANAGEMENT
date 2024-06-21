using AutoMapper;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using DeerCoffeeShop.Infrastructure.Persistence.Configurations;

namespace DeerCoffeeShop.Infrastructure.Repositories
{
    public class RestaurantRepository(ApplicationDbContext context, IMapper mapper) : RepositoryBase<Restaurant, Restaurant, ApplicationDbContext>(context, mapper), IRestaurantRepository
    {
    }
}
