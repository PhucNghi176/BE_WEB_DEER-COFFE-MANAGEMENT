using AutoMapper;
using DeerCoffeeShop.Application.Common.Mappings;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Application.Shift;
using DeerCoffeeShop.Domain.Enums;

namespace DeerCoffeeShop.Application.EmployeeShift
{
    public class EmployeeShiftDto() : IMapFrom<Domain.Entities.EmployeeShift>
    {
        
        public EmployeeDto Employee { get; set; }
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.EmployeeShift, EmployeeShiftDto>();
        }

        public static EmployeeShiftDto Create(
             DateOnly dateOfWork, DateTime checkIn, DateTime checkOut, DateTime? actual_CheckIn
            , DateTime? actual_CheckOut, DateTime? totalHours, bool isOnTime, EmployeeShiftStatus status
            , int employeeNote, string? note)
        {
            return new EmployeeShiftDto()
            {
                DateOfWork = dateOfWork,
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
    }
}
