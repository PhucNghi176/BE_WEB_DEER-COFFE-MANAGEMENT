using AutoMapper;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using DeerCoffeeShop.Infrastructure.Persistence.Configurations;

namespace DeerCoffeeShop.Infrastructure.Repositories
{
    public class EmployeeShiftRepository(ApplicationDbContext context, IMapper mapper) : RepositoryBase<EmployeeShift, EmployeeShift, ApplicationDbContext>(context, mapper), IEmployeeShiftRepository
    {
        public async Task<EmployeeShift> CheckShiftEmployee(string EmployeeID, DateOnly DateOfWork, string RestaurantID, CancellationToken cancellationToken = default)
        {
            List<EmployeeShift> empShift = await FindAllAsync(x => x.EmployeeID == EmployeeID && x.DateOfWork == DateOfWork && x.RestaurantID == RestaurantID && (x.Actual_CheckIn == null || x.Actual_CheckOut == null), cancellationToken);
            if (empShift == null)
                return null;
            foreach (EmployeeShift? item in empShift.OrderBy(x => x.CheckIn))
            {
                if (item.Actual_CheckIn == null || (item.Actual_CheckIn != null && item.Actual_CheckOut == null))
                    return item;
            }
            return null;
        }
    }
}
