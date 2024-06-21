using AutoMapper;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.FillterByReschainAndManagerID
{
    public class FillterByReschainAndManagerIDQueryHandler : IRequestHandler<FillterByReschainAndManagerIDQuery, PagedResult<RestaurantDTO>>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public FillterByReschainAndManagerIDQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<PagedResult<RestaurantDTO>> Handle(FillterByReschainAndManagerIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Func<IQueryable<Restaurant>, IQueryable<Restaurant>> querys = query =>
                {
                    if (request.resChainID != null) 
                    {
                        query = query.Where(x => x.RestaurantChainID.Equals(request.resChainID) && x.IsDeleted == false);
                    }
                    if (request.managerID != null) 
                    {
                        query = query.Where(x => x.ManagerID.Equals(request.managerID) && x.IsDeleted == false);
                    }
                    return query;
                };
                var result = await this._restaurantRepository.FindAllAsync(pageNo:request.pageNumber, pageSize:request.pageSize , querys);
                if(result.Count() == 0)
                    throw new NotFoundException($"Not found any restaurant that belong to restaurantChain ID : {request.resChainID} and managed by manager ID :{request.managerID}");
                return PagedResult<RestaurantDTO>.Create(
                    totalCount: result.TotalCount,
                    pageCount: result.PageCount,
                    pageSize: result.PageSize,
                    pageNumber: result.PageNo,
                    data: result.MapToRestaurantDTOList(_mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
