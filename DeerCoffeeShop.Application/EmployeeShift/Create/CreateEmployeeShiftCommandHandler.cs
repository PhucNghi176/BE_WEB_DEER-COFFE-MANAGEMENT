using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.Create
{
    public class CreateEmployeeShiftCommandHandler(IEmployeeShiftRepository employeeShiftRepository, IRestaurantRepository restaurantRepository
        , IShiftRepostiry shiftRepostiry, IAttdenceRepository attdenceRepository, ICurrentUserService currentUserService, IEmployeeRepository employeeRepository) : IRequestHandler<CreateEmployeeShiftCommand, string>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        private readonly IAttdenceRepository _attdenceRepository = attdenceRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<string> Handle(CreateEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            Employee? employee = await _employeeRepository.FindAsync(x => x.ID == _currentUserService.UserId, cancellationToken);
            Restaurant? restaurant = await _restaurantRepository.FindAsync(x => x.ManagerID == employee.ManagerID, cancellationToken);
            Domain.Entities.EmployeeShift empShift = new()
            {
                RestaurantID = restaurant.ID,
                EmployeeID = _currentUserService.UserId,
                DateOfWork = command.DateOfWork,
                Month = command.DateOfWork.Month,
                Year = command.DateOfWork.Year,
                CheckIn = command.CheckIn,
                CheckOut = command.CheckOut,
                Status = Domain.Enums.EmployeeShiftStatus.Absent,
                IsOnTime = false,
            };

            _employeeShiftRepository.Add(empShift);
            _ = await _employeeShiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            Attendence attdence = new()
            {
                EmployeeShiftID = empShift.ID,
                EmployeePictureUrlCheckIn = "",
                EmployeePictureUrlCheckOut = "",

            };
            _attdenceRepository.Add(attdence);
            return await _attdenceRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Creat employee shift successfully!" : "Create employee shift failed";
        }
    }
}
