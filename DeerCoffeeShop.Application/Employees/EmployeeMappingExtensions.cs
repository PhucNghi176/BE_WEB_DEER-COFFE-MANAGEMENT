using AutoMapper;
using DeerCoffeeShop.Domain.Entities;

namespace DeerCoffeeShop.Application.Employees
{
    public static class EmployeeMappingExtensions
    {
        public static EmployeeDto MapToEmployeeDto(this Employee entity, IMapper mapper, string roleName)
        {
            var dto = mapper.Map<EmployeeDto>(entity);
            dto.RoleName = roleName;
            return dto;
        }

        public static EmployeeDto MapToEmployeeDto(this Employee entity, IMapper mapper, Dictionary<int, string> Role)
        {
            var dto = mapper.Map<EmployeeDto>(entity);
            dto.RoleName = Role.ContainsKey(entity.RoleID) ? Role[entity.RoleID] : "Error";
            return dto;
        }

        public static List<EmployeeDto> MapToEmployeeDtoList(this IEnumerable<Employee> entities, IMapper mapper, Dictionary<int, string> Role)
        {
            return entities
                .Select(x =>
                    x.MapToEmployeeDto(mapper,
                         Role.ContainsKey(x.RoleID) ? Role[x.RoleID] : "Error")).ToList();
        }
    }
};


