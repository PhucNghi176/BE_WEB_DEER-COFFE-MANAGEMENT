using AutoMapper;
using DeerCoffeeShop.Application.Employees;

namespace DeerCoffeeShop.Application.EmployeeShift
{
    public static class EmployeeShiftDtoMappingExtension
    {
        public static EmployeeShiftDto MapToEmployeeShiftDto(this Domain.Entities.EmployeeShift form, IMapper mapper)
            => mapper.Map<EmployeeShiftDto>(form);
        public static EmployeeShiftDtoV2 MapToEmployeeShiftDtoV2(this Domain.Entities.EmployeeShift form, IMapper mapper)
            => mapper.Map<EmployeeShiftDtoV2>(form);
        public static List<EmployeeShiftDtoV2> MapToListEmployeeShiftDtoV2(this IEnumerable<Domain.Entities.EmployeeShift> form, IMapper mapper)
           => form.Select(x => x.MapToEmployeeShiftDtoV2(mapper)).ToList();
        public static List<EmployeeShiftDto> MapToListEmployeeShiftDto(this IEnumerable<Domain.Entities.EmployeeShift> form, IMapper mapper)
            => form.Select(x => x.MapToEmployeeShiftDto(mapper)).ToList();

        public static EmployeeShiftDto MapToEmployeeShiftDto(this Domain.Entities.EmployeeShift form, IMapper mapper
            , EmployeeDto employee)
        {
            EmployeeShiftDto dto = mapper.Map<EmployeeShiftDto>(form);
            dto.Employee = employee;

            return dto;
        }

        public static List<EmployeeShiftDto> MapToListEmployeeShiftDto(this IEnumerable<Domain.Entities.EmployeeShift> form, IMapper mapper
            , Dictionary<string, EmployeeDto> employee)
        {
            return form.Select(x =>
            {
                return x.MapToEmployeeShiftDto(mapper,

                    employee.ContainsKey(x.EmployeeID) ? employee[x.EmployeeID] : null
                    );
            }).ToList();
        }
    }
}
