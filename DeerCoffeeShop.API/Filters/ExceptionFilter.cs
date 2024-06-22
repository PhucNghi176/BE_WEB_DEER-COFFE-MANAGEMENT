using DeerCoffeeShop.Application.Common.Exceptions;
using DeerCoffeeShop.Domain.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;
using System.Diagnostics;

namespace DeerCoffeeShop.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException validationException:
                    foreach (var error in validationException.Errors)
                    {
                        context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState))
                        .AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;

                case ForbiddenAccessException:
                case UnauthorizedAccessException:
                    context.Result = new ForbidResult();
                    context.ExceptionHandled = true;
                    break;

                case NotFoundException notFoundException:
                    context.Result = new NotFoundObjectResult(new ProblemDetails
                    {
                        Detail = notFoundException.Message
                    })
                        .AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;

                case FormException formException:
                    context.Result = new UnprocessableEntityObjectResult(new
                    {
                        Status = formException.StatusCode,
                        Detail = formException.Message,
                        Data = formException.DataError
                    })
                        .AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;

                case IncorrectPasswordException:
                case TimeCheckInToSoonException:
                case DuplicateNameException:
                    context.Result = new BadRequestObjectResult(new ProblemDetails
                    {
                        Detail = context.Exception.Message
                    })
                        .AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;
            }
        }

    }

    internal static class ProblemDetailsExtensions
    {
        public static IActionResult AddContextInformation(this ObjectResult objectResult, ExceptionContext context)
        {
            if (objectResult.Value is not ProblemDetails problemDetails)
            {
                return objectResult;
            }
            problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? context.HttpContext.TraceIdentifier);
            problemDetails.Type = "https://httpstatuses.io/" + (objectResult.StatusCode ?? problemDetails.Status);
            return objectResult;
        }
    }
}
