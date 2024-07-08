﻿using DeerCoffeeShop.Application.Common.Interfaces;
using MediatR;

namespace DeerCoffeeShop.Application.Authentication.Login;

public record LoginQuery(string EmployeeID, string Password) : IRequest<LoginDTO>, IQuery
{
    public string EmployeeID { get; } = EmployeeID;
    public string Password { get; } = Password;
}
