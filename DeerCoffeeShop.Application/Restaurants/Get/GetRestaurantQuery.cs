using AutoMapper;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Common.Security;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.Get;
[Authorize(Roles = "Admin")]
public record GetRestaurantQuery : IRequest<PagedResult<RestaurantDTO>>, IQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? RestaurantName { get; set; }
    public string? RestaurantAddress { get; set; }
    public string? ManagerID { get; set; }
    public int? TotalEmployees { get; set; }

}
internal class GetRestaurantQueryHandler(IRestaurantRepository restaurantRepository, ICurrentUserService currentUserService, IMapper mapper, IEmployeeShiftRepository employeeShiftRepository, IEmployeeRepository employeeRepository) : IRequestHandler<GetRestaurantQuery, PagedResult<RestaurantDTO>>
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IMapper _mapper = mapper;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;

    public async Task<PagedResult<RestaurantDTO>> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
    {

        IQueryable<Restaurant> queryOptions(IQueryable<Restaurant> query)
        {
            query = query.Where(x => x.NgayXoa == null);
            if (!string.IsNullOrEmpty(request.RestaurantName))
            {
                query = query.Where(x => x.RestaurantName.Contains(request.RestaurantName));
            }
            if (!string.IsNullOrEmpty(request.RestaurantAddress))
            {
                query = query.Where(x => x.RestaurantAddress.Contains(request.RestaurantAddress));
            }
            if (!string.IsNullOrEmpty(request.ManagerID))
            {
                query = query.Where(x => x.ManagerID == request.ManagerID);
            }
            if (request.TotalEmployees.HasValue)
            {
                query = query.Where(x => x.TotalEmployees == request.TotalEmployees);
            }
            return query;

        }
        IPagedResult<Restaurant> list = await _restaurantRepository.FindAllAsync(request.PageNumber, request.PageSize, queryOptions, cancellationToken);
        foreach (Restaurant item in list)
        {
            item.Manager = await _employeeRepository.FindAsync(x => x.ID == item.ManagerID, cancellationToken);
        }
        return PagedResult<RestaurantDTO>.Create(list.TotalCount, list.PageCount, list.PageSize, list.PageNo, list.MapToRestaurantDTOList(_mapper));
    }
}
