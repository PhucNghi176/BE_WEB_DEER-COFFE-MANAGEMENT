using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.UpdateEmployeeShift
{
    public class UpdateEmployeeShiftCommand() : IRequest<string>, ICommand
    {
        public required string RestaurantID { get; set; }

        public required int ShiftID { get; set; }

        public required string EmployeeID { get; set; }

        public required DateTime DateOfWork { get; set; }

        public DateTime? ActualCheckIn {  get; set; }

        public DateTime? ActualCheckOut {  get; set; }

        public string? Note { get; set; }
    }
}
