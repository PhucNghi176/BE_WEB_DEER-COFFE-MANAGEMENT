using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.AssignEmployee
{
    public class AssignEmployeeToEmployeeShiftCommand : IRequest<string>, ICommand
    {
        public required string RestaurantID { get; set; }

        public required int ShiftID { get; set; }

        public required string EmployeeID { get; set; }

        public required DateTime DateOfWork { get; set; }
    }
}
