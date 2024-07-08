using AutoMapper;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.GetShiftByID;

public record GetShiftByIDQuery : IRequest<EmployeeShiftDtoV2>
{
    public string ShiftID { get; set; }
    
}
internal sealed class GetShiftByIDQueryHandler : IRequestHandler<GetShiftByIDQuery, EmployeeShiftDtoV2>
{
    private readonly IEmployeeShiftRepository _employeeShiftRepository;
    private readonly IMapper _mapper;

    public GetShiftByIDQueryHandler(IEmployeeShiftRepository employeeShiftRepository, IMapper mapper)
    {
        _employeeShiftRepository = employeeShiftRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeShiftDtoV2> Handle(GetShiftByIDQuery request, CancellationToken cancellationToken)
    {
        var shift = await _employeeShiftRepository.FindAsync(x => x.ID == request.ShiftID, cancellationToken) ?? throw new NotFoundException("Shift not found");
        return _mapper.Map<EmployeeShiftDtoV2>(shift);
    }
}
