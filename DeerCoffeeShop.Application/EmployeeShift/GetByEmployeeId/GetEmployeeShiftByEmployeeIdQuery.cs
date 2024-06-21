using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.GetByEmployeeId
{
    public class GetEmployeeShiftByEmployeeIdQuery() : IRequest<PagedResult<EmployeeShiftDto>>, IQuery
    {
        public string? EmployeeId { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }
    }
}
