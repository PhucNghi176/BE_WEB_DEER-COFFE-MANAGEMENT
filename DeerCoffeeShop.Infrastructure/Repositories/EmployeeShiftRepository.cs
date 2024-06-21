using AutoMapper;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using DeerCoffeeShop.Infrastructure.Persistence.Configurations;

namespace DeerCoffeeShop.Infrastructure.Repositories
{
    public class EmployeeShiftRepository(ApplicationDbContext context, IMapper mapper) : RepositoryBase<EmployeeShift, EmployeeShift, ApplicationDbContext>(context, mapper), IEmployeeShiftRepository
    {
    }
}
