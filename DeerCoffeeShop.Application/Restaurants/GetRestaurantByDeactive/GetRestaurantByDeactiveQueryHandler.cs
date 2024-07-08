using AutoMapper;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.GetRestaurantByDeactive
{
    public class GetRestaurantByDeactiveQueryHandler : IRequestHandler<GetRestaurantByDeactiveQuery, PagedResult<RestaurantDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        public GetRestaurantByDeactiveQueryHandler(IMapper mapper, IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<PagedResult<RestaurantDTO>> Handle(GetRestaurantByDeactiveQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IPagedResult<Domain.Entities.Restaurant> restaurantList = await this._restaurantRepository.FindAllAsync(x => x.IsDeleted == true, pageNo: request.pageNumber, pageSize: request.pageSize, cancellationToken);
                return PagedResult<RestaurantDTO>.Create(
                            totalCount: restaurantList.TotalCount,
                            pageCount: restaurantList.PageCount,
                            pageNumber: restaurantList.PageNo,
                            pageSize: restaurantList.PageSize,
                            data: restaurantList.MapToRestaurantDTOList(_mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
