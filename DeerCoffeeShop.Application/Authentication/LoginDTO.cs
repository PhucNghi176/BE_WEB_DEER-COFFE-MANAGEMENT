using AutoMapper;
using DeerCoffeeShop.Application.Common.Mappings;
using DeerCoffeeShop.Domain.Entities;

namespace DeerCoffeeShop.Application.Authentication
{

    public class LoginDTO : IMapFrom<Employee>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, LoginDTO>();

        }
        public string Id { get; set; }
        public string RoleName { get; set; }
        public required string RefreshToken { get; set; }
        public string? RestaurantID { get; set; }
        public static LoginDTO Create(string EmplopyeeID, string Role, string RefreshToken, string? restaurantID)
        {
            return new LoginDTO
            {
                Id = EmplopyeeID,
                RoleName = Role,
                RefreshToken = RefreshToken,
                RestaurantID = restaurantID
            };
        }

    }
}
