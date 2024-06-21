using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Common.Security;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.GetAll
{
    [Authorize]
    public class GetAllEmployeeShiftQuery() : IRequest<PagedResult<EmployeeShiftDto>>, IQuery
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string RestaurantId { get; set; }
    }
}
