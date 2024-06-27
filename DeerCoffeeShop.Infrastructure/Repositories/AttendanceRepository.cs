using AutoMapper;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using DeerCoffeeShop.Infrastructure.Persistence.Configurations;

namespace DeerCoffeeShop.Infrastructure.Repositories;

public class AttendanceRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<Attendence, Attendence, ApplicationDbContext>(dbContext, mapper), IAttdenceRepository
{
}