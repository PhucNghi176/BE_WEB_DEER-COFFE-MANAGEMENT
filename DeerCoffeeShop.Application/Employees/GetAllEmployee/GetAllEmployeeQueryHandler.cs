using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Constants;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.GetAllEmployee
{
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, PagedResult<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public GetAllEmployeeQueryHandler(IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<EmployeeDto>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            bool role = await _currentUserService.IsInRoleAsync("Admin");
            IPagedResult<Employee>? list = role
                ? await _employeeRepository.FindAllAsync(x => !x.IsDeleted && x.ManagerID == _currentUserService.UserId, request.PageNumber, request.PageSize, cancellationToken)
                : await _employeeRepository.FindAllAsync(x => !x.IsDeleted && x.RoleID == 2, request.PageNumber, request.PageSize, cancellationToken);
            return PagedResult<EmployeeDto>.Create(totalCount: list.TotalCount,
                               pageCount: list.PageCount,
                                              pageSize: list.PageSize,
                                                             pageNumber: list.PageNo,
                                                                            data: list.MapToEmployeeDtoList(_mapper, EmployeeRole.EmployeeRoleDictionary));
        }
    }
}