using DeerCoffeeShop.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeerCoffeeShop.Domain.Entities;

public class Attendence : Entity, ISoftDelete
{
    public required string EmployeeShiftID { get; set; }
    [DataType(DataType.ImageUrl)]
    public string? EmployeePictureUrlCheckIn { get; set; }
    [DataType(DataType.ImageUrl)]
    public string? EmployeePictureUrlCheckOut { get; set; }
    public DateTime? NgayXoa { get; set; }
    public string? NguoiXoaID { get; set; }
    [ForeignKey("NguoiXoaID")]
    public virtual Employee? NguoiXoa { get; set; }
    public bool IsDeleted { get; set; }
}
