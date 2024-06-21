using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Security;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.Delete
{
    [Authorize]
    public class DeleteEmployeeShiftCommand() : IRequest<string>, ICommand
    {
        public string EmployeeID { get; set; }
        public string RestaurantID { get; set; }
        public int ShiftID { get; set; }
    }
}
