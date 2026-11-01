


using eCommerce.Core.Entities;
using eCommerce.Core.Services;
using eCommerce.Core.ServicesContract;
using eCommerce.Core.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            //  Core services often include business logic, domain services, application services, etc.
            // Example: services.AddDbContext<MyDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUserService, UserService>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
            return services;
        }
    }
}
