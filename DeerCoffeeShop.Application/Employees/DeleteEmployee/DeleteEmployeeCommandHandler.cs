using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, ICurrentUserService currentUserService) : IRequestHandler<DeleteEmployeeCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<string> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var foundObject = await _employeeRepository.FindAsync(x => x.ID.Equals(request.EmployeeID)
            );

            if (foundObject == null) throw new NotFoundException("None employee shift of restaurant was found!");

            foundObject.NguoiXoaID = _currentUserService.UserId;
            foundObject.NgayXoa = DateTime.Now;
            foundObject.IsDeleted = true;
            //_employeeRepository.Update(foundObject);

            return await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Xóa thành công" : "Xóa thất bại";
        }
    }
}
