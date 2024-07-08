using DeerCoffeeShop.Domain.Entities.Base;

namespace DeerCoffeeShop.Domain.Entities;

public class Form : Entity
{
    public string? EmployeeID { get; set; }
    public virtual Employee? Employee { get; set; }
    public Enums.FormTypeEnum FormType { get; set; }
    public string? Content { get; set; }
    public DateTime? Date { get; set; }
    public bool IsApproved { get; set; } = false;
    public string Response { get; set; } = "";
    public string ShiftID { get; set; }= "";    
}
