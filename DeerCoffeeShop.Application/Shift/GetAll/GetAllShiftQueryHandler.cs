using AutoMapper;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Shift.GetAll
{
    public class GetAllShiftQueryHandler(IShiftRepostiry shiftRepository, IMapper mapper) : IRequestHandler<GetAllShiftQuery, PagedResult<ShiftDto>>
    {
        private readonly IShiftRepostiry _shiftRepository = shiftRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResult<ShiftDto>> Handle(GetAllShiftQuery query, CancellationToken cancellationToken)
        {
            IPagedResult<Domain.Entities.Shift> shiftList = await _shiftRepository.FindAllAsync(x => x.IsActive == true, query.PageNo, query.PageSize, cancellationToken);
            return shiftList.TotalCount == 0
                ? throw new NotFoundException("None shift was found!")
                : PagedResult<ShiftDto>.Create
                (
                    totalCount: shiftList.TotalCount,
                    pageCount: shiftList.PageCount,
                    pageSize: query.PageSize,
                    pageNumber: query.PageNo,
                    data: shiftList.MapToShiftDtoList(_mapper)
                );
        }
    }
}
