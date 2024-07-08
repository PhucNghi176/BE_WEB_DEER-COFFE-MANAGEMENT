using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.Forms.Queries.GetAbsentForms;

public record GetAbsentFormQuery : IRequest<PagedResult<FormDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal sealed class GetAbsentFormQueryHandler : IRequestHandler<GetAbsentFormQuery, PagedResult<FormDto>>
{
    private readonly IFormRepository _formRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public GetAbsentFormQueryHandler(IFormRepository formRepository, ICurrentUserService currentUserService, IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _formRepository = formRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<PagedResult<FormDto>> Handle(GetAbsentFormQuery request, CancellationToken cancellationToken)
    {
        string? UserID = _currentUserService.UserId;
        bool isManager = await _currentUserService.IsInRoleAsync("Manager");
        if (!isManager)
        {
            var forms = await _formRepository.FindAllAsync(x => x.EmployeeID == UserID && (x.FormType == Domain.Enums.FormTypeEnum.DAY_OFF_UNWANTED_SHIFT || x.FormType == Domain.Enums.FormTypeEnum.DAY_OFF_EMMERGENCY), request.PageNumber, request.PageSize, cancellationToken);
            var map = _mapper.Map<PagedResult<FormDto>>(forms);
            foreach (var item in map.Data)
            {
                var employee = await _employeeRepository.FindAsync(x => x.ID == _currentUserService.UserId, cancellationToken);
                item.Employee = _mapper.Map<EmployeeDto>(employee);
            }
            return map;
        }
        else
        {
            var forms = await _formRepository.FindAllAsync(x => x.FormType == Domain.Enums.FormTypeEnum.DAY_OFF_UNWANTED_SHIFT || x.FormType == Domain.Enums.FormTypeEnum.DAY_OFF_EMMERGENCY, request.PageNumber, request.PageSize, cancellationToken);
            var map = _mapper.Map<PagedResult<FormDto>>(forms);
            foreach (var item in forms)
            {
                map.Data.Where(x => x.ID == item.ID).FirstOrDefault().Employee = _mapper.Map<EmployeeDto>(await _employeeRepository.FindAsync(x => x.ID == item.EmployeeID, cancellationToken));
            }
            return map;
        }


    }
}

