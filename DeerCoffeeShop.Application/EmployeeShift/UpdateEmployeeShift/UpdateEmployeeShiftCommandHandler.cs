using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.UpdateEmployeeShift
{
    public class UpdateEmployeeShiftCommandHandler(IEmployeeShiftRepository employeeShiftRepository) : IRequestHandler<UpdateEmployeeShiftCommand, string>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;

        public async Task<string> Handle(UpdateEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            var foundObject = await _employeeShiftRepository.FindAsync(x => x.RestaurantID.Equals(command.RestaurantID)
            && x.ShiftID.Equals(command.ShiftID)
            && x.EmployeeID.Equals(command.EmployeeID)
            && x.DateOfWork.Equals(command.DateOfWork)
            , cancellationToken) ?? throw new NotFoundException("Employee shift was not found!");

            foundObject.Actual_CheckIn = command.ActualCheckIn;
            foundObject.Actual_CheckOut = command.ActualCheckOut;

            if (!command.ActualCheckIn.HasValue && !command.ActualCheckOut.HasValue)
                foundObject.Status = Domain.Enums.EmployeeShiftStatus.Absent;

            else if (foundObject.CheckIn.CompareTo(command.ActualCheckIn) < 0)
                foundObject.Status = Domain.Enums.EmployeeShiftStatus.Late;

            else if (foundObject.CheckOut.CompareTo(command.ActualCheckOut) > 0)
                foundObject.Status = Domain.Enums.EmployeeShiftStatus.EarlyLeave;

            else
                foundObject.Status = Domain.Enums.EmployeeShiftStatus.OnTime;

            var totalHour = command.ActualCheckIn - command.ActualCheckOut;
            var employeeNote = command.ActualCheckIn - foundObject.CheckIn;

           // foundObject.TotalHours = foundObject.DateOfWork + totalHour;
            foundObject.EmployeeNote = employeeNote.Value.Hours;
            foundObject.Note = command.Note;

            return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync() > 0 ? "Update employee shift successfully" : "Update failed";
        }
    }
}
