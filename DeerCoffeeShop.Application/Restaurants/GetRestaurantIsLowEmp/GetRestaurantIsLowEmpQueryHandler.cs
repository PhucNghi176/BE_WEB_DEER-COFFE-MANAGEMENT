using AutoMapper;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.GetRestaurantIsLowEmp
{
    public class GetRestaurantIsLowEmpQueryHandler : IRequestHandler<GetRestaurantIsLowEmpQuery, PagedResult<RestaurantDTO>>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public GetRestaurantIsLowEmpQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._restaurantRepository = restaurantRepository;
        }
        public async Task<PagedResult<RestaurantDTO>> Handle(GetRestaurantIsLowEmpQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var restaurants = await this._restaurantRepository.FindAllAsync(x => x.TotalEmployees < 10 && x.IsDeleted == false, pageNo:request.pageNumber, pageSize:request.pageSize, cancellationToken);
                return PagedResult<RestaurantDTO>.Create
                (
                totalCount: restaurants.TotalCount,
                pageCount: restaurants.PageCount,
                pageSize: restaurants.PageSize,
                pageNumber: restaurants.PageNo,
                data: restaurants.MapToRestaurantDTOList(_mapper)
                );
            }
            catch (Exception ex) 
            {

                throw new Exception($"{ex.Message}");
            }
        }
    }
}
