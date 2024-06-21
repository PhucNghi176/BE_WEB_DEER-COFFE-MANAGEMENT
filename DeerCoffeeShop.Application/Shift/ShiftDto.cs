using AutoMapper;
using DeerCoffeeShop.Application.Common.Mappings;

namespace DeerCoffeeShop.Application.Shift
{
    public class ShiftDto : IMapFrom<Domain.Entities.Shift>
    {
        public ShiftDto() { }

        public string name { get; set; }

        public int ShiftStart { get; set; }

        public int ShiftEnd { get; set; }

        public string ShiftDescription { get; set; }

        public bool IsActive { get; set; }

        public static ShiftDto Create(string name, int shiftStart, int shiftEnd
            , string shiftDescription, bool isActive)
        {
            return new ShiftDto()
            {
                name = name,
                ShiftStart = shiftStart,
                ShiftEnd = shiftEnd,
                ShiftDescription = shiftDescription,
                IsActive = isActive
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Shift, ShiftDto>();
        }
    }
}
