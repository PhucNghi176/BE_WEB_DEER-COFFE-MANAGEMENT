using AutoMapper;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Constants;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.GetEmployee
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee? employee = await _employeeRepository.FindAsync(x => x.ID.Equals(request.EmployeeId), cancellationToken);
            return employee == null
                ? throw new NotFoundException("Employee is not exist !")
                : employee.MapToEmployeeDto(_mapper, EmployeeRole.EmployeeRoleDictionary[employee.RoleID]);
        }
    }
}