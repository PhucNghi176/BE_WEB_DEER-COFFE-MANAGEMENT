using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Security;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.DeleteEmployee
{
    // [Authorize]
    public class DeleteEmployeeCommand(string employeeID) : IRequest<string>, ICommand
    {
        public string EmployeeID { get; } = employeeID;
    }
}
