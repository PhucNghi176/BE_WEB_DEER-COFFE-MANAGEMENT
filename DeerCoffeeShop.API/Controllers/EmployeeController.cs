using DeerCoffeeShop.API.Controllers.ResponseTypes;
using DeerCoffeeShop.API.Services;
using DeerCoffeeShop.Application.Authentication.Login;
using DeerCoffeeShop.Application.Employees;
using DeerCoffeeShop.Application.Employees.CreateEmployee;
using DeerCoffeeShop.Application.Employees.DeleteEmployee;
using DeerCoffeeShop.Application.Employees.GetAllEmployee;
using DeerCoffeeShop.Application.Employees.GetEmployee;
using DeerCoffeeShop.Application.Employees.GetEmployeeInfo;
using DeerCoffeeShop.Application.Employees.UpdateEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeerCoffeeShop.API.Controllers;

// [Authorize]
public class EmployeeController(ISender sender, JwtService _jwtService) : BaseController(sender)
{
    [HttpPut()]
    public async Task<ActionResult<string>> UpdateEmployee(UpdateEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        string result = await _sender.Send(new UpdateEmployeeCommand(command.EmployeeID, command.Email, command.PhoneNumber, command.Address, command.RoleId, command.FullName, command.DateOfBirth, command.IsActive), cancellationToken);
        var response = new
        {
            Message = result,
        };
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginQuery loginQuery, CancellationToken cancellationToken = default)
    {
        Application.Authentication.LoginDTO loginDTO = await _sender.Send(new LoginQuery(loginQuery.EmployeeID, loginQuery.Password), cancellationToken);
        JwtService.Token token = _jwtService.CreateToken(loginDTO.Id, loginDTO.RoleName, loginDTO.RefreshToken, loginDTO.RestaurantID);
        token.EmployeeDto = await _sender.Send(new GetEmployeeInfoQuery(loginDTO.Id), cancellationToken);
        var response = new
        {
            Message = "Login Successfully !",
            Data = token
        };
        return Ok(response);
    }

    [HttpPost("apply")]
    public async Task<ActionResult<string>> CreateEmployeeAplication([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        string result = await _sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{employee_id}")]
    public async Task<ActionResult<EmployeeDto>> GetAll([FromRoute] string employee_id, CancellationToken cancellationToken)
    {
        EmployeeDto result = await _sender.Send(new GetEmployeeQuery(employee_id), cancellationToken);
        var response = new
        {
            Message = "Get Employee Information Successfully !",
            Data = result
        };
        return Ok(response);
    }

    [HttpDelete("")]
    public async Task<ActionResult<string>> Delete(DeleteEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        string result = await _sender.Send(new DeleteEmployeeCommand(command.EmployeeID), cancellationToken);
        var response = new
        {
            Message = result,
        };
        return Ok(response);
    }


    [HttpGet("")]
    public async Task<ActionResult<EmployeeDto>> GetAll([FromQuery] GetAllEmployeeQuery query, CancellationToken cancellationToken)
    {
        Application.Common.Pagination.PagedResult<EmployeeDto> result = await _sender.Send(query, cancellationToken);
        var response = new
        {
            Message = "Get All Successfully",
            Data = result
        };
        return Ok(response);
    }

}
