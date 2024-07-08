using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.Forms.Commands.ChangeFormStatus;

public record ChangeFormStatusCommand : IRequest<string>
{
    public ChangeFormStatusCommand(string formID, bool isApprove, string response)
    {
        FormID = formID;
        IsApprove = isApprove;
        Response = response;
    }

    public string FormID { get; set; }
    public bool IsApprove { get; set; }
    public string Response { get; set; } = "";

}
internal sealed class ChangeFormStatusCommandHandler : IRequestHandler<ChangeFormStatusCommand, string>
{
    private readonly IFormRepository _formRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmployeeShiftRepository _employeeShiftRepository;

    public ChangeFormStatusCommandHandler(IFormRepository formRepository, ICurrentUserService currentUserService, IEmployeeShiftRepository employeeShiftRepository)
    {
        _formRepository = formRepository;
        _currentUserService = currentUserService;
        _employeeShiftRepository = employeeShiftRepository;
    }

    public async Task<string> Handle(ChangeFormStatusCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.FindAsync(x => x.ID == request.FormID, cancellationToken);
        if (form == null)
        {
            return "Form not found";
        }
        form.Content = request.Response;
        form.IsApproved = request.IsApprove;
        var shift = await _employeeShiftRepository.FindAsync(x => x.ID == form.ShiftID, cancellationToken);
        if (shift == null)
        {
            return "Shift not found";
        }
        if (!request.IsApprove)
        {
            _employeeShiftRepository.Remove(shift);

        }
        _employeeShiftRepository.Update(shift);
        await _formRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return "Success";
    }
}
