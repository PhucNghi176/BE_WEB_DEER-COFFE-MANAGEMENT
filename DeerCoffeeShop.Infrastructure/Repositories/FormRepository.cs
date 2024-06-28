using AutoMapper;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using DeerCoffeeShop.Infrastructure.Persistence.Configurations;

namespace DeerCoffeeShop.Infrastructure.Repositories
{
    public class FormRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<Form, Form, ApplicationDbContext>(dbContext, mapper), IFormRepository
    {
    }
}
