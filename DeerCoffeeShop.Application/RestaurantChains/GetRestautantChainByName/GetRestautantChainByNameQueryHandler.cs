using AutoMapper;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.RestaurantChains.GetRestautantChainByName
{
    public class GetRestautantChainByNameQueryHandler : IRequestHandler<GetRestautantChainByNameQuery, PagedResult<RestaurantChainDTO>>
    {
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        private readonly IMapper _mapper;
        public GetRestautantChainByNameQueryHandler(IRestaurantChainRepository restaurantChainRepository, IMapper mapper)
        {
            _restaurantChainRepository = restaurantChainRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<RestaurantChainDTO>> Handle(GetRestautantChainByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IPagedResult<Domain.Entities.RestaurantChain> resChainList = await this._restaurantChainRepository.FindAllAsync(x => x.RestaurantChainName.Contains(request.resChainName) && x.IsDeleted == false, pageNo: request.pageNumber, pageSize: request.pageSize, cancellationToken);
                return resChainList.Count() == 0
                    ? throw new NotFoundException($"Not found any restaurantChain with name {request.resChainName}")
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
