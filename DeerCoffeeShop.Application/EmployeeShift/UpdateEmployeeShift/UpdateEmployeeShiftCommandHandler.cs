using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.UpdateEmployeeShift
{
    public class UpdateEmployeeShiftCommandHandler(IEmployeeShiftRepository employeeShiftRepository) : IRequestHandler<UpdateEmployeeShiftCommand, string>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;

        public async Task<string> Handle(UpdateEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.EmployeeShift foundObject = await _employeeShiftRepository.FindAsync(x => x.RestaurantID.Equals(command.RestaurantID)

            && x.EmployeeID.Equals(command.EmployeeID)
            && x.DateOfWork.Equals(command.DateOfWork)
            , cancellationToken) ?? throw new NotFoundException("Employee shift was not found!");

            foundObject.Actual_CheckIn = command.ActualCheckIn;
            foundObject.Actual_CheckOut = command.ActualCheckOut;

            foundObject.Status = !command.ActualCheckIn.HasValue && !command.ActualCheckOut.HasValue
                ? Domain.Enums.EmployeeShiftStatus.Absent
                : Domain.Enums.EmployeeShiftStatus.OnTime;

            TimeSpan? totalHour = command.ActualCheckIn - command.ActualCheckOut;
            TimeSpan? employeeNote = command.ActualCheckIn - foundObject.CheckIn;

            // foundObject.TotalHours = foundObject.DateOfWork + totalHour;
            foundObject.EmployeeNote = employeeNote.Value.Hours;
            foundObject.Note = command.Note;

            return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync() > 0 ? "Update employee shift successfully" : "Update failed";
        }
    }
}
