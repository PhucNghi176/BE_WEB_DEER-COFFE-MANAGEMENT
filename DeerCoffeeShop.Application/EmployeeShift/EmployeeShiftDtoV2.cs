using AutoMapper;
using DeerCoffeeShop.Application.Common.Mappings;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Application.Shift;
using DeerCoffeeShop.Domain.Enums;

namespace DeerCoffeeShop.Application.EmployeeShift
{
    public class EmployeeShiftDtoV2 : IMapFrom<Domain.Entities.EmployeeShift>
    {
        public string ID { get; set; }
        public string RestaurantID { get; set; }
        public ShiftDto Shift { get; set; }
        public DateOnly DateOfWork { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime? Actual_CheckIn { get; set; }
        public DateTime? Actual_CheckOut { get; set; }
        public DateTime? TotalHours { get; set; }
        public bool IsOnTime { get; set; } = false;
        public EmployeeShiftStatus Status { get; set; } = EmployeeShiftStatus.Absent;
        public int EmployeeNote { get; set; }
        public string? Note { get; set; }
        public EmployeeDto Employee { get; set; }

        public static EmployeeShiftDtoV2 Create(string ID, string RestaurantID, EmployeeDto employee,
             DateOnly dateOfWork, int month, int year, DateTime checkIn, DateTime checkOut, DateTime? actual_CheckIn
            , DateTime? actual_CheckOut, DateTime? totalHours, bool isOnTime, EmployeeShiftStatus status
            , int employeeNote, string? note)
        {
            return new EmployeeShiftDtoV2()
            {
                ID = ID,
                Employee = employee,
                RestaurantID = RestaurantID,
                DateOfWork = dateOfWork,
                Month = month,
                Year = year,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Actual_CheckIn = actual_CheckIn,
                Actual_CheckOut = actual_CheckOut,
                TotalHours = totalHours,
                IsOnTime = isOnTime,
                Status = status,
                EmployeeNote = employeeNote,
                Note = note,
            };
        }

        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<Domain.Entities.EmployeeShift, EmployeeShiftDtoV2>();
        }
    }
}
