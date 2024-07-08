using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Method;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.GetNeededReviewShift;

public record GetNeededReviewShiftQuery : IRequest<List<EmployeeShiftDto>>, IQuery
{
    public DateOnly Date { get; set; }
    public bool IsMonth { get; set; }
}

internal sealed class GetNeededReviewShiftQueryHandler : IRequestHandler<GetNeededReviewShiftQuery, List<EmployeeShiftDto>>
{
    private readonly IEmployeeShiftRepository _employeeShiftRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetNeededReviewShiftQueryHandler(IEmployeeShiftRepository employeeShiftRepository, IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeShiftRepository = employeeShiftRepository;
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<List<EmployeeShiftDto>> Handle(GetNeededReviewShiftQuery request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.EmployeeShift> employeeShifts;
        if (!request.IsMonth)
        {
            List<DateOnly> weekDates = GetWeekDates.Get(request.Date);
            employeeShifts = await _employeeShiftRepository.FindAllAsync(x => x.DateOfWork >= weekDates[0] && x.DateOfWork <= weekDates[weekDates.Count - 1] && x.IsReviewRequired, cancellationToken);
        }
        else
        {
            employeeShifts = await _employeeShiftRepository.FindAllAsync(x => x.Month == request.Date.Month && x.IsReviewRequired, cancellationToken);
        }

        // Retrieve employee details
        foreach (Domain.Entities.EmployeeShift item in employeeShifts)
        {
            item.Employee = await _employeeRepository.FindAsync(x => x.ID == item.EmployeeID, cancellationToken);
        }

        return _mapper.Map<List<EmployeeShiftDto>>(employeeShifts);
    }
}

