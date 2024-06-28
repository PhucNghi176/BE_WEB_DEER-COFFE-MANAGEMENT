using AutoMapper;
using DeerCoffeeShop.Application.Common.Mappings;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Domain.Entities;

namespace DeerCoffeeShop.Application.Forms;

public class FormDto : IMapFrom<Form>
{
    public string ID { get; set; }
    public EmployeeDto? Employee { get; set; }
    public Domain.Enums.FormTypeEnum FormType { get; set; }
    public string? Content { get; set; }
    public DateTime? Date { get; set; }
    public bool IsApproved { get; set; }
    public void Mapping(Profile profile)
    {
        _ = profile.CreateMap<Form, FormDto>();
    }
    public static FormDto Create(string FormID, EmployeeDto employee, Domain.Enums.FormTypeEnum formType, string content, DateTime date, bool isApproved)
    {
        return new FormDto
        {
            ID = FormID,
            Employee = employee,
            FormType = formType,
            Content = content,
            Date = date,
            IsApproved = isApproved
        };
    }

}
