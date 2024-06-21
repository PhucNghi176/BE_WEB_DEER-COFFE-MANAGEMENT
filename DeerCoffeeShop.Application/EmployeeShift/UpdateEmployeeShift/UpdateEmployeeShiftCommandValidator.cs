using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.UpdateEmployeeShift
{
    public class UpdateEmployeeShiftCommandValidator : AbstractValidator<UpdateEmployeeShiftCommand>
    {
        public UpdateEmployeeShiftCommandValidator() 
        {
            Configure();
        }

        public void Configure()
        {
            RuleFor(x => x.RestaurantID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Restaurant must not be empty!");

            RuleFor(x => x.ShiftID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Shift must not be empty!");

            RuleFor(x => x.DateOfWork)
                .NotEmpty()
                .NotNull()
                .WithMessage("Date of work must not be empty!");

            RuleFor(x => x.EmployeeID)
              .NotEmpty()
              .NotNull()
              .WithMessage("Employee must not be empty!");
        }
    }
}
