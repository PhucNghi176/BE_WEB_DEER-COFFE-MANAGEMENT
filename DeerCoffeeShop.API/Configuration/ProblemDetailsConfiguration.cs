﻿using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace DeerCoffeeShop.API.Configuration
{
    public static class ProblemDetailsConfiguration
    {
        public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
        {
            _ = services.AddProblemDetails(conf => conf.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Type = $"https://httpstatuses.io/{context.ProblemDetails.Status}";

                if (context.ProblemDetails.Status != 500) { return; }
                context.ProblemDetails.Title = "Internal Server Error";
                _ = context.ProblemDetails.Extensions.TryAdd("traceId", Activity.Current?.Id ?? context.HttpContext.TraceIdentifier);

                IWebHostEnvironment env = context.HttpContext.RequestServices.GetService<IWebHostEnvironment>()!;
                if (!env.IsDevelopment()) { return; }

                IExceptionHandlerFeature? exceptionFeature = context.HttpContext.Features.Get<IExceptionHandlerFeature>();
                if (exceptionFeature is null) { return; }
                context.ProblemDetails.Detail = exceptionFeature.Error.ToString();
            });
            return services;
        }
    }
}
