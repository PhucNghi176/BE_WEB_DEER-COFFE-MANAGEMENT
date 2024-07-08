using DeerCoffeeShop.Application.Common.Behaviours;
using DeerCoffeeShop.Application.Common.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DeerCoffeeShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Transient);
            _ = services.AddMediatR(cfg =>
            {
                _ = cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                _ = cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                _ = cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                _ = cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                _ = cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                _ = cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });

            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
            _ = services.AddScoped<IValidatorProvider, ValidatorProvider>();

            return services;
        }
    }
}