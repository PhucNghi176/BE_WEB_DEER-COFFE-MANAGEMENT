using AutoMapper;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using DeerCoffeeShop.Infrastructure.Persistence.Configurations;

namespace DeerCoffeeShop.Infrastructure.Repositories
{
    public class ShiftRepository(ApplicationDbContext context, IMapper mapper) : RepositoryBase<Shift, Shift, ApplicationDbContext>(context, mapper), IShiftRepostiry
    {
    }
}
