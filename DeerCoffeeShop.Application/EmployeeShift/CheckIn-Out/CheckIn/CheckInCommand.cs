using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.CheckIn_Out.CheckIn;

public record CheckInCommand : ICommand, IRequest<string>
{
    public string EmployeeID { get; set; }
    public DateTime CheckInTime { get; set; }
    public string RestaurantID { get; set; }
}
internal class CheckInCommandHandler(IEmployeeShiftRepository employeeShiftRepository, IEmployeeRepository employeeRepository) : IRequestHandler<CheckInCommand, string>
{
    private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<string> Handle(CheckInCommand request, CancellationToken cancellationToken)
    {
        var isEmployeeExist = await _employeeRepository.AnyAsync(x => x.ID == request.EmployeeID, cancellationToken);
        if (!isEmployeeExist)
            throw new NotFoundException("Employee not found!");
        //check if that empID have a shift in that restaurant that day
        var empShift = await _employeeShiftRepository.FindAsync(x => x.EmployeeID == request.EmployeeID && x.RestaurantID == request.RestaurantID && x.DateOfWork.ToString().Contains(DateTime.Now.Date.ToString()), cancellationToken) ?? throw new NotFoundException("Employee shift not found!");
        //check if that the time check in is valid and only accept time before shift start 15mintue
        //var t1 = request.CheckInTime;
        //var t2 = empShift.Shift.ShiftStart.AddMinutes(-14);
        //if (t1 > t2)
        //    throw new TimeCheckInToSoonException("You can only check in 15 minutes before the shift start!");
        ////if all valid then update the check in time
        empShift.Actual_CheckIn = request.CheckInTime;
        return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Check in successfully!" : "Check in failed!";
    }
}
