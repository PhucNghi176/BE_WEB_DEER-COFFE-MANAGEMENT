﻿using DeerCoffeeShop.Domain.Entities.Base;
using DeerCoffeeShop.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeerCoffeeShop.Domain.Entities
{
    public class EmployeeShift : Entity, ISoftDelete
    {
        public string? EmployeeID { get; set; } // ID string type
        [ForeignKey("ID")]
        public virtual Employee? Employee { get; set; }
        public required string RestaurantID { get; set; }
        [ForeignKey("RestaurantID")]
        public virtual Restaurant Restaurant { get; set; }
        [DataType(DataType.Date)]
        public required DateOnly DateOfWork { get; set; }
        public required int Month { get; set; }
        public required int Year { get; set; }
        [DataType(DataType.Time)]
        public DateTime? CheckIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime? CheckOut { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Actual_CheckIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Actual_CheckOut { get; set; }
        [DataType(DataType.Time)]
        public int? TotalHours { get; set; }
        public required bool IsOnTime { get; set; } = false;
        public required EmployeeShiftStatus Status { get; set; } = EmployeeShiftStatus.Absent;
        public int EmployeeNote { get; set; } = 0;
        public string? Note { get; set; }
        public bool IsEmpty { get; set; } = true;
        public DateTime? NgayXoa { get; set; }
        public string? NguoiXoaID { get; set; }
        [ForeignKey("NguoiXoaID")]
        public virtual Employee? NguoiXoa { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsReviewRequired { get; set; } = false;
        public bool IsLocked { get; set; } = false;
    }
}
