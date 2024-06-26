﻿using AutoMapper;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Restaurants.GetAllRestaurantIsactive
{
    public class GetAllRestaurantIsactiveHandler : IRequestHandler<GetAllRestaurantIsactiveQuery, PagedResult<RestaurantDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        public GetAllRestaurantIsactiveHandler(IMapper mapper, IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<PagedResult<RestaurantDTO>> Handle(GetAllRestaurantIsactiveQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IPagedResult<Domain.Entities.Restaurant> restaurantList = await this._restaurantRepository.FindAllAsync(x => x.IsDeleted == false, request.pageNumber, request.pageSize, cancellationToken);
                if (restaurantList.Count() == 0)
                    throw new NotFoundException("Not found any restauratn with the condition.");
                List<RestaurantDTO> result = restaurantList.MapToRestaurantDTOList(_mapper);
                return PagedResult<RestaurantDTO>.Create(
                                totalCount: restaurantList.TotalCount,
                                pageCount: restaurantList.PageCount,
                                pageNumber: restaurantList.PageNo,
                                pageSize: restaurantList.PageSize,
                                data: result
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
