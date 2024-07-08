using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.Employees.AddDeviceToken;

public record AddDeviceTokenCommand : IRequest<string>
{
    public string EmployeeID { get; set; }
    public string DeviceToken { get; set; }
}
internal sealed class AddDeviceTokenCommandHandler(IEmployeeRepository employeeRepository) : IRequestHandler<AddDeviceTokenCommand, string>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    public async Task<string> Handle(AddDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.FindAsync(x => x.ID == request.EmployeeID, cancellationToken);
        employee.DeviceToken = request.DeviceToken;
        _employeeRepository.Update(employee);
        _ = await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return "Add device token successfully!";

    }
}

