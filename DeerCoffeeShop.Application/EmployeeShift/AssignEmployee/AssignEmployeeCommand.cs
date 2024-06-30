using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.AssignEmployee;

public class AssignEmployeeCommand : IRequest<string>, ICommand
{
    public string EmployeeID { get; set; }
    public string EmployeeShiftID { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
internal sealed class AssginEmployeeCommandHandler : IRequestHandler<AssignEmployeeCommand, string>
{
    private readonly IEmployeeShiftRepository _employeeShiftRepository;

    public AssginEmployeeCommandHandler(IEmployeeShiftRepository employeeShiftRepository)
    {
        _employeeShiftRepository = employeeShiftRepository;
    }

    public async Task<string> Handle(AssignEmployeeCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.EmployeeShift Shift = await _employeeShiftRepository.FindAsync(x => x.ID == request.EmployeeShiftID, cancellationToken) ?? throw new NotFoundException("Employee Shift not found!");
        if (Shift.EmployeeID != null)
            throw new DuplicatedObjectException("Employee already assigned to this shift!");
        Shift.EmployeeID = request.EmployeeID;
        Shift.CheckIn = request.CheckIn;
        Shift.CheckOut = request.CheckOut;
        Shift.IsReviewRequired = false;
        _employeeShiftRepository.Update(Shift);
        return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Employee assigned successfully!" : "Employee assigned failed!";

    }
}
