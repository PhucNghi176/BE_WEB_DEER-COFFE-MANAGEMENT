using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.LockDay;

public record LockDayCommand : IRequest<string>
{
    public DateOnly DateOfWork { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public bool IsLocked { get; init; }
}
internal sealed class LockDayCommandHandler : IRequestHandler<LockDayCommand, string>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmployeeShiftRepository _employeeShiftRepository;

    public LockDayCommandHandler(ICurrentUserService currentUserService, IEmployeeShiftRepository employeeShiftRepository)
    {
        _currentUserService = currentUserService;
        _employeeShiftRepository = employeeShiftRepository;
    }

    public async Task<string> Handle(LockDayCommand request, CancellationToken cancellationToken)
    {
        var list = await _employeeShiftRepository.FindAllAsync(x => x.DateOfWork == request.DateOfWork && x.CheckIn == request.Start && x.CheckOut == request.End && !x.NgayXoa.HasValue && x.RestaurantID == _currentUserService.RestaurantID, cancellationToken);
        if (request.IsLocked)
        {
            foreach (var item in list)
            {
                item.IsLocked = true;
                _employeeShiftRepository.Update(item);
            }
        }
        else
        {
            foreach (var item in list)
            {
                item.IsLocked = false;
                _employeeShiftRepository.Update(item);
            }
        }
        return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Lock day successfully!" : "Lock day failed";

    }
}
