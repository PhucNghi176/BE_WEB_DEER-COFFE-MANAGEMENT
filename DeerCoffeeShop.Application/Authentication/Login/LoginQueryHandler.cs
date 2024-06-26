﻿using DeerCoffeeShop.Application.Authentication.Refrestoken.GenerateRefreshToken;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Authentication.Login
{
    internal class LoginQueryHandler(IRestaurantRepository _restaurantRepository, IEmployeeRepository _employeeRepository, ISender sender) : IRequestHandler<LoginQuery, LoginDTO>
    {

        public async Task<LoginDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee user = await _employeeRepository.FindAsync(_ => _.ID == request.EmployeeID && _.NgayXoa == null, cancellationToken) ?? throw new NotFoundException("User not found");
            bool isTrue = _employeeRepository.VerifyPassword(request.Password, user.Password);
            if (!isTrue)
            {
                throw new IncorrectPasswordException("Password is incorrect");
            }
            string Role = "";
            Role = user.RoleID switch
            {
                1 => "Admin",
                2 => "Manager",
                3 => "Employee",
                _ => "Owner",
            };
            string refresh = sender.Send(new RefreshTokenCommand(), cancellationToken).Result.Token;
            user.RefreshToken = refresh;
            Domain.Entities.Restaurant? restaurant = await _restaurantRepository.FindAsync(_ => _.ManagerID == user.ID, cancellationToken);
            _ = await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return LoginDTO.Create(user.ID, Role, refresh, restaurant?.ID);
        }
    }
}
