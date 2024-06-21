using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Common.Security;
using MediatR;

namespace DeerCoffeeShop.Application.Shift.GetAll
{
    [Authorize]
    public class GetAllShiftQuery() : IRequest<PagedResult<ShiftDto>>, IQuery
    {

        public int PageNo { get; set; }

        public int PageSize { get; set; }
    }
}
