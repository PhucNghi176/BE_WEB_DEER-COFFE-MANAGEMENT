using AutoMapper;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByAdmin
{
    public class GetRestaurantChainByAdminQueryHandler : IRequestHandler<GetRestaurantChainByAdminQuery, PagedResult<RestaurantChainDTO>>
    {
        private readonly IRestaurantChainRepository _restaurantChinRepository;
        private readonly IMapper _mapper;

        public GetRestaurantChainByAdminQueryHandler(IRestaurantChainRepository restaurantChinRepository, IMapper mapper)
        {
            _restaurantChinRepository = restaurantChinRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<RestaurantChainDTO>> Handle(GetRestaurantChainByAdminQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IPagedResult<Domain.Entities.RestaurantChain> resChainList = await this._restaurantChinRepository.FindAllAsync(x => x.RestaurantChain_AdminID.Equals(request.adminID) && x.IsDeleted == false, pageNo: request.pageNumber, pageSize: request.pageSize, cancellationToken);
                return resChainList.Count() == 0
                    ? throw new NotFoundException($"Not found any restaurantChin was managed by admin ID {request.adminID}")
                    : PagedResult<RestaurantChainDTO>.Create(
                    totalCount: resChainList.TotalCount,
                    pageCount: resChainList.PageCount,
                    pageSize: resChainList.PageSize,
                    pageNumber: resChainList.PageNo,
                    data: resChainList.MapToRestaurantChainDTOList(_mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
