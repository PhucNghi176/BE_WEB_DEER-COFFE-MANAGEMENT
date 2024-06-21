using AutoMapper;
using DeerCoffeeShop.Application.Common.Mappings;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Application.Shift;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift
{
    public class EmployeeShiftDtoV2 : IMapFrom<Domain.Entities.EmployeeShift>
    {
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

        public static EmployeeShiftDtoV2 Create(string RestaurantID, EmployeeDto employee,
             DateOnly dateOfWork, int month, int year, DateTime checkIn, DateTime checkOut, DateTime? actual_CheckIn
            , DateTime? actual_CheckOut, DateTime? totalHours, bool isOnTime, EmployeeShiftStatus status
            , int employeeNote, string? note)
        {
            return new EmployeeShiftDtoV2()
            {
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
            profile.CreateMap<Domain.Entities.EmployeeShift, EmployeeShiftDtoV2>();
        }
    }
}
