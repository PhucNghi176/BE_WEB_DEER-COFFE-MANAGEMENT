using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.DeleteEmployee
{
    // [Authorize]
    public class DeleteEmployeeCommand(string employeeID) : IRequest<string>, ICommand
    {
        public string EmployeeID { get; } = employeeID;
    }
}
