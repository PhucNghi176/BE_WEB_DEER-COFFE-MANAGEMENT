﻿using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Shift.Delete
{
    public class DeleteShiftCommandHandler(IShiftRepostiry shiftRepostiry, ICurrentUserService currentUserService) : IRequestHandler<DeleteShiftCommand, string>
    {
        private readonly IShiftRepostiry _shiftRepository = shiftRepostiry;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<string> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Shift? foundObject = await _shiftRepository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (foundObject == null)
                throw new NotFoundException("Shift was not found!");
            foundObject.IsActive = false;
            foundObject.NguoiCapNhatID = _currentUserService.UserId;
            foundObject.NgayCapNhat = DateTime.Now;
            _shiftRepository.Update(foundObject);
            return await _shiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Remove shift successfully!" : "Remove shift failed!";
        }
    }
}
