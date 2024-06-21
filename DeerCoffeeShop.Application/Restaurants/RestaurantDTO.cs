using AutoMapper;
using DeerCoffeeShop.Application.Common.Mappings;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Domain.Entities;



namespace DeerCoffeeShop.Application.Restaurants
{
    public class RestaurantDTO : IMapFrom<Domain.Entities.Restaurant>
    {
        public string ID { get; set; }
        public string RestaurantChainID { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public int TotalEmployees { get; set; }
        public EmployeeDto Manager { get; set; }

        public RestaurantDTO() { }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Restaurant, RestaurantDTO>();
        }
        public static RestaurantDTO Create(string ID, string resChainID, string resName,
                                           string resAddress, int totalEmp, EmployeeDto employeeDto)
        {
            return new RestaurantDTO
            {
                Manager = employeeDto,
                RestaurantAddress = resAddress,
                RestaurantChainID = resChainID,
                ID = ID,
                RestaurantName = resName,
                TotalEmployees = totalEmp
            };
        }
    }
}
