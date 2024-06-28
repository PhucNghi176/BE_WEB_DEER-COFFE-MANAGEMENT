using AutoMapper;
using DeerCoffeeShop.Domain.Entities;

namespace DeerCoffeeShop.Application.Forms;

public static class FromDtoMappingExstension
{
    public static FormDto MapToFormDto(this Form projectFrom, IMapper mapper)
           => mapper.Map<FormDto>(projectFrom);

    public static List<FormDto> MapToFormDtoList(this IEnumerable<Form> projectFrom, IMapper mapper)
        => projectFrom.Select(x => x.MapToFormDto(mapper)).OrderByDescending(x => x.Date).ToList();
}
