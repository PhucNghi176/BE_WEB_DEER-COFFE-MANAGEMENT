using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.Create
{
    public class CreateEmployeeShiftCommandHandler(IEmployeeShiftRepository employeeShiftRepository, IRestaurantRepository restaurantRepository
        , IShiftRepostiry shiftRepostiry) : IRequestHandler<CreateEmployeeShiftCommand, string>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        private readonly IShiftRepostiry _shiftRepostiry = shiftRepostiry;

        public async Task<string> Handle(CreateEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            var checkDuplicated = await _employeeShiftRepository.AnyAsync(x => x.ID.Equals(command.RestaurantID)
            && x.ShiftID.Equals(command.ShiftID)
            && x.DateOfWork.Equals(command.DateOfWork), cancellationToken);

            var restaurant = await _restaurantRepository.FindAsync(x => x.ID.Equals(command.RestaurantID) && !x.IsDeleted, cancellationToken);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found!");

            var shift = await _shiftRepostiry.FindAsync(x => x.ID.Equals(command.ShiftID) && x.IsActive, cancellationToken);
            if (shift == null)
                throw new NotFoundException("Shift not found!");

            if (checkDuplicated)
                throw new DuplicatedObjectException("This shift has already been created!");

            var empShift = new Domain.Entities.EmployeeShift()
            {
                RestaurantID = command.RestaurantID,
                Restaurant = restaurant,
                ShiftID = command.ShiftID,
                Shift = shift,
                DateOfWork = command.DateOfWork,
                Month = command.Month,
                Year = command.Year,
                CheckIn = command.CheckIn,
                CheckOut = command.CheckOut,
                Status = Domain.Enums.EmployeeShiftStatus.Absent,
                IsOnTime = false,
            };

            _employeeShiftRepository.Add(empShift);

            return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync() > 0 ? "Creat employee shift successfully!" : "Create employee shift failed";
        }
    }
}
