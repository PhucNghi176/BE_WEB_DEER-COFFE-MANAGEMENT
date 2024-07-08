using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Common.Security;
using DeerCoffeeShop.Domain.Enums;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.Forms.Queries.GetAbsentFormEmployee;
[Authorize]
public record GetAbsentFormQuery : IRequest<PagedResult<FormDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public GetAbsentFormQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

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
        var isManager = await _currentUserService.IsInRoleAsync("Manager");
        var Employees = await _employeeRepository.FindAllAsync(x => x.ManagerID == UserID, cancellationToken);
        if (isManager)
        {
            var employeeIds = Employees.Select(e => e.ID).ToList();
            var forms = await _formRepository.FindAllAsync(
                x => (x.FormType == FormTypeEnum.DAY_OFF_UNWANTED_SHIFT || x.FormType == FormTypeEnum.DAY_OFF_EMMERGENCY)
                     && employeeIds.Contains(x.ID),
                request.PageNumber,
                request.PageSize,
                cancellationToken);
            return PagedResult<FormDto>.Create
                (
                    totalCount: forms.TotalCount,
                    pageCount: forms.PageCount,
                    pageSize: forms.PageSize,
                    pageNumber: forms.PageNo,
                    data: forms.MapToFormDtoList(_mapper)
                );
        }
        else
        {
            var forms = await _formRepository.FindAllAsync(x => x.EmployeeID == UserID && (x.FormType == FormTypeEnum.DAY_OFF_UNWANTED_SHIFT || x.FormType == FormTypeEnum.DAY_OFF_EMMERGENCY), request.PageNumber, request.PageSize, cancellationToken);
            return PagedResult<FormDto>.Create
                (
                    totalCount: forms.TotalCount,
                    pageCount: forms.PageCount,
                    pageSize: forms.PageSize,
                    pageNumber: forms.PageNo,
                    data: forms.MapToFormDtoList(_mapper)
                );
        }
    }
}
