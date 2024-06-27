using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Security;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.GetEmployeeShiftInAWeek;

[Authorize(Roles = "Manager")]
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

        var UserID = _currentUserService.UserId;
        var ManagerIDOfRestaurant = await _restaurantRepository.FindAsync(_ => _.ManagerID == UserID, cancellationToken);
        List<Domain.Entities.EmployeeShift> employeeShifts;

        if (!request.IsMonth)
        {
            var weekDates = GetWeekDates(request.Date);
            employeeShifts = await _employeeShiftRepository.FindAllAsync(x => x.DateOfWork >= weekDates[0] && x.DateOfWork <= weekDates[weekDates.Count - 1] && x.RestaurantID == ManagerIDOfRestaurant.ID, cancellationToken);
        }
        else
        {
            employeeShifts = await _employeeShiftRepository.FindAllAsync(x => x.Month == request.Date.Month && x.RestaurantID == ManagerIDOfRestaurant.ID, cancellationToken);
        }

        // Retrieve employee details
        foreach (var item in employeeShifts)
        {
            item.Employee = await _employeeRepository.FindAsync(x => x.ID == item.EmployeeID, cancellationToken);
        }
        return _mapper.Map<List<EmployeeShiftDtoV2>>(employeeShifts);

    }


    private static List<DateOnly> GetWeekDates(DateOnly date)
    {
        List<DateOnly> weekDates = [];

        // Get the day of the week as an integer (0 = Sunday, 1 = Monday, ..., 6 = Saturday)
        int dayOfWeek = (int)date.DayOfWeek;

        // Calculate the previous Sunday
        DateOnly startOfWeek = date.AddDays(-dayOfWeek);

        // Add each day from the previous Sunday to the next Saturday to the list
        for (int i = 0; i < 7; i++)
        {
            weekDates.Add(startOfWeek.AddDays(i));
        }

        return weekDates;
    }

}



