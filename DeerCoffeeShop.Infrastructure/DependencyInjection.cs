using DeerCoffeeShop.Domain.Common.Interfaces;
using DeerCoffeeShop.Domain.Repositories;
using DeerCoffeeShop.Infrastructure.Persistence.Configurations;
using DeerCoffeeShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeerCoffeeShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                _ = options.UseSqlServer(
                    configuration.GetConnectionString("Azure"),
                    b =>
                    {
                        _ = b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        _ = b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
                _ = options.UseLazyLoadingProxies();


            });
            _ = services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
            _ = services.AddScoped<IFaceDetectionRepository, FaceDetectionRepository>();
            _ = services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            _ = services.AddScoped<IShiftRepostiry, ShiftRepository>();
            _ = services.AddScoped<IEmployeeShiftRepository, EmployeeShiftRepository>();
            _ = services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            _ = services.AddScoped<IRoleRepository, RoleRepository>();
            _ = services.AddScoped<IRestaurantChainRepository, RestauranChainRepository>();
            _ = services.AddScoped<IFormRepository, FormRepository>();
            _ = services.AddScoped<IAttdenceRepository, AttendanceRepository>();

            return services;
        }
    }
}
