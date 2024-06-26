﻿using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Common.Security;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Forms.Queries.GetAllPagination;
[Authorize(Roles = "Admin,Manager")]
public record GetAllFormPagination : IRequest<PagedResult<FormDto>>, IQuery
{
    public GetAllFormPagination(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}
internal class GetAllFormPaginationHandler(ICurrentUserService currentUserService, IFormRepository formRepository, IMapper mapper, IEmployeeRepository employeeRepository) : IRequestHandler<GetAllFormPagination, PagedResult<FormDto>>
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IFormRepository _formRepository = formRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    public async Task<PagedResult<FormDto>> Handle(GetAllFormPagination request, CancellationToken cancellationToken)
    {
        bool role = await _currentUserService.IsInRoleAsync("Admin");
        if (role)
        {
            IPagedResult<Form> list = await _formRepository.FindAllAsync(x => x.FormType == Domain.Enums.FormTypeEnum.JOB_APPLICATION || x.FormType == Domain.Enums.FormTypeEnum.ACCEPPTED, request.PageNumber, request.PageSize, cancellationToken);
            foreach (Form item in list)
            {
                item.Employee = await _employeeRepository.FindAsync(x => x.ID == item.EmployeeID, cancellationToken);
            }
            return PagedResult<FormDto>.Create(
                list.TotalCount,
                list.PageCount,
                list.PageSize,
                list.PageNo,
                 list.MapToFormDtoList(_mapper)
           );
        }
        else
        {
            IPagedResult<Form> list = await _formRepository.FindAllAsync(x => x.FormType != Domain.Enums.FormTypeEnum.JOB_APPLICATION && x.FormType != Domain.Enums.FormTypeEnum.ACCEPPTED, request.PageNumber, request.PageSize, cancellationToken);
            foreach (Form item in list)
            {
                item.Employee = await _employeeRepository.FindAsync(x => x.ID == item.EmployeeID, cancellationToken);
            }
            return PagedResult<FormDto>.Create(
                               list.TotalCount,
                               list.PageCount,
                               list.PageSize,
                               list.PageNo,
                               list.MapToFormDtoList(_mapper));


        }
    }
}
