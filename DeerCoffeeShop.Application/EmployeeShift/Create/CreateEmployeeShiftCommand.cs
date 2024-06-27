using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.Create
{
    public class CreateEmployeeShiftCommand() : IRequest<string>, ICommand
    {
        public required DateOnly DateOfWork { get; set; }

        public required DateTime CheckIn { get; set; }

        public required DateTime CheckOut { get; set; }
    }
}
