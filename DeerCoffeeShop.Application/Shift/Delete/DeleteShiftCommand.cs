using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Security;
using MediatR;

namespace DeerCoffeeShop.Application.Shift.Delete
{
    [Authorize]
    public class DeleteShiftCommand(int id) : IRequest<string>, ICommand
    {
        public int Id { get; } = id;
    }
}
