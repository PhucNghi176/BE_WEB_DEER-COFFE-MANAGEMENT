using DeerCoffeeShop.Api.Controllers.ResponseTypes;
using DeerCoffeeShop.API.Controllers.ResponseTypes;
using DeerCoffeeShop.Application.Authentication.LoginRestaurant;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.Common.Security;
using DeerCoffeeShop.Application.Restaurants;
using DeerCoffeeShop.Application.Restaurants.AddManagerToRestaurant;
using DeerCoffeeShop.Application.Restaurants.CreateRestaurant;
using DeerCoffeeShop.Application.Restaurants.DeleteRestaurant;
using DeerCoffeeShop.Application.Restaurants.FillterByReschainAndManagerID;
using DeerCoffeeShop.Application.Restaurants.Get;
using DeerCoffeeShop.Application.Restaurants.GetAllRestaurantIsactive;
using DeerCoffeeShop.Application.Restaurants.GetRestaurantByDeactive;
using DeerCoffeeShop.Application.Restaurants.GetRestaurantIsLowEmp;
using DeerCoffeeShop.Application.Restaurants.GetRestautantByID;
using DeerCoffeeShop.Application.Restaurants.InactiveRestaurant;
using DeerCoffeeShop.Application.Restaurants.UpdateRestautant;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mime;


namespace DeerCoffeeShop.API.Controllers.RestaurantController
{
    public class RestaurantController(ISender _mediator) : BaseController(_mediator)
    {

        // POST api/<RestaurantController>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> createRestaurant(
                                        CreateRestaurantCommand command,
                                        CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        // GET api/<RestaurantController>
        [HttpGet("Get-Restaurant-By-ID/{ID}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<RestaurantDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<RestaurantDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<RestaurantDTO>>> Get(
                                                [FromRoute] Guid ID,
                                                CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetRestaurantByIDQuery(ID), cancellationToken);
            return Ok(new JsonResponse<RestaurantDTO>(result));
        }

        // GET api/<RestaurantController>
        [HttpGet("Fillter-Restaurant-By-RestaurantChainAndManager/{resChainID}/{managerID}/{pageNumber}/{pageSize}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<RestaurantDTO>>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<RestaurantDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<RestaurantDTO>>>> fillterByResChainAndManagerID(
                                                    [FromRoute] int pageSize, [FromRoute] int pageNumber, [FromRoute] string managerID, [FromRoute] string resChainID,
                                                    CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new FillterByReschainAndManagerIDQuery(pageNumber, pageSize, managerID, resChainID), cancellationToken);
            return Ok(new JsonResponse<PagedResult<RestaurantDTO>>(result));
        }
       

        // PUT api/<RestaurantController>
        [HttpPut("Inactive-Restaurant")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> inactiveRestaurant(
                                InactiveRestaurantCommand command,
                                CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }
        // PUT api/<RestaurantController>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> updateRestaurant(
                                            UpdateRestaurantCommand command,
                                            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        // PUT: api/<RestaurantController>
        [HttpPut("Add-manager-to-restaurant")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> addStaffTorestaurant(
                                            AddManagerToRestaurantCommand command,
                                            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        // DELETE api/<RestaurantController>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> deleteRestaurant(
                                                [FromQuery] string ID,
                                                CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteRestaurantCommand(ID), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }
        [HttpPost("Login-Restaurant")]
        public async Task<ActionResult<string>> LoginRestaurant(
                       LoginRestaurantQuery query,
                                  CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<string>> GetAll([FromQuery] GetRestaurantQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            var response = new
            {
                Message = "Get All Successfully",
                Data = result
            };
            return Ok(response);
        }
    }
}
