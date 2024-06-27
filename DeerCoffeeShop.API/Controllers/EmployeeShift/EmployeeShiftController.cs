using DeerCoffeeShop.Api.Controllers.ResponseTypes;
using DeerCoffeeShop.API.Controllers.ResponseTypes;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.EmployeeShift;
using DeerCoffeeShop.Application.EmployeeShift.CheckIn_Out.CheckIn;
using DeerCoffeeShop.Application.EmployeeShift.CheckIn_Out.CheckOut;
using DeerCoffeeShop.Application.EmployeeShift.Create;
using DeerCoffeeShop.Application.EmployeeShift.Delete;
using DeerCoffeeShop.Application.EmployeeShift.GetAll;
using DeerCoffeeShop.Application.EmployeeShift.GetByDay;
using DeerCoffeeShop.Application.EmployeeShift.GetByEmployeeId;
using DeerCoffeeShop.Application.EmployeeShift.GetEmployeeShiftInAWeek;
using DeerCoffeeShop.Application.EmployeeShift.UpdateEmployeeShift;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DeerCoffeeShop.API.Controllers.EmployeeShift
{
    public class EmployeeShiftController(ISender sender) : BaseController(sender)
    {
        private readonly ISender _mediator = sender;

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<EmployeeShiftDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<JsonResponse<PagedResult<EmployeeShiftDto>>>> GetAll
            ([FromQuery] GetAllEmployeeShiftQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            var list = new List<object>();
            foreach (var item in result.Data)
            {

                var testreturn = new
                {
                    title = item.Employee.FullName ?? "Not Pick",
                    start = item.CheckIn,
                    end = item.CheckOut,
                    allDay = false,
                    resource = item
                };
                list.Add(testreturn);
            }



            return Ok(list);
        }
        [HttpGet("week")]
        public async Task<IActionResult> GetEmployeeShiftInAWeekQuery([FromQuery] GetEmployeeShiftInAWeekQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            var list = new List<object>();
            foreach (var item in result)
            {

                var testreturn = new
                {
                    title = item.Employee.FullName ?? "Not Pick",
                    start = item.CheckIn,
                    end = item.CheckOut,
                    allDay = false,
                    resource = item
                };
                list.Add(testreturn);
            }
            var respond = new
            {
                Message = "Get successfully",
                Data = list
            };
            return Ok(respond);
        }
        [HttpGet("day")]
        [ProducesResponseType(typeof(PagedResult<EmployeeShiftDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<JsonResponse<PagedResult<EmployeeShiftDto>>>> GetByDay
            ([FromQuery] GetEmployeeShiftByDayQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            var list = new List<object>();
            foreach (var item in result.Data)
            {

                var testreturn = new
                {
                    title = item.Employee.FullName ?? "Not Pick",
                    start = item.CheckIn,
                    end = item.CheckOut,
                    allDay = false,
                    resource = item
                };
                list.Add(testreturn);
            }
            var respond = new
            {
                Message = "Get successfully",
                Data = list
            };
            return Ok(respond);
        }
        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<JsonResponse<string>>> DeleteEmployeeShift([FromBody] DeleteEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpGet("employee")]
        [ProducesResponseType(typeof(PagedResult<EmployeeShiftDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<JsonResponse<PagedResult<EmployeeShiftDto>>>> GetId
            ([FromQuery] GetEmployeeShiftByEmployeeIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            var respond = new
            {
                Message = "Get successfully",
                Data = result
            };
            return Ok(respond);
        }



        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> Create([FromBody] CreateEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            var respond = new
            {
                Message = "Create successfully",
                Data = result
            };
            return Ok(respond);
        }

        [HttpPost("CheckIn")]
        public async Task<ActionResult<JsonResponse<string>>> CheckIn([FromForm] CheckInCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpPost("CheckOut")]
        public async Task<ActionResult<JsonResponse<string>>> CheckOut([FromForm] CheckOutCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
