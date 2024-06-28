using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Security;
using DeerCoffeeShop.Domain.Common.Method;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.GetEmployeeShiftInAWeek;

[Authorize(Roles = "Manager,Employee")]
public record GetEmployeeShiftInAWeekQuery : IRequest<List<EmployeeShiftDtoV2>>, IQuery
{
    public DateOnly Date { get; set; }
    public bool IsMonth { get; set; }
}
internal class GetEmployeeShiftInAWeekQueryHandler(IEmployeeShiftRepository employeeShiftRepository, ICurrentUserService currentUserService, IMapper mapper, IRestaurantRepository restaurantRepository, IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeeShiftInAWeekQuery, List<EmployeeShiftDtoV2>>
{
    private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<EmployeeShiftDtoV2>> Handle(GetEmployeeShiftInAWeekQuery request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.EmployeeShift> employeeShifts;
        string? UserID = _currentUserService.UserId;
        bool isManager = await _currentUserService.IsInRoleAsync("Manager");
        Restaurant? ManagerIDOfRestaurant = await _restaurantRepository.FindAsync(_ => _.ManagerID == UserID, cancellationToken);


        if (!request.IsMonth)
        {
            List<DateOnly> weekDates = GetWeekDates.Get(request.Date);
            if (isManager)
            {
                employeeShifts = await _employeeShiftRepository.FindAllAsync(x => x.DateOfWork >= weekDates[0] && x.DateOfWork <= weekDates[weekDates.Count - 1] && x.RestaurantID == ManagerIDOfRestaurant.ID, cancellationToken);
            }
            else
            {
                Employee? User = await _employeeRepository.FindAsync(x => x.ID == UserID, cancellationToken);
                Restaurant? RestaurantID = await _restaurantRepository.FindAsync(x => x.ManagerID == User.ManagerID, cancellationToken);
                employeeShifts = await _employeeShiftRepository.FindAllAsync(x => x.DateOfWork >= weekDates[0] && x.DateOfWork <= weekDates[weekDates.Count - 1] && x.RestaurantID == RestaurantID.ID && x.EmployeeID == UserID, cancellationToken);
            }

        }
        else
        {
            employeeShifts = await _employeeShiftRepository.FindAllAsync(x => x.Month == request.Date.Month && x.RestaurantID == ManagerIDOfRestaurant.ID, cancellationToken);
        }

        // Retrieve employee details
        foreach (Domain.Entities.EmployeeShift item in employeeShifts)
        {
            item.Employee = await _employeeRepository.FindAsync(x => x.ID == item.EmployeeID, cancellationToken);
        }
        //var task = employeeShifts.Select(async item =>
        //{
        //    item.Employee = await _employeeRepository.FindAsync(x => x.ID == item.EmployeeID, cancellationToken);
        //}
        // );
        //await Task.WhenAll(task);
        return _mapper.Map<List<EmployeeShiftDtoV2>>(employeeShifts);

    }

}



