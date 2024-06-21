using AutoMapper;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using DeerCoffeeShop.Infrastructure.Persistence.Configurations;

namespace DeerCoffeeShop.Infrastructure.Repositories
{
    public class RestauranChainRepository : RepositoryBase<RestaurantChain, RestaurantChain, ApplicationDbContext>, IRestaurantChainRepository
    {
        public RestauranChainRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
