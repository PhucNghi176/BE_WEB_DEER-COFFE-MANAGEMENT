﻿using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Constants;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.GetEmployeeInfo
{
    public record GetEmployeeInfoQuery(string employeeID) : IRequest<EmployeeDto>, IQuery
    {
        public string EmployeeID { get; set; } = employeeID;
    }
    internal class GetEmployeeInfoQueryHandler(IEmployeeRepository employeeRepository, IRestaurantRepository restaurantRepository) : IRequestHandler<GetEmployeeInfoQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

        public async Task<EmployeeDto> Handle(GetEmployeeInfoQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee? employee = await _employeeRepository.FindAsync(_ => _.ID == request.EmployeeID && _.NgayXoa == null, cancellationToken);
            if (employee.RoleID == 2)
            {
                Domain.Entities.Restaurant? ResID = await _restaurantRepository.FindAsync(_ => _.ManagerID == employee.ID && _.NgayXoa == null, cancellationToken);
                return EmployeeDto.CreateDtoLogin(employee.FullName, EmployeeRole.EmployeeRoleDictionary[employee.RoleID], employee.AvatarUrl, ResID?.ID);
            }

            return EmployeeDto.CreateDtoLogin(employee.FullName, EmployeeRole.EmployeeRoleDictionary[employee.RoleID], employee.AvatarUrl, null);
        }
    }
}
