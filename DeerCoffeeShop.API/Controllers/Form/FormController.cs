using DeerCoffeeShop.API.Controllers.ResponseTypes;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Forms;
using DeerCoffeeShop.Application.Forms.Commands.AcceptEmployeeAndGeneratePassword;
using DeerCoffeeShop.Application.Forms.Commands.AcceptFormAndSendMail;
using DeerCoffeeShop.Application.Forms.Queries.GetAllPagination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeerCoffeeShop.API.Controllers.Form;


public class FormController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<FormDto>>> GetAll([FromQuery] int pageNumber, int PageSize)
    {
        var result = await _sender.Send(new GetAllFormPagination(pageNumber: pageNumber, PageSize));
        var response = new
        {
            Message = "Get All Successfully",
            Data = result
        };
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> ApproveForm(AcceptFormAndSendMailCommand command)
    {
        var resutl = await _sender.Send(new AcceptFormAndSendMailCommand(command.FormID, command.RestaurantID, command.Date));
        var response = new
        {
            Message = resutl
        };
        return Ok(response);
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> SendPassword([FromRoute] string id)
    {
        var resutl = await _sender.Send(new AcceptEmployeeAndGeneratePasswordCommand(id));
        var response = new
        {
            Message = resutl
        };
        return Ok(response);
    }
}
