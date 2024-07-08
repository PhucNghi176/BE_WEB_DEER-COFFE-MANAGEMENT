using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, ICurrentUserService currentUserService) : IRequestHandler<UpdateEmployeeCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<string> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee? foundObject = await _employeeRepository.FindAsync(x => x.ID.Equals(command.EmployeeID)
            );
            if (foundObject == null)
                throw new NotFoundException("None shift was found!");

            foundObject.FullName = command.FullName;
            foundObject.Email = command.Email;
            foundObject.PhoneNumber = command.PhoneNumber;
            foundObject.Address = command.Address;
            foundObject.RoleID = command.RoleId;
            foundObject.DateOfBirth = command.DateOfBirth;
            foundObject.IsActive = command.IsActive;
            foundObject.Address = command.Address;
            foundObject.NguoiCapNhatID = _currentUserService.UserId;
            foundObject.NgayCapNhatCuoi = DateTime.Now;

            return await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Employee Successfully" : "Update Employee Fail";
        }
    }
}
