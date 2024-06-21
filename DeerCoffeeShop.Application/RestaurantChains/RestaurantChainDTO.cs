using AutoMapper;
using DeerCoffeeShop.Application.Common.Mappings;
using DeerCoffeeShop.Domain.Entities;

namespace DeerCoffeeShop.Application.RestaurantChains
{
    public class RestaurantChainDTO : IMapFrom<RestaurantChain>
    {
        public string RestaurantChainID { get; set; }
        public string RestaurantChain_AdminID { get; set; }
        public string RestaurantChainName { get; set; }
        public string RestaurantChainHQAddress { get; set; }
        public string RestaurantChainType { get; set; }
        public int RestaurantChainTotalBranches { get; set; }
        public int RestaurantChainTotalEmployees { get; set; }
        public DateTime? NgayXoa { get; set; }
        public string? NguoiXoaID { get; set; }
        public bool IsDeleted { get; set; }

        public RestaurantChainDTO() { }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RestaurantChain, RestaurantChainDTO>();
        }
        public static RestaurantChainDTO Create(string resChainID, string resChainAdminID, string resChainName, string resChainType,
                                                string resChainAddress, int resTotalBrand, int resChainTotalEmp, bool isDelete)
        {
            return new RestaurantChainDTO
            {
                IsDeleted = isDelete,
                RestaurantChainHQAddress = resChainAddress,
                RestaurantChainID = resChainID,
                RestaurantChainName = resChainName,
                RestaurantChainTotalBranches = resTotalBrand,
                RestaurantChainTotalEmployees = resChainTotalEmp,
                RestaurantChainType = resChainType,
                RestaurantChain_AdminID = resChainAdminID,
            };
        }
    }
}
