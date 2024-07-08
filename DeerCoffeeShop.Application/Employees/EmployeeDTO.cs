using AutoMapper;
using DeerCoffeeShop.Application.Common.Mappings;
using DeerCoffeeShop.Domain.Entities;

namespace DeerCoffeeShop.Application.Employees
{
    public class EmployeeDto : IMapFrom<Employee>
    {
        public string? ID { get; set; }
        public string? FullName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? RoleName { get; set; }
        public string? RestaurantID { get; set; }
        public required bool IsActive { get; set; }
        public DateTime DateJoined { get; set; }


        public static EmployeeDto Create(string iD, string fullName, string email, string phoneNumber, string address, DateTime dateOfBirth, string roleName, bool isActive, DateTime dateJoined, string avatarUrl)
        {
            return new EmployeeDto
            {

                ID = iD,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                DateOfBirth = dateOfBirth,
                RoleName = roleName,
                IsActive = isActive,
                DateJoined = dateJoined,
                AvatarUrl = avatarUrl
            };
        }

        public static EmployeeDto CreateDtoLogin(string fullName, string roleID, string? avatarUrl, string? restaurantID)
        {
            return new EmployeeDto
            {
                RestaurantID = restaurantID,
                FullName = fullName,
                RoleName = roleID,
                AvatarUrl = avatarUrl,
                IsActive = true
            };
        }

        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<Employee, EmployeeDto>();

        }
    }
};


