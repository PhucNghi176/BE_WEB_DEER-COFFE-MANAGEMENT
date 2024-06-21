using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.GetByDay
{
    public class GetEmployeeShiftByDayQuery() : IRequest<PagedResult<EmployeeShiftDto>>, IQuery
    {
        public int PageNo {  get; set; }

        public int PageSize {  get; set; }

        public DateTime DateOfWork {  get; set; }

    }
}
