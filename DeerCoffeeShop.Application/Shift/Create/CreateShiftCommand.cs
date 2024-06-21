using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Security;
using MediatR;

namespace DeerCoffeeShop.Application.Shift.Create
{
    [Authorize]
    public class CreateShiftCommand() : IRequest<string>, ICommand
    {

        public required string shift_name { get; set; }

        public required int shift_start { get; set; }

        public required int shift_end { get; set; }

        public required string shift_description { get; set; }
    }
}
