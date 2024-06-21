using DeerCoffeeShop.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeerCoffeeShop.Domain.Entities
{
    public class Restaurant : Entity, ISoftDelete
    {
        public required string RestaurantChainID { get; set; }
        [ForeignKey("RestaurantChainID")]
        public virtual RestaurantChain RestaurantChain { get; set; }
        public required string RestaurantName { get; set; }
        public required string RestaurantAddress { get; set; }
        public required string ManagerID { get; set; }
        [ForeignKey("ManagerID")]
        public virtual Employee Manager { get; set; }
        public required int TotalEmployees { get; set; }
        public DateTime? NgayXoa { get; set; }
        public string? NguoiXoaID { get; set; }
        [ForeignKey("NguoiXoaID")]
        public virtual Employee? NguoiXoa { get; set; }
        public bool IsDeleted { get; set; }
    }
}
