using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.EmployeeShift.GetByDay
{
    public class GetEmployeeShiftByDayQueryValidator : AbstractValidator<GetEmployeeShiftByDayQuery>
    {
        public GetEmployeeShiftByDayQueryValidator()
        {
            Configure();
        }

        public void Configure()
        {
            RuleFor(x => x.PageNo)
                .NotNull()
                .NotEmpty()
                .WithMessage("Page number must not be null!");

            RuleFor(x => x.PageSize)
                .NotNull()
                .NotEmpty()
                .WithMessage("Page size must not be null!");


            RuleFor(x => x.DateOfWork)
                .NotNull()
                .NotEmpty()
                .WithMessage("Date of work must not be null!");
        }
    }
}
