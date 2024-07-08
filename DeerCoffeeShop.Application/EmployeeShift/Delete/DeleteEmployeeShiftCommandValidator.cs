using FluentValidation;

namespace DeerCoffeeShop.Application.EmployeeShift.Delete
{
    public class DeleteEmployeeShiftCommandValidator : AbstractValidator<DeleteEmployeeShiftCommand>
    {
        public DeleteEmployeeShiftCommandValidator()
        {
            Configure();
        }

        public void Configure()
        {
           
        }
    }
}
