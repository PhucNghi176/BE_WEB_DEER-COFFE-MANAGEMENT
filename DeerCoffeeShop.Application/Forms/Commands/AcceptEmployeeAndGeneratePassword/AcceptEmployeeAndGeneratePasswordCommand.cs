using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Utils;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.Forms.Commands.AcceptEmployeeAndGeneratePassword;

public class AcceptEmployeeAndGeneratePasswordCommand : IRequest<string>, ICommand
{
    public string ID { get; set; }
    public AcceptEmployeeAndGeneratePasswordCommand(string id)
    {
        ID = id;
    }
}
internal class AcceptEmployeeAndGeneratePasswordCommandHandler : IRequestHandler<AcceptEmployeeAndGeneratePasswordCommand, string>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IFormRepository _formRepository;
    public AcceptEmployeeAndGeneratePasswordCommandHandler(IEmployeeRepository employeeRepository, IFormRepository formRepository)
    {
        _employeeRepository = employeeRepository;
        _formRepository = formRepository;
    }
    public async Task<string> Handle(AcceptEmployeeAndGeneratePasswordCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.FindAsync(x => x.ID == request.ID, cancellationToken) ?? throw new NotFoundException("Form not found");
        var employee = await _employeeRepository.FindAsync(x => x.ID == form.EmployeeID, cancellationToken) ?? throw new NotFoundException("Employee ID not found");
        employee.IsActive = true;
        form.FormType = Domain.Enums.FormTypeEnum.ACCEPPTED;
        employee.Password = "$2a$11$dRZA37NpS.thXR9anJXBZehaTb7ezji2i2E5WbHGA2cwMeW4wEXAy";
        _employeeRepository.Update(employee);
        _formRepository.Update(form);
        await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        await MailUtils.SendPasswordAsync(employee.Email, employee.FullName, employee.ID,"HCM");
        return "Check Your Email!";
    }
}
