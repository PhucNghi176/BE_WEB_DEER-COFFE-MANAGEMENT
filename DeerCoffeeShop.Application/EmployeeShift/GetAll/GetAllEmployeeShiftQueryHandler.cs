using AutoMapper;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Application.Shift;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Constants;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.GetAll
{
    public class GetAllEmployeeShiftQueryHandler(IEmployeeShiftRepository employeeShiftRepository, IMapper mapper,
        IEmployeeRepository employeeRepository, IRestaurantRepository restaurantRepository, IShiftRepostiry shiftRepostiry) : IRequestHandler<GetAllEmployeeShiftQuery, PagedResult<EmployeeShiftDto>>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        private readonly IShiftRepostiry _shiftRepository = shiftRepostiry;

        public async Task<PagedResult<EmployeeShiftDto>> Handle(GetAllEmployeeShiftQuery query, CancellationToken cancellationToken)
        {
            IPagedResult<Domain.Entities.EmployeeShift> list = await _employeeShiftRepository.FindAllAsync(x => !x.IsDeleted && x.RestaurantID.Equals(query.RestaurantId), query.PageNo, query.PageSize, cancellationToken);
            if (list.TotalCount == 0)
                throw new NotFoundException("None employee shift was found!");

            Dictionary<string, EmployeeDto> employee = await _employeeRepository.FindAllToDictionaryAsync(x => x.NgayXoa == null || x.NguoiXoaID == null,
                x => x.ID, x => x.MapToEmployeeDto(_mapper, EmployeeRole.EmployeeRoleDictionary), cancellationToken);

            Dictionary<int, ShiftDto> shift = await _shiftRepository.FindAllToDictionaryAsync(x => x.IsActive, x => x.ID, x => x.MapToShiftDto(_mapper), cancellationToken);

            return PagedResult<EmployeeShiftDto>.Create
                (
                    totalCount: list.TotalCount,
                    pageCount: list.PageCount,
                    pageSize: list.PageSize,
                    pageNumber: list.PageNo,
                    data: list.MapToListEmployeeShiftDto(_mapper, employee)
                );
        }
    }
}
