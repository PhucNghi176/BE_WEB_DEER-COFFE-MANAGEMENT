using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Application.Shift;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Constants;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.GetByDay
{
    public class GetEmployeeShiftByDayQueryHandler(IEmployeeShiftRepository employeeShiftRepository, IEmployeeRepository employeeRepository
                                                , IRestaurantRepository restaurantRepository, IShiftRepostiry shiftRepostiry, IMapper mapper, ICurrentUserService currentUserService) : IRequestHandler<GetEmployeeShiftByDayQuery, PagedResult<EmployeeShiftDto>>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        private readonly IShiftRepostiry _shiftRepository = shiftRepostiry;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResult<EmployeeShiftDto>> Handle(GetEmployeeShiftByDayQuery query, CancellationToken cancellationToken)
        {

            var list = await _employeeShiftRepository.FindAllAsync(x => !x.IsDeleted
            && x.DateOfWork.Day == query.DateOfWork.Day
            && x.RestaurantID.Equals(_currentUserService.RestaurantID), query.PageNo, query.PageSize, cancellationToken);
            if (list.TotalCount == 0)
                throw new NotFoundException("None employee shift was found!");
            foreach (var item in list)
            {
                item.Employee = await _employeeRepository.FindAsync(x => x.ID.Equals(item.EmployeeID), cancellationToken);
                item.Shift = await _shiftRepository.FindAsync(x => x.ID == item.ShiftID, cancellationToken);

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
