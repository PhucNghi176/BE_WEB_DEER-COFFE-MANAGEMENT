using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Shift.Update
{
    public class UpdateShiftCommand() : IRequest<string>, ICommand
    {
        public required int shift_id { get; set; }

        public string? shift_name { get; set; }

        public int shift_start { get; set; }

        public int shift_end { get; set; }

        public string? shift_description { get; set; }

    }
}
