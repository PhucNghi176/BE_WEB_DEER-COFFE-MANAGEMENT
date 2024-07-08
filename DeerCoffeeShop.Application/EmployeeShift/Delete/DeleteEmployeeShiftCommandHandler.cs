using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.EmployeeShift.Delete
{
    public class DeleteEmployeeShiftCommandHandler(IEmployeeShiftRepository employeeShiftRepository, ICurrentUserService currentUserService) : IRequestHandler<DeleteEmployeeShiftCommand, string>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<string> Handle(DeleteEmployeeShiftCommand request, CancellationToken cancellationToken)
        {
            var shift = await _employeeShiftRepository.FindAsync(x => x.ID == request.ShiftID, cancellationToken)?? throw new NotFoundException("Shift not found");
            _employeeShiftRepository.Remove(shift);



            return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Xóa thành công" : "Xóa thất bại";
        }
    }
}
