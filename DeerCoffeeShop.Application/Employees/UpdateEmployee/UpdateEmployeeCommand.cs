using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.UpdateEmployee
{
    // [Authorize]
    public class UpdateEmployeeCommand(string employeeID, string email, string phoneNumber, string address, int roleId, string fullName, DateTime dateOfBirth, bool isActive) : IRequest<string>, ICommand
    {
        public string EmployeeID { get; } = employeeID;
        public string Email { get; } = email;
        public string PhoneNumber { get; } = phoneNumber;
        public string Address { get; } = address;
        public int RoleId { get; } = roleId;
        public string FullName { get; } = fullName;
        public DateTime DateOfBirth { get; } = dateOfBirth;
        public bool IsActive { get; } = isActive;
    }
}
