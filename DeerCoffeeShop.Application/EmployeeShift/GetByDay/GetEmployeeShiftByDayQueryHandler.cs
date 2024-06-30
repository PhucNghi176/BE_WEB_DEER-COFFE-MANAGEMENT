using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.GetByDay
{
    public class GetEmployeeShiftByDayQueryHandler(IEmployeeShiftRepository employeeShiftRepository, IEmployeeRepository employeeRepository, IMapper mapper, ICurrentUserService currentUserService) : IRequestHandler<GetEmployeeShiftByDayQuery, PagedResult<EmployeeShiftDto>>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResult<EmployeeShiftDto>> Handle(GetEmployeeShiftByDayQuery query, CancellationToken cancellationToken)
        {

            IPagedResult<Domain.Entities.EmployeeShift> list = await _employeeShiftRepository.FindAllAsync(x => !x.IsDeleted
            && x.DateOfWork == query.DateOfWork
            && x.RestaurantID.Equals(_currentUserService.RestaurantID), query.PageNo, query.PageSize, cancellationToken);
            if (list.TotalCount == 0)
                throw new NotFoundException("None employee shift was found!");
            foreach (Domain.Entities.EmployeeShift item in list)
            {
                item.Employee = await _employeeRepository.FindAsync(x => x.ID.Equals(item.EmployeeID), cancellationToken);


            }

            return PagedResult<EmployeeShiftDto>.Create
                (
                    totalCount: list.TotalCount,
                    pageCount: list.PageCount,
                    pageSize: list.PageSize,
                    pageNumber: list.PageNo,
                    data: list.MapToListEmployeeShiftDto(_mapper)
                );
        }
    }
}
