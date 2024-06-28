using DeerCoffeeShop.Api.Controllers.ResponseTypes;
using DeerCoffeeShop.API.Controllers.ResponseTypes;
using DeerCoffeeShop.Application.Common.Pagination;
using DeerCoffeeShop.Application.RestaurantChains;
using DeerCoffeeShop.Application.RestaurantChains.CreateRestaurantChain;
using DeerCoffeeShop.Application.RestaurantChains.DeleteRestaurantChain;
using DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByAdmin;
using DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByDeactive;
using DeerCoffeeShop.Application.RestaurantChains.GetRestaurantChainByID;
using DeerCoffeeShop.Application.RestaurantChains.GetRestautantChainByName;
using DeerCoffeeShop.Application.RestaurantChains.InactiveRestaurantChain;
using DeerCoffeeShop.Application.Restaurants.UpdateRestautant;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DeerCoffeeShop.API.Controllers.RestaurantChainController
{
    public class RestaurantChainController(ISender _mediator) : BaseController(_mediator)
    {

        // POST api/<RestaurantChainController>
        [HttpPost("Create-RestaurantChain")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> createRestaurantChain(
                                                CreateRestaurantChainCommand command,
                                                CancellationToken cancellationToken = default)
        {
            string result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        //Get api/<RestaurantChainController>
        [HttpGet("Get-RestairantChain-By-Admin/{ID}/{pageNumber}/{pageSize}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<RestaurantChainDTO>>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<RestaurantChainDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<RestaurantChainDTO>>>> getRestauirantChainByAdmin(
                                [FromRoute] string ID, [FromRoute] int pageNumber, [FromRoute] int pageSize,
                                CancellationToken cancellationToken = default)
        {
            PagedResult<RestaurantChainDTO> result = await _mediator.Send(new GetRestaurantChainByAdminQuery(pageNumber, pageSize, ID), cancellationToken);
            var response = new
            {
                Message = "Query Successful",
                data = result
            };
            return Ok(response);
        }
        //Get api/<RestaurantChainController>
        [HttpGet("Get-RestairantChain-By-ID/{ID}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<RestaurantChainDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<RestaurantChainDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<RestaurantChainDTO>>> getRestaurantChainByID(
                                                        [FromRoute] string ID,
                                                        CancellationToken cancellationToken = default)
        {
            RestaurantChainDTO result = await _mediator.Send(new GetRestaurantChainByIDQuery(ID), cancellationToken);
            var response = new
            {
                Message = "Query Successful",
                data = result
            };
            return Ok(response);
        }
        //Get api/<RestaurantChainController>
        [HttpGet("Get-RestairantChain-By-Name/{resName}/{pageNumber}/{pageSize}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<RestaurantChainDTO>>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<RestaurantChainDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<RestaurantChainDTO>>>> getRestaurantChainByName(
                                                        [FromRoute] int pageNumber, [FromRoute] int pageSize, [FromRoute] string resName,
                                                        CancellationToken cancellationToken = default)
        {
            PagedResult<RestaurantChainDTO> result = await _mediator.Send(new GetRestautantChainByNameQuery(pageNumber, pageSize, resName), cancellationToken);
            var response = new
            {
                Message = "Query Successful",
                data = result
            };
            return Ok(response);
        }
        //Get api/<RestaurantChainController>
        [HttpGet("Get-RestairantChain-By-Deactive/{pageNumber}/{pageSize}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<RestaurantChainDTO>>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<RestaurantChainDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<RestaurantChainDTO>>>> getRestaurantChainByName(
                                                        [FromRoute] int pageNumber, [FromRoute] int pageSize,
                                                        CancellationToken cancellationToken = default)
        {
            PagedResult<RestaurantChainDTO> result = await _mediator.Send(new GetRestaurantChainByDeactiveQuery(pageNumber, pageSize), cancellationToken);
            var response = new
            {
                Message = "Query Successful",
                data = result
            };
            return Ok(response);
        }

        //PUT api/<RestaurantChainController>
        [HttpPut("Inactive-RestaurantChain")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> inactiveRestaurantChain(
                                            InactiveRestaurantChainCommand command,
                                            CancellationToken cancellationToken = default)
        {
            string result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }
        //PUT api/<RestaurantChainController>
        [HttpPut("Update-RestaurantChain")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> updateRestaurantChain(
                                            UpdateRestaurantCommand command,
                                            CancellationToken cancellationToken = default)
        {
            string result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        //DELETE api/<RestaurantchainController>
        [HttpDelete("Delete-RestaurantChain")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> deleteRestaurantchain(
                                                        DeleteRestaurantChainCommand command,
                                                        CancellationToken cancellationToken = default)
        {
            string result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

    }
}
