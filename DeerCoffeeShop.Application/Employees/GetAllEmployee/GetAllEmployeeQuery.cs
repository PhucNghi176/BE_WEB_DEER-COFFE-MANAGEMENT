using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Common.Security;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.GetAllEmployee;

[Authorize(Roles = "Admin,Manager")]
public class GetAllEmployeeQuery : IRequest<PagedResult<EmployeeDto>>, IQuery
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetAllEmployeeQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public GetAllEmployeeQuery() { }
}


