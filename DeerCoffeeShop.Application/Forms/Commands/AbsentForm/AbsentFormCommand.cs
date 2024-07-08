using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Common.Security;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.Forms.Commands.AbsentForm
{
    [Authorize]
    public record AbsentFormCommand : IRequest<string>
    {
        public string ShiftID { get; set; }
        public string Reason { get; set; }
        public int FormType { get; set; }
        public AbsentFormCommand(string shiftID, string reason, int formType)
        {
            ShiftID = shiftID;
            Reason = reason;
            FormType = formType;
        }
    }
    internal sealed class AbsentFormCommandHandlder : IRequestHandler<AbsentFormCommand, string>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFormRepository _formRepository;

        public AbsentFormCommandHandlder(ICurrentUserService currentUserService, IEmployeeRepository employeeRepository, IFormRepository formRepository)
        {
            _currentUserService = currentUserService;
            _employeeRepository = employeeRepository;
            _formRepository = formRepository;
        }

        public async Task<string> Handle(AbsentFormCommand request, CancellationToken cancellationToken)
        {
            string? UserID = _currentUserService.UserId;
           
            var form = new Domain.Entities.Form
            {
                EmployeeID = UserID,
                ShiftID = request.ShiftID,
                FormType = (Domain.Enums.FormTypeEnum)request.FormType,
                Content = request.Reason,
                Date = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7)).DateTime,
            };
            _formRepository.Add(form);
            return await _formRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Sucess" : "Failed";



        }
    }
}
