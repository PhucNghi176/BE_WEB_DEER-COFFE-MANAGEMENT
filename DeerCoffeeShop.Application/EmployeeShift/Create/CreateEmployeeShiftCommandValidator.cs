using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.Create
{
    public class CreateEmployeeShiftCommandValidator : AbstractValidator<CreateEmployeeShiftCommand>
    {
        public CreateEmployeeShiftCommandValidator() 
        {
            Configure();
        }

        public void Configure()
        {
            
            

            RuleFor(x => x.DateOfWork)
                .NotEmpty()
                .NotNull()
                .WithMessage("Date of work must not be empty!");

            

            RuleFor(x => x.CheckIn)
              .NotEmpty()
              .NotNull()
              .WithMessage("Check in time must not be empty!");

            RuleFor(x => x.CheckOut)
              .NotEmpty()
              .NotNull()
              .WithMessage("Check out time must not be empty!");
        }
    }
}
