﻿using DeerCoffeeShop.Application.Common.Interfaces;
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
            Domain.Entities.EmployeeShift foundObject = await _employeeShiftRepository.FindAsync(x => x.EmployeeID.Equals(request.EmployeeID)
            && x.RestaurantID.Equals(request.RestaurantID)

            && x.NguoiXoaID == null) ?? throw new NotFoundException("None employee shift of restaurant was found!");

            foundObject.EmployeeID = null;
            foundObject.NguoiXoaID = _currentUserService.UserId;
            foundObject.NgayXoa = DateTime.Now;

            return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Xóa thành công" : "Xóa thất bại";
        }
    }
}
