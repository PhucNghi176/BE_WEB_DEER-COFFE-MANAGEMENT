using AutoMapper;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByDeactive
{
    public class GetRestaurantChainByDeactiveQueryHandler : IRequestHandler<GetRestaurantChainByDeactiveQuery, PagedResult<RestaurantChainDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        public GetRestaurantChainByDeactiveQueryHandler(IMapper mapper, IRestaurantChainRepository restaurantChainRepository)
        {
            _mapper = mapper;
            _restaurantChainRepository = restaurantChainRepository;
        }
        public async Task<PagedResult<RestaurantChainDTO>> Handle(GetRestaurantChainByDeactiveQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IPagedResult<Domain.Entities.RestaurantChain> restaurantChain = await this._restaurantChainRepository.FindAllAsync(x => x.IsDeleted == true, pageNo: request.pageNumber, pageSize: request.pageSize, cancellationToken);
                return restaurantChain.Count() == 0
                    ? throw new NotFoundException("Not found any resChain had been deleted")
                    : PagedResult<RestaurantChainDTO>.Create(
                                totalCount: restaurantChain.TotalCount,
                                pageCount: restaurantChain.PageCount,
                                pageNumber: restaurantChain.PageNo,
                                pageSize: restaurantChain.PageSize,
                                data: restaurantChain.MapToRestaurantChainDTOList(_mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
