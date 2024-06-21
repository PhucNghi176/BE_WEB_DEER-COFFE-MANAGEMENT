using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using DeerCoffeeShop.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.AssignEmployee
{
    public class AssignEmployeeToEmployeeShiftCommandHandler(IEmployeeShiftRepository employeeShiftRepository, IEmployeeRepository employeeRepository) : IRequestHandler<AssignEmployeeToEmployeeShiftCommand, string>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<string> Handle(AssignEmployeeToEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            var foundObject = await _employeeShiftRepository.FindAsync(x => x.RestaurantID.Equals(command.RestaurantID) 
            && x.ShiftID.Equals(command.ShiftID) 
            && x.DateOfWork.Equals(command.DateOfWork)
            && !x.IsDeleted, cancellationToken) ?? throw new NotFoundException("Employee shift of this restaurant was not found!");

            var checkEmployee = await _employeeRepository.AnyAsync(x => x.ID.Equals(command.EmployeeID), cancellationToken);
            if (checkEmployee == false)
                throw new NotFoundException("Employee was not found!");

            foundObject.EmployeeID = command.EmployeeID;

            return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync() > 0 ? "Assign employee to employee shift successfully!" : "Assign failed";
        }
    }
}
