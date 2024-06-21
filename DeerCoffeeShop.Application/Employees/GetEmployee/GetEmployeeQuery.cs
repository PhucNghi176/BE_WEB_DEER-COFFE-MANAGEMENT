using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.GetEmployee
{
    public class GetEmployeeQuery : IRequest<EmployeeDto>, IQuery

    {
        public string EmployeeId { get; set; }
        public GetEmployeeQuery(string employee_id)
        {
            EmployeeId = employee_id;
        }

        public GetEmployeeQuery() { }
    }
};


