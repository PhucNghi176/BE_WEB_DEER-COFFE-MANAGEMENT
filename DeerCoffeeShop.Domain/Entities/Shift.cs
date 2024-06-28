using DeerCoffeeShop.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeerCoffeeShop.Domain.Entities
{
    public class Shift : DefineTable
    {
        [Column("ShiftName")]
        public override required string Name { get => base.Name; set => base.Name = value; }
        public required int ShiftStart { get; set; }
        public required int ShiftEnd { get; set; }
        public required string ShiftDescription { get; set; }
        public required bool IsActive { get; set; }
    }
}