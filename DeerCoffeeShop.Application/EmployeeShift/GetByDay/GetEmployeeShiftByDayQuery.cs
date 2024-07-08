using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.GetByDay
{
    public class GetEmployeeShiftByDayQuery() : IRequest<PagedResult<EmployeeShiftDto>>, IQuery
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public DateOnly DateOfWork { get; set; }

    }
}
