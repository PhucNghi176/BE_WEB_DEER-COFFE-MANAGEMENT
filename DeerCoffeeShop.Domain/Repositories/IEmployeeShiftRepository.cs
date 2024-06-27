using DeerCoffeeShop.Domain.Entities;

namespace DeerCoffeeShop.Domain.Repositories
{
    public interface IEmployeeShiftRepository : IEFRepository<EmployeeShift, EmployeeShift>
    {
        Task<Domain.Entities.EmployeeShift> CheckShiftEmployee(string EmployeeID, DateOnly DateOfWork, CancellationToken cancellationToken = default);
    }
}
